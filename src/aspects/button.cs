using SFML.Graphics;
using SFML.System;
using Rogui.Themes;

namespace Rogui
{
    public class Button : Aspect
    {

        public virtual Panel Body { get; set; }
        public Label BtnText = new Label();

        public override FloatRect Bounds => this.Body.Bounds;

        public ThemePanel? DisplayTheme {
            get => this.Body.Theme;
            set => this.Body.Theme = value;
        }

        // public override Vector2f AbsolutePosition {
        //     get => this.Body.AbsolutePosition;
        //     set {
        //         this.Body.AbsolutePosition = new Vector2f(
        //             value.X + this.MarginLeft,
        //             value.Y + this.MarginTop) + this.Position;

        //         // this.BtnText.AbsolutePosition = new Vector2f(
        //         //     value.X + this.MarginLeft + this.BorderLeft,
        //         //     value.Y + this.MarginTop + this.BorderTop) + this.Position;
        //     }
        // }

        public override float Border { set => this.Body.Border = value; }
        public override float BorderLeft {
            get => this.Body.BorderLeft;
            set => this.Body.BorderLeft = value;
        }
        public override float BorderTop {
            get => this.Body.BorderTop;
            set => this.Body.BorderTop = value;
        }
        public override float BorderRight {
            get => this.Body.BorderRight;
            set => this.Body.BorderRight = value;
        }
        public override float BorderBottom {
            get => this.Body.BorderBottom;
            set => this.Body.BorderBottom = value;
        }

        public override Color BorderColor {
            get => this.Body.BorderColor;
            set => this.Body.BorderColor = value;
        }

        public override Color FillColor {
            get => this.Body.FillColor;
            set => this.Body.FillColor = value;
        }

        public string DisplayedString {
            get => this.BtnText.DisplayedString;
            set => this.BtnText.DisplayedString = value;
        }

        private ThemeButton? _Theme;
        public new ThemeButton? Theme {
            get => this._Theme;
            set {
                this._Theme = value;
                if(value is not null && value.Normal is not null)
                {
                    this.DisplayTheme = value.Normal;
                }
            }
        }

        public Button(string text) : base()
        {
            this.BlockInput = true;
            this.Body = new Panel();
            base.StateChanged += this.OnStateChange;
            this.BtnText.DisplayedString = text;
            this.Body.Add(this.BtnText);
            base.Add(this.Body);
        }

        private void OnTransform(object? sender, EventArgs e)
        {
            var _bnds = this.BtnText.Bounds;
            this.Body.Size =  new Vector2f(_bnds.Width, _bnds.Height);
        }

        private void OnStateChange(object? sender, EventArgs e)
        {
            if(this.Theme is not null)
            {
                if(this.Pressed && this.Theme.Pressed is not null)
                {
                    this.DisplayTheme = this.Theme.Pressed;
                }
                else if(this.Hover && this.Theme.Hover is not null)
                {
                    this.DisplayTheme = this.Theme.Hover;
                }
                else if(this.Theme.Normal is not null)
                {
                    this.DisplayTheme = this.Theme.Normal;
                }
            }
        }

        public override string ToString()
        {
            return $"{this.GetType()}: {this.BtnText.DisplayedString}";
        }
    }
}