using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace LaserBox
{

    public class Mirror
    {
        Texture2D texture;

        Vector2 position;
        Vector2 origin = new Vector2(35, 35);
        float angle;
        Rectangle hitBox = new Rectangle();

        Laser laser;

        float laserAngleIn;
        Vector2 laserInPosition;

        bool activated = false;


        int counter = 0;
        int limit = 5;


        RotatedRectangle rect;

        public void setPosition(Vector2 p)
        {
            position = p;

            hitBox.X = (int)p.X - 50;
            hitBox.Y = (int)p.Y - 40;
        }


        public void setAngle(float a)
        {
            angle += 0.03f;
        }



        

        public Rectangle getHitBox()
        {
            return hitBox;
        }

        public bool CollidesWithMirror(Rectangle t)
        {
            if (rect.Intersects(t))
            {
                return true;
            }
            return false;
        }

        public Vector2 getPosition()
        {
            return position;
        }


        public Mirror(Vector2 p, float a, Director d)
        {
            position = p;
            angle = a;

            laser = new Laser(p, angle, d , this);

            hitBox.Width = 70;
            hitBox.Height = 70;



            rect = new RotatedRectangle(new Rectangle((int)position.X -45, (int)position.Y -30, 14, 50), 0f);

            rect.Origin = new Vector2(8, 26);
        }



        public void load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites//Mirror");

            laser.load(content);


           // angle = MathHelper.ToRadians(-45);
        }




        public void update()
        {


            rect.ChangePosition((int)position.X -30, (int)position.Y -30);
            rect.Rotation = angle;


            if (activated == true)
            {

                float laserAngle;


                float a = angle - laserAngleIn;

                laserAngle = angle + a;

                laserAngle += MathHelper.ToRadians(270);

                laser.setPosition(laserInPosition, laserAngle );
                laser.on();

               
            }
            else
            {
                laser.off();
            }

            if (counter > limit)
            {
                activated = false;
            }
            else
            {
                counter++;
            }

        }


        public void on()
        {
            activated = true;
            counter = 0;
        }


        public void off()
        {
            activated = false;
        
        }

        public void on(float angleT , Vector2 laserPos)
        {
            laserInPosition = laserPos;
            laserAngleIn = angleT;
            activated = true;
            counter = 0;

            angleT += MathHelper.ToRadians(-90);




      



            if (MathHelper.Distance(MathHelper.WrapAngle(angle), MathHelper.WrapAngle(angleT)) > MathHelper.ToRadians(90)) 
            {
                float a = MathHelper.ToDegrees(MathHelper.WrapAngle(angle));
                float n = MathHelper.ToDegrees(MathHelper.WrapAngle(angleT));

            }

        }


        public void draw(SpriteBatch sb)
        {




            sb.Draw(texture, rect.UpperLeftCorner(), null, Color.White, angle, Vector2.Zero, 1, SpriteEffects.None, 1);


            laser.draw(sb);

          //  drawHitBox(sb);
        }


        private void drawHitBox(SpriteBatch sb)
        {
            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y), null, Color.Red, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y), null, Color.Red, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X, hitBox.Y + hitBox.Height), null, Color.Red, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, new Vector2(hitBox.X + hitBox.Width, hitBox.Y + hitBox.Height), null, Color.Red, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);


            sb.Draw(texture, rect.UpperLeftCorner(), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, rect.UpperRightCorner(), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, rect.LowerLeftCorner(), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
            sb.Draw(texture, rect.LowerRightCorner(), null, Color.Gold, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);

            sb.Draw(texture, rect.UpperLeftCorner() + rect.Origin, null, Color.Red, 0, new Vector2(0, 0), 0.1f, SpriteEffects.None, 1);
        }
    }
}

