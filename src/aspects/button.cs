using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Rogui.Extensions;
using Rogui.Themes;
using Rogui.Primitives;

namespace Rogui
{
    public class Button : Aspect
    {
        public event EventHandler? OnClick;

        public Panel BtnBody = new Panel();
        public Label BtnText = new Label();
        public override bool BlockInput { get; set; } = true;

        public override float Padding {
            set {
                this.BtnText.Margin = value;
                this.HandleTextChange(this, EventArgs.Empty);
            }
        }

        public override Vector2f Position {
            get => base.Position;
            set {
                base.Position = new Vector2f(
                    value.X + this.MarginLeft,
                    value.Y + this.MarginTop);

                this.BtnBody.Position = new Vector2f(
                    base.Position.X,
                    base.Position.Y);

                this.BtnText.Position = new Vector2f(
                    base.Position.X + this.BorderWidth,
                    base.Position.Y + this.BorderWidth);
            }
        }

        public override float Margin {
            set {
                base.Margin = value;
                this.HandleTextChange(this, EventArgs.Empty);
            }
        }

        public override float PaddingBottom {
            get => this.BtnText.MarginBottom;
            set { 
                this.BtnText.MarginBottom = value;
                this.HandleTextChange(this, EventArgs.Empty);
            }
        }

        public override float PaddingTop {
            get => this.BtnText.MarginTop;
            set { 
                this.BtnText.MarginTop = value;
                this.HandleTextChange(this, EventArgs.Empty);
            }
        }

        public override float PaddingLeft {
            get => this.BtnText.MarginLeft;
            set { 
                this.BtnText.MarginLeft = value;
                this.HandleTextChange(this, EventArgs.Empty);
            }
        }

        public override float PaddingRight {
            get => this.BtnText.MarginRight;
            set { 
                this.BtnText.MarginRight = value;
                this.HandleTextChange(this, EventArgs.Empty);
            }
        }

        public override float MarginBottom {
            get => base.MarginBottom;
            set {
                base.MarginBottom = value;
                this.HandleTextChange(this, EventArgs.Empty);
            }
        }

        public override float MarginLeft {
            get => base.MarginLeft; 
            set {
                base.MarginLeft = value; 
                this.HandleTextChange(this, EventArgs.Empty);
            }
        }

        public override float MarginRight {
            get => base.MarginRight;
            set {
                base.MarginRight = value;
                this.HandleTextChange(this, EventArgs.Empty);
            }
        }

        public override float MarginTop {
            get => base.MarginTop; 
            set {
                base.MarginTop = value; 
                this.HandleTextChange(this, EventArgs.Empty);
            }
        }

        public override float BorderWidth {
            get => this.BtnBody.BorderWidth;
            set {
                this.BtnBody.BorderWidth = value;
                this.HandleTextChange(this, EventArgs.Empty);
            }
        }

        public override Color BorderColor {
            get => this.BtnBody.BorderColor;
            set => this.BtnBody.BorderColor = value;
        }

        public override Color FillColor {
            get => this.BtnBody.FillColor;
            set => this.BtnBody.FillColor = value;
        }

        public override bool Hover {
            get => base.Hover;
            set {
                base.Hover = value;
                if(!value && this.Pressed)
                {
                    this.Pressed = false;
                }
                this.SetColor();
            }
        }

        private bool _Pressed;
        public bool Pressed {
            get => this._Pressed;
            set {
                this._Pressed = value;
                this.SetColor();
            }
        }

        private ThemeButton _ThemeNormal;
        public ThemeButton ThemeNormal { 
            get => this._ThemeNormal;
            set {
                this._ThemeNormal = value;
                this.Theme = value;
            }
        }

        public ThemeButton ThemeHover { get; set; }
        public ThemeButton ThemePressed { get; set; }

        public ThemeButton Theme {
            set {
                if(value.FillColor is not null) 
                    { this.FillColor = (Color)value.FillColor; }
                if(value.BorderColor is not null) 
                    { this.BorderColor = (Color)value.BorderColor; }
                if(value.BorderWidth is not null) 
                    { this.BorderWidth = (float)value.BorderWidth; }
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
            }
        }

        public Button(string text) : base()
        {
            this.BtnText.StringChanged += this.HandleTextChange;
            this.BtnText.DisplayedString = text;
            base.Add(this.BtnBody, this.BtnText);
            this.BlockInput = true;
            this.SetColor();
        }

        public override MouseButtonEventArgs? ProcessMousePress(
            object? sender, MouseButtonEventArgs? e)
        {
            if(this.QueryPress(e))
                { this.Pressed = true; }
            else
                { this.Pressed = false; }
            return base.ProcessMousePress(sender, e);
        }

        public override MouseButtonEventArgs? ProcessMouseRelease(
            object? sender, MouseButtonEventArgs? e)
        {
            if(this.QueryRelease(e))
            {
                this.Pressed = false;
                this.OnClick?.Invoke(this, EventArgs.Empty);
            }
            return base.ProcessMouseRelease(sender, e);
        }

        private void HandleTextChange(object? sender, EventArgs e)
        {
            var _bnds = this.BtnText.Bounds;
            this.BtnBody.Size =  new Vector2f(_bnds.Width, _bnds.Height);
        }

        private void SetColor()
        {
            if(this.Pressed)
            {
                this.Theme = this.ThemePressed;
            }
            else if(this.Hover)
            {
                this.Theme = this.ThemeHover;
            }
            else 
            {
                this.Theme = this.ThemeNormal;
            }
        }

        public override string ToString()
        {
            return $"{this.GetType()}: {this.BtnText.DisplayedString}";
        }

        private bool QueryPress(MouseButtonEventArgs? e)
        {
            return e is not null && this.Hover && e.Button == Mouse.Button.Left && !this.Pressed;
        }

        private bool QueryRelease(MouseButtonEventArgs? e)
        {
            return e is not null && this.Hover && e.Button == Mouse.Button.Left && this.Pressed;
        }
    }
}