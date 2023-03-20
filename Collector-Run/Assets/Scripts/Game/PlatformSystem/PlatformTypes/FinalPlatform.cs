using System.Collections;
using Bases;
using Extenders;
using Game.PickerSystem;
using GameEvents;
using UnityEngine;

namespace Game.PlatformSystem.PlatformTypes
{
    public class FinalPlatform : PlatformBase
    {
        public override PlatformType PlatformType => PlatformType.FINAL;

        private void OnTriggerEnter(Collider other)
        {
            var picker = other.GetComponent<PickerPhysicsController>();
            if (picker == null) return;

            StartCoroutine(Timer());
            IEnumerator Timer()
            {
                yield return 2.0f.GetWait();
                GameEventBus.InvokeEvent(GameEventType.SUCCESS);
            }
        }
    }
}
