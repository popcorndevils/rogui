using SFML.Graphics;
using SFML.System;

namespace Rogui
{
    public class Panel : Aspect
    {
        private RectangleShape BodyBG = new RectangleShape();
        private RectangleShape BodyFG = new RectangleShape();

        public override FloatRect Bounds => this.BodyBG.GetGlobalBounds();

        public override float BorderWidth { 
            get => base.BorderWidth; 
            set {
                base.BorderWidth = value;
                this.SetBorder();
            }
        }
        
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
                this.BodyBG.Position = value;
                this.SetBorder();
            }
        }

        public override Vector2f Size {
            get => base.Size;
            set {
                base.Size = new Vector2f(
                    value.X + (this.BorderWidth * 2),
                    value.Y + (this.BorderWidth * 2));
                this.SetBorder();
            }
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            this.BodyBG.Draw(t, s);
            this.BodyFG.Draw(t, s);
        }

        private void SetBorder()
        {
            this.BodyBG.Size = new Vector2f(
                    this.Size.X + (this.BorderWidth * 2),
                    this.Size.Y + (this.BorderWidth * 2));
            this.BodyFG.Size = this.Size;
            this.BodyFG.Position = new Vector2f(
                this.Position.X + this.BorderWidth,
                this.Position.Y + this.BorderWidth);
        }
    }
}