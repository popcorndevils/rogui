using SFML.System;
using SFML.Graphics;

namespace Rogui.Primitives
{
    public class Label : Aspect
    {
        public Font Font;
        private Text GText;
        public override Drawable Shape {
            get => this.GText;
        }

        public override FloatRect Bounds {
            get => this.GText.GetGlobalBounds();
        } 

        public override Vector2f Size {
            get {
                var _bnds = this.GText.GetGlobalBounds();
                return new Vector2f(_bnds.Width, _bnds.Height);
            }
        }

        public override Vector2f Position {
            get => this.GText.Position;
            set => this.GText.Position = value;
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