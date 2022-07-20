using SFML.System;
using SFML.Graphics;

namespace Rogui
{
    public class Label : BaseLabel
    {

        public Label()
        {
            this.Font = new Font("./res/fonts/consola.ttf");
            this.TextShape = new Text("", this.Font, 24);
        }

        public Label(string text)
        {
            this.Font = new Font("./res/fonts/consola.ttf");
            this.TextShape = new Text(text, this.Font, 24);
        }
    }
}