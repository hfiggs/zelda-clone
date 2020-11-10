using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1.Item
{
    static class ItemListFactory
    {
        public static LinkedList<IItem> GetItemList()
        {
            const int x = 100, y = 80;
            var itemPosition = new Vector2(x, y);

            var itemList = new LinkedList<IItem>();
            itemList.AddLast(new Key(itemPosition));

            return itemList;
        }
    }
}
