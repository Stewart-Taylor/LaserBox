using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace LaserBox
{

    public class Cube
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle hitBox = new Rectangle();
        private int width = 68;
        private int height = 65;

        public void setPosition(Vector2 position)
        {
            this.position = position;

            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;
        }

        public Rectangle getHitBox()
        {
            return hitBox;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public Cube(Vector2 position)
        {
            this.position = position;

            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;
            hitBox.Width = width;
            hitBox.Height = height;
        }



        public void load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites//Cube");

            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;
        }


        public void draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

           // drawHitBox(sb);
        }


        private void drawHitBox(SpriteBatch sb)
        {
            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y + hitBox.Height), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y + hitBox.Height), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
        }

    }
}

