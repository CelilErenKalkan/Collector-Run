using Managers;
using UnityEngine;

namespace Game.CollectorSystem
{
    public class CollectorPhysicsController : MonoBehaviour
    {
        private CollectorPhysicsManager _collectorPhysicsManager;
        private CollectorMovementController _collectorMovementController;
        
        public void Initialize(CollectorPhysicsManager collectorPhysicsManager, CollectorMovementController collectorMovementController)
        {
            _collectorPhysicsManager = collectorPhysicsManager;
            _collectorMovementController = collectorMovementController;
        }

        public void PushCollectables()
        {
            _collectorMovementController.Deactivate();
            foreach (var collectable in _collectorPhysicsManager.GetCollectables())
            {
                collectable.Push();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Collectable collectable)) return;
            _collectorPhysicsManager.AddCollectable(collectable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out Collectable collectable)) return;
            _collectorPhysicsManager.RemoveCollectable(collectable);
        }
    }
}
