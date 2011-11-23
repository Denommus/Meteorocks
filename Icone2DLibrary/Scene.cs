using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Icone2DLibrary.Objects;


namespace Icone2DLibrary
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Scene : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Scene(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            player = InitializeShip();
        }

        List<Sprite> sprites = new List<Sprite>();
        List<Sprite> markedToRemove = new List<Sprite>();
        Ship player;
        SpriteBatch spriteBatch;

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyState = Keyboard.GetState();
            player.KeyState = keyState;

            player.Update(seconds);

            foreach(Sprite s in sprites)
            {
                s.Update(seconds);
            }

            foreach (Sprite s in markedToRemove)
            {
                sprites.Remove(s);
            }

            markedToRemove.RemoveRange(0, markedToRemove.Count);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            player.Draw(spriteBatch);
            foreach (Sprite s in sprites)
            {
                s.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void AddSprite<T>() where T : Sprite, new()
        {
            Sprite t = new T();
            t.BeforeInitialize(this);
            t.Initialize();
            t.AfterInitialize();
            sprites.Add(t);
        }

        public void AddBullet(float rotation, Vector2 shipPosition)
        {
            Bullet bullet = new Bullet();
            bullet.BeforeInitialize(this);
            bullet.Initialize();
            bullet.AfterInitialize();
            bullet.SetInitialParams(rotation, shipPosition);
            sprites.Add(bullet);
        }

        public void RemoveSprite(Sprite remove)
        {
            markedToRemove.Add(remove);
        }

        public Ship InitializeShip()
        {
            Ship ship = new Ship();
            ship.BeforeInitialize(this);
            ship.Initialize();
            ship.AfterInitialize();
            return ship;
        }
    }
}
