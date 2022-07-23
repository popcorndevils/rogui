using SFML.System;

namespace Rogui
{
    public class VBox : BaseBox
    {
        public VBox() : base() {}
        public VBox(params Aspect[] aspects) : base(aspects) {}

        protected override void UpdateLayout()
        {
            float _bottom = this.AbsolutePosition.Y + this.MarginTop;
            float _left = this.AbsolutePosition.X + this.MarginLeft;
            foreach(Aspect a in this.Children)
            {
                a.AbsolutePosition = new Vector2f(_left, _bottom);
                _bottom += a.Bounds.Height + this.MarginSeparator;
            }
        }
    }
}