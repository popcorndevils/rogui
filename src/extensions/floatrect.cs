using SFML.Graphics;
using SFML.Window;

namespace Rogui.Extensions
{
    public static class FloatRectExtensions
    {
        public static bool Contains(this FloatRect rect, MouseMoveEventArgs e)
        {
            return rect.Contains(e.X, e.Y);
        }
    }
}