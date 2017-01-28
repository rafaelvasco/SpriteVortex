using System;
using Vortex.Drawing;
using Vortex.Input;

namespace SpriteVortex
{
    public static class Parser
    {
        public static float ParseFloat(string floatString)
        {
            return float.Parse(floatString);
        }

        public static bool ParseBool(string boolString)
        {
            return bool.Parse(boolString);
        }

        public static int ParseInt(string intString)
        {
            return int.Parse(intString);
        }

        public static ColorU ParseColorU(string colorString)
        {
            return new ColorU(uint.Parse(colorString));
        }

       
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);

        }



        public static ControlConfig ParseControl(string keyString, string buttonString)
        {
            var controlConfig = new ControlConfig();

            if (keyString != "none")
            {
                controlConfig.Key = ParseEnum<Key>(keyString);
            }
            else
            {
                controlConfig.Key = null;
            }

            controlConfig.MouseButton = ParseEnum<MouseButton>(buttonString);

            return controlConfig;
        }

    }
}
