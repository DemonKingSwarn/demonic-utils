using Godot;

using FileAccess = Godot.FileAccess;

namespace DemonKingSwarn.Utils;

public class Encryption
{
    private static RandomNumberGenerator? rng;

    private static string keyPath = "user://key";
    private static string ivPath = "user://iv";

    public static void GenerateKey()
    {
        if (FileAccess.FileExists(keyPath) && FileAccess.FileExists(ivPath)) return;

        rng = new RandomNumberGenerator();
        rng.Randomize();

        byte[] keyBytes = new byte[32];
        byte[] ivBytes = new byte[16];

        for (int i = 0; i < keyBytes.Length; i++) keyBytes[i] = (byte)rng.RandiRange(0, 255);
        for (int i = 0; i < ivBytes.Length; i++) ivBytes[i] = (byte)rng.RandiRange(0, 255);

        string keyB64 = System.Convert.ToBase64String(keyBytes);
        string ivB64 = System.Convert.ToBase64String(ivBytes);

        var keyFile = FileAccess.Open(keyPath, FileAccess.ModeFlags.Write);
        var ivFile = FileAccess.Open(ivPath, FileAccess.ModeFlags.Write);

        keyFile.StoreLine(keyB64);
        ivFile.StoreLine(ivB64);

        keyFile.Close();
        ivFile.Close();
    }

    private static string LoadKey(string keyPath)
    {
        string tempKeyPath = keyPath + ".tmp";
        FileOps.Copy(keyPath, tempKeyPath);
        var keyFile = FileAccess.Open(tempKeyPath, FileAccess.ModeFlags.Read);
        DirAccess.RemoveAbsolute(tempKeyPath);
        return keyFile.GetAsText();
    }

    private static string LoadIV(string ivPath)
    {
        string tempIVPath = ivPath + ".tmp";
        FileOps.Copy(ivPath, tempIVPath);
        var ivFile = FileAccess.Open(tempIVPath, FileAccess.ModeFlags.Read);
        DirAccess.RemoveAbsolute(tempIVPath);
        return ivFile.GetAsText();
    }

    public static void EncryptFile(string src, string dest)
    {
        string key = keyPath;
        string iv = ivPath;

        string keyB64 = LoadKey(key);
        string ivB64 = LoadIV(iv);

        byte[] keyBytes = System.Convert.FromBase64String(keyB64);
        byte[] ivBytes = System.Convert.FromBase64String(ivB64);

        var saveFile = FileAccess.Open(src, FileAccess.ModeFlags.Read);
        byte[] fileData = saveFile.GetBuffer((int)saveFile.GetLength());
        saveFile.Close();

        byte[] paddedData = PadData(fileData);

        var aes = new AesContext();
        aes.Start(AesContext.Mode.CbcEncrypt, keyBytes, ivBytes);
        byte[] encryptedData = aes.Update(paddedData);
        aes.Finish();

        var outputFile = FileAccess.Open(dest, FileAccess.ModeFlags.Write);
        outputFile.StoreBuffer(encryptedData);
        outputFile.Close();
    }

    public static void DecryptFile(string src, string dest)
    {
        string key = keyPath;
        string iv = ivPath;
        string keyB64 = LoadKey(key);
        string ivB64 = LoadIV(iv);

        byte[] keyBytes = System.Convert.FromBase64String(keyB64);
        byte[] ivBytes = System.Convert.FromBase64String(ivB64);

        var encryptedFile = FileAccess.Open(src, FileAccess.ModeFlags.Read);
        byte[] encryptedData = encryptedFile.GetBuffer((int)encryptedFile.GetLength());
        encryptedFile.Close();

        var aes = new AesContext();
        aes.Start(AesContext.Mode.CbcDecrypt, keyBytes, ivBytes);
        byte[] decryptedData = aes.Update(encryptedData);
        aes.Finish();

        byte[] originalData = RemovePadding(decryptedData);

        var outputFile = FileAccess.Open(dest, FileAccess.ModeFlags.Write);
        outputFile.StoreBuffer(originalData);
        outputFile.Close();
    }

    private static byte[] PadData(byte[] data)
    {
        int paddingLength = 16 - (data.Length % 16);
        byte[] padded = new byte[data.Length + paddingLength];
        System.Array.Copy(data, padded, data.Length);

        for (int i = data.Length; i < paddingLength; i++) padded[i] = (byte)paddingLength;

        return padded;
    }


    private static byte[] RemovePadding(byte[] paddedData)
    {
        if (paddedData.Length == 0) return paddedData;

        int paddingLength = paddedData[paddedData.Length - 1];
        int dataLength = paddedData.Length - paddingLength;

        byte[] data = new byte[dataLength];
        System.Array.Copy(paddedData, data, dataLength);

        return data;
    }
}
