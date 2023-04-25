global using Newtonsoft.Json;
global using System.Reflection;
global using System.Text;
global using Xorog.UniversalExtensions;
global using System.Drawing;
global using StreamableUploader.Entities;
global using System.Net;
global using System.Security.Cryptography;
global using StreamableUploader.Util;
global using Microsoft.Win32;
global using System.Security.Principal;
global using System.Diagnostics;

namespace StreamableUploader;

public partial class Form1 : Form
{
    public static readonly string Version = "1.0";

    Dictionary<string, string> videoFormats = new()
    {
        { "MPEG-4", ".mp4" },
        { "MOV", ".mov" },
        { "WebM", ".webm" },
        { "AVI", ".avi" },
        { "WMV", ".wmv" },
        { "MKV", ".mkv" },
        { "MPEG-4 Visual", ".m4v" }
    };

    bool ContextMenuInstantUpload = false;
    bool InstantUpload = false;

    string ConfigDir = "";
    Config? LoadedConfig = null;

    public Form1()
    {
        var DocsDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        if (!Directory.Exists($"{DocsDir}\\Fortunevale\\"))
            Directory.CreateDirectory($"{DocsDir}\\Fortunevale\\");

        if (!Directory.Exists($"{DocsDir}\\Fortunevale\\StreamableUploader\\"))
            Directory.CreateDirectory($"{DocsDir}\\Fortunevale\\StreamableUploader\\");

        ConfigDir = $"{DocsDir}\\Fortunevale\\StreamableUploader\\";
        Environment.CurrentDirectory = $"{DocsDir}\\Fortunevale\\StreamableUploader\\";

        if (!File.Exists($"{ConfigDir}config.json"))
            new Config().Save();

        LoadedConfig = JsonConvert.DeserializeObject<Config>(File.ReadAllText($"{ConfigDir}config.json")) ?? new Config();

        InitializeComponent();

        var arguments = Environment.GetCommandLineArgs().ToList();
        if (arguments.Contains("--file"))
        {
            if (!SelectVideo(arguments[arguments.IndexOf("--file") + 1]))
            {
                MessageBox.Show("The file you're trying to upload is not a video file.", "Exception occured.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (arguments.Contains("--instant-upload"))
                {
                    InstantUpload = true;
                }
            }
        }

        if (arguments.Contains("--add-context") || arguments.Contains("--remove-context"))
        {
            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                MessageBox.Show("The app needs to be running as administrator in order to be added to the context menu.", "Exception occured.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                Environment.Exit(0);
                return;
            }

            RegistryKey? SystemFileAssociationsKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Classes\\SystemFileAssociations");

            if (SystemFileAssociationsKey is null)
            {
                MessageBox.Show("You seem to be running an unsupported operating system.", "Exception occured.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                Environment.Exit(0);
                return;
            }

            if (arguments.Contains("--add-context"))
            {
                foreach (var type in videoFormats.Values)
                {
                    var location = Process.GetCurrentProcess().MainModule?.FileName;

                    RegistryKey? fileTypeKey = SystemFileAssociationsKey.OpenSubKey(type, true);
                    if (fileTypeKey is null)
                        fileTypeKey = SystemFileAssociationsKey.CreateSubKey(type);



                    RegistryKey? shellKey = fileTypeKey.OpenSubKey("shell", true);
                    if (shellKey is null)
                        shellKey = fileTypeKey.CreateSubKey("shell");



                    RegistryKey key = shellKey.CreateSubKey($"FortunevaleUploadStreamable");
                    key.SetValue("", "Share Video");
                    key.SetValue("Icon", $"\"{ConfigDir}\\streamable.ico\",0");

                    RegistryKey commandKey = key.CreateSubKey("command");
                    commandKey.SetValue("", $"\"{location}\" {(arguments.Contains("--use-instant-upload") ? "--instant-upload " : "")}--file \"%1\"");
                }

                Environment.Exit(0);
                Application.Exit();
                return;
            }

            if (arguments.Contains("--remove-context"))
            {
                foreach (var type in videoFormats.Select(x => x.Value).Append("*"))
                {
                    RegistryKey? subKey = SystemFileAssociationsKey.OpenSubKey($"{type}\\FortunevaleUploadStreamable", true);

                    if (subKey is not null)
                        subKey.DeleteSubKeyTree("");
                }

                foreach (var type in videoFormats.Select(x => x.Value).Append("*"))
                {
                    RegistryKey? subKey = SystemFileAssociationsKey.OpenSubKey($"{type}\\shell\\FortunevaleUploadStreamable", true);

                    if (subKey is not null)
                        subKey.DeleteSubKeyTree("");
                }

                Environment.Exit(0);
                Application.Exit();
                return;
            }
        }
    }

    bool ContextMenuOptionExists = false;
    private async void Form1_Shown(object sender, EventArgs e)
    {
        this.Text += $" | v{Version}";

        try
        {
            RegistryKey? SystemFileAssociationsKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Classes\\SystemFileAssociations");

            if (SystemFileAssociationsKey is null)
            {
                MessageBox.Show("You seem to be running an unsupported operating system.", "Exception occured.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                Environment.Exit(0);
                return;
            }

            foreach (var type in videoFormats.Values)
            {
                RegistryKey? subKey = SystemFileAssociationsKey.OpenSubKey($"{type}\\shell\\FortunevaleUploadStreamable", false);

                if (subKey is not null)
                {
                    if (((string)(subKey.OpenSubKey("command")?.GetValue("") ?? "")).Contains("--instant-upload"))
                        ContextMenuInstantUpload = true;

                    ContextMenuOptionExists = true;
                    contextMenuButton.Text = "Delete Context Menu Option..";

                    instantUploadToggle.Checked = ContextMenuInstantUpload;
                    break;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        _ = Task.Run(async () =>
        {
            await Task.Delay(100);
            this.Invoke(new Action(() =>
            {
                if (InstantUpload)
                {
                    uploadButton_Click(this, EventArgs.Empty);
                }
            }));
        });

        if (File.Exists($"{ConfigDir}streamable.ico"))
        {
            this.Icon = new Icon($"{ConfigDir}streamable.ico");
            return;
        }

        HttpClient client = new HttpClient();
        var IconStream = await client.GetStreamAsync("https://statics.streamable.com/static/favicon.ico");
        var IconBitmap = Image.FromStream(IconStream);
        this.Icon = Utilities.IconFromImage(IconBitmap);

        using (FileStream outputStream = File.OpenWrite($"{ConfigDir}streamable.ico"))
        {
            this.Icon.Save(outputStream);
        }
    }

    FileInfo? Video = null;

    private bool SelectVideo(string Path)
    {
        var b = new FileInfo(Path);

        if (!b.Exists)
            return false;

        if (!videoFormats.Any(x => b.FullName.EndsWith(x.Value, StringComparison.CurrentCultureIgnoreCase)))
            return false;

        Video = b;
        titleBox.Text = b.Name[..^b.Extension.Length];
        pathDisplay.Text = b.FullName;

        return true;
    }

    private void ChangeStatus(string status, int prog = 0, Color? color = null)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(ChangeStatus, status, prog, color);
            return;
        }

        color ??= Color.Gray;

        this.progressLabel.Text = status;
        this.progressBar1.Value = prog;
        this.progressLabel.ForeColor = color.Value;
    }

    private void EnableUI(bool enable)
    {
        browseButton.Enabled = enable;
        titleBox.Enabled = enable;
        uploadButton.Enabled = enable;
        progressBar1.Visible = !enable;
        contextMenuButton.Enabled = enable;
        updateLoginButton.Enabled = enable;
        instantUploadToggle.Enabled = enable;
    }

    private async void uploadButton_Click(object sender, EventArgs e)
    {
        if (Video is null || LoadedConfig is null)
        {
            ChangeStatus("* Video required.", 0, Color.Red);
            return;
        }

        EnableUI(false);

        (string Username, string Password) loginData;

        try
        {
            loginData = LoadedConfig.RetrieveUsernameAndPassword();
        }
        catch (NullReferenceException)
        {
            Authentication authentication = new Authentication();
            if (authentication.ShowDialog() == DialogResult.OK)
            {
                LoadedConfig.SetUsernameAndPassword(authentication.RetrievedUsername, authentication.RetrievedPassword);
                loginData = LoadedConfig.RetrieveUsernameAndPassword();
            }
            else
            {
                EnableUI(true);
                return;
            }
        }

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.streamable.com/upload");
        request.Headers.Add("Authorization", Utilities.GetBasicAuthHeader(loginData.Username, loginData.Password));
        var content = new MultipartFormDataContent();
        FileStream videoStream = Video.OpenRead();
        content.Add(new StreamContent(videoStream), titleBox.Text, titleBox.Text);
        request.Content = content;

        _ = Task.Run(async () =>
        {
            while (videoStream.CanRead)
            {
                this.Invoke(new Action(() =>
                {
                    var prog = UniversalExtensions.CalculatePercentage(videoStream.Position, Video.Length);
                    ChangeStatus($"{prog}% | Uploading your video..", prog, Color.Green);
                }));
                await Task.Delay(100);
            }
        });

        try
        {
            var response = (await client.SendAsync(request)).EnsureSuccessStatusCode();
            StreamableResponse? parsedResponse = JsonConvert.DeserializeObject<StreamableResponse>(await response.Content.ReadAsStringAsync());

            if (parsedResponse is null)
                throw new NullReferenceException("Response could not be parsed.");

            Clipboard.SetText($"https://streamable.com/{parsedResponse.shortcode}");

            this.Hide();
            MessageBox.Show($"Upload completed. Copied video url to your clipboard: 'https://streamable.com/{parsedResponse.shortcode}'.", "Video uploaded.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        catch (IOException)
        {
            ChangeStatus($"An error prevented the upload from completing.", 0, Color.Red);
            MessageBox.Show("Unable to upload your video. Please check your password.", "Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (HttpRequestException ex)
        {
            ChangeStatus($"An error prevented the upload from completing.", 0, Color.Red);

            if (ex.StatusCode == HttpStatusCode.Forbidden)
            {
                MessageBox.Show("Unable to upload your video. Please check your password.", "Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                MessageBox.Show("The upload endpoint no longer exists, please check the GitHub repository for an update.", "Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ex.StatusCode == HttpStatusCode.InternalServerError)
            {
                MessageBox.Show("Streamable was unable to process the upload request, please try again. If the issue persists, check their site directly.", "Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"StatusCode {(int?)(ex.StatusCode) ?? -1} encountered, Exception unhandled.", "Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            ChangeStatus($"An error prevented the upload from completing.", 0, Color.Red);
            MessageBox.Show(ex.Message, "Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            throw;
        }
        finally
        {
            EnableUI(true);
            videoStream.Close();
        }
    }

    private void browseButton_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            Title = "Select a video..",
            Filter = "All files (*.*)|*.*|" + string.Join("|", videoFormats.Select(x => $"{x.Key} ({x.Value})|*{x.Value}")),
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            SelectVideo(openFileDialog.FileName);
        }
    }

    private void updateLoginButton_Click(object sender, EventArgs e)
    {
        Authentication authentication = new Authentication();
        if (authentication.ShowDialog() == DialogResult.OK)
        {
            LoadedConfig?.SetUsernameAndPassword(authentication.RetrievedUsername, authentication.RetrievedPassword);
        }
    }

    private void contextMenuButton_Click(object sender, EventArgs e)
    {
        try
        {
            var location = Process.GetCurrentProcess().MainModule?.FileName;

            if (location == null)
                return;

            if (ContextMenuOptionExists)
            {
                if (ContextMenuInstantUpload != instantUploadToggle.Checked)
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = location,
                        Arguments = $"--add-context{(instantUploadToggle.Checked ? " --use-instant-upload" : "")}",
                        Verb = "runas",
                        UseShellExecute = true
                    });

                    ContextMenuInstantUpload = instantUploadToggle.Checked;
                    ContextMenuOptionExists = true;
                    contextMenuButton.Text = "Delete Context Menu Option..";
                    return;
                }

                Process.Start(new ProcessStartInfo()
                {
                    FileName = location,
                    Arguments = "--remove-context",
                    Verb = "runas",
                    UseShellExecute = true
                });

                ContextMenuOptionExists = false;
                contextMenuButton.Text = "Create Context Menu Option..";
                return;
            }

            Process.Start(new ProcessStartInfo()
            {
                FileName = location,
                Arguments = $"--add-context{(instantUploadToggle.Checked ? " --use-instant-upload" : "")}",
                Verb = "runas",
                UseShellExecute = true
            });

            ContextMenuInstantUpload = instantUploadToggle.Checked;
            ContextMenuOptionExists = true;
            contextMenuButton.Text = "Delete Context Menu Option..";
        }
        catch { }
    }

    private void instantUploadToggle_CheckedChanged(object sender, EventArgs e)
    {
        if (ContextMenuOptionExists)
        {
            if (ContextMenuInstantUpload != instantUploadToggle.Checked)
            {
                contextMenuButton.Text = "Modify Context Menu Option..";
            }
            else
            {
                contextMenuButton.Text = "Delete Context Menu Option..";
            }
        }
    }
}
