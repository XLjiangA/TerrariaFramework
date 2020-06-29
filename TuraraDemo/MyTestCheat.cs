using GameHack;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

[HInfo("Turara", "GameDemo", "0.0.2.2")]
public class MyTestCheat : GameH
{
    private UserInterface InGameUI_map = new UserInterface();
    private UserInterface InGameUI_main = new UserInterface();
    private CheatUI Cu = null;
    private MapClone Mc = null;
    public static bool CanESP = false;
    public static bool CanGodMode = false;
    public static bool ItemMode = false;
    public static bool OpenEditMap = false;

    public override void Start()
    {
        Main.InGameUIDic["M"] = InGameUI_map;
        Mc = new MapClone();
        Main.InGameUIDic["A"] = InGameUI_main;
        Cu = new CheatUI();

    }
    public override void Update()
    {
        if (CanGodMode)
        {
            var selfPLayer = Terraria.Main.player[Terraria.Main.myPlayer];
            selfPLayer.statLife = selfPLayer.statLifeMax;
            selfPLayer.statMana = selfPLayer.statManaMax;
        }
        if (ItemMode)
        {
            var selfPLayer = Terraria.Main.player[Terraria.Main.myPlayer];
            foreach (var i in selfPLayer.inventory)
            {
                i.stack = i.maxStack;
            }
        }
        /* if (Terraria.Main.keyState.IsKeyDown(Keys.Q))
         {
           var selfPLayer = Terraria.Main.player[Terraria.Main.myPlayer];
           var i = Item.NewItem(selfPLayer.position,selfPLayer.Size,9,999);
           Main.item[i].netDefaults(9);
           Main.item[i].stack = 999;
           Main.item[i].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
           Main.item[i].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
           Main.item[i].noGrabDelay = 100;
           Main.item[i].newAndShiny = false;
           Terraria.Main.NewText($"哇,{selfPLayer.name}从虚空从中拿出了{Main.item[i].stack}个{Main.item[i].Name}!!!", 255, 255, 0);
         }
         */
        if (InputUtils.Key_Q)
        {
            OpenEditMap = !OpenEditMap;
        }
        if (OpenEditMap)
        {
            if (InGameUI_map.CurrentState != Mc)
            {
                InGameUI_map.SetState(Mc);
            }
        }
        else
        {
            if (InGameUI_map.CurrentState == Mc)
            {
                InGameUI_map.SetState(null);
            }
        }
        if (Main.playerInventory)
        {
            if (InGameUI_main.CurrentState != Cu)
            {
                InGameUI_main.SetState(Cu);
            }
        }
        else
        {
            if (InGameUI_main.CurrentState == Cu)
            {
                InGameUI_main.SetState(null);
            }
        }

    }
    public override void OnGUI(GameTime gameTime)
    {
        if (OpenEditMap)
        {

            if (Mc.Photon)
            {
                var esp = Mc.EndPoint.Point.ToScreenPosition();
                Terraria.Main.spriteBatch.DrawString(FontAssets.MouseText.Value, $"X:{Mc.EndPoint.Point.X},Y:{Mc.EndPoint.Point.Y}", new Vector2(esp.X, esp.Y + 10), Color.Yellow);
                Terraria.Utils.DrawRectangle(Main.spriteBatch, Mc.StartPoint.Point, Mc.EndPoint.Point, Color.OrangeRed, Color.DarkRed, 2f);
            }
        }
        if (CanESP)
        {
            int i = 0;
            foreach (var n in Main.npc)
            {
                if (!n.friendly && n.active)
                {
                    var nlPos = new Vector2(n.position.X, n.position.Y + n.height);
                    var nPos = n.position;
                    var nSize = new Vector2(n.position.X + n.width, n.position.Y + n.height);
                    var self = Main.player[Main.myPlayer];
                    var sPos = new Vector2(self.position.X, self.position.Y + self.height);
                    var Dis = (int)Terraria.Utils.Distance(sPos, nPos);
                    Terraria.Main.spriteBatch.DrawString(FontAssets.MouseText.Value, n.FullName, nPos.ToScreenPosition(), Color.DeepSkyBlue);
                    Terraria.Main.spriteBatch.DrawString(FontAssets.MouseText.Value, $"距离:{Dis}像素", nlPos.ToScreenPosition(), Color.Yellow);
                    Terraria.Utils.DrawRectangle(Main.spriteBatch, nPos, nSize, Color.Red, Color.LightSkyBlue, 1.5f);
                    Terraria.Utils.DrawLine(Main.spriteBatch, nlPos, sPos, Color.Red, Color.LightSkyBlue, 1.5f);
                    i++;
                }

            }
            Terraria.Main.spriteBatch.DrawString(FontAssets.MouseText.Value, $"邪恶NPC总数:{i}", new Vector2(500, 600), Color.Yellow);
        }
    }
    public override void Exit()
    {
    }
}

