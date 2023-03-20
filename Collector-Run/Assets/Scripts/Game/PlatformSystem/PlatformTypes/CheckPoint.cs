using System.Collections;
using DG.Tweening;
using Extenders;
using Game.PickerSystem;
using UnityEngine;
using static Extenders.Actions;

namespace Game.PlatformSystem.PlatformTypes
{
    public class CheckPoint : Platform
    {
        public override PlatformType PlatformType => PlatformType.CHECKPOINT;
        private int _target;

        [SerializeField] private CheckPointCounter checkPointCounter;
        [SerializeField] private Transform gate1;
        [SerializeField] private Transform gate2;

        private Vector3 _firstPos;

        public void SetTarget(int aim)
        {
            _target = aim;
            checkPointCounter.Initialize(_target);
        }

        private void CheckContinue(Picker picker)
        {
            var counter = checkPointCounter.GetCounter();
            if (counter >= _target)
            {
                checkPointCounter.SuccessfulAction();
                gate1.transform.DORotate(new Vector3(-60,90,90), 1f);
                gate2.transform.DORotate(new Vector3(60,90,90), 1f).OnComplete(()=>
                {
                    CHECKPOINT?.Invoke();
                });
                
                picker.OnPointGained?.Invoke(counter * 5);
            }
            else
            {
                LEVEL_END?.Invoke(false);
            }
        }

        private void CheckContinueInTime(Component picker)
        {
            StartCoroutine(Timer());
            IEnumerator Timer()
            {
                yield return 2.0f.GetWait();
                if (picker.TryGetComponent(out Picker pickerBase))
                    CheckContinue(pickerBase);
            }
        }

        private void Reset()
        {
            gate1.transform.eulerAngles = new Vector3(0,90,90);
            gate2.transform.eulerAngles = new Vector3(0,90,90);
        }

        private void OnEnable()
        {
            SUCCESS += Reset;
            FAIL += Reset;
        }
        
        private void OnDisable()
        {
            SUCCESS -= Reset;
            FAIL -= Reset;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PickerPhysicsController pickerPhysicsController)) return;
            pickerPhysicsController.PushCollectables();
            CheckContinueInTime(pickerPhysicsController);
        }
    }
}
