using SFML.System;
using SFML.Graphics;
using Rogui.Extensions;

namespace Rogui
{
    public class AnimLabel : BaseLabel, IAnimate
    {
        
        // ███████╗██╗   ██╗███████╗███╗   ██╗████████╗███████╗
        // ██╔════╝██║   ██║██╔════╝████╗  ██║╚══██╔══╝██╔════╝
        // █████╗  ██║   ██║█████╗  ██╔██╗ ██║   ██║   ███████╗
        // ██╔══╝  ╚██╗ ██╔╝██╔══╝  ██║╚██╗██║   ██║   ╚════██║
        // ███████╗ ╚████╔╝ ███████╗██║ ╚████║   ██║   ███████║
        // ╚══════╝  ╚═══╝  ╚══════╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝
        public event EventHandler<AnimateState>? AnimationFinished;
        public new event EventHandler? Transformed;

        public Text BoundsText;
        public float Timer;
        public AnimateState State { get; set; }
        public AnimateDirection AnimDirection { get; set; }

        public bool StartOpen { 
            get => this._StartOpen;
            set {
                this._StartOpen = value;
                if(value)
                {
                    this.Visible = true;
                    this.CurrentString = this.DisplayedString;
                    this.State = AnimateState.OPEN;
                }
            }
        }

        public float AnimSpeed {
            get => this._AnimSpeed;
            set {
                this._AnimSpeed = value;
                if(this.DisplayedString is not null)
                {
                    this._TXTGrowth = value / this.DisplayedString.Length * 1000;
                }
                if(this.DisplayedString is not null)
                {
                    this._MVGrowth = this.BoundsText.GetGlobalBounds().GetSize() / this.DisplayedString.Length;
                }
            }
        }

        public override FloatRect Bounds {
            get {
                var _bnds = this.BoundsText.GetLocalBounds();
                var _pos = this.PositionGlobal;
                return new FloatRect(
                    _pos.X - this.MarginLeft,
                    _pos.Y - this.MarginTop,
                    _bnds.Width + (_bnds.Left * 2) + this.MarginLeft + this.MarginRight,
                    _bnds.Height + (_bnds.Top * 2) + this.MarginTop + this.MarginBottom);
            }
        } 

        public string? CurrentString {
            get {
                if(this.TextShape is not null)
                {
                    return this.TextShape.DisplayedString;
                }
                else
                {
                    return null;
                }
            }
            set {
                if(this.TextShape is not null)
                {
                    this.TextShape.DisplayedString = value;
                }
            }
        }

        public override string? DisplayedString {
            get => this.BoundsText.DisplayedString;
            set {
                this.BoundsText.DisplayedString = value;
                this.Transformed?.Invoke(this, EventArgs.Empty);
            }
        }


        //  ██████╗ ██╗   ██╗███████╗██████╗ ██████╗ ██╗██████╗ ███████╗███████╗
        // ██╔═══██╗██║   ██║██╔════╝██╔══██╗██╔══██╗██║██╔══██╗██╔════╝██╔════╝
        // ██║   ██║██║   ██║█████╗  ██████╔╝██████╔╝██║██║  ██║█████╗  ███████╗
        // ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗██╔══██╗██║██║  ██║██╔══╝  ╚════██║
        // ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║██║  ██║██║██████╔╝███████╗███████║
        //  ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝╚═════╝ ╚══════╝╚══════╝
        public override void Update(float? ms)
        {
            ((IAnimate)this).Animate(ms);
            base.Update(ms);
        }

        public AnimLabel()
        {
            this.Font = new Font("./res/fonts/consola.ttf");
            this.TextShape = new Text("", this.Font, 24);
            this.BoundsText = new Text("", this.Font, 24);
        }

        public AnimLabel(string text)
        {
            this.Font = new Font("./res/fonts/consola.ttf");
            this.TextShape = new Text("", this.Font, 24);
            this.BoundsText = new Text(text, this.Font, 24);
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
            this.State = AnimateState.OPENING;
        }

        public void Close()
        {
            this.Visible = true;
            this.State = AnimateState.CLOSING;
        }

        public void AdjustOffset()
        {
            if(this.CurrentString is not null && this.DisplayedString is not null)
            {
                var _char_diff = this.DisplayedString.Length - this.CurrentString.Length;
                switch(this.AnimDirection)
                {
                    case AnimateDirection.CENTER:
                        var _amt = new Vector2f(this._MVGrowth.X * _char_diff / 2, 0);
                        this.PositionOffset = new Vector2f() + _amt;
                        break;
                }
            }
        }

        public void AnimOpen(float ms)
        {
            if(this.DisplayedString is not null && this.CurrentString is not null)
            {
                this.Timer += ms;
                if(this.Timer >= this._TXTGrowth)
                {
                    this.Timer = 0;
                    if(this.CurrentString.Length >= this.DisplayedString.Length)
                    {
                        this.CurrentString = this.DisplayedString;
                        this.State = AnimateState.OPEN;
                        this.AnimationFinished?.Invoke(this, this.State);
                    }
                    else
                    {
                        this.CurrentString = this.DisplayedString.Substring(0, this.CurrentString.Length + 1);
                    }
                }
            }
        }

        public void AnimClose(float ms)
        {
            if(this.DisplayedString is not null && this.CurrentString is not null)
            {
                this.Timer += ms;
                if(this.Timer >= this._TXTGrowth)
                {
                    this.Timer = 0;
                    if(this.CurrentString.Length <= 0)
                    {
                        this.CurrentString = "";
                        this.State = AnimateState.CLOSED;
                        this.AnimationFinished?.Invoke(this, this.State);
                    }
                    else
                    {
                        this.CurrentString = this.CurrentString.Substring(0, this.CurrentString.Length - 1);
                    }
                }
            }
        }
                
        
        // ██╗  ██╗██╗██████╗ ██████╗ ███████╗███╗   ██╗
        // ██║  ██║██║██╔══██╗██╔══██╗██╔════╝████╗  ██║
        // ███████║██║██║  ██║██║  ██║█████╗  ██╔██╗ ██║
        // ██╔══██║██║██║  ██║██║  ██║██╔══╝  ██║╚██╗██║
        // ██║  ██║██║██████╔╝██████╔╝███████╗██║ ╚████║
        // ╚═╝  ╚═╝╚═╝╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝
        private float _AnimSpeed;
        private float _TXTGrowth;
        private Vector2f _MVGrowth;
        private bool _StartOpen;
    }
}