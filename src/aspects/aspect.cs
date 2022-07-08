using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Rogui
{
    public abstract class Aspect : Drawable
    {
        // common aspect properties
        public virtual float BorderWidth { get; set; }
        public virtual Color BorderColor { get; set; }
        public virtual Color FillColor { get; set; }
        public virtual bool Visible { get; set; } = true;
        public virtual bool BlockInput { get; set; }
        public virtual FloatRect Bounds { get; }
        public virtual Vector2f Position { get; set; }
        public virtual Vector2f Size { get; set; }
        public virtual Aspect? Parent { get; set; }
        public virtual float MarginLeft { get; set; }
        public virtual float MarginRight { get; set; }
        public virtual float MarginTop { get; set; }
        public virtual float MarginBottom { get; set; }

        // a default shape for drawing
        public virtual Drawable Shape {
            get { return new RectangleShape(); }
            set { }
        }

        // helper to set all margins at once
        public virtual float Margin {
            set {
                this.MarginBottom = value;
                this.MarginTop = value;
                this.MarginLeft = value;
                this.MarginRight = value;
            }
        }

        public virtual void Update(float? ms) { }

        // default draw action, just draw the shape if aspect is visible
        public virtual void Draw(RenderTarget t, RenderStates s)
        {
            if(this.Visible)
            {
                this.Shape.Draw(t, s);
            }
        }

        // handlers for input events received from parents
        public virtual bool ProcessKey(object? sender, EventArgs e)
        {
            return this.BlockInput;
        }

        public virtual bool ProcessMouseMove(object? sender, MouseMoveEventArgs e)
        {
            return this.BlockInput;
        }

        public virtual bool ProcessMousePress(object? sender, MouseButtonEventArgs e)
        {
            return this.BlockInput;
        }

        public virtual bool ProcessMouseRelease(object? sender, MouseButtonEventArgs e)
        {
            return this.BlockInput;
        }
    }
}