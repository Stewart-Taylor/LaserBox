using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace LaserBox
{

    public class Laser
    {
        Texture2D textureGlow;
        Texture2D texture;
        Texture2D textureOverlay;

        Texture2D test;

        Vector2 position;
        float angle;

        Vector2 previousPosition;
        float previousAngle;

        Vector2 targetPosition;

        Vector2 overlayOrigin;

        Color color = Color.White;
        Director director;

        float maxDistance = 1300;


        int counter;
        int limit = 50;

        Mirror sourceMirror;

        bool isOn = false;







        public void setPosition(Vector2 p , float a)
        {
            position = p;
            angle = a + MathHelper.ToRadians(90);
        }

  

        public Laser(Vector2 p , float a , Director d)
        {
            position = p;
            director = d;

            angle = a + MathHelper.ToRadians(90);
        }

        public Laser(Vector2 p, float a, Director d , Mirror m)
        {
            position = p;
            director = d;

            sourceMirror = m;

            angle = a + MathHelper.ToRadians(90);
        }



   


        public void load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites//Laser");
            textureOverlay = content.Load<Texture2D>("Sprites//Laser2");
            test = content.Load<Texture2D>("Sprites//Pixel");
            textureGlow = content.Load<Texture2D>("Sprites//Glow");
        }




        public void on()
        {
            isOn = true;
            update();
            previousPosition = position;
            previousAngle = angle;
        }


        public void off()
        {
            isOn = false;
        }



        public void update()
        {
            updateCheck();
        }





        private void updateCheck()
        {
            Rectangle r = new Rectangle();
            Vector2 finder = position;
            Vector2 velocity;


            r.Width =1;
            r.Height=1;


            do
            {



                velocity.X = (float)Math.Cos(angle + MathHelper.ToRadians(-90) ) * 1f;
                velocity.Y = (float)Math.Sin(angle + MathHelper.ToRadians(-90) ) * 1f;


                finder += velocity;


                r.X = (int)finder.X;
                r.Y = (int)finder.Y;


                if (collided(r))
                {
                    break;
                } 

            } while (Vector2.Distance(finder, position) < maxDistance);



            targetPosition = finder;

          //  targetPosition = new Vector2(200, 200);
        }


     




        private bool collided(Rectangle f)
        {
            if (f.Intersects(director.cube.getHitBox()))
            {

                foreach (Mirror m in director.getMirrors())
                {
                    m.off();
                }


                return true;
            }

            if (mirrorCollide(f))
            {
                return true;
            }
           
            return false;
        }




        private bool mirrorCollide(Rectangle f)
        {
            foreach (Mirror m in director.getMirrors())
            {
                if (f.Intersects(m.getHitBox()))
                {

                    if (sourceMirror != null)
                    {
                        if (!(sourceMirror.Equals(m)))
                        {

                            if (m.CollidesWithMirror(f))
                            {


                                m.on(angle, targetPosition);

                                return true;

                            }
                        }
                    }
                    else
                    {
                       
                        // TARGET = target

                             if (m.CollidesWithMirror(f))
                            {

                        m.on(angle, targetPosition);
                        // targetPosition = m.getPosition();

                        return true;
                            }

                       

                    }
                }
            }

            return false;
        }



     


    



        private Vector2 getDistance(Vector2 position , Vector2 targetPosition)
        {
            Vector2 distance = targetPosition - position;
            return distance;
        }


        private Vector2 getScaleStretch()
        {
            Vector2 stretch_factor = new Vector2();
            stretch_factor.X = getDistance(position , targetPosition).Length() / texture.Height;
            return stretch_factor;
        }





        private float getRotation()
        {
            float rot;
            //rotate towards point
            float XDistance = targetPosition.X - position.X;
            float YDistance = targetPosition.Y - position.Y;

            //Calculate the required rotation by doing a two-variable arc-tan
            rot = (float)Math.Atan2(YDistance, XDistance);
            //offset
            rot -= MathHelper.PiOver2;

            return rot;
        }





        public void draw(SpriteBatch sb)
        {
            if (isOn == true)
            {
               sb.Draw(texture, (position + getDistance(position, targetPosition) / 2), null, color, getRotation(), new Vector2(texture.Width, texture.Height) / 2, new Vector2(0.3f, getScaleStretch().X), SpriteEffects.None, 1);

               sb.Draw(textureGlow, targetPosition, null, Color.White, 0, new Vector2(35, 35), 1, SpriteEffects.None, 1);

                sb.End();
                sb.Begin(SpriteSortMode.Deferred, BlendState.Additive);

                sb.Draw(textureGlow, targetPosition, null, Color.White, 0, new Vector2(35, 35), 1, SpriteEffects.None, 1);
                sb.Draw(textureGlow, position, null, Color.White, 0, new Vector2(35, 35), 1, SpriteEffects.None, 1);

                sb.Draw(textureOverlay, (position + getDistance(position, targetPosition) / 2), null, Color.White, getRotation(), new Vector2(texture.Width, texture.Height) / 2, new Vector2(0.6f, getScaleStretch().X), SpriteEffects.None, 1);

                sb.End();
                sb.Begin();
            }
        }


    }
}

