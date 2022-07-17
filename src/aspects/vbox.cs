using SFML.System;

namespace Rogui
{
    public class VBox : Aspect
    {
        public float MarginSeparator;

        public override Vector2f AbsolutePosition {
            get => base.AbsolutePosition;
            set {
                base.AbsolutePosition = new Vector2f(
                    value.X + this.MarginLeft,
                    value.Y + this.MarginTop);

                this.UpdateLayout();
            }
        }

        protected override void UpdateLayout()
        {
            float _bottom = this.AbsolutePosition.Y + this.MarginTop;
            foreach(Aspect a in this.Children)
            {
                a.AbsolutePosition = new Vector2f(
                    this.AbsolutePosition.X + this.MarginLeft, _bottom);
                var _bnds = a.Bounds;
                _bottom += _bnds.Height + this.MarginSeparator;
            }
        }
    }
}