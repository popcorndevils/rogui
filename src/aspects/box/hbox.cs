using SFML.System;

namespace Rogui
{
    public class HBox : BaseBox
    {
        public HBox() : base() {}
        public HBox(params Aspect[] aspects) : base(aspects) {}

        protected override void UpdateLayout()
        {
            float _left = this.WindowPosition.X;
            float _top = this.WindowPosition.Y;
            foreach(Aspect a in this.Children)
            {
                a.PositionGlobal = new Vector2f(_left, _top);
                _left += a.Bounds.Width + this.MarginSeparator;
            }
        }
    }
}