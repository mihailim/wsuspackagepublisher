using System;

namespace ScriptLauncher
{
    class Program
    {
        static int Main(string[] args)
        {
            string verb = string.Empty;
            string command = string.Empty;

            if (args.Length == 0)
                return 255;         // ScriptLauncher must be launch with one argument.

            if (args[0].ToLower().StartsWith("registry:"))
            {
                verb = "registry";
                command = args[0].Substring(9);
            }
            if (args[0].ToLower().StartsWith("file:"))
            {
                verb = "file";
                command = args[0].Substring(5);
            }

            try
            {
                switch (verb)
                {
                    case "file":
                        return LaunchFile(command);
                    case "registry":
                        return LaunchRegistry(command);
                    default:
                        return 254; // ScriptLauncher has no valid argument.
                }
            }
            catch (Exception ex)
            {
                return 65535;       // ScriptLauncher throw an Exception;
            }
        }

        private static int LaunchFile(string filePath)
        {
            string file = string.Empty;
            string arguments = string.Empty;
            char[] quote = new char[] { '"' };

            if (filePath.IndexOf('/') != -1)
            {
                file = filePath.Substring(0, filePath.IndexOf('/') - 1);
                arguments = filePath.Substring(filePath.IndexOf('/'), filePath.Length - file.Length - 1);
            }
            else
                file = filePath;

            file = file.Trim(quote);

            if (!System.IO.File.Exists(file))
                return 253;         // ScriptLauncher has not found the file;

            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo(file, arguments);
            sInfo.CreateNoWindow = true;
            sInfo.UseShellExecute = false;
            System.Diagnostics.Process process = System.Diagnostics.Process.Start(sInfo);

            while (!process.HasExited)
            {
                System.Threading.Thread.Sleep(1000);
            }
            return process.ExitCode;
        }

        private static int LaunchRegistry(string key)
        {
            Microsoft.Win32.RegistryKey hiveKey;
            string subKeyName;
            Microsoft.Win32.RegistryKey subKey;
            string valueName;


            if (key.EndsWith(@"\"))
                key = key.Substring(0, key.LastIndexOf(@"\") - 1);

            if (key.IndexOf(@"\") == -1)
                return 252;         // ScriptLauncher was launch with improper RegistryKey.

            string hive = key.Substring(0, key.IndexOf(@"\"));

            switch (hive.ToUpper())
            {
                case "HKEY_LOCAL_MACHINE":
                    hiveKey = Microsoft.Win32.Registry.LocalMachine;
                    break;
                case "HKEY_CURRENT_USER":
                    hiveKey = Microsoft.Win32.Registry.CurrentUser;
                    break;
                default:
                    return 251;     // ScriptLauncher was launch with improper RegistryHive value.
            }

            valueName = key.Substring(key.LastIndexOf(@"\") + 1);
            int start = hive.Length + 1;
            int length = key.IndexOf(valueName) - 1;
            subKeyName = key.Substring(start, length - start);
            subKey = hiveKey.OpenSubKey(subKeyName, false);
            object data = subKey.GetValue(valueName);

            return LaunchFile(data.ToString());
        }
    }
}
