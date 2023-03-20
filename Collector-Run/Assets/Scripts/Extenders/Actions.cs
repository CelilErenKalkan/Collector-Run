using System;

namespace Extenders
{
    public enum LevelEndType
    {
        CHECKPOINT,
        SUCCESS,
        FAIL
    }
    
    public static class Actions
    {
        public static Action Success;
        public static Action Fail;
        public static Action CheckPoint;
    }
}
