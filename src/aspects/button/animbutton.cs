using SFML.Graphics;
using SFML.System;

namespace Rogui
{
    public class AnimButton : BaseButton<AnimPanel, AnimLabel>, IAnimate
    {
        public event EventHandler<AnimateState>? AnimationFinished;

        public override Vector2f TrueCenter {
            get {
                // TODO somewhat works, need to account for slight difference
                var _pos = this.Body.WindowPosition - this.Body.PositionOffset;
                return _pos + (this.Body.MaxSize / 2);
            }
        }
        
        public bool StartOpen { 
            get => this.Body.StartOpen;
            set {
                this.Body.StartOpen = value;
                this.Text.StartOpen = value;
            }
        }

        public new Color? BorderColor => this.Body.BorderColor;
        
        public AnimateState State {
            get => this.Body.State;
            set => this.Body.State = value;
        }

        public AnimateDirection AnimDirection {
            get => this.Body.AnimDirection;
            set {
                this.Body.AnimDirection = value;
                this.Text.AnimDirection = value;
            }
        }

        public virtual float AnimSpeed {
            get => this.Body.AnimSpeed;
            set {
                this.Body.AnimSpeed = value;
                this.Text.AnimSpeed = value;
            }
        }
        
        public AnimButton(string? description = null) : base(description)
        {
            this.Text.AnimationFinished += this.HandleAnimation;
            this.Body.AnimationFinished += this.HandleAnimation;
            this.Text.AnimSpeed = 1;
            this.Body.AnimSpeed = 1;
        }

        public void Open() 
        { 
            this.Body.Open(true); 
            this.Text.Open();
        }

        public void Close() 
        { 
            this.Body.Close(true);
            this.Text.Close(); 
        }

        public void HandleAnimation(object? sender, AnimateState state)
        {
            var _txt_match = this.Text.State == state;
            var _bdy_match = this.Body.State == state;
            var _anim_finished = _txt_match == _bdy_match;
            if(_anim_finished)
            {
                this.AnimationFinished?.Invoke(this, state);
            }
        }
    }
}