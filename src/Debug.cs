using Godot;

namespace DemonKingSwarn.Utils;

public class Debug
{
  public static void Error(string msg)
  {
    GD.PrintRich($"[color=red]{msg}[/color]");
  }

  public static void Warning(string msg)
  {
    GD.PrintRich($"[color=yellow]{msg}[/color]");
  }

  public static void Log(string msg)
  {
    GD.PrintRich(msg);
  }
}
