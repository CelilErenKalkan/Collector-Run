using System;

namespace Extenders
{
    public static class Actions
    {
        public static Action LEVEL_START;
        public static Action<bool> LEVEL_END;
        public static Action SUCCESS;
        public static Action FAIL;
        public static Action CHECKPOINT;
    }
}
