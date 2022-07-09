using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Rogui.Extensions;

namespace Rogui
{
    public class Button : Aspect
    {
        private bool CanSetBody = true;

        public event EventHandler? OnClick;

        public Panel BtnBody = new Panel();
        public Label BtnText = new Label();

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
                this.CanSetBody = false;
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

        private Color _ColorNormal;
        public Color ColorNormal {
            get => this._ColorNormal;
            set {
                this._ColorNormal = value;
                this.SetColor();
            }
        }

        private Color _ColorHover;
        public Color ColorHover {
            get => this._ColorHover;
            set {
                this._ColorHover = value;
                this.SetColor();
            }
        }

        private Color _ColorPressed;
        public Color ColorPressed {
            get => this._ColorPressed;
            set {
                this._ColorPressed = value;
                this.SetColor();
            }
        }

        public override Color FillColor {
            get => this.BtnBody.FillColor;
            set => this.BtnBody.FillColor = value;
        }

        private bool _Hover;
        public bool Hover {
            get => this._Hover;
            set {
                this._Hover = value;
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

        public Button(string text) : base()
        {
            this.BtnText.StringChanged += this.HandleTextChange;
            this.BtnText.DisplayedString = text;
            base.Add(this.BtnBody, this.BtnText);
            this.SetColor();
        }

        public override bool ProcessMouseMove(object? sender, MouseMoveEventArgs e)
        {
            var _hover = this.Bounds.Contains(e);

            if(this.Hover != _hover)
            {
                this.Hover = _hover;
            }

            if(this.Hover)
            {
                return this.BlockInput;
            }
            else
            {
                if(this.Pressed)
                {
                    this.Pressed = false;
                }
            }

            return false;
        }

        public override bool ProcessMousePress(object? sender, MouseButtonEventArgs e)
        {
            if(this.Hover && e.Button == Mouse.Button.Left && !this.Pressed)
            {
                this.Pressed = true;
            }
            return this.BlockInput;
        }

        public override bool ProcessMouseRelease(object? sender, MouseButtonEventArgs e)
        {
            if(this.Hover)
            {
                if(this.Pressed && e.Button == Mouse.Button.Left)
                {
                    this.Pressed = false;
                    this.OnClick?.Invoke(this, EventArgs.Empty);
                }
                return this.BlockInput;
            }

            return false;
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
                this.FillColor = this.ColorPressed;
            }
            else if(this.Hover)
            {
                this.FillColor = this.ColorHover;
            }
            else 
            {
                this.FillColor = this.ColorNormal;
            }
        }

        public override string ToString()
        {
            return $"{this.GetType()}: {this.BtnText.DisplayedString}";
        }
    }
}