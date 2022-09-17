using System.Collections.Generic;
using SFML.Graphics;

namespace Rogui
{
    public class Flicker : IAnimate
    {
        public event EventHandler<AnimateState>? AnimationFinished;

        public AnimateState State { get; set; }
        public float AnimSpeed { get; set; }
        public Dictionary<string, Reel> Animations;

        public string? Animation {
            get => this._Animation;
            set {
                if(value is not null && this.Animations.ContainsKey(value))
                {
                    this._Animation = value;
                }
            }
        }

        public Flicker()
        {
            this.Animations = new Dictionary<string, Reel>();
        }

        public void AddReel(string name, Reel reel)
        {
            this.Animations.Add(name, reel);
        }

        public void AddReel(string name, params Sprite[] sprites)
        {
            this.Animations.Add(name, new Reel(sprites));
        }

        private string? _Animation;
    }
}