﻿using Game1.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Projectile
{
    class ProjectileSpriteSheet
    {
        private Texture2D projectileSprites;
        private int rows, columns;

        public ProjectileSpriteSheet(Texture2D spriteSheet, int columns, int rows)
        {
            projectileSprites = spriteSheet;
            this.columns = columns;
            this.rows = rows;
        }

        public Rectangle PickSprite(int column, int row)
        {
            int width = projectileSprites.Width / columns;
            int height = projectileSprites.Height / rows;

            return new Rectangle(width * column, height * row, width, height);
        }

        public Texture2D GetTexture()
        {
            return projectileSprites;
        }
    }
}

