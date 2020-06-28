using GameHack;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

[HInfo("Turara", "GameDemo", "0.0.2.2")]
public class MyTestCheat : GameH
{
    private UserInterface InGameUI = new UserInterface();
    private CheatUI Cu = null;
    public static bool CanESP = false;
    public static bool CanGodMode = false;
    public static bool ItemMode = false;
    public override void Start()
    {
        Main.InGameUIDic["A"] = InGameUI;

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

        if (Main.playerInventory)
        {
            if (InGameUI.CurrentState != Cu)
            {
                InGameUI.SetState(Cu);
            }
        }
        else
        {
            if (InGameUI.CurrentState == Cu)
            {
                InGameUI.SetState(null);
            }
        }

    }
    public override void OnGUI(GameTime gameTime)
    {
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

