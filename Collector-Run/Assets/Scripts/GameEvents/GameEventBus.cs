using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEvents
{
    public static class GameEventBus
    {
        private static List<GameEvent> _levelEvents = new List<GameEvent>
        {
            new GameEvent(GameEventType.FAIL),
            new GameEvent(GameEventType.SUCCESS),
            new GameEvent(GameEventType.CHECKPOINT)
        };
        
                
        public static void InvokeEvent(GameEventType type)
        {
            var specificEvent = _levelEvents?.FirstOrDefault(x => x.LevelEventType == type);
            specificEvent?.Invoke();
        }

        public static void SubscribeEvent(GameEventType type, Action action)
        {
            var specificEvent = _levelEvents?.FirstOrDefault(x => x.LevelEventType == type);
            specificEvent?.Subscribe(action);
        }
    }
}
