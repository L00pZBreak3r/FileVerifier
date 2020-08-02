namespace FileVerifierLib.UserControls
{
  partial class FileHashListView
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    //private static System.ComponentModel.ComponentResourceManager resources;

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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = Properties.FileHashListViewResources.ResourceManager; //new System.ComponentModel.ComponentResourceManager(typeof(FileHashListView));
      columnHeader1 = new System.Windows.Forms.ColumnHeader();
      columnHeader2 = new System.Windows.Forms.ColumnHeader();
      columnHeader3 = new System.Windows.Forms.ColumnHeader();
      columnHeader4 = new System.Windows.Forms.ColumnHeader();
      ItemStateImages = new System.Windows.Forms.ImageList(components);
      SuspendLayout();
      // 
      // FileHashListView
      // 
      Name = "FileHashListView";
      Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2,
            columnHeader3,
            columnHeader4});
      //Dock = System.Windows.Forms.DockStyle.Fill;
      //Location = new System.Drawing.Point(0, 0);
      //TabIndex = 0;
      //this.Size = new System.Drawing.Size(392, 188);
      FullRowSelect = true;
      ShowItemToolTips = true;
      SmallImageList = ItemStateImages;
      UseCompatibleStateImageBehavior = false;
      this.View = System.Windows.Forms.View.Details;
      HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      // 
      // columnHeader1
      // 
      columnHeader1.Text = string.Empty;
      columnHeader1.Width = 20;
      // 
      // columnHeader2
      // 
      columnHeader2.Text = Properties.FileHashListViewResources.COLUMN_NAME_TEXT;
      columnHeader2.Width = 180;
      // 
      // columnHeader3
      // 
      columnHeader3.Text = Properties.FileHashListViewResources.COLUMN_HASH_TEXT;
      columnHeader3.Width = 180;
      // 
      // columnHeader4
      // 
      columnHeader4.Text = Properties.FileHashListViewResources.COLUMN_RESULT_TEXT;
      columnHeader4.Width = 100;
      // 
      // ItemStateImages
      // 
      ItemStateImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ItemStateImages.ImageStream")));
      ItemStateImages.TransparentColor = System.Drawing.Color.Magenta;
      ItemStateImages.Images.SetKeyName(IMAGE_INDEX_UNPROCESSED, "White.bmp");
      ItemStateImages.Images.SetKeyName(IMAGE_INDEX_PROCESSING, "Orange.bmp");
      ItemStateImages.Images.SetKeyName(IMAGE_INDEX_OK, "Green.bmp");
      ItemStateImages.Images.SetKeyName(IMAGE_INDEX_ERROR, "Red.bmp");

      ResumeLayout(false);

    }

    #endregion

    /*public override System.Windows.Forms.ColumnHeaderStyle HeaderStyle
    {
      get
      {
        return base.HeaderStyle;
      }
      set
      {
        base.HeaderStyle = value;
      }
    }

    public override System.Windows.Forms.View View
    {
      get
      {
        return base.View;
      }
      set
      {
        base.View = value;
      }
    }*/


    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.ImageList ItemStateImages;
  }
}
