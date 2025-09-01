# Encryption

This script creates encryption key and initialization vector and also lets the user encrypt or decrypt a file.

Uses AES 256 CBC to encrypt the files.

**Example**:

```csharp
using Godot;
using DemonKingSwarn.Utils;

using FileAccess = Godot.FileAccess;

[GlobalClass]
public partial class SaveManager : Node
{
  string saveFile = "/path/to/some/save";

  public SaveManager() 
  {
    // If the key isn't already generated, will generate a new one
    Encryption.GenerateKey();
  }

  void Save()
  {
    string tempFile = saveFile + ".tmp";
    Encryption.EncryptFile(saveFile, tempFile);
    DirAccess.RenameAbsolute(tempFile, saveFile);
  }
 
  void Load()
  {
    string tempFile = saveFile + ".tmp";
    Encryption.DecryptFile(saveFile, tempFile);
    // Load that tempFile
  }
}
```
