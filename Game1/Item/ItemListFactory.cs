using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1.Item
{
    static class ItemListFactory
    {
        public static LinkedList<IItem> GetItemList()
        {
            var itemPosition = new Vector2(200, 200);

            var itemList = new LinkedList<IItem>();
            itemList.AddLast(new Bomb(itemPosition));
            itemList.AddLast(new Bow(itemPosition));
            itemList.AddLast(new Clock(itemPosition));
            itemList.AddLast(new Compass(itemPosition));
            itemList.AddLast(new Fairy(itemPosition));
            itemList.AddLast(new Heart(itemPosition));
            itemList.AddLast(new HeartContainer(itemPosition));
            itemList.AddLast(new ItemBoomerang(itemPosition));
            itemList.AddLast(new Key(itemPosition));
            itemList.AddLast(new Map(itemPosition));
            itemList.AddLast(new RupeeBlue(itemPosition));
            itemList.AddLast(new RupeeYellow(itemPosition));
            itemList.AddLast(new Triforce(itemPosition));

            return itemList;
        }
    }
}
