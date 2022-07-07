using SFML.System;

namespace Rogui.Containers
{
    public class VBox : Container
    {
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
            base.UpdateLayout();
        }
    }
}