using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamableUploader;
public partial class Authentication : Form
{
    public string RetrievedUsername = "";
    public string RetrievedPassword = "";

    public Authentication(string preSetUsername = "", string preSetPassword = "")
    {
        InitializeComponent();

        usernameBox.Text = preSetUsername;
        passwordBox.Text = preSetPassword;

        this.DialogResult = DialogResult.Cancel;
    }

    private async void browseButton_Click(object sender, EventArgs e)
    {
        if (usernameBox.Text.IsNullOrWhiteSpace())
        {
            var pre = usernameBox.BackColor;

            usernameBox.BackColor = Color.Red;
            await Task.Delay(1000);
            usernameBox.BackColor = pre;
            usernameBox.Focus();
            return;
        }

        if (passwordBox.Text.IsNullOrWhiteSpace())
        {
            var pre = passwordBox.BackColor;

            passwordBox.BackColor = Color.Red;

            await Task.Delay(1000);
            passwordBox.BackColor = pre;
            passwordBox.Focus();
            return;
        }

        RetrievedPassword = passwordBox.Text;
        RetrievedUsername = usernameBox.Text;
        this.DialogResult = DialogResult.OK;
    }

    private void register_Click(object sender, EventArgs e)
    {
        string url = "https://streamable.com/signup?signup_src=landing".Replace("&", "^&");
        Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
    }

    private void passwordBox_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = true;
            browseButton.PerformClick();
        }
    }
}
