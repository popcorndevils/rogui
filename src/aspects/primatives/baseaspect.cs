using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Rogui.Extensions;

namespace Rogui.Primitives
{
    public abstract class BaseAspect : Drawable
    {
        // common aspect properties
        public virtual float BorderWidth { get; set; }
        public virtual Color BorderColor { get; set; }
        public virtual Color FillColor { get; set; }
        public virtual bool Visible { get; set; } = true;
        public virtual bool Hover { get; set; }
        public virtual bool BlockInput { get; set; }
        public virtual FloatRect Bounds { get; }
        public virtual Vector2f Position { get; set; }
        public virtual Vector2f RelativePosition { get; set; }
        public virtual Vector2f Size { get; set; }
        public virtual Aspect? Parent { get; set; }
        public virtual float MarginLeft { get; set; }
        public virtual float MarginRight { get; set; }
        public virtual float MarginTop { get; set; }
        public virtual float MarginBottom { get; set; }
        public virtual float PaddingLeft { get; set; }
        public virtual float PaddingRight { get; set; }
        public virtual float PaddingTop { get; set; }
        public virtual float PaddingBottom { get; set; }

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

        // helper to set all margins at once
        public virtual float Padding {
            set {
                this.PaddingBottom = value;
                this.PaddingTop = value;
                this.PaddingLeft = value;
                this.PaddingRight = value;
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
        public virtual EventArgs? ProcessKey(object? sender, EventArgs? e)
        {
            if(this.BlockInput)
            {
                return EventArgs.Empty;
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
            if(this.BlockInput)
            {
                return e;
            }
            return e;
        }

        public virtual MouseButtonEventArgs? ProcessMouseRelease(object? sender, MouseButtonEventArgs? e)
        {
            if(this.BlockInput)
            {
                return e;
            }
            return e;
        }
    }
}