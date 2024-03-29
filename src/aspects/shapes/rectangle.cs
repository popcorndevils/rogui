using SFML.Graphics;
using SFML.System;

namespace Rogui.Shapes
{
    public class Rectangle : Aspect
    {
        private RectangleShape Shape = new RectangleShape();

        public override FloatRect Bounds => this.Shape.GetGlobalBounds();
        public override Vector2f TrueCenter => this.WindowPosition + (this.Shape.Size / 2);

        public override Vector2f Size {
            get => base.Size;
            set  {
                this.Shape.Size = value;
                base.Size = value;
            }
        }

        public override Color? FillColor {
            get => this.Shape.FillColor;
            set {
                if(value is not null)
                {
                    this.Shape.FillColor = (Color)value;
                }
            }
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            if(this.Visible)
            {
                this.Shape.Position = this.WindowPosition;
                this.Shape.Draw(t, s);
            }
        }
    }
}