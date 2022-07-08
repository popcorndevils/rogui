using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Rogui.Extensions;

namespace Rogui
{
    public class Button : Aspect
    {
        public event EventHandler? OnClick;

        public Panel BtnBody = new Panel();
        public Label BtnText;

        public override FloatRect Bounds => this.BtnBody.Bounds;

        public override Vector2f Position { 
            get => this.BtnBody.Position;
            set {
                this.BtnBody.Position = value;                
                this.BtnText.Position = new Vector2f(
                    value.X + this.MarginLeft + this.BorderWidth,
                    value.Y + this.MarginTop + this.BorderWidth);
            }
        }

        public override float MarginBottom {
            get => this.BtnBody.MarginBottom;
            set {
                this.BtnBody.MarginBottom = value;
                this.SetBody();
            }
        }

        public override float MarginLeft {
            get => this.BtnBody.MarginLeft; 
            set {
                this.BtnBody.MarginLeft = value; 
                this.SetBody();
            }
        }

        public override float MarginRight {
            get => this.BtnBody.MarginRight;
            set {
                this.BtnBody.MarginRight = value;
                this.SetBody();
            }
        }

        public override float MarginTop {
            get => this.BtnBody.MarginTop; 
            set {
                this.BtnBody.MarginTop = value; 
                this.SetBody();
            }
        }

        public override float BorderWidth {
            get => this.BtnBody.BorderWidth;
            set {
                this.BtnBody.BorderWidth = value;
                this.SetBody();
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

        public Button(string text)
        {
            this.BtnText = new Label(text);
            this.SetBody();
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            this.BtnBody.Draw(t, s);
            this.BtnText.Draw(t, s);
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

        private void SetBody()
        {
            var _bnds = this.BtnText.Bounds;
            this.BtnBody.Size =  new Vector2f(_bnds.Width, _bnds.Height);
            this.Position = this.Position;
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