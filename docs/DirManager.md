# DirManager

This script manages directory creation if it doesnt exist and it returns the directory in the form of string. It also provides a boolean which checks if the game is running under wine or not, wine is a compatibility layer which lets you run windows apps and games under Linux and Mac.

**Example**:

```csharp
using Godot;
using DemonKingSwarn.Utils;

public partial class Example : Node 
{
  string gameName = "SuperPacker";
  string gameDir;
  
  public override void _Ready()
  {
    // On Windows it will be located at %APPDATA%/gameName
    // On Mac and Linux it will be located at ~/.config/gameName
    gameDir = DirManager.Init(gameName);
    
    if(DirManager.isRunningInWine) Debug.Log("We are inside wine!");
  }
}
```
