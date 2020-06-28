using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GameHack;


public class GameFinds
{
    public bool IsFormSteam = false;

    public List<GameH> HackTypeList = new List<GameH>();

    private string GameTag = "[TuraraGameConfig]";

    private string GameConfig = "Turara.ini";

    public void writeKey(bool value)
    {
        string[] configs = new string[]
        {
                GameTag,
                $"IsFormSteam:{value}"
        };
        File.WriteAllLines(GameConfig, configs);
    }
    public Dictionary<string, string> Settings = new Dictionary<string, string>();
    public void ReadKey()
    {
        var values = File.ReadAllLines(GameConfig);
        if (values[0] == GameTag)
        {
            try
            {
                var set = values[1].Split(':');
                Settings[set[0]] = set[1];
            }
            catch
            {
            }
        }
    }
    public GameFinds Init(string path)
    {
    LoadSetting:
        if (!File.Exists(GameConfig))
        {
            if (DialogResult.Yes == MessageBox.Show("检测到您是第一次使用本客户端.\r\n请您务必认真回答我的问题\r\n请问您是正版用户吗？", "一个小小的问题⁄(⁄ ⁄•⁄ω⁄•⁄ ⁄)⁄", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                writeKey(true);
            }
            else
            {
                writeKey(false);
            }
        }
        ReadKey();
        try
        {
            IsFormSteam = bool.Parse(Settings["IsFormSteam"]);
        }
        catch
        {
            File.Delete(GameConfig);
            goto LoadSetting;
        }
        if (Directory.Exists(path))
        {

            var files = Directory.GetFiles(path, "*.dll");
            foreach (var file in files)
            {
                var gBytes = File.ReadAllBytes(file);
                var gAsm = Assembly.Load(gBytes);

                var types = gAsm.GetTypes().Where(p => { return p.BaseType == typeof(GameH); });
                if (types.Count() > 0)
                {
                    foreach (var gT in types)
                    {
                        var GH = (GameH)gAsm.CreateInstance(gT.Name, false);
                        HackTypeList.Add(GH);
                    }
                }
            }
            return this;
        }
        Directory.CreateDirectory(path);
        return this;
    }
    public void ListStart()
    {
        HackTypeList.ForEach(a => { a.Start(); });
    }
    public void ListUpdate()
    {
        HackTypeList.ForEach(a => { a.Update(); });
    }
    public void ListOnGUI(GameTime Gametime)
    {
        HackTypeList.ForEach(a => { a.OnGUI(Gametime); });
    }
    public void ListExit()
    {
        HackTypeList.ForEach(a => { a.Exit(); });
    }
}

