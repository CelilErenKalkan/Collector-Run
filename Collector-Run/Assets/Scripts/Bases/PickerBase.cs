using System;
using Game.PickerSystem;
using Managers;
using UnityEngine;
using static Extenders.Actions;

namespace Bases
{
    public class PickerBase : MonoBehaviour
    {
        public Action<int> OnPointGained;
        
        private Camera _pickerCamera;
        private Vector3 _cameraOffset;
        
        private PickerPhysicsManager _pickerPhysicsManager;

        private PickerPhysicsController _pickerPhysicsController;
        private PickerMovementController _pickerMovementController;

        public void Initialize()
        {
            _pickerCamera = _pickerCamera == null ? Camera.main : _pickerCamera;
            _cameraOffset = _pickerCamera.transform.position - transform.position;
            
            _pickerPhysicsManager = new PickerPhysicsManager();

            _pickerMovementController = GetComponent<PickerMovementController>();
            _pickerPhysicsController = GetComponent<PickerPhysicsController>();
            
            _pickerMovementController.Initialize(_pickerCamera);
            _pickerPhysicsController.Initialize(_pickerPhysicsManager,_pickerMovementController);
        }

        private void ActivatePickerMovement()
        {
            _pickerMovementController.Activate();
        }

        private void OnEnable()
        {
            CheckPoint += ActivatePickerMovement;
            Fail += ActivatePickerMovement;
        }
        
        private void OnDisable()
        {
            CheckPoint -= ActivatePickerMovement;
            Fail -= ActivatePickerMovement;
        }

        private void LateUpdate()
        {
            if(_pickerCamera == null)
                return;
            
            _pickerCamera.transform.position = new Vector3(_pickerCamera.transform.position.x,_pickerCamera.transform.position.y,
                transform.position.z + _cameraOffset.z);
        }
    }
}
