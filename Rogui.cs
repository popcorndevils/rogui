using SFML.Graphics;
using Rogui.Containers;

namespace Rogui
{
    public class UIController : Drawable
    {
        private RenderWindow? _Window;
        private Container MainUI = new Container();

        public void Add(params Aspect[] aspects)
        {
            this.MainUI.Add(aspects);
        }

        public void Update(float? ms)
        {
            this.MainUI.Update(ms);
        }

        public void Draw(RenderTarget t, RenderStates s)
        {
            this.MainUI.Draw(t, s);
        }

        public void OnKeyPressed(object? sender, EventArgs e)
        {
            
        }

        public void OnWindowClosed(object? sender, EventArgs e)
        {

        }
    }
}