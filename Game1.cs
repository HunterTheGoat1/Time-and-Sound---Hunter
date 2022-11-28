using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Time_and_Sound___Hunter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D bombTexture;
        Texture2D boomTexture;
        SpriteFont titleFont;
        Rectangle bombRect;
        float seconds, startTime;
        MouseState mouseState;
        SoundEffect boom;
        SoundEffectInstance boomInstance;
        bool boomDraw = false;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            bombRect = new Rectangle(50, 50, 700, 400);
            this.Window.Title = "Time & Sound - Hunter  (Left click to reset the bomb)";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bombTexture = Content.Load<Texture2D>("bomb");
            titleFont = Content.Load<SpriteFont>("TextFont");
            boom = Content.Load<SoundEffect>("explosion");
            boomInstance = boom.CreateInstance();
            boomInstance.IsLooped = false;

            boomTexture = Content.Load<Texture2D>("boom");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }

            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (seconds >= 15)
            {
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }



            if (seconds > 15)
            {
                boomInstance.Play();
                boomDraw = true;
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }

            if (boomInstance.State == SoundState.Stopped && boomDraw)
            {
                Exit();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();


            if (boomDraw)
            {
                _spriteBatch.Draw(boomTexture, bombRect, Color.White);
            }
            else
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                _spriteBatch.DrawString(titleFont, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}