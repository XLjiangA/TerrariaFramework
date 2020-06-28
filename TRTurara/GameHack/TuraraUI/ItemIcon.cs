using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace GameHack.TuraraUI
{
    public class ItemIcon : UIElement
    {
        public ItemIcon(int id, float ItemScale = 1)
        {
            this._item = NewItem(id);
            _ItemScale = ItemScale;
            this.Width.Set(32f * _ItemScale, 0f);
            this.Height.Set(32f * _ItemScale, 0f);
        }
        public void SetItem(int id)
        {
            this._item = NewItem(id);
        }
        private Item NewItem(int id)
        {
            var item = new Item();
            item.SetDefaults(id);
            return item;
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = base.GetDimensions();
            Main.DrawItemIcon(spriteBatch, this._item, dimensions.Center(), Color.White * (IsMouseHovering ? 1 : 0.4f), 32f * _ItemScale);
        }
        private float _ItemScale;
        private Item _item;

    }
}
