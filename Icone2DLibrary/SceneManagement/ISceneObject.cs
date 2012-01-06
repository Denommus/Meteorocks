using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Icone2DLibrary.Physics;

namespace Icone2DLibrary.SceneManagement
{
    public interface ISceneObject
    {
        void Initialize(Scene scene, Random random);
        void Update(float seconds);
        void Draw(SpriteBatch spriteBatch);
        Circle Circle { get; }
    }
}
