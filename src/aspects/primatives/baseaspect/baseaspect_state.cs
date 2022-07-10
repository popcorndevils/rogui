using SFML.System;

namespace Rogui.Primitives
{
    public abstract partial class BaseAspect
    {
        // aspect events
        public event EventHandler? StateChanged;

        // aspect states
        private bool _Hover;
        private bool _Pressed;
        public virtual bool Visible { get; set; } = true;
        public virtual bool BlockInput { get; set; } = false;

        public virtual bool Hover {
            get => this._Hover;
            set {
                if(value != this._Hover)
                {
                    this._Hover = value;
                    if(!value && this.Pressed)
                    {
                        this._Pressed = false;
                    }
                    this.StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual bool Pressed {
            get => this._Pressed;
            set {
                this._Pressed = value;
                this.StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}