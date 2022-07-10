using SFML.Window;
using Rogui.Extensions;

namespace Rogui
{
    public abstract partial class Primitive
    {
        public event EventHandler? OnClick;
        // handlers for input events received from parents
        public virtual EventArgs? ProcessKey(object? sender, EventArgs? e)
        {
            if(this.BlockInput)
            {
                return null;
            }
            return e;
        }

        public virtual MouseMoveEventArgs? ProcessMouseMove(object? sender, MouseMoveEventArgs? e)
        {
            if(e is not null && this.Bounds.Contains(e))
            {
                this.Hover = true;
            }
            else
            {
                this.Hover = false;
            }

            if(this.Hover && this.BlockInput)
            {
                return null;
            }

            return e;
        }

        public virtual MouseButtonEventArgs? ProcessMousePress(
            object? sender, MouseButtonEventArgs? e)
        {
            if(this.QueryPress(e))
                { this.Pressed = true; }
            else
                { this.Pressed = false; }

            if(this.Hover && this.BlockInput)
            {
                return null;
            }
            return e;
        }

        public virtual MouseButtonEventArgs? ProcessMouseRelease(
            object? sender, MouseButtonEventArgs? e)
        {
            if(this.QueryRelease(e))
            {
                this.Pressed = false;
                this.OnClick?.Invoke(this, EventArgs.Empty);
            }
            if(this.Hover && this.BlockInput)
            {
                return null;
            }
            return e;
        }

        private bool QueryPress(MouseButtonEventArgs? e)
        {
            return e is not null && this.Hover && e.Button == Mouse.Button.Left && !this.Pressed;
        }

        private bool QueryRelease(MouseButtonEventArgs? e)
        {
            return e is not null && this.Hover && e.Button == Mouse.Button.Left && this.Pressed;
        }
    }
}