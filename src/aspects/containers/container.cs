using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Rogui.Containers
{
    public class Container : Aspect
    {
        public override event EventHandler? OnTransform;
        public List<Aspect> Children = new List<Aspect>();

        public float MarginSeparator = 0f;
        public float MarginLeft = 0f;
        public float MarginRight = 0f;
        public float MarginTop = 0f;
        public float MarginBottom = 0f;
        public float Margin {
            set {
                this.MarginBottom = value;
                this.MarginTop = value;
                this.MarginLeft = value;
                this.MarginRight = value;
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

        public override Vector2f Size {
            get {
                var _bnds = this.Bounds;
                return new Vector2f(_bnds.Width, _bnds.Height);
            }
        }

        private Vector2f _Position = new Vector2f();
        public override Vector2f Position {
            get => this._Position;
            set {
                this._Position = value;
                this.UpdateLayout();
            }
        }

        public Container() {}
        public Container(params Aspect[] aspects)
        {
            this.Add(aspects);
        }

        public void Add(params Aspect[] aspects)
        {
            foreach(Aspect a in aspects)
            {
                this.Children.Add(a);
            }
            this.UpdateLayout();
        }

        protected virtual void UpdateLayout()
        { 
            this.OnTransform?.Invoke(this, EventArgs.Empty);
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

        public override bool ProcessKey(object? sender, EventArgs e)
        {
            bool stop = false;
            foreach(Aspect a in this.Children)
            {   
                stop = a.ProcessKey(sender, e);
                if(stop)
                {
                    return true;
                }
            }
            return base.ProcessKey(sender, e);
        }

        public override bool ProcessMouseMove(object? sender, MouseMoveEventArgs e)
        {
            bool stop = false;
            foreach(Aspect a in this.Children)
            {   
                stop = a.ProcessMouseMove(sender, e);
                if(stop)
                {
                    return true;
                }
            }
            return base.ProcessMouseMove(sender, e);
        }

        public override bool ProcessMouseButton(object? sender, MouseButtonEventArgs e)
        {
            bool stop = false;
            foreach(Aspect a in this.Children)
            {   
                stop = a.ProcessMouseButton(sender, e);
                if(stop)
                {
                    return true;
                }
            }
            return base.ProcessMouseButton(sender, e);
        }
    }
}