namespace StreamableUploader.Util;

public class Utilities
{
    public static string GetBasicAuthHeader(string username, string password)
        => $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))}";

    public static Icon IconFromImage(Image img)
    {
        var ms = new System.IO.MemoryStream();
        var bw = new System.IO.BinaryWriter(ms);
        bw.Write((short)0);
        bw.Write((short)1);
        bw.Write((short)1);
        var w = img.Width;
        if (w >= 256)
            w = 0;
        bw.Write((byte)w);
        var h = img.Height;
        if (h >= 256)
            h = 0;
        bw.Write((byte)h);
        bw.Write((byte)0);
        bw.Write((byte)0);
        bw.Write((short)0);
        bw.Write((short)0);
        var sizeHere = ms.Position;
        bw.Write((int)0);
        var start = (int)ms.Position + 4;
        bw.Write(start);
        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        var imageSize = (int)ms.Position - start;
        ms.Seek(sizeHere, System.IO.SeekOrigin.Begin);
        bw.Write(imageSize);
        ms.Seek(0, System.IO.SeekOrigin.Begin);
        return new Icon(ms);
    }
}
