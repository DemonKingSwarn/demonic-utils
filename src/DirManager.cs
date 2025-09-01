using Godot;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace DemonKingSwarn.Utils;

public class DirManager
{
    public static string? fullPath;
    static string winRegistryKey = @"Software\Wine";

    public static bool isRunningInWine = false;

    public static string Init(string gameSaveDir)
    {   
        if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            string homeDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            fullPath = Path.Combine(homeDirectory, ".config", gameSaveDir);
        }
        else if(isWindows())
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(winRegistryKey))
            {
                if (key != null)
                {
                    isRunningInWine = true;
                }    
            }

            string appData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            fullPath = Path.Combine(appData, gameSaveDir);
        }

        if(!Directory.Exists($"{fullPath}"))
        {
            Directory.CreateDirectory(fullPath);            
        }

        return fullPath;
    }


    public static bool isWindows()
    {
      bool runningOnWin = false;
      if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        runningOnWin = true;
      }

      return runningOnWin;
    }
}
