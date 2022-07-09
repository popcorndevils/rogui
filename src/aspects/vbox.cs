using SFML.System;

namespace Rogui
{
    public class VBox : Aspect
    {
        public float MarginSeparator;

        public override Vector2f Position {
            get => base.Position;
            set {
                base.Position = new Vector2f(
                    value.X + this.MarginLeft,
                    value.Y + this.MarginTop);

                this.UpdateLayout();

                // foreach(Aspect c in this.Children)
                // {
                //     c.Position = new Vector2f
                //     (this.Position.X + this.PaddingLeft,
                //     this.Position.Y + this.PaddingTop);
                // }
            }
        }

        protected override void UpdateLayout()
        {
            float _bottom = this.Position.Y + this.MarginTop;
            foreach(Aspect a in this.Children)
            {
                a.Position = new Vector2f(
                    this.Position.X + this.MarginLeft, _bottom + this.MarginSeparator);
                var _bnds = a.Bounds;
                _bottom = _bnds.Top + _bnds.Height;
            }
            // base.UpdateLayout();
        }
    }
}