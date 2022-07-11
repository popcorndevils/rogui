using SFML.Graphics;
using SFML.Window;

namespace Rogui
{
    public class UIController : Aspect
    {
        // private RenderWindow? _Window;

        public void OnKeyPressed(object? sender, EventArgs e)
        {
            this.ProcessKey(sender, e);
        }

        public void OnMouseMoved(object? sender, MouseMoveEventArgs e)
        {
            this.ProcessMouseMove(sender, e);
        }

        public void OnMouseButtonPressed(object? sender, MouseButtonEventArgs e)
        {
            this.ProcessMousePress(sender, e);
        }

        public void OnMouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            this.ProcessMouseRelease(sender, e);
        }
    }
}