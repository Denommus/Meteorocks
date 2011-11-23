using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Icone2DLibrary;
using Microsoft.Xna.Framework.Graphics;

namespace Icone2DLibrary.Objects
{
    class Bullet : Sprite
    {
        public override void Initialize()
        {
            texture = game.Content.Load<Texture2D>(@"Sprites/Fireball");
            scale = 0.2f;
        }

        Vector2 speed = Vector2.Zero;
        float timeUntilVanish = 2;

        public void SetInitialParams(float rotation, Vector2 position)
        {
            this.position = position;
            this.rotation = rotation;
            speed = new Vector2((float)Math.Sin(rotation), -(float)Math.Cos(rotation)) * 300;
        }

        public override void Update(float seconds)
        {
            position += speed * seconds;
            timeUntilVanish -= seconds;
            if (timeUntilVanish <= 0)
                scene.RemoveSprite(this);
        }
    }
}
