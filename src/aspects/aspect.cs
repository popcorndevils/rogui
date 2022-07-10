using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Rogui.Primitives;

namespace Rogui
{
    public abstract class Aspect : BaseAspect
    {
        public List<Aspect> Children = new List<Aspect>();

        public override Vector2f Position {
            get => base.Position;
            set {
                base.Position = new Vector2f(
                    value.X + this.MarginLeft + this.RelativePosition.X,
                    value.Y + this.MarginTop + this.RelativePosition.Y);

                foreach(Aspect c in this.Children)
                {
                    c.Position = new Vector2f
                    (this.Position.X + this.PaddingLeft,
                    this.Position.Y + this.PaddingTop);
                }
            }
        }

        public override Vector2f Size {
            get => base.Size;
            set {
                base.Size = value;
                this.UpdateLayout();
            }
        }

        public override FloatRect Bounds {
            get {
                var _x = new float[this.Children.Count];
                var _y = new float[this.Children.Count];
                var _x2 = new float[this.Children.Count];
                var _y2 = new float[this.Children.Count];

                for(int i = 0; i < this.Children.Count; i++)
                {
                    var _bnd = this.Children[i].Bounds;
                    _x[i] = _bnd.Left;
                    _y[i] = _bnd.Top;
                    _x2[i] = _bnd.Left + _bnd.Width;
                    _y2[i] = _bnd.Top + _bnd.Height;
                }

                return new FloatRect(
                    _x.Min(),
                    _y.Min(),
                    _x2.Max() - _x.Min(),
                    _y2.Max() - _y.Min());
            }
        }

        public Aspect() {}
        public Aspect(params Aspect[] aspects)
        {
            this.Add(aspects);
        }

        public void Add(params Aspect[] aspects)
        {
            foreach(Aspect a in aspects)
            {
                a.Parent = this;
                this.Children.Add(a);
            }
            this.UpdateLayout();
        }

        protected virtual void UpdateLayout()
        { 
            this.Position = this.Position;
        }

        public override void Update(float? ms)
        { 
            foreach(Aspect a in this.Children)
            {
                a.Update(ms);
            }
        }

        public override void Draw(RenderTarget t, RenderStates s)
        {
            if(this.Visible)
            {
                foreach(Aspect a in this.Children)
                {
                    a.Draw(t, s);
                }
            }
        }

        public override EventArgs? ProcessKey(
            object? sender, EventArgs? e)
        {
            EventArgs? prev = e;
            foreach(Aspect a in this.Children)
            {   
                prev = a.ProcessKey(sender, e);
            }
            return base.ProcessKey(sender, e);
        }

        public override MouseMoveEventArgs? ProcessMouseMove(
            object? sender, MouseMoveEventArgs? e)
        {
            MouseMoveEventArgs? prev = e;
            for(int i = this.Children.Count - 1; i >= 0; i--)
            {
                Aspect a = this.Children[i];
                prev = a.ProcessMouseMove(sender, prev);
            }
            return base.ProcessMouseMove(sender, prev);
        }

        public override MouseButtonEventArgs? ProcessMousePress(
            object? sender, MouseButtonEventArgs? e)
        {
            MouseButtonEventArgs? prev = e;
            for(int i = this.Children.Count - 1; i >= 0; i--)
            {
                Aspect a = this.Children[i];
                prev = a.ProcessMousePress(sender, prev);
            }
            return base.ProcessMousePress(sender, prev);
        }

        public override MouseButtonEventArgs? ProcessMouseRelease(
            object? sender, MouseButtonEventArgs? e)
        {
            MouseButtonEventArgs? prev = e;
            for(int i = this.Children.Count - 1; i >= 0; i--)
            {
                Aspect a = this.Children[i];
                prev = a.ProcessMouseRelease(sender, prev);
            }
            return base.ProcessMouseRelease(sender, prev);
        }
    }
}