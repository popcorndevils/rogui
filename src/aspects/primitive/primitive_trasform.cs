using SFML.System;
using SFML.Graphics;
using Rogui.Themes;

namespace Rogui
{
    public abstract partial class Primitive
    {
        public virtual event EventHandler? Transformed;

        // ██╗  ██╗███████╗██╗     ██████╗ ███████╗██████╗ ███████╗
        // ██║  ██║██╔════╝██║     ██╔══██╗██╔════╝██╔══██╗██╔════╝
        // ███████║█████╗  ██║     ██████╔╝█████╗  ██████╔╝███████╗
        // ██╔══██║██╔══╝  ██║     ██╔═══╝ ██╔══╝  ██╔══██╗╚════██║
        // ██║  ██║███████╗███████╗██║     ███████╗██║  ██║███████║
        // ╚═╝  ╚═╝╚══════╝╚══════╝╚═╝     ╚══════╝╚═╝  ╚═╝╚══════╝

        /// <summary>
        /// Set a uniform margin width.
        /// </summary>
        public virtual float Margin {
            set {
                this._MarginBottom = value;
                this._MarginTop = value;
                this._MarginLeft = value;
                this._MarginRight = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Set a uniform padding width.
        /// </summary>
        public virtual float Padding {
            set {
                this._PaddingBottom = value;
                this._PaddingTop = value;
                this._PaddingLeft = value;
                this._PaddingRight = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Set a uniform border width.
        /// </summary>
        public virtual float Border {
            set {
                this._BorderBottom = value;
                this._BorderTop = value;
                this._BorderLeft = value;
                this._BorderRight = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        // ephemeral properties
        public virtual Vector2f WindowPosition {
            get {
                var _out = this.PositionGlobal + 
                           this.PositionLocal + 
                           this.MarginPosition + 
                           this.PositionOffset;
                return _out;
            }
        }
        public virtual Vector2f TrueCenter { get; }

        public virtual float MarginWidth => this.MarginLeft + this.MarginRight;
        public virtual float MarginHeight => this.MarginTop + this.MarginBottom;
        public virtual float BorderWidth => this.BorderLeft + this.BorderRight;
        public virtual float BorderHeight => this.BorderTop + this.BorderBottom;
        public virtual Vector2f MarginPosition => new Vector2f(this.MarginLeft, this.MarginTop);
        public virtual Vector2f MarginSize => new Vector2f( this.MarginWidth, this.MarginHeight);
        public virtual Vector2f BorderPosition  => new Vector2f(this.BorderLeft, this.BorderTop);
        public virtual Vector2f BorderSize => new Vector2f(this.BorderWidth, this.BorderHeight);
        public virtual FloatRect InputBounds => this.Bounds;


        // ██████╗ ██████╗  ██████╗ ██████╗ ███████╗██████╗ ████████╗██╗███████╗███████╗
        // ██╔══██╗██╔══██╗██╔═══██╗██╔══██╗██╔════╝██╔══██╗╚══██╔══╝██║██╔════╝██╔════╝
        // ██████╔╝██████╔╝██║   ██║██████╔╝█████╗  ██████╔╝   ██║   ██║█████╗  ███████╗
        // ██╔═══╝ ██╔══██╗██║   ██║██╔═══╝ ██╔══╝  ██╔══██╗   ██║   ██║██╔══╝  ╚════██║
        // ██║     ██║  ██║╚██████╔╝██║     ███████╗██║  ██║   ██║   ██║███████╗███████║
        // ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝╚══════╝╚══════╝
        /// <summary>
        /// Position relative to parent.
        /// </summary>
        public virtual Vector2f PositionLocal {
            get => this._PositionLocal;
            set {
                this._PositionLocal = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }


        /// <summary>
        /// The actual placement in 2d space.
        /// </summary>
        public virtual Vector2f PositionGlobal {
            get => this._PositionGlobal;
            set {
                this._PositionGlobal = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Allows some aspects to alter themselves temporarily for animations.
        /// </summary>
        public virtual Vector2f PositionOffset {
            get => this._OffsetPosition;
            set {
                this._OffsetPosition = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Used to set the display size of aspects.
        /// </summary>
        public virtual Vector2f Size {
            get => this._Size;
            set {
                this._Size = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Optionally used by aspects to set a maximum size;
        /// </summary>
        public virtual Vector2f MaxSize {
            get => this._MaxSize;
            set {
                this._MaxSize = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float MarginSeparator {
            get => this._MarginSeparator;
            set {
                this._MarginSeparator = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float MarginLeft {
            get => this._MarginLeft;
            set {
                this._MarginLeft = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float MarginRight {
            get => this._MarginRight;
            set {
                this._MarginRight = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float MarginTop {
            get => this._MarginTop;
            set {
                this._MarginTop = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float MarginBottom {
            get => this._MarginBottom;
            set {
                this._MarginBottom = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float PaddingLeft {
            get => this._PaddingLeft;
            set {
                this._PaddingLeft = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float PaddingRight {
            get => this._PaddingRight;
            set {
                this._PaddingRight = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float PaddingTop {
            get => this._PaddingTop;
            set {
                this._PaddingTop = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float PaddingBottom {
            get => this._PaddingBottom;
            set {
                this._PaddingBottom = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float BorderLeft {
            get => this._BorderLeft;
            set {
                this._BorderLeft = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float BorderRight {
            get => this._BorderRight;
            set {
                this._BorderRight = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float BorderTop {
            get => this._BorderTop;
            set {
                this._BorderTop = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual float BorderBottom {
            get => this._BorderBottom;
            set {
                this._BorderBottom = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual Color? BorderColor {
            get => this._BorderColor;
            set {
                this._BorderColor = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual Color? FillColor {
            get => this._FillColor;
            set {
                this._FillColor = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual Theme? Theme {
            get => this._Theme;
            set {
                this._Theme = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        // ██╗  ██╗██╗██████╗ ██████╗ ███████╗███╗   ██╗
        // ██║  ██║██║██╔══██╗██╔══██╗██╔════╝████╗  ██║
        // ███████║██║██║  ██║██║  ██║█████╗  ██╔██╗ ██║
        // ██╔══██║██║██║  ██║██║  ██║██╔══╝  ██║╚██╗██║
        // ██║  ██║██║██████╔╝██████╔╝███████╗██║ ╚████║
        // ╚═╝  ╚═╝╚═╝╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝
        private Vector2f _PositionLocal;
        private Vector2f _PositionGlobal;
        private Vector2f _OffsetPosition;
        private Vector2f _Size;
        private Vector2f _MaxSize;
        private float _MarginSeparator;
        private float _MarginLeft;
        private float _MarginTop;
        private float _MarginRight;
        private float _MarginBottom;
        private float _PaddingLeft;
        private float _PaddingTop;
        private float _PaddingRight;
        private float _PaddingBottom;
        private float _BorderLeft;
        private float _BorderTop;
        private float _BorderRight;
        private float _BorderBottom;
        private Color? _BorderColor;
        private Color? _FillColor;
        private Theme? _Theme = new Theme();
    }
}