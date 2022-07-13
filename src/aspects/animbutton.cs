
namespace Rogui
{
    public class AnimButton : Button
    {
        public new AnimPanel BtnBody { get; set; }

        public event EventHandler Opened {
            add {
                this.BtnBody.Opened += value;
            }
            remove {
                this.BtnBody.Opened -= value;
            }
        }

        public event EventHandler Closed {
            add {
                this.BtnBody.Closed += value;
            }
            remove {
                this.BtnBody.Closed -= value;
            }
        }

        public bool IsOpen => this.BtnBody.IsOpen;
        public bool IsClosed => this.BtnBody.IsClosed;
        public bool IsOpening => this.BtnBody.IsOpening;
        public bool IsClosing => this.BtnBody.IsClosing;

        public AnimButton(string text) : base(text) 
        {
            this.BtnBody = new AnimPanel();
            // TODO edit animated panel in order to close and open button.
        }

        public void Open()
        {
            this.BtnBody.Open();
        }

        public void Close()
        {
            this.BtnBody.Close();
        }
    }
}