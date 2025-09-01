# Debug

This script provides a unity like `Debug.*` classes for Godot.

**Example**:

```csharp
using Godot;
using DemonKingSwarn.Utils;

public partial class Example : Node
{
  public override void _Ready()
  {
    Debug.Log("just a print");
    Debug.Warning("[!] This is a warning!");
    Debug.Error("ERROR!");
  }
}
```
