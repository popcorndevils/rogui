using SFML.Graphics;

namespace Rogui
{
    public enum CrownDisplaySide {
        LEFT, TOP_LEFT, TOP, TOP_RIGHT, RIGHT, BOTTOM_RIGHT, BOTTOM, BOTTOM_LEFT
    }

    public class CrownButton<T> : AnimButton, IAnimate
    where T : BaseBox, new()
    {
        private T Buttons = new T();

        public CrownButton(string description) :
        base(description)
        {
            this.OnClick += this.HandleClick;
            this.Add(this.Buttons);
        }

        public void AddButtons(params Aspect[] buttons)
        {
            foreach(Aspect a in buttons)
            {
                if(a is LineButton c)
                {
                    this.Buttons.Add(c);
                }
            }
        }

        public void HandleClick(object? sender, EventArgs e)
        {
            foreach(Aspect a in this.Buttons.Children)
            {
                if(a is IAnimate c)
                {
                    c.Toggle();
                }
            }
        }

        protected override void UpdateLayout()
        {
            base.UpdateLayout();
            // TODO Logic to offset buttons container
            this.Buttons.AbsolutePosition += new SFML.System.Vector2f(500, 500);
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            if(this.Visible)
            {
                foreach(Aspect a in this.Buttons.Children)
                {
                    if(a is LineButton b)
                    {
                        b.Line.Draw(t, s);
                    }
                }
                this.Body.Draw(t, s);
                foreach(Aspect a in this.Buttons.Children)
                {
                    if(a is LineButton b)
                    {
                        b.Button.Draw(t, s);
                    }
                }
            }
        }
    }
}