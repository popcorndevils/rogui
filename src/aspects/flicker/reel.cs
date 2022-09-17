using SFML.Graphics;

namespace Rogui
{
    public class Reel
    {
        private Sprite[]? Frames;

        public Reel(params Sprite[] frames)
        {
            this.Frames = frames;
        }
    }
}