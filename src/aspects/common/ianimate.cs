
namespace Rogui
{
    public interface IAnimate
    {
        public event EventHandler<AnimState>? AnimationFinished;

        public AnimState State { get; set; }
        public AnimDirection AnimDirection { get; set; }
        public float AnimSpeed { get; set; }
        public bool StartOpen { get; set; }


        public void Open();
        public void Close();
        public void Toggle()
        {
            if(this.State == AnimState.OPEN || this.State == AnimState.OPENING)
            {
                this.Close();
            }
            else
            {
                this.Open();
            }
        }

        public void UpdateAnimations(float? ms)
        {
            if(ms is not null)
            {
                switch(this.State)
                {
                    case AnimState.OPENING:
                        this.AnimOpen((float)ms);
                        this.AdjustOffset();
                        break;
                    case AnimState.CLOSING:
                        this.AnimClose((float)ms);
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
    }
}