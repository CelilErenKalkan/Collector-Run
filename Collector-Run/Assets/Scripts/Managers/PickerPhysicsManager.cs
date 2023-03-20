﻿using System.Collections.Generic;
using Game;

namespace Managers
{
    public class PickerPhysicsManager
    {
        private readonly List<Collectable> _collectables;

        public PickerPhysicsManager()
        {
            _collectables = new List<Collectable>();
        }

        public void AddCollectable(Collectable collectable)
        {
            _collectables.Add(collectable);
        }

        public void RemoveCollectable(Collectable collectable)
        {
            _collectables.Remove(collectable);
        }

        public List<Collectable> GetCollectables()
        {
            return _collectables;
        }
        
        public void Clear()
        {
            _collectables.Clear();
        }
    }
}
