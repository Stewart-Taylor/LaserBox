using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace LaserBox
{

    public class Director 
    {
        public static Random random = new Random();

        public Cube cube = new Cube(new Vector2(400, 600));
        Source source;
        Mirror mirror;

       public ContentManager content;

        List<Mirror> mirrors = new List<Mirror>();

        int counter = 0;
        int limit = 100;

        Texture2D background;

        public Director()
        {
            source  = new Source(new Vector2(100,300) , 0 , this);
            mirror   = new Mirror(new Vector2(300, 200) , 0 , this);
            
            mirrors.Add(mirror);
        }


        public List<Mirror> getMirrors()
        {
            return mirrors;
        }


        public void load(ContentManager c)
        {
            content = c;

            background = content.Load<Texture2D>("Sprites//Background");

            source.load(content);
            cube.load(content);
            mirror.load(content);

        }




        public void update()
        {
            source.update();

            foreach (Mirror m in mirrors)
            {
                m.update();
            }

            input();
        }



        private void input()
        {
            MouseState mouseStateCurrent, mouseStatePrevious;
            KeyboardState keyboardStateCurrent, keyboardStatePrevious;
            mouseStateCurrent = Mouse.GetState();
            keyboardStateCurrent = Keyboard.GetState();

            if (mouseStateCurrent.LeftButton == ButtonState.Pressed)
            {
                mirrors.Last().setPosition(new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y));
            }

            if (mouseStateCurrent.RightButton == ButtonState.Pressed)
            {
                mirrors.Last().setAngle(0);
            }

            if (keyboardStateCurrent.IsKeyDown(Keys.Enter) )
            {
                cube.setPosition(new Vector2(mouseStateCurrent.X - 35, mouseStateCurrent.Y - 35));
            }


            if (counter > limit)
            {
                counter = 0;
                if (keyboardStateCurrent.IsKeyDown(Keys.M))
                {
                    Mirror m = new Mirror(new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y), 0, this);
                    m.load(content);
                    mirrors.Add(m);
                }
            }
            else
            {
                counter++;
            }
               



        }



        public void draw(SpriteBatch sb)
        {

            sb.Draw(background, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            source.draw(sb);
            cube.draw(sb);

            foreach (Mirror m in mirrors)
            {
                m.draw(sb);
            }

        }
    }
}

