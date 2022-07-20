using SFML.System;
using SFML.Graphics;

namespace Rogui
{
    public class AnimLabel : BaseLabel
    {
        public new event EventHandler? Transformed;
        public Text BoundsText;
        public float Timer;

        public override FloatRect Bounds {
            get {
                var _bnds = this.BoundsText.GetLocalBounds();
                var _pos = this.AbsolutePosition;
                return new FloatRect(
                    _pos.X - this.MarginLeft,
                    _pos.Y - this.MarginTop,
                    _bnds.Width + (_bnds.Left * 2) + this.MarginLeft + this.MarginRight,
                    _bnds.Height + (_bnds.Top * 2) + this.MarginTop + this.MarginBottom);
            }
        } 

        public string? CurrentString {
            get => base.DisplayedString;
            set => base.DisplayedString = value;
        }

        public override string? DisplayedString {
            get => this.BoundsText.DisplayedString;
            set {
                this.BoundsText.DisplayedString = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public override void Update(float? ms)
        {
            if(ms is not null)
            {
                this.Timer += (float)ms;
            }
            if(this.Timer >= 1000)
            {
                this.Timer = 0;
                if(this.DisplayedString is not null && this.CurrentString is not null && 
                   this.CurrentString.Length < this.DisplayedString.Length)
                {
                    this.CurrentString = this.DisplayedString.Substring(0, this.CurrentString.Length + 1);
                    
                }
                else
                {
                    this.CurrentString = "";
                }
            }

        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            if(this.Visible)
            {
                var _pos = this.AbsolutePosition + this.Position + this.MarginPosition + this.OffsetPosition;
                this.TextShape.Position = _pos;
                this.BoundsText.Position = _pos;
                this.TextShape.Draw(t, s);
            }
        }

        public AnimLabel()
        {
            this.Font = new Font("./res/fonts/consola.ttf");
            this.TextShape = new Text("", this.Font, 24);
            this.BoundsText = new Text("", this.Font, 24);
        }

        public AnimLabel(string text)
        {
            this.Font = new Font("./res/fonts/consola.ttf");
            this.TextShape = new Text("", this.Font, 24);
            this.BoundsText = new Text(text, this.Font, 24);
        }
    }
}