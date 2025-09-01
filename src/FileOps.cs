using Godot;
using FileAccess = Godot.FileAccess;

namespace DemonKingSwarn.Utils;

public class FileOps
{
    public static void Copy(string source, string destination)
    {
        using var sourceFile = FileAccess.Open(source, FileAccess.ModeFlags.Read);
        using var destinationFile = FileAccess.Open(destination, FileAccess.ModeFlags.Write);
        long chunkSize = 8192;
        while (sourceFile.GetPosition() < sourceFile.GetLength())
        {
            ulong remainingBytes = sourceFile.GetLength() - sourceFile.GetPosition();
            long bytesToRead = (long)Mathf.Min(chunkSize, (long)remainingBytes);
            byte[] chunk = sourceFile.GetBuffer(bytesToRead);
            destinationFile.StoreBuffer(chunk);
        }

        sourceFile.Close();
        destinationFile.Close();
    }
}
