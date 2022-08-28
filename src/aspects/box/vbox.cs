using SFML.System;

namespace Rogui
{
    public class VBox : BaseBox
    {
        public VBox() : base() {}
        public VBox(params Aspect[] aspects) : base(aspects) {}

        protected override void UpdateLayout()
        {
            float _bottom = this.WindowPosition.Y;
            float _left = this.WindowPosition.X;
            foreach(Aspect a in this.Children)
            {
                a.PositionGlobal = new Vector2f(_left, _bottom);
                _bottom += a.Bounds.Height + this.MarginSeparator;
            }
        }
    }
}