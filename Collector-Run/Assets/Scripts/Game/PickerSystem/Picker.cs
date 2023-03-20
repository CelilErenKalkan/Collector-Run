using System;
using Managers;
using UnityEngine;
using static Extenders.Actions;

namespace Game.PickerSystem
{
    public class Picker : MonoBehaviour
    {
        public Action<int> OnPointGained;
        
        private Camera _camera;
        private Vector3 _cameraOffset;
        
        private PickerPhysicsManager _pickerPhysicsManager;
        private PickerPhysicsController _pickerPhysicsController;
        private PickerMovementController _pickerMovementController;

        public void Initialize()
        {
            _camera = _camera == null ? Camera.main : _camera;
            _cameraOffset = _camera.transform.position - transform.position;
            
            _pickerPhysicsManager = new PickerPhysicsManager();
            
            if (TryGetComponent(out PickerMovementController pickerMovementController))
                _pickerMovementController = pickerMovementController;
            if (TryGetComponent(out PickerPhysicsController pickerPhysicsController))
                _pickerPhysicsController = pickerPhysicsController;

            _pickerMovementController.Initialize(_camera);
            _pickerPhysicsController.Initialize(_pickerPhysicsManager,_pickerMovementController);
        }

        private void ActivatePickerMovement()
        {
            _pickerMovementController.Activate();
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
