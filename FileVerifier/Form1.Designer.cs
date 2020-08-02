using FileVerifierLib.ByteStringConversion;

namespace FileVerifier
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.button1 = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.label1 = new System.Windows.Forms.Label();
      this.linkLabel2 = new System.Windows.Forms.LinkLabel();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.button2 = new System.Windows.Forms.Button();
      this.saveMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.fileHashListView1 = new FileVerifierLib.UserControls.FileHashListView();
      this.saveMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.Location = new System.Drawing.Point(422, 285);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "Calculate";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(94, 12);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(403, 20);
      this.textBox1.TabIndex = 4;
      this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
      // 
      // comboBox1
      // 
      this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[] {
            "Verify checksums",
            "Create MD5 checksums",
            "Create SHA1 checksums",
            "Create SHA256 checksums",
            "Create SHA384 checksums",
            "Create SHA512 checksums"});
      this.comboBox1.Location = new System.Drawing.Point(94, 64);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(160, 21);
      this.comboBox1.TabIndex = 5;
      this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.textBox1_TextChanged);
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new System.Drawing.Point(12, 15);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(62, 13);
      this.linkLabel1.TabIndex = 7;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Root folder:";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 67);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(37, 13);
      this.label1.TabIndex = 8;
      this.label1.Text = "Mode:";
      // 
      // linkLabel2
      // 
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.Location = new System.Drawing.Point(12, 41);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new System.Drawing.Size(76, 13);
      this.linkLabel2.TabIndex = 9;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "Checksum file:";
      this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(94, 38);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(403, 20);
      this.textBox2.TabIndex = 10;
      this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new System.Drawing.Point(260, 66);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(236, 17);
      this.checkBox1.TabIndex = 11;
      this.checkBox1.Text = "Update file list during the calculation process";
      this.checkBox1.UseVisualStyleBackColor = true;
      // 
      // button2
      // 
      this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.button2.ContextMenuStrip = this.saveMenu;
      this.button2.Enabled = false;
      this.button2.Location = new System.Drawing.Point(341, 285);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 23);
      this.button2.TabIndex = 12;
      this.button2.Text = "Save";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // saveMenu
      // 
      this.saveMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
      this.saveMenu.Name = "saveMenu";
      this.saveMenu.Size = new System.Drawing.Size(111, 48);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Checked = true;
      this.toolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
      this.toolStripMenuItem1.Text = "Bytes";
      this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_CheckedChanged);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(110, 22);
      this.toolStripMenuItem2.Text = "Base64";
      this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem1_CheckedChanged);
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.Location = new System.Drawing.Point(12, 285);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(323, 23);
      this.progressBar1.Step = 1;
      this.progressBar1.TabIndex = 13;
      this.progressBar1.Visible = false;
      // 
      // fileHashListView1
      // 
      this.fileHashListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.fileHashListView1.FullRowSelect = true;
      this.fileHashListView1.Hashes = null;
      this.fileHashListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.fileHashListView1.Location = new System.Drawing.Point(12, 91);
      this.fileHashListView1.Name = "fileHashListView1";
      this.fileHashListView1.ProcessingMode = FileVerifierLib.HashHelpers.HashAlgorithmType.Unknown;
      this.fileHashListView1.RefreshInProcess = false;
      this.fileHashListView1.RootPath = "";
      this.fileHashListView1.ShowItemToolTips = true;
      this.fileHashListView1.Size = new System.Drawing.Size(485, 188);
      this.fileHashListView1.TabIndex = 6;
      this.fileHashListView1.UseCompatibleStateImageBehavior = false;
      this.fileHashListView1.View = System.Windows.Forms.View.Details;
      this.fileHashListView1.ItemStateChanged += new FileVerifierLib.UserControls.FileHashListView.ItemStateChangedEventHandler(this.fileHashListView1_ItemStateChanged);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(509, 320);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.linkLabel2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.fileHashListView1);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.button1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(525, 356);
      this.Name = "Form1";
      this.Text = "File Verifier";
      this.saveMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.ComboBox comboBox1;
    private FileVerifierLib.UserControls.FileHashListView fileHashListView1;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.LinkLabel linkLabel2;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.ContextMenuStrip saveMenu;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
  }
}

