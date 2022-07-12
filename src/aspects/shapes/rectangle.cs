using SFML.Graphics;
using SFML.System;

namespace Rogui.Shapes
{
    public class Rectangle : Aspect
    {
        private RectangleShape Shape = new RectangleShape();

        public override FloatRect Bounds {
            get {
                var _bnds = this.Shape.GetGlobalBounds();
                return new FloatRect(
                    _bnds.Left - this.MarginLeft,
                    _bnds.Top - this.MarginTop,
                    _bnds.Width + this.MarginLeft + this.MarginRight,
                    _bnds.Height + this.MarginTop + this.MarginBottom);
            }
        }

        public override Vector2f Size {
            get => this.Shape.Size;
            set => this.Shape.Size = value;
        }

        public override Color FillColor {
            get => this.Shape.FillColor;
            set => this.Shape.FillColor = value;
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            if(this.Visible)
            {
                this.Shape.Position = this.AbsolutePosition + this.Position + this.MarginPosition + this.OffsetPosition;
                this.Shape.Draw(t, s);
            }
        }
    }
}