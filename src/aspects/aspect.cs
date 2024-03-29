using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Rogui
{
    public class Aspect : Primitive
    {
        public event EventHandler? ChildTransformed;

        public List<Aspect> Children = new List<Aspect>();

        public override bool Visible {
            get => base.Visible;
            set {
                base.Visible = value;
                base.AcceptInput = value;
            }
        }

        public override float Padding { 
            set {
                foreach(Aspect a in this.Children)
                {
                    a.Margin = value;
                }
            }
        }

        public override float PaddingLeft {
            get => base.PaddingLeft;
            set {
                foreach(Aspect a in this.Children)
                {
                    a.MarginLeft = value;
                }
                base.PaddingLeft = value;
            }
        }

        public override float PaddingTop {
            get => base.PaddingTop;
            set {
                foreach(Aspect a in this.Children)
                {
                    a.MarginTop = value;
                }
                base.PaddingTop = value;
            }
        }

        public override float PaddingRight {
            get => base.PaddingRight;
            set {
                foreach(Aspect a in this.Children)
                {
                    a.MarginRight = value;
                }
                base.PaddingRight = value;
            }
        }

        public override float PaddingBottom {
            get => base.PaddingBottom;
            set {
                foreach(Aspect a in this.Children)
                {
                    a.MarginBottom = value;
                }
                base.PaddingBottom = value;
            }
        }

        public override FloatRect Bounds {
            get {
                if(this.Children.Count > 0)
                {
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
                return new FloatRect();
            }
        }

        public Aspect() {}
        public Aspect(params Aspect[] aspects)
        {
            this.Add(aspects);
        }

        public virtual void Add(params Aspect[] aspects)
        {
            foreach(Aspect a in aspects)
            {
                a.Parent = this;
                a.Transformed += this.HandleChildTransformation;
                this.Children.Add(a);
            }
        }

        public virtual void Insert(int index, Aspect aspect)
        {
            aspect.Parent = this;
            aspect.Transformed += this.HandleChildTransformation;
            this.Children.Insert(index, aspect);
        }

        protected virtual void UpdateLayout()
        {            
            var _pos = this.PositionGlobal + this.MarginPosition + this.PositionLocal + this.PositionOffset;
            foreach(Aspect c in this.Children)
            {
                c.PositionGlobal = _pos;
            }
        }

        public override void Update(float? ms)
        { 
            foreach(Aspect a in this.Children)
            {
                a.Update(ms);
                a.UpdateLayout();
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
            if(this.AcceptInput)
            {
                MouseMoveEventArgs? prev = e;
                for(int i = this.Children.Count - 1; i >= 0; i--)
                {
                    Aspect a = this.Children[i];
                    prev = a.ProcessMouseMove(sender, prev);
                }
                return base.ProcessMouseMove(sender, prev);
            }
            else
            {
                return e;
            }
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

        public void HandleChildTransformation(object? sender, EventArgs e)
        {
            this.ChildTransformed?.Invoke(this, EventArgs.Empty);
        }
    }
}