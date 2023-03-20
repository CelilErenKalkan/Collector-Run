using System;
using Managers;
using UnityEngine;
using static Extenders.Actions;

namespace Game.CollectorSystem
{
    public class Collector : MonoBehaviour
    {
        public Action<int> OnPointGained;
        
        private Camera _camera;
        private Vector3 _cameraOffset;
        
        private CollectorPhysicsManager _collectorPhysicsManager;
        private CollectorPhysicsController _collectorPhysicsController;
        private CollectorMovementController _collectorMovementController;

        public void Initialize()
        {
            _camera = _camera == null ? Camera.main : _camera;
            _cameraOffset = _camera.transform.position - transform.position;
            
            _collectorPhysicsManager = new CollectorPhysicsManager();
            
            if (TryGetComponent(out CollectorMovementController collectorMovementController))
                _collectorMovementController = collectorMovementController;
            if (TryGetComponent(out CollectorPhysicsController collectorPhysicsController))
                _collectorPhysicsController = collectorPhysicsController;

            _collectorMovementController.Initialize(_camera);
            _collectorPhysicsController.Initialize(_collectorPhysicsManager,_collectorMovementController);
        }

        private void ActivatePickerMovement()
        {
            _collectorMovementController.Activate();
        }

        private void OnEnable()
        {
            CHECKPOINT += ActivatePickerMovement;
            FAIL += ActivatePickerMovement;
        }
        
        private void OnDisable()
        {
            CHECKPOINT -= ActivatePickerMovement;
            FAIL -= ActivatePickerMovement;
        }

        private void LateUpdate()
        {
            if(_camera == null)
                return;
            
            _camera.transform.position = new Vector3(_camera.transform.position.x,_camera.transform.position.y,
                transform.position.z + _cameraOffset.z);
        }
    }
}
