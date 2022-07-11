using SFML.System;

namespace Rogui.Extensions
{
    public static class Vector2fExtensions
    {
        public static float GetAngleTo(this Vector2f pt1, Vector2f pt2)
        {
            return (float)(Math.Atan2(pt2.Y - pt1.Y, pt2.X - pt1.X) * ( 180 / Math.PI ));
        }
        
        public static float GetDistanceTo(this Vector2f pt1, Vector2f pt2)
        {
            return (float)(Math.Sqrt(Math.Pow(pt2.X - pt1.X, 2) + Math.Pow(pt2.Y - pt1.Y, 2)));
        }
    }
}