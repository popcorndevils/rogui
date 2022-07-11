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

        public override Vector2f AbsolutePosition {
            get => this.Shape.Position;
            set {
                this.Shape.Position = new Vector2f(
                    value.X + this.MarginLeft + this.Position.X,
                    value.Y + this.MarginTop + this.Position.Y
                );
            }
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            if(this.Visible)
            {
                this.Shape.Draw(t, s);
            }
        }
    }
}