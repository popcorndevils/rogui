
namespace Rogui
{
    public class Flicker : IAnimate
    {
        public event EventHandler<AnimateState>? AnimationFinished;

        public AnimateState State { get; set; }
        public AnimateDirection AnimDirection { get; set; }
        public float AnimSpeed { get; set; }
    }
}