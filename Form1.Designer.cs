namespace StreamableUploader;

partial class Form1
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        this.pathDisplay = new Label();
        this.browseButton = new Button();
        this.titleBox = new TextBox();
        this.uploadButton = new Button();
        this.progressBar1 = new ProgressBar();
        this.progressLabel = new Label();
        this.updateLoginButton = new Button();
        this.contextMenuButton = new Button();
        this.instantUploadToggle = new CheckBox();
        SuspendLayout();
        // 
        // pathDisplay
        // 
        this.pathDisplay.Location = new Point(12, 9);
        this.pathDisplay.Name = "pathDisplay";
        this.pathDisplay.Size = new Size(432, 23);
        this.pathDisplay.TabIndex = 0;
        this.pathDisplay.Text = "Select a file path..";
        this.pathDisplay.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // browseButton
        // 
        this.browseButton.BackColor = Color.Purple;
        this.browseButton.FlatAppearance.BorderSize = 0;
        this.browseButton.FlatStyle = FlatStyle.Flat;
        this.browseButton.Location = new Point(450, 9);
        this.browseButton.Name = "browseButton";
        this.browseButton.Size = new Size(64, 23);
        this.browseButton.TabIndex = 1;
        this.browseButton.Text = "Browse..";
        this.browseButton.UseVisualStyleBackColor = false;
        this.browseButton.Click += browseButton_Click;
        // 
        // titleBox
        // 
        this.titleBox.BackColor = Color.FromArgb(16, 16, 16);
        this.titleBox.BorderStyle = BorderStyle.FixedSingle;
        this.titleBox.ForeColor = Color.White;
        this.titleBox.Location = new Point(12, 35);
        this.titleBox.Name = "titleBox";
        this.titleBox.Size = new Size(502, 23);
        this.titleBox.TabIndex = 2;
        this.titleBox.TextAlign = HorizontalAlignment.Center;
        // 
        // uploadButton
        // 
        this.uploadButton.BackColor = Color.Purple;
        this.uploadButton.FlatAppearance.BorderSize = 0;
        this.uploadButton.FlatStyle = FlatStyle.Flat;
        this.uploadButton.Location = new Point(12, 62);
        this.uploadButton.Name = "uploadButton";
        this.uploadButton.Size = new Size(502, 23);
        this.uploadButton.TabIndex = 6;
        this.uploadButton.Text = "Upload";
        this.uploadButton.UseVisualStyleBackColor = false;
        this.uploadButton.Click += uploadButton_Click;
        // 
        // progressBar1
        // 
        this.progressBar1.Location = new Point(12, 62);
        this.progressBar1.Margin = new Padding(1);
        this.progressBar1.Name = "progressBar1";
        this.progressBar1.Size = new Size(502, 23);
        this.progressBar1.TabIndex = 7;
        this.progressBar1.Visible = false;
        // 
        // progressLabel
        // 
        this.progressLabel.ForeColor = Color.Gray;
        this.progressLabel.Location = new Point(12, 86);
        this.progressLabel.Name = "progressLabel";
        this.progressLabel.Size = new Size(502, 18);
        this.progressLabel.TabIndex = 8;
        this.progressLabel.Text = "..";
        this.progressLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // updateLoginButton
        // 
        this.updateLoginButton.BackColor = Color.Purple;
        this.updateLoginButton.FlatAppearance.BorderSize = 0;
        this.updateLoginButton.FlatStyle = FlatStyle.Flat;
        this.updateLoginButton.Location = new Point(335, 107);
        this.updateLoginButton.Name = "updateLoginButton";
        this.updateLoginButton.Size = new Size(179, 24);
        this.updateLoginButton.TabIndex = 9;
        this.updateLoginButton.Text = "Update Login Information..";
        this.updateLoginButton.UseVisualStyleBackColor = false;
        this.updateLoginButton.Click += updateLoginButton_Click;
        // 
        // contextMenuButton
        // 
        this.contextMenuButton.BackColor = Color.Purple;
        this.contextMenuButton.FlatAppearance.BorderSize = 0;
        this.contextMenuButton.FlatStyle = FlatStyle.Flat;
        this.contextMenuButton.Location = new Point(12, 107);
        this.contextMenuButton.Name = "contextMenuButton";
        this.contextMenuButton.Size = new Size(179, 24);
        this.contextMenuButton.TabIndex = 10;
        this.contextMenuButton.Text = "Create Context Menu Option..";
        this.contextMenuButton.UseVisualStyleBackColor = false;
        this.contextMenuButton.Click += contextMenuButton_Click;
        // 
        // instantUploadToggle
        // 
        this.instantUploadToggle.Location = new Point(12, 137);
        this.instantUploadToggle.Name = "instantUploadToggle";
        this.instantUploadToggle.Size = new Size(502, 24);
        this.instantUploadToggle.TabIndex = 11;
        this.instantUploadToggle.Text = "Instantly upload video when using context menu";
        this.instantUploadToggle.UseVisualStyleBackColor = true;
        this.instantUploadToggle.CheckedChanged += instantUploadToggle_CheckedChanged;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.FromArgb(13, 13, 13);
        this.ClientSize = new Size(526, 166);
        this.Controls.Add(this.instantUploadToggle);
        this.Controls.Add(this.contextMenuButton);
        this.Controls.Add(this.updateLoginButton);
        this.Controls.Add(this.progressLabel);
        this.Controls.Add(this.progressBar1);
        this.Controls.Add(this.uploadButton);
        this.Controls.Add(this.titleBox);
        this.Controls.Add(this.browseButton);
        this.Controls.Add(this.pathDisplay);
        this.ForeColor = Color.White;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.Icon = (Icon)resources.GetObject("$this.Icon");
        this.MaximizeBox = false;
        this.Name = "Form1";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Upload a video to Streamable..";
        this.TopMost = true;
        Shown += Form1_Shown;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label pathDisplay;
    private Button browseButton;
    private TextBox titleBox;
    private Button uploadButton;
    private ProgressBar progressBar1;
    private Label progressLabel;
    private Button updateLoginButton;
    private Button contextMenuButton;
    private CheckBox instantUploadToggle;
}
