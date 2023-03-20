using System;
using System.Collections.Generic;
using Game;
using Game.PlatformSystem.PlatformTypes;
using UnityEngine;

namespace Managers.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level Datas/New Level", order = 1)]
    public class LevelData : ScriptableObject
    {
        public List<PlatformData> platformDatas;
        public List<ObjectGroupData> objectGroupDatas;
    }
    
    [Serializable]
    public class PlatformData
    {
        public Vector3 position;
        public PlatformType platformType;
        [Tooltip("non Checkpoint platforms' checkPointCount will be ignored.")]
            public int checkPointCount;
    }

    [Serializable]
    public class ObjectGroupData
    {
        public Vector3 position;
        public ObjectGroupType objectGroupType;
    }
}
