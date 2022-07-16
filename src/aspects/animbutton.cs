using SFML.System;

namespace Rogui
{
    public class AnimButton : Button
    {
        protected new AnimPanel Body { get; set; }

        public event EventHandler Opened {
            add {
                this.Body.Opened += value;
            }
            remove {
                this.Body.Opened -= value;
            }
        }

        public event EventHandler Closed {
            add {
                this.Body.Closed += value;
            }
            remove {
                this.Body.Closed -= value;
            }
        }

        public bool IsOpen => this.Body.IsOpen;
        public bool IsClosed => this.Body.IsClosed;
        public bool IsOpening => this.Body.IsOpening;
        public bool IsClosing => this.Body.IsClosing;

        public AnimButton(string text)
        {
            this.Body = new AnimPanel();
            this.Body.Add(new Label(text));
            this.Body.CurrentSize = new Vector2f();
            this.Body.Opened += this.HandleOpen;
            // TODO edit animated panel in order to close and open button.
        }

        public void Open()
        {
            this.Body.Open();
        }

        public void Close()
        {
            this.Body.Close();
        }

        public void HandleOpen(object? sender, EventArgs e)
        {
            Console.WriteLine("Body Opened");
        }
    }
}