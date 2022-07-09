using SFML.Graphics;

namespace Rogui.Themes
{
    public struct ThemeButton
    {
        public Color? FillColor;
        public Color? BorderColor;
        public float? BorderWidth;
        public float? MarginLeft;
        public float? MarginTop;
        public float? MarginRight;
        public float? MarginBottom;
        public float? PaddingLeft;
        public float? PaddingTop;
        public float? PaddingRight;
        public float? PaddingBottom;

        public float Margin {
            set {
                this.MarginLeft = value;
                this.MarginTop = value;
                this.MarginRight = value;
                this.MarginBottom = value;
            }
        }

        public float Padding {
            set {
                this.PaddingLeft = value;
                this.PaddingTop = value;
                this.PaddingRight = value;
                this.PaddingBottom = value;
            }
        }
    }
}