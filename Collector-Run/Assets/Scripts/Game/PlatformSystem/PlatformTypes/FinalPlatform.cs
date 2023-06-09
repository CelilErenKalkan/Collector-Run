﻿using System.Collections;
using Extenders;
using Game.CollectorSystem;
using UnityEngine;
using static Extenders.Actions;

namespace Game.PlatformSystem.PlatformTypes
{
    public class FinalPlatform : Platform
    {
        public override PlatformType PlatformType => PlatformType.FINAL;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out CollectorPhysicsController collector)) return;
            
            StartCoroutine(Timer());
            IEnumerator Timer()
            {
                yield return 2.0f.GetWait();
                LEVEL_END?.Invoke(true);
            }
        }
    }
}
