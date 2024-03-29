using SFML.Graphics;
using Rogui.Themes;
using SFML.System;

namespace Rogui
{
    public abstract class BaseButton<T, U> : Aspect 
    where T : Panel, new() 
    where U : BaseLabel, new()
    {
        public T Body = new T();
        public U Text = new U();
        
        public new event EventHandler? OnClick;
        public new event EventHandler? StateChanged;

        public override bool Visible { 
            get => this.Body.Visible; 
            set => this.Body.Visible = value;
        }
        public override bool AcceptInput { 
            get => this.Body.AcceptInput; 
            set => this.Body.AcceptInput = value;
        }
        public override FloatRect Bounds => this.Body.Bounds;
        public override FloatRect InputBounds => this.Body.InputBounds;
        public override Vector2f WindowPosition => this.Body.WindowPosition;
        public override Vector2f TrueCenter => this.Body.TrueCenter;
        public Vector2f InteriorPosition => this.Body.InteriorPosition;

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

        public string DisplayString {
            get {
                if(this.Text.DisplayedString is not null)
                {
                    return this.Text.DisplayedString;
                }
                else
                {
                    return "";
                }
            }
            set => this.Text.DisplayedString = value;
        }

        public BaseButton(string? description = null) : base()
        {
            this.BlockInput = true;
            if(description is not null)
                { this.DisplayString = description; }
            this.Body.Add(this.Text);
            this.Add(this.Body);
            base.StateChanged += this.HandleBodyState;
            this.Body.OnClick += this.HandleClick;
        }

        public void HandleClick(object? sender, EventArgs e)
        {
            this.OnClick?.Invoke(this, e);
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
            this.StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            return $"{this.GetType()}: {this.Text.DisplayedString}";
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