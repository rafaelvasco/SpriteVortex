using System.Collections;

namespace SpriteVortex
{
    public static class Guard
    {
        public static bool CheckNull(object obj)
        {
            return obj == null;
        }

        public static bool CheckListEmpty(IList list)
        {
            return list.Count == 0;
        }
    }
}
