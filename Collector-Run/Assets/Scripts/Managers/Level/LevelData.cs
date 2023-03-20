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
        public PlatformType platformType;
        public Vector3 position;
        [Tooltip("non Checkpoint platforms' checkPointCount will be ignored.")]
            public int checkPointCount;
    }

    [Serializable]
    public class ObjectGroupData
    {
        public ObjectGroupType objectGroupType;
        public Vector3 position;
    }
}
