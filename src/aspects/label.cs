using SFML.System;
using SFML.Graphics;

namespace Rogui
{
    public class Label : Aspect
    {
        public new event EventHandler? Transformed;
        public Font Font;
        public Text GText;
        public Drawable Shape {
            get => this.GText;
        }

        public override FloatRect Bounds {
            get {
                var _bnds = this.GText.GetLocalBounds();
                var _pos = this.AbsolutePosition;
                return new FloatRect(
                    _pos.X - this.MarginLeft,
                    _pos.Y - this.MarginTop,
                    _bnds.Width + (_bnds.Left * 2) + this.MarginLeft + this.MarginRight,
                    _bnds.Height + (_bnds.Top * 2) + this.MarginTop + this.MarginBottom);
            }
        } 

        public override Vector2f AbsolutePosition {
            get => base.AbsolutePosition;
            set {
                this.GText.Position = new Vector2f(
                    value.X + this.MarginLeft + this.Position.X,
                    value.Y + this.MarginTop + this.Position.Y
                );
                base.AbsolutePosition = value;
            }
        }

        public string DisplayedString {
            get => this.GText.DisplayedString;
            set {
                this.GText.DisplayedString = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            this.GText.Draw(t, s);
        }

        public Label()
        {
            this.Font = new Font("./res/fonts/consola.ttf");
            this.GText = new Text("", this.Font, 24);
        }

        public Label(string text)
        {
            this.Font = new Font("./res/fonts/consola.ttf");
            this.GText = new Text(text, this.Font, 24);
        }
    }
}