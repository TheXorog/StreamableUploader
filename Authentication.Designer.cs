namespace StreamableUploader;

partial class Authentication
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Authentication));
        this.label1 = new Label();
        this.usernameBox = new TextBox();
        this.label2 = new Label();
        this.passwordBox = new TextBox();
        this.browseButton = new Button();
        this.register = new Button();
        SuspendLayout();
        // 
        // label1
        // 
        this.label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        this.label1.Location = new Point(12, 9);
        this.label1.Name = "label1";
        this.label1.Size = new Size(260, 24);
        this.label1.TabIndex = 0;
        this.label1.Text = "Username";
        this.label1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // usernameBox
        // 
        this.usernameBox.BackColor = Color.FromArgb(16, 16, 16);
        this.usernameBox.BorderStyle = BorderStyle.FixedSingle;
        this.usernameBox.ForeColor = Color.White;
        this.usernameBox.Location = new Point(12, 36);
        this.usernameBox.Name = "usernameBox";
        this.usernameBox.PlaceholderText = "Streamable Account Username";
        this.usernameBox.Size = new Size(260, 23);
        this.usernameBox.TabIndex = 3;
        this.usernameBox.TextAlign = HorizontalAlignment.Center;
        // 
        // label2
        // 
        this.label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        this.label2.Location = new Point(12, 67);
        this.label2.Name = "label2";
        this.label2.Size = new Size(260, 24);
        this.label2.TabIndex = 4;
        this.label2.Text = "Password";
        this.label2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // passwordBox
        // 
        this.passwordBox.BackColor = Color.FromArgb(16, 16, 16);
        this.passwordBox.BorderStyle = BorderStyle.FixedSingle;
        this.passwordBox.ForeColor = Color.White;
        this.passwordBox.Location = new Point(12, 94);
        this.passwordBox.Name = "passwordBox";
        this.passwordBox.PasswordChar = '*';
        this.passwordBox.PlaceholderText = "Streamable Account Password";
        this.passwordBox.Size = new Size(260, 23);
        this.passwordBox.TabIndex = 5;
        this.passwordBox.TextAlign = HorizontalAlignment.Center;
        this.passwordBox.KeyUp += passwordBox_KeyUp;
        // 
        // browseButton
        // 
        this.browseButton.BackColor = Color.Purple;
        this.browseButton.FlatAppearance.BorderSize = 0;
        this.browseButton.FlatStyle = FlatStyle.Flat;
        this.browseButton.Location = new Point(174, 123);
        this.browseButton.Name = "browseButton";
        this.browseButton.Size = new Size(98, 23);
        this.browseButton.TabIndex = 6;
        this.browseButton.Text = "Save";
        this.browseButton.UseVisualStyleBackColor = false;
        this.browseButton.Click += browseButton_Click;
        // 
        // register
        // 
        this.register.BackColor = Color.Purple;
        this.register.FlatAppearance.BorderSize = 0;
        this.register.FlatStyle = FlatStyle.Flat;
        this.register.Location = new Point(12, 123);
        this.register.Name = "register";
        this.register.Size = new Size(98, 23);
        this.register.TabIndex = 7;
        this.register.Text = "Register..";
        this.register.UseVisualStyleBackColor = false;
        this.register.Click += register_Click;
        // 
        // Authentication
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.FromArgb(13, 13, 13);
        this.ClientSize = new Size(284, 157);
        this.Controls.Add(this.register);
        this.Controls.Add(this.browseButton);
        this.Controls.Add(this.passwordBox);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.usernameBox);
        this.Controls.Add(this.label1);
        this.ForeColor = Color.White;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.Icon = (Icon)resources.GetObject("$this.Icon");
        this.Name = "Authentication";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Authentication";
        this.TopMost = true;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox usernameBox;
    private Label label2;
    private TextBox passwordBox;
    private Button browseButton;
    private Button register;
}