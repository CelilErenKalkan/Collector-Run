using System;
using System.Collections.Generic;
using Bases;
using UnityEngine;

namespace Managers.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level Datas/New Level", order = 1)]
    public class LevelData : ScriptableObject
    {
        public List<PlatformData> platformDatas;
        public List<BallPackData> ballPackDatas;
    }
    
    [Serializable]
    public class PlatformData
    {
        // Platforms that are not Checkpoint type's checkpoint count will be ignored.
        public Vector3 position;
        public PlatformType platformType;
        public int checkPointCount;
    }

    [Serializable]
    public class BallPackData
    {
        public Vector3 position;
        public BallPackType ballPackType;
    }
}
