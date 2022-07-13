using SFML.Graphics;
using SFML.System;
using Rogui.Themes;

namespace Rogui
{
    public class Panel : Aspect
    {
        protected Shapes.Rectangle BodyBG = new Shapes.Rectangle();
        protected Shapes.Rectangle BodyFG = new Shapes.Rectangle();

        public override FloatRect Bounds => this.BodyBG.Bounds;
        
        public override Color FillColor {
            get => this.BodyFG.FillColor;
            set => this.BodyFG.FillColor = value;
        }

        public override Color BorderColor {
            get => this.BodyBG.FillColor;
            set => this.BodyBG.FillColor = value;
        }

        public override Vector2f AbsolutePosition {
            get => this.BodyBG.AbsolutePosition;
            set {
                this.BodyBG.AbsolutePosition = value + this.MarginPosition;
                this.BodyFG.AbsolutePosition = value + this.MarginPosition + this.BorderPosition;
            }
        }

        public override Vector2f Position {
            get => this.BodyBG.Position;
            set {
                this.BodyBG.Position = value;
                this.BodyFG.Position = value;
            }
        }

        public override Vector2f OffsetPosition {
            get => this.BodyBG.OffsetPosition;
            set {
                this.BodyBG.OffsetPosition = value;
                this.BodyFG.OffsetPosition = value;
            }
        }

        public override Vector2f Size {
            get => this.BodyFG.Size;
            set {
                this.BodyFG.Size = value;
                this.BodyBG.Size = new Vector2f(
                    value.X + this.BorderLeft + this.BorderRight,
                    value.Y + this.BorderTop + this.BorderBottom);
            }
        }

        public Panel()
        {
            this.Add(this.BodyBG, this.BodyFG);
        }
    }
}