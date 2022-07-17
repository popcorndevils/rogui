namespace Rogui
{
    public class AnimButton : BaseButton<AnimPanel>
    {
        public event EventHandler<AnimState>? AnimationFinished;
        
        public bool IsOpen => this.Body.IsOpen;
        public bool IsClosed => this.Body.IsClosed;
        public bool IsClosing => this.Body.IsClosing;
        public bool IsOpening => this.Body.IsOpening;
        
        public AnimButton(string? description = null) : base(description)
        {
            this.Body.AnimationFinished += this.HandleAnimationFinished;
        }

        public void Open() { this.Body.Open(); }
        public void Close() { this.Body.Close(); }

        public void HandleAnimationFinished(object? sender, AnimState state)
        {
            this.AnimationFinished?.Invoke(this, state);
        }
    }
}