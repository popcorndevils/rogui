using SFML.System;

namespace Rogui.Shapes
{
    public class AnimLine : Line
    {
        public event EventHandler? Closed;
        public event EventHandler? Opened;


        public bool IsOpen { get; private set; }
        public bool IsClosed {get; private set; }
        public bool IsClosing { get; private set; }
        public bool IsOpening { get; private set; }
        private float MSGrowth { get; set; }
        private float _AnimSpeed;
        public float AnimSpeed {
            get => this._AnimSpeed;
            set {
                this._AnimSpeed = value;
                this.MSGrowth = this.Length / value / 1000;
            }
        }

        public AnimLine(Vector2f start, Vector2f end, float width = 1) :
        base(start, end, width) 
        {
            this.Shape.Size = new Vector2f(0, width);
            this.AnimSpeed = 1f;
        }

        public AnimLine(float x1, float y1, float x2, float y2, float width = 1) :
        base(x1, y1, x2, y2, width)
        {
            this.Shape.Size = new Vector2f(0, width);
            this.AnimSpeed = 1f;
        }

        public override void Update(float? ms)
        {
            if(ms is not null)
            {
                float _growth_amt = this.MSGrowth * (float)ms;
                if(this.IsOpening)
                {
                    var _new_len = this.Shape.Size.X + _growth_amt;
                    if(_new_len > this.Length)
                    {
                        this.Shape.Size = new Vector2f(this.Length, this.Width);
                        this.IsOpening = false;
                        this.IsOpen = true;
                        this.Opened?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        this.Shape.Size = new Vector2f(_new_len, this.Shape.Size.Y);
                    }
                }
                else if(this.IsClosing)
                {
                    var _new_len = this.Shape.Size.X - _growth_amt;
                    if(_new_len < 0)
                    {
                        this.Shape.Size = new Vector2f(0, this.Width);
                        this.Visible = false;
                        this.IsClosing = false;
                        this.IsClosed = true;
                        this.Closed?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        this.Shape.Size = new Vector2f(_new_len, this.Shape.Size.Y);
                    }
                }
            }
        }

        public void Open()
        {
            this.Visible = true;
            this.IsClosed = false;
            this.IsClosing = false;
            this.IsOpen = false;
            this.IsOpening = true;
        }

        public void Close()
        {
            this.Visible = true;
            this.IsClosed = false;
            this.IsOpen = false;
            this.IsOpening = false;
            this.IsClosing = true;
        }
    }
}