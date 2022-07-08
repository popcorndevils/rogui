using SFML.Graphics;
using SFML.Window;

namespace Rogui
{
    public class UIController : Aspect
    {
        private RenderWindow? _Window;
        private Container MainUI = new Container();

        public void Add(params Aspect[] aspects)
        {
            this.MainUI.Add(aspects);
        }

        public override void Update(float? ms)
        {
            this.MainUI.Update(ms);
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            this.MainUI.Draw(t, s);
        }

        public void OnKeyPressed(object? sender, EventArgs e)
        {
            this.MainUI.ProcessKey(sender, e);
        }

        public void OnMouseMoved(object? sender, MouseMoveEventArgs e)
        {
            this.MainUI.ProcessMouseMove(sender, e);
        }

        public void OnMouseButtonPressed(object? sender, MouseButtonEventArgs e)
        {
            this.MainUI.ProcessMousePress(sender, e);
        }

        public void OnMouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            this.MainUI.ProcessMouseRelease(sender, e);
        }
    }
}