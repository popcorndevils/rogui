using SFML.Graphics;
using SFML.System;
using Rogui.Extensions;

namespace Rogui.Shapes
{
    public class Line : Aspect
    {
        // ██████╗ ██████╗  ██████╗ ██████╗ ███████╗██████╗ ████████╗██╗███████╗███████╗
        // ██╔══██╗██╔══██╗██╔═══██╗██╔══██╗██╔════╝██╔══██╗╚══██╔══╝██║██╔════╝██╔════╝
        // ██████╔╝██████╔╝██║   ██║██████╔╝█████╗  ██████╔╝   ██║   ██║█████╗  ███████╗
        // ██╔═══╝ ██╔══██╗██║   ██║██╔═══╝ ██╔══╝  ██╔══██╗   ██║   ██║██╔══╝  ╚════██║
        // ██║     ██║  ██║╚██████╔╝██║     ███████╗██║  ██║   ██║   ██║███████╗███████║
        // ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝╚══════╝╚══════╝  
        protected RectangleShape Shape = new RectangleShape();
        public override FloatRect Bounds => this.Shape.GetGlobalBounds();
    
        protected float Width {
            get => this.Size.Y;
            set {
                this.Size = new Vector2f(this.Length, value);
                this.Origin = new Vector2f(0, value / 2);
                this.Position = this.PointStart - new Vector2f(0, value / 2);
            }
        }

        protected virtual float Length  {
            get => this.Size.X;
            set {
                this.Size = new Vector2f(value, this.Width);
            }
        }

        protected Vector2f PointStart {
            get => this._PointStart;
            set {
                this._PointStart = value;
                this.Resize();
            }
        }
        protected Vector2f PointEnd {
            get => this._PointEnd;
            set {
                this._PointEnd = value;
                this.Resize();
            }
        }

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

        public override Vector2f Size {
            get => this.Shape.Size;
            set => this.Shape.Size = value;
        }

        public override Color FillColor {
            get => this.Shape.FillColor;
            set => this.Shape.FillColor = value;
        }

        public Vector2f Origin {
            get => this.Shape.Origin;
            set => this.Shape.Origin = value;
        }

        public float Rotation {
            get => this.Shape.Rotation;
            set => this.Shape.Rotation = value;
        }


        //  ██████╗ ██████╗ ███╗   ██╗███████╗████████╗██████╗ ██╗   ██╗ ██████╗████████╗
        // ██╔════╝██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔══██╗██║   ██║██╔════╝╚══██╔══╝
        // ██║     ██║   ██║██╔██╗ ██║███████╗   ██║   ██████╔╝██║   ██║██║        ██║   
        // ██║     ██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══██╗██║   ██║██║        ██║   
        // ╚██████╗╚██████╔╝██║ ╚████║███████║   ██║   ██║  ██║╚██████╔╝╚██████╗   ██║   
        //  ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝  ╚═════╝   ╚═╝   
        public Line() {}
        public Line(Vector2f start, Vector2f end, float width = 1)
            { this.Init(start.X, start.Y, end.X, end.Y, width); }
        public Line(float x1, float y1, float x2, float y2, float width = 1)
            { this.Init(x1, y1, x2, y2, width); }
        private void Init(float x1, float y1, float x2, float y2, float width)
        {
            this._PointStart = new Vector2f(x1, y1);
            this._PointEnd = new Vector2f(x2, y2);
            this.Width = width;
            this.Resize();
        }


        //  ██████╗ ██╗   ██╗███████╗██████╗ ██████╗ ██╗██████╗ ███████╗███████╗
        // ██╔═══██╗██║   ██║██╔════╝██╔══██╗██╔══██╗██║██╔══██╗██╔════╝██╔════╝
        // ██║   ██║██║   ██║█████╗  ██████╔╝██████╔╝██║██║  ██║█████╗  ███████╗
        // ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗██╔══██╗██║██║  ██║██╔══╝  ╚════██║
        // ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║██║  ██║██║██████╔╝███████╗███████║
        //  ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝╚═════╝ ╚══════╝╚══════╝
        public override void Draw(RenderTarget t, RenderStates s)
        {
            if(this.Visible)
            {
                this.Shape.Draw(t, s);
            }
        }

        // ██╗  ██╗███████╗██╗     ██████╗ ███████╗██████╗ ███████╗
        // ██║  ██║██╔════╝██║     ██╔══██╗██╔════╝██╔══██╗██╔════╝
        // ███████║█████╗  ██║     ██████╔╝█████╗  ██████╔╝███████╗
        // ██╔══██║██╔══╝  ██║     ██╔═══╝ ██╔══╝  ██╔══██╗╚════██║
        // ██║  ██║███████╗███████╗██║     ███████╗██║  ██║███████║
        // ╚═╝  ╚═╝╚══════╝╚══════╝╚═╝     ╚══════╝╚═╝  ╚═╝╚══════╝
        protected virtual void Resize()
        {
            this.Length = this.PointStart.GetDistanceTo(this.PointEnd);
            this.Size = new Vector2f(this.Length, this.Width);
            this.Origin = new Vector2f(0, this.Width / 2);
            this.Rotation = this.PointStart.GetAngleTo(this.PointEnd);
        }                                                


        // ██╗  ██╗██╗██████╗ ██████╗ ███████╗███╗   ██╗
        // ██║  ██║██║██╔══██╗██╔══██╗██╔════╝████╗  ██║
        // ███████║██║██║  ██║██║  ██║█████╗  ██╔██╗ ██║
        // ██╔══██║██║██║  ██║██║  ██║██╔══╝  ██║╚██╗██║
        // ██║  ██║██║██████╔╝██████╔╝███████╗██║ ╚████║
        // ╚═╝  ╚═╝╚═╝╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝
        private Vector2f _PointStart;
        private Vector2f _PointEnd;
    }
}