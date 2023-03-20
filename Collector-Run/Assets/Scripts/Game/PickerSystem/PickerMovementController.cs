﻿using System;
using UnityEngine;
using static Extenders.Actions;

namespace Game.PickerSystem
{
    public class PickerMovementController : MonoBehaviour
    {
        private bool _active;
        private float _forwardSpeed;
        private float _xSpeed;

        private Camera _pickerCamera;
        private Vector3 _mousePos;
        private float _distanceToScreen;

        public void Initialize(Camera pickerCamera)
        {
            _pickerCamera = pickerCamera;
            _forwardSpeed = 5;
            _xSpeed = 10f;
            Activate();
        }

        public void Activate()
        {
            _active = true;
        }

        public void Deactivate()
        {
            _active = false;
        }

        private void OnNewLevel()
        {
            _forwardSpeed = 5;
        }
        
        private void OnLevelEnd(bool isSuccess)
        {
            _forwardSpeed = 0;
        }
        
        private void OnEnable()
        {
            SUCCESS += OnNewLevel;
            FAIL += OnNewLevel;
            LEVEL_END += OnLevelEnd;
        }
        
        private void OnDisable()
        {
            SUCCESS -= OnNewLevel;
            FAIL -= OnNewLevel;
            LEVEL_END -= OnLevelEnd;
        }
        
        private void FixedUpdate()
        {
            if(!_active) return;
            
            if (Input.GetMouseButton(0))
            {
                var position = Input.mousePosition;
                
                _distanceToScreen = _pickerCamera.WorldToScreenPoint(gameObject.transform.position).z;
                _mousePos = _pickerCamera.ScreenToWorldPoint(new Vector3(position.x, position.y, _distanceToScreen ));
                float direction = _xSpeed;
                direction = _mousePos.x > transform.position.x ? direction : -direction;
                
                if(Math.Abs(_mousePos.x - transform.position.x) > 0.5f)
                    transform.Translate(Time.fixedDeltaTime * direction,0,0);
            }
            
            transform.Translate(0,0,Time.fixedDeltaTime * _forwardSpeed);
        }
    }
}
