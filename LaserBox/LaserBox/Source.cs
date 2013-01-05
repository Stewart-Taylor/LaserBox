using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace LaserBox
{

    public class Source
    {

        Texture2D texture;
      
        Vector2 position;
        Vector2 origin = new Vector2(35,35);
        float angle;
       
        Laser laser;


        public void setPosition(Vector2 p)
        {
            position = p;
        }


        public void setAngle(float a)
        {
            angle += 0.03f;
        }




        public Source(Vector2 p , float a ,Director d)
        {
            position = p;
            angle = a;

          //  laser.setPosition(new Vector2(position.X + 32, position.Y), angle);

            laser = new Laser(new Vector2(position.X +30, position.Y), angle , d );
        }



        public void load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites//Source");
            laser.load(content);
        }




        public void update()
        {
            laser.on();
           
        }




        public void draw(SpriteBatch sb)
        {
            laser.draw(sb);
            sb.Draw(texture, position, null, Color.White, angle, origin, 1, SpriteEffects.None, 1);
        }


    }
}

