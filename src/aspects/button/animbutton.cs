namespace Rogui
{
    public class AnimButton : BaseButton<AnimPanel>
    {
        public event EventHandler? Opened;
        public event EventHandler? Closed;
        
        public bool IsOpen => this.Body.IsOpen;
        public bool IsClosed => this.Body.IsClosed;
        public bool IsClosing => this.Body.IsClosing;
        public bool IsOpening => this.Body.IsOpening;
        
        public AnimButton(string? description = null) : base(description) { }

        public void Open() { this.Body.Open(); }
        public void Close() { this.Body.Close(); }

        public void HandlePanelOpened(object? sender, EventArgs e)
            { this.Opened?.Invoke(this, EventArgs.Empty); }

        public void HandlePanelClosed(object? sender, EventArgs e)
            { this.Closed?.Invoke(this, EventArgs.Empty); }
    }
}