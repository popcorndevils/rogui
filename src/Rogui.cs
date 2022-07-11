using SFML.Graphics;
using SFML.Window;

namespace Rogui
{
    public class UIController : Aspect
    {
        private RenderWindow? _Window;
        public RenderWindow? Window {
            get => this._Window;
            set {
                this._Window = value;
                if(value is not null)
                {
                    value.KeyPressed += this.OnKeyPressed;
                    value.MouseMoved += this.OnMouseMoved;
                    value.MouseButtonPressed += this.OnMouseButtonPressed;
                    value.MouseButtonReleased += this.OnMouseButtonReleased;
                }
            }
        }

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