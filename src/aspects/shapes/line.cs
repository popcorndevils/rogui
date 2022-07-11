using SFML.Graphics;
using SFML.System;
using Rogui.Extensions;

namespace Rogui.Shapes
{
    public class Line : Aspect
    {
        public RectangleShape Rect;
        public override FloatRect Bounds => this.Rect.GetGlobalBounds();

        private Vertex V1, V2, V3, V4;
        private Vertex[] Vertices => new Vertex[] {this.V1, this.V2, this.V3, this.V4};
        private Vector2f _PointStart;
        private Vector2f _PointEnd;
        private Vector2f _Offset;
        private float Width;
        private float MaxLength;

        public override Vector2f AbsolutePosition {
            get => base.AbsolutePosition;
            set {
                this.Rect.Position = new Vector2f(
                    value.X + this.MarginLeft + this.Position.X,
                    value.Y + this.MarginTop + this.Position.Y
                );
                base.AbsolutePosition = value;
            }
        }

        public override Color FillColor {
            get => this.Rect.FillColor;
            set => this.Rect.FillColor = value;
        }

        public Vector2f PointStart {
            get => this._PointStart;
            set {
                this._PointStart = value;
            }
        }

        public Vector2f PointEnd {
            get => this._PointEnd;
            set {
                this._PointEnd = value;
            }
        }

        public Line(float x1, float y1, float x2, float y2, float width)
        {
            this.PointStart = new Vector2f(x1, y1);
            this.PointEnd = new Vector2f(x2, y2);
            this.Width = width;

            this.MaxLength = this.PointStart.GetDistanceTo(this.PointEnd);
            this.Position = this.PointStart - new Vector2f(0, width / 2);

            this.Rect = new RectangleShape(
                new Vector2f(0, width)){
                Origin = new Vector2f(0, width / 2),
                Rotation = this.PointStart.GetAngleTo(this.PointEnd)};

            this.FillColor = new Color(255, 0, 0);
        }

        public override void Update(float? ms)
        {
            this.Rect.Rotation += .01f;
            var _new_len = this.Rect.Size.X + .01f;
            if(_new_len > this.MaxLength)
            {
                _new_len = 0;
            }
            this.Rect.Size = new Vector2f(_new_len, this.Width);
            if(this.Rect.Rotation > 360)
            {
                this.Rect.Rotation = 0;
            }
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            this.Rect.Draw(t, s);
        }
    }
}