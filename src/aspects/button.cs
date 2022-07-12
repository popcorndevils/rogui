using SFML.Graphics;
using SFML.System;
using Rogui.Themes;

namespace Rogui
{
    public class Button : Aspect
    {

        public Panel BtnBody = new Panel();
        public Label BtnText = new Label();

        public override FloatRect Bounds => this.BtnBody.Bounds;

        public override Vector2f AbsolutePosition {
            get => this.BtnBody.AbsolutePosition;
            set {
                this.BtnBody.AbsolutePosition = new Vector2f(
                    value.X + this.MarginLeft,
                    value.Y + this.MarginTop) + this.Position;

                this.BtnText.AbsolutePosition = new Vector2f(
                    value.X + this.MarginLeft + this.BorderLeft,
                    value.Y + this.MarginTop + this.BorderTop) + this.Position;
            }
        }

        public override float Padding { set => this.BtnText.Margin = value; }
        public override float PaddingLeft {
            get => this.BtnText.MarginLeft;
            set => this.BtnText.MarginLeft = value;
        }
        public override float PaddingTop {
            get => this.BtnText.MarginTop;
            set => this.BtnText.MarginTop = value;
        }
        public override float PaddingRight {
            get => this.BtnText.MarginRight;
            set => this.BtnText.MarginRight = value;
        }
        public override float PaddingBottom {
            get => this.BtnText.MarginBottom;
            set => this.BtnText.MarginBottom = value;
        }

        public override float Border { set => this.BtnBody.Border = value; }
        public override float BorderLeft {
            get => this.BtnBody.BorderLeft;
            set => this.BtnBody.BorderLeft = value;
        }
        public override float BorderTop {
            get => this.BtnBody.BorderTop;
            set => this.BtnBody.BorderTop = value;
        }
        public override float BorderRight {
            get => this.BtnBody.BorderRight;
            set => this.BtnBody.BorderRight = value;
        }
        public override float BorderBottom {
            get => this.BtnBody.BorderBottom;
            set => this.BtnBody.BorderBottom = value;
        }

        public override Color BorderColor {
            get => this.BtnBody.BorderColor;
            set => this.BtnBody.BorderColor = value;
        }

        public override Color FillColor {
            get => this.BtnBody.FillColor;
            set => this.BtnBody.FillColor = value;
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

        public ThemePanel DisplayTheme {
            set {
                if(value.FillColor is not null) 
                    { this.FillColor = (Color)value.FillColor; }
                if(value.BorderColor is not null) 
                    { this.BorderColor = (Color)value.BorderColor; }
                if(value.MarginLeft is not null) 
                    { this.MarginLeft = (float)value.MarginLeft; }
                if(value.MarginTop is not null) 
                    { this.MarginTop = (float)value.MarginTop; }
                if(value.MarginRight is not null) 
                    { this.MarginRight = (float)value.MarginRight; }
                if(value.MarginBottom is not null) 
                    { this.MarginBottom = (float)value.MarginBottom; }
                if(value.PaddingLeft is not null) 
                    { this.PaddingLeft = (float)value.PaddingLeft; }
                if(value.PaddingTop is not null) 
                    { this.PaddingTop = (float)value.PaddingTop; }
                if(value.PaddingRight is not null) 
                    { this.PaddingRight = (float)value.PaddingRight; }
                if(value.PaddingBottom is not null) 
                    { this.PaddingBottom = (float)value.PaddingBottom; }
                if(value.BorderLeft is not null) 
                    { this.BorderLeft = (float)value.BorderLeft; }
                if(value.BorderTop is not null) 
                    { this.BorderTop = (float)value.BorderTop; }
                if(value.BorderRight is not null) 
                    { this.BorderRight = (float)value.BorderRight; }
                if(value.BorderBottom is not null) 
                    { this.BorderBottom = (float)value.BorderBottom; }
            }
        }

        public Button(string text) : base()
        {
            // this.BtnText.TextChanged += this.OnTransform;
            this.BtnText.Transformed += this.OnTransform;
            this.BtnBody.Transformed += this.OnTransform;
            this.Transformed += this.OnTransform;
            base.StateChanged += this.OnStateChange;
            this.BtnText.DisplayedString = text;
            base.Add(this.BtnBody, this.BtnText);
            this.BlockInput = true;
        }

        private void OnTransform(object? sender, EventArgs e)
        {
            var _bnds = this.BtnText.Bounds;
            this.BtnBody.Size =  new Vector2f(_bnds.Width, _bnds.Height);
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