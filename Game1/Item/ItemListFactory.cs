using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1.Item
{
    static class ItemListFactory
    {
        public static LinkedList<IItem> GetItemList()
        {
            var itemPosition = new Vector2(100, 80);

            var itemList = new LinkedList<IItem>();
            itemList.AddLast(new Key(itemPosition));

            return itemList;
        }
    }
}
