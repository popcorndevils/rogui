using SFML.Graphics;
using SFML.System;

namespace Rogui
{
    public class CrownButton : AnimButton
    {
        private VBox Buttons = new VBox();

        public CrownButton(string description) :
        base(description)
        {
            this.Add(this.Buttons);
        }

        public void AddButtons(params LineButton[] buttons)
        {
            this.Buttons.Add(buttons);
            this.OnClick += this.HandleClick;
        }

        protected override void UpdateLayout()
        {
            base.UpdateLayout();
            var _origin = this.TrueCenter;
            foreach(Aspect a in this.Buttons.Children)
            {
                if(a is LineButton b)
                {
                    b.PointStart = _origin;
                }
            }
        }

        public void HandleClick(object? sender, EventArgs e)
        {
            foreach(Aspect a in this.Buttons.Children)
            {
                if(a is LineButton b)
                {
                    if(!b.IsOpening && !b.IsOpen)
                        { b.Open(); }
                    else
                        { b.Close(); }
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
                        b.Body.Draw(t, s);
                    }
                }
            }
        }
    }
}