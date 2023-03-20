using System;

namespace GameEvents
{
    public class GameEvent
    {
        private event Action _levelAction;
        public GameEventType LevelEventType;
        
        public GameEvent(GameEventType levelEventType)
        {
            LevelEventType = levelEventType;
        }

        public void Invoke()
        {
            _levelAction?.Invoke();
        }

        public void Subscribe(Action action)
        {
            _levelAction += action;
        }
    }

    public enum GameEventType
    {
        CHECKPOINT,
        SUCCESS,
        FAIL
    }
}
