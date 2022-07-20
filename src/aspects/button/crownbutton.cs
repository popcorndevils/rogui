using SFML.Graphics;

namespace Rogui
{
    public class CrownButton : AnimButton, IAnimate
    {
        private VBox Buttons = new VBox();

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

        protected override void UpdateLayout()
        {
            base.UpdateLayout();
            var _origin = this.TrueCenter;
            foreach(Aspect a in this.Buttons.Children)
            {
                if(a is LineButton c)
                {
                    c.PointStart = _origin;
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