using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml;
using WinAuth2SDA.Data;
using WinAuth2SDA.Properties;
using System.Resources;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;

namespace WinAuth2SDA
{
    public partial class FrmMain : Form
    {
        private HttpClient _httpClient { get; init; }

        public FrmMain()
        {
            InitializeComponent();

            var proxy = new WebProxy {
                Address = new Uri("http://192.168.31.45:8080"),
            };

            var httpClientHandler = new HttpClientHandler {
                Proxy = proxy,
            };

            _httpClient = new HttpClient(handler: httpClientHandler) {
                BaseAddress = new Uri("https://api.steampowered.com"),
                DefaultRequestHeaders =
                {
                    { "User-Agent", "Mozilla/5.0 (Linux; U; Android 4.1.1; en-us; Google Nexus 4 - 4.1.1 - API 16 - 768x1280 Build/JRO03S) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30" },
                },

            };

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tsAuthor.Text = "作者: Chr_";
            var version = Assembly.GetExecutingAssembly().GetName().Version ?? new Version("0.0.0.0");
            tsVersion.Text = $"版本: {version}";
            tsGithub.Text = "获取源码";
            txtWinAuthFile.Text = GlobalConfig.Default.WinAuth;
            if (string.IsNullOrEmpty(txtWinAuthFile.Text))
            {
                var winAuth = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WinAuth", "winauth.xml");
                if (Path.Exists(winAuth))
                {
                    txtWinAuthFile.Text = winAuth;
                }
                else
                {
                    MessageBox.Show("自动识别 winauth.xml 路径失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            txtMaFolder.Text = GlobalConfig.Default.MaFolder;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalConfig.Default.MaFolder = txtMaFolder.Text;
            GlobalConfig.Default.WinAuth = txtWinAuthFile.Text;
            GlobalConfig.Default.Save();
        }

        private void btnMaFolder_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog {
                ShowNewFolderButton = true,
                AutoUpgradeEnabled = true,
                InitialDirectory = txtMaFolder.Text,
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    txtMaFolder.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnWinAuthFile_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog {
                Title = "选择 winauth.xml 所在的位置",
                Filter = "XML文件|*.xml|全部文件|*.*",
                InitialDirectory = txtWinAuthFile.Text,
                CheckPathExists = true,
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.FileName))
                {
                    txtWinAuthFile.Text = dialog.FileName;
                }
            }
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            string winAuthFile = txtWinAuthFile.Text;
            string maFolder = txtMaFolder.Text;

            if (!Path.Exists(winAuthFile))
            {
                MessageBox.Show("winauth.xml 文件路径不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtWinAuthFile.Focus();
                return;
            }
            if (!Path.Exists(maFolder))
            {
                MessageBox.Show("输出文件夹路径不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaFolder.Focus();
                return;
            }

            string failedFolder = Path.Combine(maFolder, "信息缺失");
            if (!Path.Exists(failedFolder))
            {
                Directory.CreateDirectory(failedFolder);
            }

            var secretdataList = new List<string>();
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(winAuthFile);
                var root = xmlDoc.DocumentElement;
                var nodes = root?.SelectNodes("//WinAuthAuthenticator[@type='WinAuth.SteamAuthenticator']/authenticatordata/secretdata");

                if (nodes != null)
                {
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        var node = nodes[i];
                        if (node != null)
                        {
                            secretdataList.Add(node.InnerText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("解析XML出错, 转换失败\r\n{0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!secretdataList.Any())
            {
                MessageBox.Show("未识别到有效的Steam令牌信息, 转换失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int success = 0, failed = 0;

            foreach (var data in secretdataList)
            {
                var parts = data.Split('|');
                if (parts.Length != 5 || string.IsNullOrWhiteSpace(parts[2]) || string.IsNullOrWhiteSpace(parts[3]))
                {
                    if (MessageBox.Show(string.Format("{0} 参数错误\r\n确认 - 继续运行, 取消 - 终止操作", data), "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }

                try
                {
                    string device = HexString2String(parts[2]);
                    string body = HexString2String(parts[3]);
                    string extra = HexString2String(parts[4]);

                    var authData = JsonSerializer.Deserialize<WinAuthData>(body);
                    var authSession = !string.IsNullOrEmpty(extra) ? JsonSerializer.Deserialize<WinAuthSessionData>(extra) : null;

                    if (authData == null)
                    {
                        if (MessageBox.Show(string.Format("{0} {1}无法解析的令牌数据\r\n确认 - 继续运行, 取消 - 终止操作", body, extra), "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                        {
                            return;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    var maFile = new MaFileData {
                        SharedSecret = authData.SharedSecret,
                        SerialNumber = authData.SerialNumber,
                        RevocationCode = authData.RevocationCode,
                        Uri = authData.Uri,
                        ServerTime = authData.ServerTime,
                        AccountName = authData.AccountName,
                        TokenGid = authData.TokenGid,
                        IdentitySecret = authData.IdentitySecret,
                        Secret1 = authData.Secret1,
                        status = authData.status,
                        FullyEnrolled = true,
                        DeviceId = device,
                    };

                    if (authSession != null && !string.IsNullOrEmpty(authSession.Cookies) && !string.IsNullOrEmpty(authSession.SteamId) && !string.IsNullOrEmpty(authSession.OAuthToken))
                    {
                        string? sessionId = null, login = null, loginSecure = null;

                        var cookies = authSession.Cookies.Split(';');

                        foreach (var cookie in cookies)
                        {
                            var kv = cookie.Split('=');
                            if (kv.Length >= 2)
                            {
                                string key = kv[0].Trim();
                                string value = kv[1].Trim();

                                if (key == "sessionid")
                                {
                                    sessionId = value;
                                }
                                else if (key == "steamLoginSecure")
                                {
                                    loginSecure = value;
                                }
                                else if (key.StartsWith("steamMachineAuth"))
                                {
                                    login = value;
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(loginSecure))
                        {
                            var payload = new Dictionary<string, string>(1) {
                            { "access_token", authSession.OAuthToken },
                        };

                            using var request = new HttpRequestMessage(HttpMethod.Post, "/IMobileAuthService/GetWGToken/v0001") {
                                Content = new FormUrlEncodedContent(payload)
                            };
                            var response = await _httpClient.SendAsync(request);
                            var stream = await response.Content.ReadAsStreamAsync();
                            var tokenData = await JsonSerializer.DeserializeAsync<IMobileAuthServiceResponse>(stream);

                            login = tokenData?.Response?.Token;
                            loginSecure = tokenData?.Response?.TokenSecure;
                        }

                        if (!long.TryParse(authSession.SteamId, out long steamId))
                        {
                            if (MessageBox.Show(string.Format("{0} 不是有效的Steam ID\r\n确认 - 继续运行, 取消 - 终止操作", authSession.SteamId), "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                            {
                                return;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        maFile.Session = new SessionData {
                            SessionID = sessionId,
                            SteamLogin = login,
                            SteamLoginSecure = loginSecure,
                            WebCookie = null,
                            OAuthToken = authSession.OAuthToken,
                            SteamID = steamId,
                        };
                    }

                    string filePath;

                    if (maFile.Session != null)
                    {
                        success++;
                        filePath = Path.Combine(maFolder, string.Format("{0}.maFile", maFile.Session.SteamID));
                    }
                    else
                    {
                        failed++;
                        filePath = Path.Combine(failedFolder, string.Format("{0}.maFile", maFile.AccountName));
                    }

                    //生成maFile文件
                    bool exist = File.Exists(filePath);
                    if (!exist || MessageBox.Show(string.Format("{0} 已存在, 是否覆盖?\r\n是 - 覆盖文件, 否 - 跳过该文件", filePath), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using var maStream = File.Open(filePath, exist ? FileMode.Truncate : FileMode.CreateNew, FileAccess.Write);
                        var configJson = JsonSerializer.Serialize(maFile);
                        await maStream.WriteAsync(Encoding.UTF8.GetBytes(configJson));
                        await maStream.FlushAsync();
                    }
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(string.Format("{0} 出现意外错误\r\n确认 - 继续运行, 取消 - 终止操作", ex.Message), ex.ToString(), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if (failed == 0 && Directory.GetFiles(failedFolder).Length == 0)
            {
                Directory.Delete(failedFolder);
                MessageBox.Show(string.Format("操作完成, 导出完整令牌 {0} 个", success), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(string.Format("操作完成, 导出完整令牌 {0} 个, 不完整需要重新登陆的令牌 {1} 个\r\n不完整令牌保存在 {2}", success, failed, failedFolder), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (MessageBox.Show("是否更新 manifest.json 文件?\r\n不更新会导致 SDA 不显示导入的账号", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                btnUpdate_Click(sender, e);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            string maFolder = txtMaFolder.Text;

            if (!Path.Exists(maFolder))
            {
                MessageBox.Show("输出文件夹路径不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaFolder.Focus();
                return;
            }

            var filePaths = Directory.EnumerateFiles(maFolder, "*.maFile");
            if (!filePaths.Any())
            {
                MessageBox.Show("令牌文件夹下未找到可用令牌 (需要以 .maFile 结尾)", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var sdaEntries = new Dictionary<long, ManifestEntryData>();

            string manifestPath = Path.Combine(maFolder, "manifest.json");
            bool exist = File.Exists(manifestPath);
            ManifestData? manifest = null;

            if (exist)
            {
                try
                {
                    using var maStream = File.Open(manifestPath, FileMode.Open, FileAccess.Read);
                    manifest = await JsonSerializer.DeserializeAsync<ManifestData>(maStream);
                    if (manifest?.Entries != null)
                    {
                        foreach (var entry in manifest.Entries)
                        {
                            sdaEntries.TryAdd(entry.SteamId, entry);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(string.Format("读取 {0} 失败, 继续操作会导致旧的内容被覆盖\r\n{1}\r\n确认 - 继续运行, 取消 - 终止操作", manifestPath, ex.Message), ex.ToString(), MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }

            if (manifest == null)
            {
                manifest = new ManifestData();
            }

            int success = 0, failed = 0;

            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileName(filePath);
                try
                {
                    using var maStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                    var maFile = await JsonSerializer.DeserializeAsync<MaFileData>(maStream);
                    var steamId = maFile?.Session?.SteamID;
                    if (steamId != null)
                    {
                        var entry = new ManifestEntryData {
                            EncryptionIv = null,
                            EncryptionSalt = null,
                            FileName = fileName,
                            SteamId = steamId.Value,
                        };
                        if (sdaEntries.TryAdd(steamId.Value, entry))
                        {
                            success++;
                        }
                        else
                        {
                            failed++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("读取 {0} 失败, 该文件可能被加密\r\n{1}", filePath, ex.Message), ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            manifest.Entries = sdaEntries.Values.ToList();

            using var msStream = File.Open(manifestPath, exist ? FileMode.Truncate : FileMode.CreateNew, FileAccess.Write);
            var json = JsonSerializer.Serialize(manifest);
            await msStream.WriteAsync(Encoding.UTF8.GetBytes(json));
            await msStream.FlushAsync();

            MessageBox.Show(string.Format("操作完成, 成功新增令牌 {0} 个, 已存在的令牌 {1} 个", success, failed), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string HexString2String(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
            {
                return hexString;
            }
            else
            {
                var bytes = Convert.FromHexString(hexString);
                return Encoding.UTF8.GetString(bytes);
            }
        }


        private static void OpenLink(string uri)
        {
            Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });
        }

        private void tsAuthor_Click(object sender, EventArgs e)
        {
            const string target = "https://steamcommunity.com/id/Chr_/";
            OpenLink(target);
        }

        private void tsGithub_Click(object sender, EventArgs e)
        {
            const string target = "https://github.com/chr233/WinAuth2SDA";
            OpenLink(target);
        }

        private void tsVersion_Click(object sender, EventArgs e)
        {
            const string target = "https://github.com/chr233/WinAuth2SDA/releases";
            OpenLink(target);
        }

    }
}
