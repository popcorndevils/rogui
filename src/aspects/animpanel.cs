using SFML.System;

namespace Rogui
{
    public class AnimPanel : Panel
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
        public AnimDirection AnimDirection {
            get => this._AnimDirection;
            set {
                this._AnimDirection = value;
            }
        }

        public Vector2f MaxSize {  
            get => this._MaxSize;
            private set {
                this._MaxSize = value;
                this._MSGrowth = value / this.AnimSpeed / 1000;
            }
        }

        public float AnimSpeed {
            get => this._AnimSpeed;
            set {
                this._AnimSpeed = value;
                this._MSGrowth = this.MaxSize / value / 1000;
            }
        }

        private Vector2f CurrentSize {
            get => base.BodyFG.Size;
            set {
                base.BodyFG.Size = value;
                this.BodyBG.Size = new Vector2f(
                    value.X + this.BorderLeft + this.BorderRight,
                    value.Y + this.BorderTop + this.BorderBottom);
            }
        }

        public override Vector2f Size {
            get => this.MaxSize;
            set {
                this.MaxSize = value;
            }
        }


        //  ██████╗ ██████╗ ███╗   ██╗███████╗████████╗██████╗ ██╗   ██╗ ██████╗████████╗
        // ██╔════╝██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔══██╗██║   ██║██╔════╝╚══██╔══╝
        // ██║     ██║   ██║██╔██╗ ██║███████╗   ██║   ██████╔╝██║   ██║██║        ██║   
        // ██║     ██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══██╗██║   ██║██║        ██║   
        // ╚██████╗╚██████╔╝██║ ╚████║███████║   ██║   ██║  ██║╚██████╔╝╚██████╗   ██║   
        //  ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝  ╚═════╝   ╚═╝   
        public AnimPanel() :
        base() 
        {
            this.CurrentSize = new Vector2f(0, 0);
            this.AnimSpeed = 1f;
            Console.WriteLine(this.AnimDirection);
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
                Vector2f chg_amt = this._MSGrowth * (float)ms;
                if(this.IsOpening)
                {
                    this.AnimOpen(chg_amt);
                }
                else if(this.IsClosing)
                {
                    this.AnimClose(chg_amt);
                }
            }
        }


        // ███╗   ███╗███████╗████████╗██╗  ██╗ ██████╗ ██████╗ ███████╗
        // ████╗ ████║██╔════╝╚══██╔══╝██║  ██║██╔═══██╗██╔══██╗██╔════╝
        // ██╔████╔██║█████╗     ██║   ███████║██║   ██║██║  ██║███████╗
        // ██║╚██╔╝██║██╔══╝     ██║   ██╔══██║██║   ██║██║  ██║╚════██║
        // ██║ ╚═╝ ██║███████╗   ██║   ██║  ██║╚██████╔╝██████╔╝███████║
        // ╚═╝     ╚═╝╚══════╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ╚══════╝
        public void Open()
        {
            Console.WriteLine("OPENING");
            this.Visible = true;
            this.IsClosed = false;
            this.IsClosing = false;
            this.IsOpen = false;
            this.IsOpening = true;
        }

        public void Close()
        {
            Console.WriteLine("OPENING");
            this.Visible = true;
            this.IsClosed = false;
            this.IsOpen = false;
            this.IsOpening = false;
            this.IsClosing = true;
        }

        private void AnimOpen(Vector2f grw_amt)
        {
            var _new_size = this.CurrentSize + grw_amt;
            if(_new_size.X > this.MaxSize.X || _new_size.Y > this.MaxSize.Y)
            {
                this.CurrentSize = this.MaxSize;
                this.IsOpening = false;
                this.IsOpen = true;
                // this.Position = new Vector2f(0, 0);
                this.Opened?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                this.CurrentSize = _new_size;
                switch(this.AnimDirection)
                {
                    case AnimDirection.CENTER:
                        break;
                }
                
            }
        }

        private void AnimClose(Vector2f shr_amt)
        {
            var _new_len = this.CurrentSize - shr_amt;
            if(_new_len.X < 0 || _new_len.Y < 0)
            {
                this.CurrentSize = new Vector2f();
                this.Visible = false;
                this.IsClosing = false;
                this.IsClosed = true;
                this.Closed?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                this.CurrentSize = _new_len;
            }
        }


        // ██╗  ██╗██╗██████╗ ██████╗ ███████╗███╗   ██╗
        // ██║  ██║██║██╔══██╗██╔══██╗██╔════╝████╗  ██║
        // ███████║██║██║  ██║██║  ██║█████╗  ██╔██╗ ██║
        // ██╔══██║██║██║  ██║██║  ██║██╔══╝  ██║╚██╗██║
        // ██║  ██║██║██████╔╝██████╔╝███████╗██║ ╚████║
        // ╚═╝  ╚═╝╚═╝╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝
        private float _AnimSpeed;
        private Vector2f _MSGrowth;
        private Vector2f _MaxSize;
        private AnimDirection _AnimDirection;
    }
}