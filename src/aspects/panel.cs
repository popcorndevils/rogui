using SFML.Graphics;
using SFML.System;
using Rogui.Primitives;

namespace Rogui
{
    public class Panel : Aspect
    {
        private Rectangle BodyBG = new Rectangle();
        private Rectangle BodyFG = new Rectangle();

        public override FloatRect Bounds => this.BodyBG.Bounds;
        
        public override Color FillColor {
            get => this.BodyFG.FillColor;
            set => this.BodyFG.FillColor = value;
        }

        public override Color BorderColor {
            get => this.BodyBG.FillColor;
            set => this.BodyBG.FillColor = value;
        }

        public override Vector2f Position {
            get => this.BodyBG.Position;
            set {
                this.BodyBG.Position = new Vector2f(
                    value.X + this.MarginLeft,
                    value.Y + this.MarginTop
                );
                this.BodyFG.Position = new Vector2f(
                    value.X + this.MarginLeft + this.BorderLeft,
                    value.Y + this.MarginTop + this.BorderTop
                );
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