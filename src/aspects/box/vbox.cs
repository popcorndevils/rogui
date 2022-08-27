using SFML.System;

namespace Rogui
{
    public class VBox : BaseBox
    {
        public VBox() : base() {}
        public VBox(params Aspect[] aspects) : base(aspects) {}

        protected override void UpdateLayout()
        {
            float _bottom = this.AbsolutePosition.Y;
            float _left = this.AbsolutePosition.X;
            foreach(Aspect a in this.Children)
            {
                a.AbsolutePosition = new Vector2f(_left, _bottom);
                _bottom += a.Bounds.Height + this.MarginSeparator;
            }
        }
    }
}