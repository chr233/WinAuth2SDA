namespace WinAuth2SDA
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            groupBox3 = new GroupBox();
            btnMaFolder = new Button();
            txtMaFolder = new TextBox();
            btnConvert = new Button();
            sS = new StatusStrip();
            tsAuthor = new ToolStripStatusLabel();
            tsGithub = new ToolStripStatusLabel();
            tsVersion = new ToolStripStatusLabel();
            groupBox1 = new GroupBox();
            btnWinAuthFile = new Button();
            txtWinAuthFile = new TextBox();
            btnUpdate = new Button();
            groupBox3.SuspendLayout();
            sS.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(btnMaFolder);
            groupBox3.Controls.Add(txtMaFolder);
            groupBox3.Location = new Point(12, 76);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(496, 58);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "SDA 下的 mainFiles 目录";
            // 
            // btnMaFolder
            // 
            btnMaFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaFolder.Location = new Point(415, 22);
            btnMaFolder.Name = "btnMaFolder";
            btnMaFolder.Size = new Size(75, 23);
            btnMaFolder.TabIndex = 3;
            btnMaFolder.Text = "…";
            btnMaFolder.UseVisualStyleBackColor = true;
            btnMaFolder.Click += btnMaFolder_Click;
            // 
            // txtMaFolder
            // 
            txtMaFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMaFolder.Location = new Point(6, 22);
            txtMaFolder.Name = "txtMaFolder";
            txtMaFolder.PlaceholderText = "保存 .maFile 和 manifest.json 文件的位置";
            txtMaFolder.Size = new Size(403, 23);
            txtMaFolder.TabIndex = 2;
            // 
            // btnConvert
            // 
            btnConvert.Anchor = AnchorStyles.Top;
            btnConvert.Location = new Point(12, 140);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(166, 37);
            btnConvert.TabIndex = 4;
            btnConvert.Text = "开始转换令牌文件";
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += btnConvert_Click;
            // 
            // sS
            // 
            sS.Items.AddRange(new ToolStripItem[] { tsAuthor, tsGithub, tsVersion });
            sS.Location = new Point(0, 189);
            sS.Name = "sS";
            sS.Size = new Size(520, 22);
            sS.TabIndex = 8;
            sS.Text = "statusStrip1";
            // 
            // tsAuthor
            // 
            tsAuthor.IsLink = true;
            tsAuthor.LinkBehavior = LinkBehavior.HoverUnderline;
            tsAuthor.LinkColor = Color.Black;
            tsAuthor.Name = "tsAuthor";
            tsAuthor.Size = new Size(64, 17);
            tsAuthor.Text = "作者: Chr_";
            tsAuthor.VisitedLinkColor = Color.Black;
            tsAuthor.Click += tsAuthor_Click;
            // 
            // tsGithub
            // 
            tsGithub.IsLink = true;
            tsGithub.LinkBehavior = LinkBehavior.HoverUnderline;
            tsGithub.LinkColor = Color.Black;
            tsGithub.Name = "tsGithub";
            tsGithub.Size = new Size(409, 17);
            tsGithub.Spring = true;
            tsGithub.Text = "获取源码";
            tsGithub.VisitedLinkColor = Color.Black;
            tsGithub.Click += tsGithub_Click;
            // 
            // tsVersion
            // 
            tsVersion.IsLink = true;
            tsVersion.LinkBehavior = LinkBehavior.HoverUnderline;
            tsVersion.LinkColor = Color.Black;
            tsVersion.Name = "tsVersion";
            tsVersion.Size = new Size(32, 17);
            tsVersion.Text = "版本";
            tsVersion.VisitedLinkColor = Color.Black;
            tsVersion.Click += tsVersion_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(btnWinAuthFile);
            groupBox1.Controls.Add(txtWinAuthFile);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(496, 58);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "winauth.xml 文件路径";
            // 
            // btnWinAuthFile
            // 
            btnWinAuthFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnWinAuthFile.Location = new Point(415, 22);
            btnWinAuthFile.Name = "btnWinAuthFile";
            btnWinAuthFile.Size = new Size(75, 23);
            btnWinAuthFile.TabIndex = 1;
            btnWinAuthFile.Text = "...";
            btnWinAuthFile.UseVisualStyleBackColor = true;
            btnWinAuthFile.Click += btnWinAuthFile_Click;
            // 
            // txtWinAuthFile
            // 
            txtWinAuthFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtWinAuthFile.Location = new Point(6, 22);
            txtWinAuthFile.Name = "txtWinAuthFile";
            txtWinAuthFile.PlaceholderText = "winauth.xml 文件的路径";
            txtWinAuthFile.Size = new Size(403, 23);
            txtWinAuthFile.TabIndex = 0;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Top;
            btnUpdate.Location = new Point(342, 140);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(166, 37);
            btnUpdate.TabIndex = 5;
            btnUpdate.Text = "自动更新 mainifest.json";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 211);
            Controls.Add(sS);
            Controls.Add(groupBox1);
            Controls.Add(groupBox3);
            Controls.Add(btnUpdate);
            Controls.Add(btnConvert);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(9999, 250);
            MinimumSize = new Size(500, 250);
            Name = "FrmMain";
            Text = "WinAuth 导出文件转换为 .maFiles - By Chr_ 2023";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            sS.ResumeLayout(false);
            sS.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox3;
        private Button btnMaFolder;
        private TextBox txtMaFolder;
        private Button btnConvert;
        private StatusStrip sS;
        private ToolStripStatusLabel tsAuthor;
        private ToolStripStatusLabel tsGithub;
        private ToolStripStatusLabel tsVersion;
        private GroupBox groupBox1;
        private Button btnWinAuthFile;
        private TextBox txtWinAuthFile;
        private Button btnUpdate;
    }
}
