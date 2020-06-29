using Microsoft.Xna.Framework.Input;
using Terraria;


public static class InputUtils
{
    private static bool _Key_Q = false;
    public static bool Key_Q
    {
        get
        {
            bool flag = false;
            if (Main.keyState.IsKeyDown(Keys.Q) && !_Key_Q)
            {
                _Key_Q = true;
                flag = true;
            }
            else if (Main.keyState.IsKeyUp(Keys.Q))
            {
                _Key_Q = false;
            }
            return flag;
        }
    }
    private static bool _Key_C = false;
    public static bool key_C
    {
        get
        {
            bool flag = false;
            if (Main.keyState.IsKeyDown(Keys.C) && !_Key_C)
            {
                _Key_C = true;
                flag = true;
            }
            else if (Main.keyState.IsKeyUp(Keys.C))
            {
                _Key_C = false;
            }
            return flag;
        }
    }
    private static bool _Key_X = false;
    public static bool key_X
    {
        get
        {
            bool flag = false;
            if (Main.keyState.IsKeyDown(Keys.X) && !_Key_X)
            {
                _Key_X = true;
                flag = true;
            }
            else if (Main.keyState.IsKeyUp(Keys.X))
            {
                _Key_X = false;
            }
            return flag;
        }
    }
    private static bool _Key_V = false;
    public static bool key_V
    {
        get
        {
            bool flag = false;
            if (Main.keyState.IsKeyDown(Keys.V) && !_Key_V)
            {
                _Key_V = true;
                flag = true;
            }
            else if (Main.keyState.IsKeyUp(Keys.V))
            {
                _Key_V = false;
            }
            return flag;
        }
    }
}

