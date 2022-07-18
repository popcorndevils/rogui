using SFML.System;
using Rogui.Shapes;
using Rogui.Themes;

namespace Rogui
{
    /// <summary>
    /// Button that is attached to an animated line.
    /// </summary>
    public class LineButton : Aspect
    {
        public event EventHandler<AnimState>? AnimationFinished;

        private AnimLine Line;
        private AnimButton Button;
        
        public bool IsOpen => this.Button.IsOpen && this.Line.IsOpen;
        public bool IsClosed => this.Button.IsClosed && this.Line.IsClosed;
        public bool IsClosing => this.Button.IsClosing || this.Line.IsClosing;
        public bool IsOpening => this.Button.IsOpening || this.Line.IsOpening;

        public new ThemeButton? Theme {
            get => this.Button.Theme;
            set => this.Button.Theme = value;
        }

        public new event EventHandler? OnClick {
            add {
                this.Button.OnClick += value;
            }
            remove {
                this.Button.OnClick -= value;
            }
        }

        public LineButton(string description, Vector2f start, Vector2f end, float width = 1)
        {
            this.Line = new AnimLine(start, end, width);
            this.Button = new AnimButton(description) {Position = end};
            this.Add(this.Line, this.Button);

            this.Line.AnimationFinished += this.HandleLine;
            this.Button.AnimationFinished += this.HandleButton;
        }

        public void Open()
        {
            this.Line.Open();
        }

        public void Close()
        {
            this.Button.Close();
        }

        public void HandleLine(object? sender, AnimState state)
        {
            switch(state)
            {
                case AnimState.OPEN:
                    this.Button.Open();
                    break;
                case AnimState.CLOSED:
                    this.AnimationFinished?.Invoke(this, AnimState.CLOSED);
                    break;
            }
        }

        public void HandleButton(object? sender, AnimState state)
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
    }
}