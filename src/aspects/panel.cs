using SFML.Graphics;
using SFML.System;
using Rogui.Themes;

namespace Rogui
{
    public class Panel : Aspect
    {
        protected Shapes.Rectangle BodyBG = new Shapes.Rectangle();
        protected Shapes.Rectangle BodyFG = new Shapes.Rectangle();

        public override FloatRect Bounds => this.BodyBG.Bounds;
        
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
                this.BodyBG.AbsolutePosition = new Vector2f(
                    value.X + this.MarginLeft,
                    value.Y + this.MarginTop) + this.Position;
                this.BodyFG.AbsolutePosition = new Vector2f(
                    value.X + this.MarginLeft + this.BorderLeft,
                    value.Y + this.MarginTop + this.BorderTop) + this.Position;
            }
        }

        public override Vector2f Size {
            get => this.BodyFG.Size;
            set {
                this.BodyFG.Size = value;
                this.BodyBG.Size = new Vector2f(
                    value.X + this.BorderLeft + this.BorderRight,
                    value.Y + this.BorderTop + this.BorderBottom);
            }
        }

        public new ThemePanel Theme {
            set {
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

        public Panel()
        {
            this.Add(this.BodyBG, this.BodyFG);
        }
    }
}