using System;
using System.Runtime.InteropServices;


public class ConsoleUtils
{
    internal static void Create()
    {
        if (AllocConsole())
        {
            var hwndConsole = GetConsoleWindow();
            Console.Title = "Turara v0.0.2.2 Open-Beta";
            SetForegroundWindow(hwndConsole);
        }
    }

    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();
}

