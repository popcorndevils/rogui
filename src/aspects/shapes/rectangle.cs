using SFML.Graphics;
using SFML.System;

namespace Rogui.Shapes
{
    public class Rectangle : Aspect
    {
        private RectangleShape Shape = new RectangleShape();

        public override FloatRect Bounds => this.Shape.GetGlobalBounds();

        public override Vector2f Size {
            get => base.Size;
            set  {
                this.Shape.Size = value;
                base.Size = value;
            }
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