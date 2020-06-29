using GameHack.TuraraUI;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

public class MapClone : UIState
{
    private bool _hovered;

    private UITextPanel<string> _BGPanel;

    public UIList _TempMapList;

    public MapPoint StartPoint = new MapPoint();

    public MapPoint EndPoint = new MapPoint();

    private MapTemp _UseTile = null;

    public bool Photon = false;

    public bool Photoned = false;


    public override void OnActivate()
    {
        _BGPanel = new UITextPanel<string>("");
        _BGPanel.Width.Set(100, 0);
        _BGPanel.Height.Set(80, 0);
        _BGPanel.Top.Set(20, 0f);
        _BGPanel.Left.Set(1270, 0f);
        _BGPanel.OnMouseOver += Mc_Over;
        _BGPanel.OnMouseOut += Mc_Out;
        var _CopyButton = new UITextPanel<string>("复制快照", 0.75f);
        _CopyButton.Top.Set(-7, 0f);
        _CopyButton.Left.Set(-5, 0f);
        _CopyButton.OnClick += CopyPhoto_Click;
        _CopyButton.OnMouseOver += UIEventUtils.FadedMouseOver;
        _CopyButton.OnMouseOut += UIEventUtils.FadedMouseOut;
        _BGPanel.Append(_CopyButton);
        var _CutButton = new UITextPanel<string>("剪切快照", 0.75f);
        _CutButton.Top.Set(27, 0f);
        _CutButton.Left.Set(-5, 0f);
        _CutButton.OnClick += CutPhoto_Click;
        _CutButton.OnMouseOver += UIEventUtils.FadedMouseOver;
        _CutButton.OnMouseOut += UIEventUtils.FadedMouseOut;
        _BGPanel.Append(_CutButton);
        base.Append(_BGPanel);
        base.OnUpdate += MapClone_OnUpdate;
    }
    private void Mc_Over(UIMouseEvent evt, UIElement listeningElement)
    {
        this._hovered = true;
    }
    private void Mc_Out(UIMouseEvent evt, UIElement listeningElement)
    {
        this._hovered = false;
    }
    private bool CheckPhoton()
    {
        return _UseTile != null;
    }
    private void rePhoton()
    {
        Photon = false;
        Photoned = false;
    }
    private void CopyPhoto_Click(UIMouseEvent evt, UIElement listeningElement)
    {
        _UseTile = new MapTemp(StartPoint.TilePos, EndPoint.TilePos);
        rePhoton();
        Terraria.Main.NewText($"复制快照[成功].", 128, 0, 128);
        SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
    }
    private void CutPhoto_Click(UIMouseEvent evt, UIElement listeningElement)
    {
        _UseTile = new MapTemp(StartPoint.TilePos,EndPoint.TilePos,true);
        rePhoton();
        Terraria.Main.NewText($"剪切快照[成功].", 128, 0, 128);
        SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
    }
    private void ParseMap()
    {
        if (CheckPhoton())
        {
            _UseTile.ParseTo(mapEditUtils.GetMousePosTile);
            Terraria.Main.NewText($"粘贴快照[成功].", 128, 0, 128);
            SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
        }
    }
    private void MapClone_OnUpdate(UIElement listeningElement)
    {
        if (Main.mouseLeft)
        {
            if (Photon && Photoned && !_hovered)
            {
                rePhoton();
            }
        }
        if (!Photon)
        {
            StartPoint = new MapPoint(Main.MouseWorld, mapEditUtils.GetMousePosTile);
        }
        if (Main.mouseLeft && !Photoned)
        {
            _hovered = true;
            Photon = true;
            EndPoint = new MapPoint(Main.MouseWorld, mapEditUtils.GetMousePosTile);

        }
        if (Main.mouseLeftRelease && Photon&&!Photoned)
        {
            Photoned = true;
            _hovered = false;
        }
        if (this._hovered)
        {
            Main.LocalPlayer.mouseInterface = true;
        }
        if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
        {
            if (InputUtils.key_C)
            {
                CopyPhoto_Click(null, null);
            }
            else if (InputUtils.key_X)
            {
                CutPhoto_Click(null, null);
            }
            else if (InputUtils.key_V)
            {
                ParseMap();
            }
        }
    }
    public void BolckMouse(bool value)
    {
        _hovered = value;
    }
}
public class MapTemp
{
    public Vector2 Start { get; set; }
    public Vector2 End { get; set; }
    public Tile[,] TempTiles { get; set; }

    public MapTemp(Vector2 s, Vector2 e,bool IsClear = false)
    {
        Start = s;
        End = e;
        var _x = (int)(e.X - s.X);
        var _y = (int)(e.Y - s.Y);
        TempTiles = new Tile[Math.Abs(_x), Math.Abs(_y)];
        for (int i= 0; i<_x;i++ )
        {
            for (int j = 0; j < _y; j++)
            {

                var tile = Main.tile[i + (int)e.X - _x, j + (int)e.Y - _y];
                TempTiles[i, j] = (Tile)tile.Clone();
                if (IsClear) tile.ClearEverything();
            }
        }
    }
    public void ParseTo(Vector2 pos)
    {
        var s = Start;
        var e = End;
        var _x = (int)(e.X - s.X);
        var _y = (int)(e.Y - s.Y);
        for (int i = 0; i < _x; i++)
        {
            for (int j = 0; j < _y; j++)
            {
                Main.tile[i + (int)pos.X-_x/2, j + (int)pos.Y-_y] = (Tile)TempTiles[i, j].Clone(); 
            }
        }
    }

}
public class MapPoint
{
    public Vector2 TilePos { get; set; }

    public Vector2 Point { get; set; }

    public MapPoint()
    {

    }

    public MapPoint(Vector2 p, Vector2 t)
    {
        Point = p;
        TilePos = t;
    }

}

