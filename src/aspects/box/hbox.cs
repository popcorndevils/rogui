using SFML.System;

namespace Rogui
{
    public class HBox : Aspect
    {
        protected override void UpdateLayout()
        {
            float _left = this.AbsolutePosition.X + this.MarginLeft;
            float _top = this.AbsolutePosition.Y + this.MarginTop;
            foreach(Aspect a in this.Children)
            {
                a.AbsolutePosition = new Vector2f(_left, _top);
                _left += a.Bounds.Width + this.MarginSeparator;
            }
        }
    }
}