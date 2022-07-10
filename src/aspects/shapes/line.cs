using SFML.Graphics;
using SFML.System;

namespace Rogui.Shapes
{
    public class Line : Aspect
    {
        public override FloatRect Bounds {
            get {
                var _x = new float[this.Vertices.Length];
                var _y = new float[this.Vertices.Length];

                for(int i = 0; i < this.Vertices.Length; i++)
                {
                    _x[i] = this.Vertices[i].Position.X;
                    _y[i] = this.Vertices[i].Position.Y;
                }

                return new FloatRect(
                    _x.Min(),
                    _y.Min(),
                    _x.Max() - _x.Min(),
                    _y.Max() - _y.Min());
            }
        }

        private Vertex V1, V2, V3, V4;
        private Vertex[] Vertices => new Vertex[] {this.V1, this.V2, this.V3, this.V4};
        private Vector2f _PointStart;
        private Vector2f _PointEnd;
        private Vector2f _Offset;
        private float Width;

        public override Color FillColor {
            get => base.FillColor;
            set {
                this.V1.Color = value;
                this.V2.Color = value;
                this.V3.Color = value;
                this.V4.Color = value;
                base.FillColor = value;
            }
        }

        public Vector2f Offset {
            set {
                Console.WriteLine(value);
                this._Offset = value;
                this.V1.Position = this.PointStart - value;
                this.V2.Position = this.PointStart + value;
                this.V3.Position = this.PointEnd + value;
                this.V4.Position = this.PointEnd - value;
            }
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
            this.Width = width;
            this.PointStart = new Vector2f(x1, y1);
            this.PointEnd = new Vector2f(x2, y2);
            var _dir = this.PointEnd - this.PointStart;
            var _unitDir = _dir / (float)Math.Sqrt(Math.Pow(_dir.X, 2) + Math.Pow(_dir.Y, 2));
            var _unitPerp = new Vector2f(-_unitDir.Y, _unitDir.X);
            this.Offset = (this.Width / 2) * _unitPerp;

            this.FillColor = new Color(255, 0, 0);
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            t.Draw(this.Vertices, PrimitiveType.Quads);
        }
    }
}