using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Rogui
{
    public abstract class Aspect : Drawable
    {
        public virtual event EventHandler OnTransform {
            add {}
            remove {}
        }

        public virtual Drawable Shape {
            get { return new RectangleShape(); }
            set { }
        }

        public virtual bool Visible { get; set; } = true;
        public virtual bool BlockInput { get; set; }
        public virtual FloatRect Bounds { get; }
        public virtual Vector2f Position { get; set; }
        public virtual Vector2f Size { get; }
        public virtual Aspect? Parent { get; set; }
        public virtual float MarginLeft { get; set; }
        public virtual float MarginRight { get; set; }
        public virtual float MarginTop { get; set; }
        public virtual float MarginBottom { get; set; }
        public virtual float Margin {
            set {
                this.MarginBottom = value;
                this.MarginTop = value;
                this.MarginLeft = value;
                this.MarginRight = value;
            }
        }

        public virtual void Update(float? ms) { }

        public virtual void Draw(RenderTarget t, RenderStates s)
        {
            if(this.Visible)
            {
                this.Shape.Draw(t, s);
            }
        }

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