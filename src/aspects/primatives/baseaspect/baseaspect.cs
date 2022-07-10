using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Rogui.Extensions;

namespace Rogui.Primitives
{
    public abstract partial class BaseAspect : Drawable
    {
        // common aspect properties
        public virtual Aspect? Parent { get; set; }
        public virtual FloatRect Bounds { get; }

        public virtual void Update(float? ms) { }
        public virtual void Draw(RenderTarget t, RenderStates s) { }
    }
}