using SFML.Graphics;
using SFML.System;
using Rogui.Extensions;

namespace Rogui.Shapes
{
    public class Line : Aspect
    {
        protected RectangleShape Shape = new RectangleShape();
        protected float Width { get; set; }
        protected virtual float Length { get; set; }
        protected Vector2f PointStart { get; set; }
        protected Vector2f PointEnd { get; set; }

        public override FloatRect Bounds => this.Shape.GetGlobalBounds();

        public override Vector2f AbsolutePosition {
            get => base.AbsolutePosition;
            set {
                this.Shape.Position = new Vector2f(
                    value.X + this.MarginLeft + this.Position.X,
                    value.Y + this.MarginTop + this.Position.Y
                );
                base.AbsolutePosition = value;
            }
        }

        public override Color FillColor {
            get => this.Shape.FillColor;
            set => this.Shape.FillColor = value;
        }

        private void Init(float x1, float y1, float x2, float y2, float width)
        {
            this.PointStart = new Vector2f(x1, y1);
            this.PointEnd = new Vector2f(x2, y2);
            this.Width = width;

            this.Length = this.PointStart.GetDistanceTo(this.PointEnd);
            this.Position = this.PointStart - new Vector2f(0, width / 2);

            this.Shape.Size = new Vector2f(this.Length, width);
            this.Shape.Origin = new Vector2f(0, this.Width / 2);
            this.Shape.Rotation = this.PointStart.GetAngleTo(this.PointEnd);
        }

        public Line() {}
        public Line(Vector2f start, Vector2f end, float width = 1)
            { this.Init(start.X, start.Y, end.X, end.Y, width); }
        public Line(float x1, float y1, float x2, float y2, float width = 1)
            { this.Init(x1, y1, x2, y2, width); }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            if(this.Visible)
            {
                this.Shape.Draw(t, s);
            }
        }
    }
}