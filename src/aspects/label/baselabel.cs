using SFML.Graphics;
using SFML.System;

namespace Rogui
{
    public abstract class BaseLabel : Aspect
    {
        public new event EventHandler? Transformed;
        public virtual Text? TextShape { get; init; }
        public Font? Font { get; init; }
        public Vector2f RenderPosition => this.AbsolutePosition + this.Position + this.MarginPosition + this.OffsetPosition;
        
        public override FloatRect Bounds {
            get {
                if(this.TextShape is not null)
                {
                    var _bnds = this.TextShape.GetLocalBounds();
                    var _pos = this.AbsolutePosition;
                    return new FloatRect(
                        _pos.X - this.MarginLeft,
                        _pos.Y - this.MarginTop,
                        _bnds.Width + (_bnds.Left * 2) + this.MarginLeft + this.MarginRight,
                        _bnds.Height + (_bnds.Top * 2) + this.MarginTop + this.MarginBottom);
                }
                else
                {
                    return new FloatRect();
                }
            }
        } 

        public override Vector2f AbsolutePosition {
            get => base.AbsolutePosition;
            set {
                if(this.TextShape is not null)
                {
                    this.TextShape.Position = new Vector2f(
                        value.X + this.MarginLeft + this.Position.X,
                        value.Y + this.MarginTop + this.Position.Y
                    );
                }
                base.AbsolutePosition = value;
            }
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            if(this.Visible && this.TextShape is not null)
            {
                this.TextShape.Position = this.RenderPosition;
                this.TextShape.Draw(t, s);
            }
        }

        public virtual string? DisplayedString {
            get => this._DisplayedString;
            set {
                this._DisplayedString = value;
                if(this.TextShape is not null)
                {
                    this.TextShape.DisplayedString = value;
                }
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }

        private string? _DisplayedString;
    }
}