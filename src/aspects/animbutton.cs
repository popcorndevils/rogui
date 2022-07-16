using SFML.Graphics;
using SFML.System;
using Rogui.Themes;

namespace Rogui
{
    public class AnimButton : Aspect
    {

        public event EventHandler? Closed;
        public event EventHandler? Opened;

        public bool IsOpen => this.Body.IsOpen;
        public bool IsClosed => this.Body.IsClosed;
        public bool IsOpening => this.Body.IsOpening;
        public bool IsClosing => this.Body.IsClosing;

        public virtual AnimPanel Body { get; set; }
        public Label BtnText = new Label();

        public override FloatRect Bounds => this.Body.Bounds;

        public ThemePanel? DisplayTheme {
            get => this.Body.Theme;
            set => this.Body.Theme = value;
        }

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

        public AnimButton()
        {
            this.BlockInput = true;
            this.Body = new AnimPanel();
            base.StateChanged += this.OnStateChange;
            this.Body.Add(this.BtnText);
            base.Add(this.Body);
        }
        public AnimButton(string text)
        {
            this.BlockInput = true;
            this.Body = new AnimPanel();
            this.Body.Opened += this.HandleBodyOpened;
            this.Body.Closed += this.HandleBodyClosed;
            base.StateChanged += this.OnStateChange;
            this.BtnText.DisplayedString = text;
            this.Body.Add(this.BtnText);
            base.Add(this.Body);
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

        public void Open()
        {
            this.Body.Open();
        }

        public void Close()
        {
            this.Body.Close();
        }

        public void HandleBodyClosed(object? sender, EventArgs e)
        {
            this.Closed?.Invoke(this, EventArgs.Empty);
        }

        public void HandleBodyOpened(object? sender, EventArgs e)
        {
            this.Opened?.Invoke(this, EventArgs.Empty);
        }
    }
}