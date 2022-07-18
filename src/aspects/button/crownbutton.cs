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
            var _origin = this.Body.AbsolutePosition + (this.Body.Size / 2);
            foreach(Aspect a in this.Buttons.Children)
            {
                if(a is LineButton b)
                {
                    b.PointStart =_origin;
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
    }
}