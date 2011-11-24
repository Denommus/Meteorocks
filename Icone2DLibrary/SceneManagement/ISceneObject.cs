using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Icone2DLibrary.SceneManagement
{
    public interface ISceneObject
    {
        void Initialize(Scene scene);
        void Update(float seconds);
        void Draw(SpriteBatch spriteBatch);
    }
}
