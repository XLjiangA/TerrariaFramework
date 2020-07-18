using GameHack.TuraraUI;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

public class CheatUI : UIState
{

    private bool _hovered;
    public TextBox _TextBox = null;
    public ItemIcon _imgButton = null;
    public UITextPanel<string> _BGPanel = null;
    public UITextPanel<string> _GodButton = null;
    public UITextPanel<string> _ESPButton = null;
    public UITextPanel<string> _ItemHackButton = null;

    public override void OnActivate()
    {
        InitializePage();
    }
    public void InitializePage()
    {
        _BGPanel = new UITextPanel<string>("");
        _BGPanel.Width.Set(100, 0);
        _BGPanel.Height.Set(150, 0);
        _BGPanel.Top.Set(20, 0f);
        _BGPanel.Left.Set(570, 0f);
        _BGPanel.OnMouseOver += CheatUI_Over;
        _BGPanel.OnMouseOut += CheatUIl_Out; ;
        this.Append(_BGPanel);
        _ESPButton = new UITextPanel<string>("射线透视", 0.75f);
        _ESPButton.Top.Set(21, 0f);
        _ESPButton.Left.Set(-5, 0f);
        _ESPButton.OnClick += ESP_OnClick;
        _ESPButton.OnMouseOver += UIEventUtils.FadedMouseOver;
        _ESPButton.OnMouseOut += UIEventUtils.FadedMouseOut;
        _BGPanel.Append(_ESPButton);
        _GodButton = new UITextPanel<string>("上帝模式", 0.75f);
        _GodButton.Top.Set(56, 0f);
        _GodButton.Left.Set(-5, 0f);
        _GodButton.OnClick += God_OnClick;
        _GodButton.OnMouseOver += UIEventUtils.FadedMouseOver;
        _GodButton.OnMouseOut += UIEventUtils.FadedMouseOut;
        _BGPanel.Append(_GodButton);
        _ItemHackButton = new UITextPanel<string>("无限物品", 0.75f);
        _ItemHackButton.Top.Set(91, 0f);
        _ItemHackButton.Left.Set(-5, 0f);
        _ItemHackButton.OnClick += Item_OnClick;
        _ItemHackButton.OnMouseOver += UIEventUtils.FadedMouseOver;
        _ItemHackButton.OnMouseOut += UIEventUtils.FadedMouseOut;
        _BGPanel.Append(_ItemHackButton);
        _TextBox = new TextBox("1", 0.75f);
        _TextBox.Width.Set(60, 0);
        _TextBox.Height.Set(28, 0);
        _TextBox.Top.Set(-7, 0f);
        _TextBox.Left.Set(-5, 0f);
        _TextBox.OnUpdate += Text_Update;
        _TextBox.OnClick += Text_OnClick;
        _TextBox.OnTextChange += Text_Change;
        _BGPanel.Append(_TextBox);
        _imgButton = new ItemIcon(1, 0.75f);
        _imgButton.Top.Set(-5, 0f);
        _imgButton.Left.Set(55, 0f);
        _imgButton.OnUpdate += _imgButton_OnUpdate;
        _BGPanel.Append(_imgButton);
        OnUpdate += CheatUI_OnUpdate;
    }
    private void CheatUI_OnUpdate(UIElement listeningElement)
    {
        if (this._hovered)
        {
            Main.LocalPlayer.mouseInterface = true;
        }
    }
    private void CheatUI_Over(UIMouseEvent evt, UIElement listeningElement)
    {
        this._hovered = true;
    }
    private void CheatUIl_Out(UIMouseEvent evt, UIElement listeningElement)
    {
        this._hovered = false;
    }
    private void Item_OnClick(UIMouseEvent evt, UIElement listeningElement)
    {
        SoundEngine.PlaySound(8, -1, -1, 1, 1f, 0f);
        MyTestCheat.ItemMode = !MyTestCheat.ItemMode;
        if (MyTestCheat.ItemMode)
        {
            Terraria.Main.NewText($"无限物品[开启].", 0, 255, 0);
        }
        else
        {
            Terraria.Main.NewText($"无限物品[关闭].", 255, 0, 0);
        }
    }
    private void God_OnClick(UIMouseEvent evt, UIElement listeningElement)
    {
        SoundEngine.PlaySound(8, -1, -1, 1, 1f, 0f);
        MyTestCheat.CanGodMode = !MyTestCheat.CanGodMode;
        if (MyTestCheat.CanGodMode)
        {
            Terraria.Main.NewText($"上帝模式[开启].", 0, 255, 0);
        }
        else
        {
            Terraria.Main.NewText($"上帝模式[关闭].", 255, 0, 0);
        }
    }
    private void ESP_OnClick(UIMouseEvent evt, UIElement listeningElement)
    {
        SoundEngine.PlaySound(8, -1, -1, 1, 1f, 0f);
        MyTestCheat.CanESP = !MyTestCheat.CanESP;
        if (MyTestCheat.CanESP)
        {
            Terraria.Main.NewText($"射线透视[开启].", 0, 255, 0);
        }
        else
        {
            Terraria.Main.NewText($"射线透视[关闭].", 255, 0, 0);
        }
    }
    private void Text_OnClick(UIMouseEvent evt, UIElement listeningElement)
    {
        SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
        bool flag = _TextBox.Text.Length == 0;
        _TextBox.Write("");
    }
    private void Text_Change(UIElement listeningElement)
    {
        try
        {

            var id = int.Parse(_TextBox.Text);
            _imgButton.SetItem(id);
        }
        catch
        {
            _imgButton.SetItem(0);
        }
    }
    bool IsKeyUp = false;
    private void Text_Update(UIElement listeningElement)
    {
        if ((Main.keyState.IsKeyDown(Keys.NumPad1) && !IsKeyUp) || (Main.keyState.IsKeyDown(Keys.D1) && !IsKeyUp))
        {
            _TextBox.Write("1");
            IsKeyUp = true;
        }
        else if ((Main.keyState.IsKeyDown(Keys.NumPad2) && !IsKeyUp) || (Main.keyState.IsKeyDown(Keys.D2) && !IsKeyUp))
        {
            _TextBox.Write("2");
            IsKeyUp = true;
        }
        else if ((Main.keyState.IsKeyDown(Keys.NumPad3) && !IsKeyUp) || (Main.keyState.IsKeyDown(Keys.D3) && !IsKeyUp))
        {
            _TextBox.Write("3");
            IsKeyUp = true;
        }
        else if ((Main.keyState.IsKeyDown(Keys.NumPad4) && !IsKeyUp) || (Main.keyState.IsKeyDown(Keys.D4) && !IsKeyUp))
        {
            _TextBox.Write("4");
            IsKeyUp = true;
        }
        else if ((Main.keyState.IsKeyDown(Keys.NumPad5) && !IsKeyUp) || (Main.keyState.IsKeyDown(Keys.D5) && !IsKeyUp))
        {
            _TextBox.Write("5");
            IsKeyUp = true;
        }
        else if ((Main.keyState.IsKeyDown(Keys.NumPad6) && !IsKeyUp) || (Main.keyState.IsKeyDown(Keys.D6) && !IsKeyUp))
        {
            _TextBox.Write("6");
            IsKeyUp = true;
        }
        else if ((Main.keyState.IsKeyDown(Keys.NumPad7) && !IsKeyUp) || (Main.keyState.IsKeyDown(Keys.D7) && !IsKeyUp))
        {
            _TextBox.Write("7");
            IsKeyUp = true;
        }
        else if ((Main.keyState.IsKeyDown(Keys.NumPad8) && !IsKeyUp) || (Main.keyState.IsKeyDown(Keys.D8) && !IsKeyUp))
        {
            _TextBox.Write("8");
            IsKeyUp = true;
        }
        else if ((Main.keyState.IsKeyDown(Keys.NumPad9) && !IsKeyUp) || (Main.keyState.IsKeyDown(Keys.D9) && !IsKeyUp))
        {
            _TextBox.Write("9");
            IsKeyUp = true;
        }
        else if ((Main.keyState.IsKeyDown(Keys.NumPad0) && !IsKeyUp) || (Main.keyState.IsKeyDown(Keys.D0) && !IsKeyUp))
        {
            _TextBox.Write("0");
            IsKeyUp = true;
        }
        else if (Main.keyState.IsKeyDown(Keys.Back)  && !IsKeyUp)
        {
            _TextBox.Backspace();
            IsKeyUp = true;
        }
        else if (Main.keyState.GetPressedKeys().Where(p => { return (p >= (Keys)96 && p <= (Keys)105)||(p>=(Keys)48&&p<=(Keys)57) || p == Keys.Back; }).Count() == 0)
        {
            IsKeyUp = false;
        }

    }
    private void _imgButton_OnUpdate(UIElement listeningElement)
    {
        try
        {
            if (Main.mouseLeft && _imgButton.IsMouseHovering)
            {
                var Id = int.Parse(_TextBox.Text);
                if (Main.mouseItem.type != Id)
                {
                    Main.mouseItem = new Item();
                    Main.mouseItem.netDefaults(Id);
                    Main.mouseItem.stack = 1;
                }
                else
                {
                    Main.mouseItem.stack++;
                    if (Main.mouseItem.stack > Main.mouseItem.maxStack)
                    {
                        Main.mouseItem.stack = Main.mouseItem.maxStack;
                        return;
                    }
                }
                SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
            }
        }
        catch
        {
        }
    }
}