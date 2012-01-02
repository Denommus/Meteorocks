using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Icone2DLibrary.Physics
{
    public struct Circle
    {
        public Circle(float radius, Vector2 position)
        {
            this.radius = radius;
            this.position = position;
        }

        public float radius;
        public Vector2 position;

        public static Circle Empty
        {
            get { return new Circle(0.0f, Vector2.Zero); }
        }

        public bool Contains(int x, int y)
        {
            return Contains(new Point(x, y));
        }

        public bool Contains(Point point)
        {
            Vector2 vDistance = new Vector2(point.X - position.X, point.Y - position.Y);
            return vDistance.LengthSquared() < (radius * radius) ? true : false;
        }

        public bool Contains(Circle circle)
        {
            Vector2 vDistance = new Vector2(circle.position.X - position.X, circle.position.Y - position.Y);
            return vDistance.LengthSquared() < ((radius + circle.radius) * (radius + circle.radius)) ? true : false;
        }
    }
}
