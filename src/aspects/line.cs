using SFML.Graphics;
using SFML.System;

namespace Rogui
{
    public class Line : Aspect
    {
        private RectangleShape LineShape;
        public Drawable Shape => this.LineShape;

        public Line(int x, int y, int w, int h)
        {
            this.LineShape = new RectangleShape(new Vector2f(w, h));
        }
    }
}