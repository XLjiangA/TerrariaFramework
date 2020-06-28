using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using GameHack;


static class HackRun
{
    static GameFinds GF = null;

    static bool DebugIsShow = false;

    static Dictionary<string, Assembly> AsmloadDic = new Dictionary<string, Assembly>();
    static Assembly AsmRef(string resourceName)
    {
        if (AsmloadDic.ContainsKey(resourceName))
        {
            return AsmloadDic[resourceName];
        }
        Assembly result = null;
        using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        {
            byte[] array = new byte[manifestResourceStream.Length];
            manifestResourceStream.Read(array, 0, array.Length);
            result = Assembly.Load(array);
        }
        return AsmloadDic[resourceName] = result;
    }

    static void OopenConsole(bool DebugIsShow)
    {
        if (DebugIsShow)
        {
            ConsoleUtils.Create();
        }
    }
    [STAThread]
    static void Main(string[] args)
    {

        DebugIsShow = args.Contains("-Console");
        OopenConsole(DebugIsShow);
        AppDomain.CurrentDomain.AssemblyResolve += delegate (object sender, ResolveEventArgs sargs)
        {

            string resourceName = new AssemblyName(sargs.Name).Name;
            return AsmRef(resourceName);

        };
        AsmRef("Terraria");
        GF = new GameFinds();
        GF.Init("GameMods");
        bool gameRun = true;
        TuraraGame.GameUpdate = GameUpdate;
        new Thread(() =>
        {
            while (gameRun)
            {
                TuraraGame.GameUpdate.Invoke();
                Thread.Sleep(100);
            }
        })
        {
            IsBackground = true
        }.Start();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("============================================");
        Console.ResetColor();
        Debug.Log("[Turara] 欢迎使用Turara框架v2.0", ConsoleColor.Magenta);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("============================================");
        Console.ResetColor();
        Debug.Log("[Turara] 当前Mod列表:", ConsoleColor.DarkCyan);
        var htl = new List<GameH>(GF.HackTypeList);
        int i = 0;
        foreach (var ht in htl)
        {

            var hi = ht.GetType().GetCustomAttribute<HInfo>();
            if (hi != null)
            {
                i++;
                Debug.Log($" {hi.HName}({hi.Version}) by {hi.Developer}", ConsoleColor.Green, $"[{i}]");
            }
            else
            {
                GF.HackTypeList.Remove(ht);
            }
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("============================================");
        Console.ResetColor();
        Debug.Log("本框架完全基于.Net 4.5编写.", ConsoleColor.DarkRed);
        Debug.Log("Made out of interest :)", ConsoleColor.DarkRed);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("============================================");
        Console.ResetColor();
        args = new string[] { GF.IsFormSteam.ToString() };
        AsmloadDic["Terraria"].EntryPoint.Invoke(null, new object[] { args });
        GF.ListExit();
        gameRun = false;
    }
    public static bool JustFirstLoad = false;
    public static void GameGUI(GameTime gameTime)
    {
        try
        {
            if (JustFirstLoad)
            {
                GF.ListOnGUI(gameTime);
            }
        }
        catch
        {
        }

    }
    public static void GameUpdate()
    {
        try
        {
            if (Terraria.Main.instance != null && !JustFirstLoad)
            {
                var Ver = "-Turara" + (GF.IsFormSteam ? "(正版)" : "(非正版)");
                Terraria.Main.versionNumber += Ver;
                Terraria.Main.versionNumber2 += Ver;
                Terraria.Main.instance.gameGUI = GameGUI;
                GF.ListStart();
                JustFirstLoad = true;
            }
            if (JustFirstLoad)
            {
                GF.ListUpdate();
            }
        }
        catch
        {
        }

    }
}
public static class TuraraDelegate
{
    public delegate void GameUpdateDelegate();

}
public static class TuraraGame
{
    public static TuraraDelegate.GameUpdateDelegate GameUpdate;
}

