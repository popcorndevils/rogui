using SFML.Graphics;
using SFML.System;
using Rogui.Themes;
using Rogui.Extensions;

namespace Rogui
{
    public class Panel : Aspect
    {
        protected Aspect Contents = new Aspect();
        protected Shapes.Rectangle BodyBG = new Shapes.Rectangle();
        protected Shapes.Rectangle BodyFG = new Shapes.Rectangle();

        public override FloatRect Bounds => this.BodyBG.Bounds;
        public FloatRect InteriorBounds => this.BodyFG.Bounds;
        public Vector2f ContentSize => this.Contents.Bounds.GetSize();

        public override Vector2f TruePosition => this.BodyBG.TruePosition;
        public override Vector2f TrueCenter => this.BodyBG.TrueCenter;
        
        public override Color? FillColor {
            get => this.BodyFG.FillColor;
            set => this.BodyFG.FillColor = value;
        }

        public override Color? BorderColor {
            get => this.BodyBG.FillColor;
            set => this.BodyBG.FillColor = value;
        }

        public override Vector2f Size {
            get => this.BodyFG.Size;
            set {
                this.BodyFG.Size = value;
                this.BodyBG.Size = value + this.BorderSize;
            }
        }

        private ThemePanel? _Theme;
        public new ThemePanel? Theme {
            get { return this._Theme;}
            set {
                this._Theme = value;
                if(value is not null)
                {
                    if(value.FillColor is not null) 
                        { this.FillColor = (Color)value.FillColor; }
                    if(value.BorderColor is not null) 
                        { this.BorderColor = (Color)value.BorderColor; }
                    if(value.MarginLeft is not null) 
                        { this.MarginLeft = (float)value.MarginLeft; }
                    if(value.MarginTop is not null) 
                        { this.MarginTop = (float)value.MarginTop; }
                    if(value.MarginRight is not null) 
                        { this.MarginRight = (float)value.MarginRight; }
                    if(value.MarginBottom is not null) 
                        { this.MarginBottom = (float)value.MarginBottom; }
                    if(value.PaddingLeft is not null) 
                        { this.PaddingLeft = (float)value.PaddingLeft; }
                    if(value.PaddingTop is not null) 
                        { this.PaddingTop = (float)value.PaddingTop; }
                    if(value.PaddingRight is not null) 
                        { this.PaddingRight = (float)value.PaddingRight; }
                    if(value.PaddingBottom is not null) 
                        { this.PaddingBottom = (float)value.PaddingBottom; }
                    if(value.BorderLeft is not null) 
                        { this.BorderLeft = (float)value.BorderLeft; }
                    if(value.BorderTop is not null) 
                        { this.BorderTop = (float)value.BorderTop; }
                    if(value.BorderRight is not null) 
                        { this.BorderRight = (float)value.BorderRight; }
                    if(value.BorderBottom is not null) 
                        { this.BorderBottom = (float)value.BorderBottom; }
                }
            }
        }

        public override float Padding { set => this.Contents.Padding = value; }
        public override float PaddingLeft {
            get => this.Contents.PaddingLeft;
            set => this.Contents.PaddingLeft = value;
        }
        public override float PaddingTop {
            get => this.Contents.PaddingTop;
            set => this.Contents.PaddingTop = value;
        }
        public override float PaddingRight {
            get => this.Contents.PaddingRight;
            set => this.Contents.PaddingRight = value;
        }
        public override float PaddingBottom {
            get => this.Contents.PaddingBottom;
            set => this.Contents.PaddingBottom = value;
        }

        public override void Add(params Aspect[] aspects)
        {
            this.Contents.Add(aspects);
            // reapply theme so children can inherit properties and update layout
            this.Theme = this.Theme;
            this.UpdateLayout();
        }

        public Panel(): base() { this.Init(); }
        public Panel(params Aspect[] aspects): base() { this.Init(aspects); }
        private void Init(params Aspect[] aspects)
        {
            base.Add(this.BodyBG, this.BodyFG, this.Contents);
            this.Contents.ChildTransformed += this.OnTransformed;
            this.Add(aspects);
        }

        public virtual void OnTransformed(object? sender, EventArgs e)
        {
            this.Size = this.ContentSize;
        }

        protected override void UpdateLayout()
        {            
            base.UpdateLayout();
            this.BodyFG.AbsolutePosition += this.BorderPosition;
            this.Contents.AbsolutePosition += this.BorderPosition;
        }
    }
}