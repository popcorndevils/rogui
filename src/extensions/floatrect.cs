using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Rogui.Extensions
{
    public static class FloatRectExtensions
    {
        public static bool Contains(this FloatRect rect, MouseMoveEventArgs e)
        {
            return rect.Contains(e.X, e.Y);
        }
        public static Vector2f GetSize(this FloatRect rect)
        {
            return new Vector2f(rect.Width, rect.Height);
        }
    }
}