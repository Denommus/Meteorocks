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


namespace Icone2DLibrary.SceneManagement
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

        List<ISceneObject> sprites = new List<ISceneObject>();
        Ship player;
        SpriteBatch spriteBatch;
        float secondsToNextMeteor = 0;

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

            secondsToNextMeteor -= seconds;
            if (secondsToNextMeteor <= 0)
            {
                secondsToNextMeteor = 10;
                AddSceneObject<Meteor>();
            }

            player.KeyState = keyState;

            player.Update(seconds);

            for (int i = 0; i < sprites.Count; i++)
            {
                ISceneObject s1 = sprites[i];
                if (s1 != null)
                {
                    s1.Update(seconds);
                    Collide((ISceneObject)player, s1);
                    for (int j = i; j < sprites.Count; j++)
                    {
                        ISceneObject s2 = sprites[j];
                        if (s2 != null)
                            Collide(s1, s2);
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            player.Draw(spriteBatch);
            foreach (ISceneObject s in sprites)
            {
                s.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void AddSceneObject<T>() where T : ISceneObject, new()
        {
            ISceneObject t = new T();
            t.Initialize(this);
            sprites.Add(t);
        }

        public void AddBullet(float rotation, Vector2 shipPosition, Vector2 shipSpeed)
        {
            Bullet bullet = new Bullet();
            bullet.Initialize(this);
            bullet.SetInitialParams(rotation, shipPosition, shipSpeed);
            sprites.Add(bullet);
        }

        public Ship InitializeShip()
        {
            Ship ship = new Ship();
            ship.Initialize(this);
            return ship;
        }

        public void RemoveSceneObject(ISceneObject sceneObject)
        {
            sprites.Remove(sceneObject);
        }

        private void Collide(ISceneObject s1, ISceneObject s2)
        {
            //TODO: Destroyed meteor must generate tiny meteors
            if (s1.GetType() == typeof(Bullet) && s2.GetType() == typeof(Meteor))
            {
                if (s1.Circle.Contains(s2.Circle))
                {
                    RemoveSceneObject(s1);
                    RemoveSceneObject(s2);
                }
            }
            else if (s1.GetType() == typeof(Meteor) && s2.GetType() == typeof(Bullet))
            {
                if (s1.Circle.Contains(s2.Circle))
                {
                    RemoveSceneObject(s1);
                    RemoveSceneObject(s2);
                }
            }
        }
    }
}
