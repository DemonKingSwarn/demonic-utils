# FileOps

This script provides a memory efficient way to copy a file.

**Example**:

```csharp
using Godot;
using DemonKingSwarn.Utils;

public partial class Example : Node
{
  public override void _Ready()
  {
    FileOps.Copy(src, dest);
  }
}
```
