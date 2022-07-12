using SFML.System;
using Rogui.Extensions;

namespace Rogui.Shapes
{
    public class AnimLine : Line
    {
        // ███████╗██╗   ██╗███████╗███╗   ██╗████████╗███████╗
        // ██╔════╝██║   ██║██╔════╝████╗  ██║╚══██╔══╝██╔════╝
        // █████╗  ██║   ██║█████╗  ██╔██╗ ██║   ██║   ███████╗
        // ██╔══╝  ╚██╗ ██╔╝██╔══╝  ██║╚██╗██║   ██║   ╚════██║
        // ███████╗ ╚████╔╝ ███████╗██║ ╚████║   ██║   ███████║
        // ╚══════╝  ╚═══╝  ╚══════╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝
        public event EventHandler? Closed;
        public event EventHandler? Opened;


        // ██████╗ ██████╗  ██████╗ ██████╗ ███████╗██████╗ ████████╗██╗███████╗███████╗
        // ██╔══██╗██╔══██╗██╔═══██╗██╔══██╗██╔════╝██╔══██╗╚══██╔══╝██║██╔════╝██╔════╝
        // ██████╔╝██████╔╝██║   ██║██████╔╝█████╗  ██████╔╝   ██║   ██║█████╗  ███████╗
        // ██╔═══╝ ██╔══██╗██║   ██║██╔═══╝ ██╔══╝  ██╔══██╗   ██║   ██║██╔══╝  ╚════██║
        // ██║     ██║  ██║╚██████╔╝██║     ███████╗██║  ██║   ██║   ██║███████╗███████║
        // ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝╚══════╝╚══════╝
        public bool IsOpen { get; private set; }
        public bool IsClosed {get; private set; }
        public bool IsClosing { get; private set; }
        public bool IsOpening { get; private set; }

        public float MaxLength {  
            get => this._MaxLength;
            private set {
                this._MaxLength = value;
                this._MSGrowth = value / this.AnimSpeed / 1000;
            }
        }

        public float AnimSpeed {
            get => this._AnimSpeed;
            set {
                this._AnimSpeed = value;
                this._MSGrowth = this.MaxLength / value / 1000;
            }
        }


        //  ██████╗ ██████╗ ███╗   ██╗███████╗████████╗██████╗ ██╗   ██╗ ██████╗████████╗
        // ██╔════╝██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔══██╗██║   ██║██╔════╝╚══██╔══╝
        // ██║     ██║   ██║██╔██╗ ██║███████╗   ██║   ██████╔╝██║   ██║██║        ██║   
        // ██║     ██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══██╗██║   ██║██║        ██║   
        // ╚██████╗╚██████╔╝██║ ╚████║███████║   ██║   ██║  ██║╚██████╔╝╚██████╗   ██║   
        //  ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝  ╚═════╝   ╚═╝   
        public AnimLine(Vector2f start, Vector2f end, float width = 1) :
        base(start, end, width) 
        {
            this.Size = new Vector2f(0, width);
            this.AnimSpeed = 1f;
        }

        public AnimLine(float x1, float y1, float x2, float y2, float width = 1) :
        base(x1, y1, x2, y2, width)
        {
            this.MaxLength = this.PointStart.GetDistanceTo(this.PointEnd);
            this.Size = new Vector2f(0, width);
            this.AnimSpeed = 1f;
        }


        //  ██████╗ ██╗   ██╗███████╗██████╗ ██████╗ ██╗██████╗ ███████╗███████╗
        // ██╔═══██╗██║   ██║██╔════╝██╔══██╗██╔══██╗██║██╔══██╗██╔════╝██╔════╝
        // ██║   ██║██║   ██║█████╗  ██████╔╝██████╔╝██║██║  ██║█████╗  ███████╗
        // ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗██╔══██╗██║██║  ██║██╔══╝  ╚════██║
        // ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║██║  ██║██║██████╔╝███████╗███████║
        //  ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝╚═════╝ ╚══════╝╚══════╝
        public override void Update(float? ms)
        {
            if(ms is not null)
            {
                float _growth_amt = this._MSGrowth * (float)ms;
                if(this.IsOpening)
                {
                    var _new_len = this.Length + _growth_amt;
                    if(_new_len > this.MaxLength)
                    {
                        this.Length = this.MaxLength;
                        this.IsOpening = false;
                        this.IsOpen = true;
                        this.Opened?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        this.Length = _new_len;
                    }
                }
                else if(this.IsClosing)
                {
                    var _new_len = this.Length - _growth_amt;
                    if(_new_len < 0)
                    {
                        this.Length = 0;
                        this.Visible = false;
                        this.IsClosing = false;
                        this.IsClosed = true;
                        this.Closed?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        this.Length = _new_len;
                    }
                }
            }
        }

        protected override void CalculateDimensions()
        {
            this.MaxLength = this.PointStart.GetDistanceTo(this.PointEnd);
            if(this.IsOpen)
            {
                this.Length = this.MaxLength;
            }
            this.Origin = new Vector2f(0, this.Width / 2);
            this.Rotation = this.PointStart.GetAngleTo(this.PointEnd);
        }


        // ███╗   ███╗███████╗████████╗██╗  ██╗ ██████╗ ██████╗ ███████╗
        // ████╗ ████║██╔════╝╚══██╔══╝██║  ██║██╔═══██╗██╔══██╗██╔════╝
        // ██╔████╔██║█████╗     ██║   ███████║██║   ██║██║  ██║███████╗
        // ██║╚██╔╝██║██╔══╝     ██║   ██╔══██║██║   ██║██║  ██║╚════██║
        // ██║ ╚═╝ ██║███████╗   ██║   ██║  ██║╚██████╔╝██████╔╝███████║
        // ╚═╝     ╚═╝╚══════╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ╚══════╝
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

        // ██╗  ██╗██╗██████╗ ██████╗ ███████╗███╗   ██╗
        // ██║  ██║██║██╔══██╗██╔══██╗██╔════╝████╗  ██║
        // ███████║██║██║  ██║██║  ██║█████╗  ██╔██╗ ██║
        // ██╔══██║██║██║  ██║██║  ██║██╔══╝  ██║╚██╗██║
        // ██║  ██║██║██████╔╝██████╔╝███████╗██║ ╚████║
        // ╚═╝  ╚═╝╚═╝╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝
        private float _MaxLength;
        private float _AnimSpeed;
        private float _MSGrowth;
    }
}