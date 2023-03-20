using System;
using System.Collections;
using Bases;
using DG.Tweening;
using Extenders;
using Game.PickerSystem;
using UnityEngine;
using static Extenders.Actions;

namespace Game.PlatformSystem
{
    public class CheckPoint : PlatformBase
    {
        public override PlatformType PlatformType => PlatformType.CHECKPOINT;
        private int _target;

        private CheckPointCounterPlatform _checkPointCounterPlatform;
        private Transform _gate1;
        private Transform _gate2;

        private Vector3 _firstPos;

        public override void Initialize()
        {
            base.Initialize();
            _checkPointCounterPlatform = GetComponentInChildren<CheckPointCounterPlatform>(true);            
            _gate1 = transform.Find("Gate1");
            _gate2 = transform.Find("Gate2");
        }

        public void SetTarget(int aim)
        {
            _target = aim;
            _checkPointCounterPlatform.Initialize(_target);
        }

        private void CheckContinue(PickerBase picker)
        {
            var counter = _checkPointCounterPlatform.GetCounter();
            if (counter >= _target)
            {
                _checkPointCounterPlatform.SuccessfulAction();
                _gate1.transform.DORotate(new Vector3(-60,90,90), 1f);
                _gate2.transform.DORotate(new Vector3(60,90,90), 1f).OnComplete(()=>
                {
                    Actions.CheckPoint?.Invoke();
                });
                picker.OnPointGained?.Invoke(counter * 5);
            }
            else
            {
                Fail?.Invoke();
            }
        }

        private void CheckContinueInTime(Component picker)
        {
            StartCoroutine(Timer());
            IEnumerator Timer()
            {
                yield return 2.0f.GetWait();
                if (picker.TryGetComponent(out PickerBase pickerBase))
                    CheckContinue(pickerBase);
            }
        }

        private void Reset()
        {
            _gate1.transform.eulerAngles = new Vector3(0,90,90);
            _gate2.transform.eulerAngles = new Vector3(0,90,90);
        }

        private void OnEnable()
        {
            Success += Reset;
            Fail += Reset;
        }
        
        private void OnDisable()
        {
            Success -= Reset;
            Fail -= Reset;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PickerPhysicsController pickerPhysicsController)) return;
            pickerPhysicsController.PushCollectables();
            CheckContinueInTime(pickerPhysicsController);
        }
    }
}
