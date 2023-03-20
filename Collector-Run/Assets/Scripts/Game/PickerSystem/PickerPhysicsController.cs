using Managers;
using UnityEngine;

namespace Game.PickerSystem
{
    public class PickerPhysicsController : MonoBehaviour
    {
        private PickerPhysicsManager _pickerPhysicsManager;
        private PickerMovementController _pickerMovementController;
        
        public void Initialize(PickerPhysicsManager pickerPhysicsManager, PickerMovementController pickerMovementController)
        {
            _pickerPhysicsManager = pickerPhysicsManager;
            _pickerMovementController = pickerMovementController;
        }

        public void PushCollectables()
        {
            _pickerMovementController.Deactivate();
            foreach (var collectable in _pickerPhysicsManager.GetCollectables())
            {
                collectable.Push();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Collectable collectable)) return;
            _pickerPhysicsManager.AddCollectable(collectable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out Collectable collectable)) return;
            _pickerPhysicsManager.RemoveCollectable(collectable);
        }
    }
}
