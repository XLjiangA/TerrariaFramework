using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent;
using System;

namespace GameHack.TuraraUI
{
    public class TextBox : UIPanel
    {
        public event ElementEvent OnTextChange;

        protected string _text;

        protected float _textScale = 1f;

        protected Vector2 _textSize = Vector2.Zero;

        protected Color _color = Color.White;

        protected bool _drawPanel = true;

        public float TextHAlign = 0.5f;

        private int _cursor;

        private int _frameCount;

        private int _maxLength = 4;

        public bool ShowInputTicker = true;

        public bool HideSelf;

        public bool DrawPanel
        {
            get
            {
                return this._drawPanel;
            }
            set
            {
                this._drawPanel = value;
            }
        }

        public float TextScale
        {
            get
            {
                return this._textScale;
            }
            set
            {
                this._textScale = value;
            }
        }
        public Vector2 TextSize
        {
            get
            {
                return this._textSize;
            }
        }
        public string Text
        {
            get
            {
                if (this._text != null)
                {
                    return this._text.ToString();
                }
                return "";
            }
        }
        public Color TextColor
        {
            get
            {
                return this._color;
            }
            set
            {
                this._color = value;
            }
        }
        public TextBox(string text, float textScale = 1f)
        {
            this.SetText(text, textScale);
        }
        public void BaseSetText(string text)
        {
            this.SetText(text, this._textScale);
        }
        public void BaseSetText(string text, float textScale)
        {
            Vector2 vector = new Vector2((FontAssets.MouseText.Value).MeasureString(text.ToString()).X, 16f) * textScale;
            this._text = text;
            this._textScale = textScale;
            this._textSize = vector;
        }

        protected void BaseDrawSelf(SpriteBatch spriteBatch)
        {
            if (this._drawPanel)
            {
                base.DrawSelf(spriteBatch);
            }
            this.DrawText(spriteBatch);
        }
        protected void DrawText(SpriteBatch spriteBatch)
        {
            CalculatedStyle innerDimensions = base.GetInnerDimensions();
            Vector2 pos = innerDimensions.Position();
            pos.Y -= 8f * this._textScale;
            pos.X += (innerDimensions.Width - this._textSize.X) * this.TextHAlign;
            Utils.DrawBorderString(spriteBatch, this.Text, pos, this._color, this._textScale, 0f, 0f, -1);
        }

        public void Write(string text)
        {
            BaseSetText(Text.Insert(this._cursor, text));
            this._cursor += text.Length;
            OnTextChange(this);
        }

        public void SetText(string text, float textScale)
        {
            if (text == null)
            {
                text = "";
            }
            if (text.Length > this._maxLength)
            {
                text = text.Substring(0, this._maxLength);
            }
            BaseSetText(text, textScale);
            this._cursor = Math.Min(Text.Length, this._cursor);
        }

        public void SetTextMaxLength(int maxLength)
        {
            this._maxLength = maxLength;
        }
        public void Backspace()
        {
            if (this._cursor == 0)
            {
                return;
            }
            BaseSetText(Text.Substring(0, Text.Length - 1));
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (this.HideSelf)
            {
                return;
            }
            this._cursor = Text.Length;
            BaseDrawSelf(spriteBatch);
            this._frameCount++;
            if ((this._frameCount %= 40) > 20)
            {
                return;
            }
            if (this.ShowInputTicker)
            {
                CalculatedStyle innerDimensions = base.GetInnerDimensions();
                Vector2 pos = innerDimensions.Position();
                Vector2 vector = new Vector2((FontAssets.MouseText.Value).MeasureString(Text.Substring(0, this._cursor)).X, 16f) * TextScale;
                pos.Y -= 8f * TextScale;
                pos.X += (innerDimensions.Width - TextSize.X) * this.TextHAlign + vector.X - (4f) * TextScale + 6f;
                Utils.DrawBorderString(spriteBatch, "|", pos, TextColor, TextScale, 0f, 0f, -1);
            }
        }

    }
}
