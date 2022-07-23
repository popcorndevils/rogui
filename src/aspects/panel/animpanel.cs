using SFML.System;

namespace Rogui
{
    // TODO change to inherit from Aspect and just use Panel as container?
    public class AnimPanel : Panel, IAnimate
    {
        
        // ███████╗██╗   ██╗███████╗███╗   ██╗████████╗███████╗
        // ██╔════╝██║   ██║██╔════╝████╗  ██║╚══██╔══╝██╔════╝
        // █████╗  ██║   ██║█████╗  ██╔██╗ ██║   ██║   ███████╗
        // ██╔══╝  ╚██╗ ██╔╝██╔══╝  ██║╚██╗██║   ██║   ╚════██║
        // ███████╗ ╚████╔╝ ███████╗██║ ╚████║   ██║   ███████║
        // ╚══════╝  ╚═══╝  ╚══════╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝
        public event EventHandler<AnimateState>? AnimationFinished;


        // ██████╗ ██████╗  ██████╗ ██████╗ ███████╗██████╗ ████████╗██╗███████╗███████╗
        // ██╔══██╗██╔══██╗██╔═══██╗██╔══██╗██╔════╝██╔══██╗╚══██╔══╝██║██╔════╝██╔════╝
        // ██████╔╝██████╔╝██║   ██║██████╔╝█████╗  ██████╔╝   ██║   ██║█████╗  ███████╗
        // ██╔═══╝ ██╔══██╗██║   ██║██╔═══╝ ██╔══╝  ██╔══██╗   ██║   ██║██╔══╝  ╚════██║
        // ██║     ██║  ██║╚██████╔╝██║     ███████╗██║  ██║   ██║   ██║███████╗███████║
        // ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝╚══════╝╚══════╝
        public AnimateState State { get; set; }

        public bool StartOpen { 
            get => this._StartOpen;
            set {
                this._StartOpen = value;
                if(value)
                {
                    this.Size = this.ContentSize;   
                    this.CurrentSize = this.Size;
                    this.Visible = true;
                    this.Contents.Visible = true;
                    this.State = AnimateState.OPEN;
                }
            }
        }

        public AnimateDirection AnimDirection {
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
            this.State = AnimateState.CLOSED;
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
                ((IAnimate)this).UpdateAnimations((float)ms);
            }
            base.Update(ms);
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
            this.State = AnimateState.OPENING;
        }

        public void Open(bool visible_contents = false)
        {
            this.Visible = true;
            this.Contents.Visible = visible_contents;
            this.State = AnimateState.OPENING;
        }

        public void Close()
        {
            this.Visible = true;
            this.Contents.Visible = false;
            this.State = AnimateState.CLOSING;
        }

        public void Close(bool visible_contents = false)
        {
            this.Visible = true;
            this.Contents.Visible = visible_contents;
            this.State = AnimateState.CLOSING;
        }

        public void AdjustOffset()
        {
            switch(this.AnimDirection)
            {
                case AnimateDirection.CENTER:
                    this.OffsetPosition = (this.MaxSize / 2) + (this.CurrentSize / -2);
                    break;
            }
            this.Contents.OffsetPosition = new Vector2f(0, 0) - this.OffsetPosition;
        }

        public void AnimOpen(float ms)
        {
            Vector2f chg_amt = this._MSGrowth * (float)ms;
            var _new_size = this.CurrentSize + chg_amt;
            if(_new_size.X > this.MaxSize.X || _new_size.Y > this.MaxSize.Y)
            {
                this.CurrentSize = this.MaxSize;
                this.State = AnimateState.OPEN;
                this.OffsetPosition = new Vector2f(0, 0);
                this.AnimationFinished?.Invoke(this, this.State);
                this.Contents.Visible = true;
            }
            else
            {
                this.CurrentSize = _new_size;
            }
        }

        public void AnimClose(float ms)
        {
            Vector2f chg_amt = this._MSGrowth * (float)ms;
            var _new_size = this.CurrentSize - chg_amt;
            if(_new_size.X < 0 || _new_size.Y < 0)
            {
                this.CurrentSize = new Vector2f();
                this.Visible = false;
                this.State = AnimateState.CLOSED;
                this.AnimationFinished?.Invoke(this, this.State);
            }
            else
            {
                this.CurrentSize = _new_size;
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
        private AnimateDirection _AnimDirection;
        private bool _StartOpen;
    }
}