
namespace Rogui
{
    public enum AnimateDirection
    {
        CENTER, LEFT, TOP_LEFT, TOP, TOP_RIGHT, RIGHT, BOTTOM_RIGHT, BOTTOM, BOTTOM_LEFT
    }

    public enum AnimateState
    {
        OPEN, CLOSED, OPENING, CLOSING, CYCLE
    }

    public interface IAnimate
    {
        public event EventHandler<AnimateState>? AnimationFinished;

        public AnimateState State { get; set; }
        public AnimateDirection AnimDirection { get; set; }
        public float AnimSpeed { get; set; }
        public bool StartOpen { get; set; }


        public void Open();
        public void Close();
        public void Toggle()
        {
            if(this.State == AnimateState.OPEN || this.State == AnimateState.OPENING)
            {
                this.Close();
            }
            else
            {
                this.Open();
            }
        }

        public void Animate(float? ms)
        {
            if(ms is not null)
            {
                switch(this.State)
                {
                    case AnimateState.OPENING:
                        this.AnimOpen((float)ms);
                        this.AdjustOffset();
                        break;
                    case AnimateState.CLOSING:
                        this.AnimClose((float)ms);
                        this.AdjustOffset();
                        break;
                    case AnimateState.CYCLE:
                        this.AnimCycle((float)ms);
                        this.AdjustOffset();
                        break;
                }
            }
        }

        /// <summary>
        /// Run after animations are calculated to reposition elements.
        /// </summary>
        public void AdjustOffset() 
        {
            throw new NotImplementedException(
                $"IAnimate.AdjustOffset not implemented for type {this.GetType().ToString()}");
        }

        /// <summary>
        /// Update elements during opening animation;
        /// </summary>
        /// <param name="ms">Delta time from last frame in milliseconds.</param>
        public void AnimOpen(float ms)
        {
            throw new NotImplementedException(
                $"IAnimate.AnimOpen not implemented for type {this.GetType().ToString()}");
        }

        /// <summary>
        /// Update elements during closing animation;
        /// </summary>
        /// <param name="ms">Delta time from last frame in milliseconds.</param>
        public void AnimClose(float ms)
        {
            throw new NotImplementedException(
                $"IAnimate.AnimClose not implemented for type {this.GetType().ToString()}");
        }

        /// <summary>
        /// Update elements during cycle animation;
        /// </summary>
        /// <param name="ms">Delta time from last frame in milliseconds.</param>
        public void AnimCycle(float ms)
        {
            throw new NotImplementedException(
                $"IAnimate.AnimClose not implemented for type {this.GetType().ToString()}");
        }
    }
}