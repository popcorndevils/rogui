using SFML.System;
using SFML.Graphics;

namespace Rogui.Primitives
{
    public class Label : Aspect
    {
        public event EventHandler? StringChanged;
        public Font Font;
        public Text GText;
        public override Drawable Shape {
            get => this.GText;
        }

        public override FloatRect Bounds {
            get {
                var _bnds = this.GText.GetLocalBounds();
                var _pos = this.Position;
                return new FloatRect(
                    _pos.X - this.MarginLeft,
                    _pos.Y - this.MarginTop,
                    _bnds.Width + (_bnds.Left * 2) + this.MarginLeft + this.MarginRight,
                    _bnds.Height + (_bnds.Top * 2) + this.MarginTop + this.MarginBottom);
            }
        } 

        public override Vector2f Size {
            get {
                var _bnds = this.Bounds;
                return new Vector2f(_bnds.Width, _bnds.Height);
            }
        }

        public override Vector2f Position {
            get => base.Position;
            set {
                base.Position = value;
                this.GText.Position = new Vector2f(
                    value.X + this.MarginLeft,
                    value.Y + this.MarginTop
                );
                
            }
        }

        public string DisplayedString {
            get => this.GText.DisplayedString;
            set {
                this.GText.DisplayedString = value;
                this.StringChanged?.Invoke(this, EventArgs.Empty);
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