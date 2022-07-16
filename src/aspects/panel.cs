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
        public Vector2f ContentSize => this.Contents.Bounds.GetSize();
        
        public override Color FillColor {
            get => this.BodyFG.FillColor;
            set => this.BodyFG.FillColor = value;
        }

        public override Color BorderColor {
            get => this.BodyBG.FillColor;
            set => this.BodyBG.FillColor = value;
        }

        public override Vector2f AbsolutePosition {
            get => this.BodyBG.AbsolutePosition;
            set {
                this.BodyBG.AbsolutePosition = value + this.MarginPosition;
                this.BodyFG.AbsolutePosition = value + this.MarginPosition + this.BorderPosition;
                this.Contents.AbsolutePosition = value + this.MarginPosition + this.BorderPosition;
            }
        }

        public override Vector2f Position {
            get => this.BodyBG.Position;
            set {
                this.BodyBG.Position = value;
                this.BodyFG.Position = value;
                this.Contents.Position = value;
            }
        }

        public override Vector2f OffsetPosition {
            get => this.BodyBG.OffsetPosition;
            set {
                this.BodyBG.OffsetPosition = value;
                this.BodyFG.OffsetPosition = value;
            }
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
                foreach(Aspect a in this.Contents.Children)
                {
                    a.MarginLeft = value;
                }
                base.PaddingLeft = value;
            }
        }

        public override float PaddingTop {
            get => base.PaddingTop;
            set {
                foreach(Aspect a in this.Contents.Children)
                {
                    a.MarginTop = value;
                }
                base.PaddingTop = value;
            }
        }

        public override float PaddingRight {
            get => base.PaddingRight;
            set {
                foreach(Aspect a in this.Contents.Children)
                {
                    a.MarginRight = value;
                }
                base.PaddingRight = value;
            }
        }

        public override float PaddingBottom {
            get => base.PaddingBottom;
            set {
                foreach(Aspect a in this.Contents.Children)
                {
                    a.MarginBottom = value;
                }
                base.PaddingBottom = value;
            }
        }

        public override void Add(params Aspect[] aspects)
        {
            foreach(Aspect a in aspects)
            {
                a.Transformed += this.OnTransformed;
                this.Contents.Add(a);
            }
            this.UpdateLayout();
            // reapply theme so children can inherit properties
            this.Theme = this.Theme;
        }

        public Panel()
        {
            base.Add(this.BodyBG, this.BodyFG, this.Contents);
        }

        public virtual void OnTransformed(object? sender, EventArgs e)
        {
            this.Size = this.ContentSize;
        }
    }
}