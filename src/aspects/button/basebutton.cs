using SFML.Graphics;
using SFML.Window;
using Rogui.Themes;
using SFML.System;

namespace Rogui
{
    public abstract class BaseButton<T> : Aspect where T : Panel, new()
    {
        public T Body = new T();
        public Label BtnText = new Label();

        public override FloatRect Bounds => this.Body.Bounds;
        public override Vector2f TruePosition => this.Body.TruePosition;
        public override Vector2f TrueCenter => this.Body.TrueCenter;

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

        public ThemePanel? DisplayTheme {
            get => this.Body.Theme;
            set => this.Body.Theme = value;
        }

        public string DisplayedString {
            get => this.BtnText.DisplayedString;
            set => this.BtnText.DisplayedString = value;
        }

        public BaseButton(string? description = null) : base()
        {
            this.BlockInput = false;
            if(description is not null)
                { this.DisplayedString = description; }
            this.Body.Add(this.BtnText);
            this.Add(this.Body);
            base.StateChanged += this.HandleBodyState;
        }

        protected virtual void HandleBodyState(object? sender, EventArgs e)
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

        // ██╗  ██╗██╗██████╗ ██████╗ ███████╗███╗   ██╗
        // ██║  ██║██║██╔══██╗██╔══██╗██╔════╝████╗  ██║
        // ███████║██║██║  ██║██║  ██║█████╗  ██╔██╗ ██║
        // ██╔══██║██║██║  ██║██║  ██║██╔══╝  ██║╚██╗██║
        // ██║  ██║██║██████╔╝██████╔╝███████╗██║ ╚████║
        // ╚═╝  ╚═╝╚═╝╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝
        private ThemeButton? _Theme;
    }
}