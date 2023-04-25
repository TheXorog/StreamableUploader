namespace StreamableUploader.Entities;

public class Config
{
    public void Save() 
        => File.WriteAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Fortunevale\\StreamableUploader\\config.json", JsonConvert.SerializeObject(this));

    public string EncryptedUsername { get; set; } = "";

    public string EncryptedPassword { get; set; } = "";

    public void SetUsernameAndPassword(string newUsername, string newPassword)
    {
        EncryptedUsername = Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(newUsername), null, DataProtectionScope.CurrentUser));
        EncryptedPassword = Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(newPassword), null, DataProtectionScope.CurrentUser));

        this.Save();
    }

    public (string Username, string Password) RetrieveUsernameAndPassword()
    {
        if (EncryptedUsername.IsNullOrWhiteSpace() || EncryptedPassword.IsNullOrWhiteSpace())
            throw new NullReferenceException("Missing username or password.");

        var username = Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(EncryptedUsername), null, DataProtectionScope.CurrentUser));
        var password = Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(EncryptedPassword), null, DataProtectionScope.CurrentUser));

        if (username.IsNullOrWhiteSpace() || password.IsNullOrWhiteSpace())
            throw new NullReferenceException("Missing username or password.");

        return (username, password);
    }
}
