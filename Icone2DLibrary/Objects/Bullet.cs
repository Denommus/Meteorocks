using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Icone2DLibrary.SceneManagement;
using Icone2DLibrary.Objects.SpriteStruct;
using Icone2DLibrary.Physics;

namespace Icone2DLibrary.Objects
{
    class Bullet : ISceneObject
    {
        public void Initialize(Scene scene, Random random)
        {
            this.scene = scene;
            game = scene.Game;
            sprite.texture = game.Content.Load<Texture2D>(@"Sprites/Fireball");
            sprite.scale = 0.2f;
            sprite.depth = 0;
            sprite.origin = new Vector2(sprite.texture.Width / 2, sprite.texture.Height / 2);
            circle.radius = sprite.origin.X < sprite.origin.Y ? sprite.origin.X : sprite.origin.Y;
            circle.radius *= sprite.scale;
        }


        Scene scene;
        Game game;
        Vector2 speed;
        float distanceUntilVanish = 250;
        Sprite sprite = new Sprite();
        Circle circle;

        public void SetInitialParams(float rotation, Vector2 position, Vector2 shipSpeed)
        {
            sprite.position = position;
            sprite.rotation = rotation;
            speed = new Vector2((float)Math.Sin(rotation), -(float)Math.Cos(rotation)) * 500;
            speed += shipSpeed;
        }

        public void Update(float seconds)
        {
            sprite.position += speed * seconds;
            
            distanceUntilVanish -= speed.Length() * seconds;
            if (distanceUntilVanish <= 0)
                scene.RemoveSceneObject(this);
            circle.position = sprite.position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        public Circle Circle { get { return circle; } }
    }
}
