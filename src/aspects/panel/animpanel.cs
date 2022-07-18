using SFML.System;

namespace Rogui
{
    // TODO change to inherit from Aspect and just use Panel as container?
    public class AnimPanel : Panel
    {
        
        // ███████╗██╗   ██╗███████╗███╗   ██╗████████╗███████╗
        // ██╔════╝██║   ██║██╔════╝████╗  ██║╚══██╔══╝██╔════╝
        // █████╗  ██║   ██║█████╗  ██╔██╗ ██║   ██║   ███████╗
        // ██╔══╝  ╚██╗ ██╔╝██╔══╝  ██║╚██╗██║   ██║   ╚════██║
        // ███████╗ ╚████╔╝ ███████╗██║ ╚████║   ██║   ███████║
        // ╚══════╝  ╚═══╝  ╚══════╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝
        public event EventHandler<AnimState>? AnimationFinished;


        // ██████╗ ██████╗  ██████╗ ██████╗ ███████╗██████╗ ████████╗██╗███████╗███████╗
        // ██╔══██╗██╔══██╗██╔═══██╗██╔══██╗██╔════╝██╔══██╗╚══██╔══╝██║██╔════╝██╔════╝
        // ██████╔╝██████╔╝██║   ██║██████╔╝█████╗  ██████╔╝   ██║   ██║█████╗  ███████╗
        // ██╔═══╝ ██╔══██╗██║   ██║██╔═══╝ ██╔══╝  ██╔══██╗   ██║   ██║██╔══╝  ╚════██║
        // ██║     ██║  ██║╚██████╔╝██║     ███████╗██║  ██║   ██║   ██║███████╗███████║
        // ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝╚══════╝╚══════╝
        public bool StartOpen { 
            get => this._StartOpen;
            set {
                this._StartOpen = value;
                if(value)
                {
                    this.Size = this.ContentSize;   
                    this.CurrentSize = this.Size;
                    this.Visible = true;
                    this.IsOpen = true;
                    this.Contents.Visible = true;
                }
            }
        }
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

        public float AnimSpeed {
            get => this._AnimSpeed;
            set {
                this._AnimSpeed = value;
                this._MSGrowth = this.MaxSize / value / 1000;
            }
        }

        public Vector2f CurrentSize {
            get => base.Size;
            set => base.Size = value;
        }

        public override Vector2f Size {
            get => this.MaxSize;
            set {
                this.MaxSize = value;
                this._MSGrowth = value / this.AnimSpeed / 1000;
            }
        }


        //  ██████╗ ██████╗ ███╗   ██╗███████╗████████╗██████╗ ██╗   ██╗ ██████╗████████╗
        // ██╔════╝██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔══██╗██║   ██║██╔════╝╚══██╔══╝
        // ██║     ██║   ██║██╔██╗ ██║███████╗   ██║   ██████╔╝██║   ██║██║        ██║   
        // ██║     ██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══██╗██║   ██║██║        ██║   
        // ╚██████╗╚██████╔╝██║ ╚████║███████║   ██║   ██║  ██║╚██████╔╝╚██████╗   ██║   
        //  ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝  ╚═════╝   ╚═╝   
        public AnimPanel() : base() { this.Init(); }
        public AnimPanel(params Aspect[] aspects) : base() { this.Init(aspects); }
        private void Init(params Aspect[] aspects)
        {
            this.AnimSpeed = 1f;
            this.Add(aspects);
            this.CurrentSize = new Vector2f();
            this.Visible = false;
            this.IsOpen = false;
            this.Contents.Visible = false;
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
            this.Visible = true;
            this.Contents.Visible = false;
            this.IsClosed = false;
            this.IsClosing = false;
            this.IsOpen = false;
            this.IsOpening = true;
        }

        public void Close()
        {
            this.Visible = true;
            this.Contents.Visible = false;
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
                this.OffsetPosition = new Vector2f(0, 0);
                this.AnimationFinished?.Invoke(this, AnimState.OPEN);
                this.Contents.Visible = true;
            }
            else
            {
                this.CurrentSize = _new_size;
                switch(this.AnimDirection)
                {
                    case AnimDirection.CENTER:
                        this.OffsetPosition = (this.MaxSize / 2) + (_new_size / -2);
                        break;
                }
                
            }
        }

        private void AnimClose(Vector2f shr_amt)
        {
            var _new_size = this.CurrentSize - shr_amt;
            if(_new_size.X < 0 || _new_size.Y < 0)
            {
                this.CurrentSize = new Vector2f();
                this.Visible = false;
                this.IsClosing = false;
                this.IsClosed = true;
                this.AnimationFinished?.Invoke(this, AnimState.CLOSED);
            }
            else
            {
                this.CurrentSize = _new_size;
                switch(this.AnimDirection)
                {
                    case AnimDirection.CENTER:
                        this.OffsetPosition = (this.MaxSize / 2) + (_new_size / -2);
                        break;
                }
            }
        }

        public override void OnTransformed(object? sender, EventArgs e)
        {
            this.Size = this.ContentSize;
        }

        // ██╗  ██╗██╗██████╗ ██████╗ ███████╗███╗   ██╗
        // ██║  ██║██║██╔══██╗██╔══██╗██╔════╝████╗  ██║
        // ███████║██║██║  ██║██║  ██║█████╗  ██╔██╗ ██║
        // ██╔══██║██║██║  ██║██║  ██║██╔══╝  ██║╚██╗██║
        // ██║  ██║██║██████╔╝██████╔╝███████╗██║ ╚████║
        // ╚═╝  ╚═╝╚═╝╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝
        private float _AnimSpeed;
        private Vector2f _MSGrowth;
        private AnimDirection _AnimDirection;
        private bool _StartOpen;
    }
}