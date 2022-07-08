using SFML.System;
using SFML.Graphics;

namespace Rogui
{
    public class Label : Aspect
    {
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
                    _pos.X,
                    _pos.Y,
                    _bnds.Width + (_bnds.Left * 2),
                    _bnds.Height + (_bnds.Top * 2));
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
                this.GText.Position = value;
            }
        }

        public string DisplayedString {
            get => this.GText.DisplayedString;
            set => this.GText.DisplayedString = value;
        }

        public Label(string text)
        {
            this.Font = new Font("./res/fonts/consola.ttf");
            this.GText = new Text(text, this.Font, 24);
        }
    }
}