using System;

namespace SpriteVortex
{
    public static class MathHelper
    {
        public const float E = 2.718282f;
        public const float LOG10_E = 0.4342945f;
        public const float LOG2_E = 1.442695f;
        public const float PI = 3.141593f;
        public const float PI_OVER2 = 1.570796f;
        public const float PI_OVER4 = 0.7853982f;
        public const float TWO_PI = 6.283185f;


        /// <summary>
        /// Clamps a value between to values min and max
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>Clamped value</returns>
        public static float Clamp(float value, float min, float max)
        {
            value = (value > max) ? max : value;
            value = (value < min) ? min : value;
            return value;
        }

        public static float CalculateCameraZoom(Camera camera,int mouseDelta)
        {
            var wheelDeltaSign = Math.Sign(mouseDelta);
            float zoom = (float)Math.Round(camera.Zoom * 10.0f) * 10.0f + wheelDeltaSign * 10.0f;
            return (zoom / 100.0f); 
        }

        public static float CalculateCameraZoomFactor(Camera camera, int mouseDelta)
        {
            return CalculateCameraZoom(camera, mouseDelta)/camera.Zoom;
        }

        /// <summary>
        /// Quadratic interpolation between two points with ease in
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static float EaseInQuad(float value1, float value2, float amount)
        {
            var t = 1 - amount;
            return (float)((value2 - value1) * (Math.Sin(-t * (Math.PI / 2)) + 1));
        }


        /// <summary>
        /// Linear interpolation between two points
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static float Lerp(float value1, float value2, float amount)
        {
            return (((value2 - value1) * amount));
        }

        /// <summary>
        /// Returns greatest value of given values
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static float Max(float value1, float value2)
        {
            return Math.Max(value1, value2);
        }
        /// <summary>
        /// Returns lowest value between given values
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static float Min(float value1, float value2)
        {
            return Math.Min(value1, value2);
        }

        public static float SmoothStep(float value1, float value2, float amount)
        {
            float num = Clamp(amount, 0f, 1f);
            return Lerp(value1, value2, (num * num) * (3f - (2f * num)));
        }


       
       
       
        /// <summary>
        /// Finds next power of two value starting from given value
        /// Reference: http://en.wikipedia.org/wiki/Power_of_two#Algorithm_to_find_the_next-highest_power_of_two
        /// </summary>
        /// <param name="k"></param>
        /// <returns>Next power of two value from given value</returns>
        public static int FindNextPowerOfTwo(int k)
        {
            k--;
            for (int i = 1; i < sizeof(int) * 8; i <<= 1)
                k = k | k >> i;
            return k + 1;
        }
    }
}
