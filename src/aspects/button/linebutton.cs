using SFML.System;
using Rogui.Shapes;
using Rogui.Themes;

namespace Rogui
{
    public class LineButton : Aspect, IAnimate
    {
        public event EventHandler<AnimateState>? AnimationFinished;

        public AnimLine Line;
        public AnimButton Button;

        public Vector2f ButtonMarginPos => this.Button.Body.MarginPosition;
        public Vector2f ButtonSize => this.Button.Body.Size;

        public override Vector2f AbsolutePosition {
            get => this.PointEnd; 
            set => this.PointEnd = value;
        }

        public float AnimSpeed {
            get => this.Button.AnimSpeed + this.Line.AnimSpeed;
            set {
                this.Button.AnimSpeed = value / 2;
                this.Line.AnimSpeed = value / 2;
            }
        }

        public new ThemeButton? Theme {
            get => this.Button.Theme;
            set => this.Button.Theme = value;
        }

        public Vector2f PointStart {
            get => this.Line.PointStart;
            set => this.Line.PointStart = value;
        }

        public Vector2f PointEnd {
            get => this.Line.PointEnd;
            set => this.Line.PointEnd = value;
        }

        public AnimateState State { 
            get {
                if(this.Line.State == AnimateState.OPEN)
                {
                    return this.Button.State;
                }
                else
                {
                    return this.Line.State;
                }
            }
            set {}
        }

        public AnimateDirection AnimDirection {
            get => this.Button.AnimDirection;
            set {
                this.Button.AnimDirection = value;
                this.Line.AnimDirection = value;
            }
        }
        
        public bool StartOpen {
            get => this.Button.StartOpen;
            set {
                this.Button.StartOpen = value;
                this.Line.StartOpen = value;
            }
        }
        
        public LineButton(string description, Vector2f start, Vector2f end, float width = 1) : base()
        {
            this.Line = new AnimLine(start, end, width);
            this.Button = new AnimButton(description);
            this.Add(this.Line, this.Button);
            this.Button.AnimationFinished += this.HandleAnimation;
            this.Line.AnimationFinished += this.HandleAnimation;
            this.Button.StateChanged += this.HandleButtonState;
        }

        public void Open()
        {
            this.Line.Open();
        }

        public void Close()
        {
            this.Button.Close();
        }

        protected override void UpdateLayout()
        {          
            if(this.Parent is not null)
            {
                this.PointStart = this.Parent.TrueCenter;
            }
            var _origin = this.Line.PointEnd - this.Button.Body.MarginPosition;
            switch(this.AnimDirection)
            {
                case AnimateDirection.TOP_LEFT:
                    this.Button.AbsolutePosition = _origin;
                    break;
                case AnimateDirection.CENTER:
                    this.Button.AbsolutePosition = _origin - (this.ButtonSize / 2);
                    break;
            }
        }

        public void HandleButtonState(object? sender, EventArgs e)
        {
            
            this.Line.FillColor = this.Button.BorderColor;
        }

        public void HandleAnimation(object? sender, AnimateState state)
        {
            if(sender is AnimButton a)
            {
                switch(state)
                {
                    case AnimateState.OPEN:
                        this.AnimationFinished?.Invoke(this, state);
                        break;
                    case AnimateState.CLOSED:
                        this.Line.Close();
                        break;
                }
            }
            else
            {
                switch(state)
                {
                    case AnimateState.OPEN:
                        this.Button.Open();
                        break;
                    case AnimateState.CLOSED:
                        this.AnimationFinished?.Invoke(this, state);
                        break;
                }
            }
        }
    }
}