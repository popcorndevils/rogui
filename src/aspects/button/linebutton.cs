using SFML.Graphics;
using SFML.System;
using Rogui.Shapes;

namespace Rogui
{
    public class LineButton : BaseButton<AnimPanel>
    {
        public event EventHandler<AnimState>? AnimationFinished;
        
        private AnimLine Line;

        public bool IsOpen => this.Body.IsOpen;
        public bool IsClosed => this.Body.IsClosed;
        public bool IsClosing => this.Body.IsClosing;
        public bool IsOpening => this.Body.IsOpening;

        public AnimDirection AnimDirection {
            get => this.Body.AnimDirection;
            set => this.Body.AnimDirection = value;
        }

        public float AnimSpeed {
            get => this._AnimSpeed;
            set {
                this._AnimSpeed = value;
                this.Body.AnimSpeed = value / 2;
                this.Line.AnimSpeed = value / 2;
            }
        }
        
        public LineButton(string description, Vector2f start, Vector2f end, float width = 1) : base(description)
        {
            this.Line = new AnimLine(start, end, width);
            this.Insert(0, this.Line);
            this.Line.AnimationFinished += this.HandleLineAnimation;
            this.Body.AnimationFinished += this.HandleBodyAnimation;
        }

        public void Open() { this.Line.Open(); }
        public void Close() { this.Body.Close(); }

        public void HandleAnimationFinished(object? sender, AnimState state)
        {
            this.AnimationFinished?.Invoke(this, state);
        }

        protected override void UpdateLayout()
        {          
            if(this.Line is not null && this.Body is not null)
            {
                var _origin = this.Line.PointEnd - this.Body.MarginPosition - this.Body.BorderPosition;
                switch(this.AnimDirection)
                {
                    case AnimDirection.TOP_LEFT:
                        this.Body.AbsolutePosition = _origin;
                        break;
                    case AnimDirection.CENTER:
                        this.Body.AbsolutePosition = _origin - this.Body.Size / 2;
                        break;
                }
            }
        }

        protected override void HandleBodyState(object? sender, EventArgs e)
        {
            if(this.Theme is not null)
            {
                if(this.Body.Pressed && this.Theme.Pressed is not null)
                {
                    this.DisplayTheme = this.Theme.Pressed;
                    this.Line.FillColor = this.Theme.Pressed.BorderColor;
                }
                else if(this.Body.Hover && this.Theme.Hover is not null)
                {
                    this.DisplayTheme = this.Theme.Hover;
                    this.Line.FillColor = this.Theme.Hover.BorderColor;
                }
                else if(this.Theme.Normal is not null)
                {
                    this.DisplayTheme = this.Theme.Normal;
                    this.Line.FillColor = this.Theme.Normal.BorderColor;
                }
                
            }
        }

        public void HandleLineAnimation(object? sender, AnimState state)
        {
            switch(state)
            {
                case AnimState.OPEN:
                    this.Body.Open();
                    break;
                case AnimState.CLOSED:
                    this.AnimationFinished?.Invoke(this, AnimState.CLOSED);
                    break;
            }
        }

        public void HandleBodyAnimation(object? sender, AnimState state)
        {
            switch(state)
            {
                case AnimState.OPEN:
                    this.AnimationFinished?.Invoke(this, AnimState.OPEN);
                    break;
                case AnimState.CLOSED:
                    this.Line.Close();
                    break;
            }
        }


        // ██╗  ██╗██╗██████╗ ██████╗ ███████╗███╗   ██╗
        // ██║  ██║██║██╔══██╗██╔══██╗██╔════╝████╗  ██║
        // ███████║██║██║  ██║██║  ██║█████╗  ██╔██╗ ██║
        // ██╔══██║██║██║  ██║██║  ██║██╔══╝  ██║╚██╗██║
        // ██║  ██║██║██████╔╝██████╔╝███████╗██║ ╚████║
        // ╚═╝  ╚═╝╚═╝╚═════╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝
        private float _AnimSpeed;
    }
}