
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
                        break;
                    case AnimState.CLOSING:
                        this.AnimClose((float)ms);
                        break;
                }
            }
        }

        public void AnimOpen(float ms)
        {
            throw new NotImplementedException(
                $"IAnimate.AnimOpen not implemented for type {this.GetType().ToString()}");
        }
        public void AnimClose(float ms)
        {
            throw new NotImplementedException(
                $"IAnimate.AnimClose not implemented for type {this.GetType().ToString()}");
        }
    }
}