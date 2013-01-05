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

        Texture2D texture;
        Vector2 position;
        Rectangle hitBox = new Rectangle();


        public void setPosition(Vector2 p)
        {
            position = p;

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



        public Cube(Vector2 p)
        {
            position = p;

            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;

            hitBox.Width = 68;
            hitBox.Height = 65;
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

