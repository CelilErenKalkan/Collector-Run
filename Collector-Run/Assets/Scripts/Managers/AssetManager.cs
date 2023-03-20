using System.Collections.Generic;
using System.Linq;
using Bases;
using Extenders;
using Managers.Level;
using UnityEngine;

namespace Managers
{
    public class AssetManager : MonoSingleton<AssetManager>
    {
        private const string LEVEL_PATH = "Levels";
        private const string BALLPACK_PATH = "Objects";
        private const string PLATFORM_PATH = "Platforms";

        private List<LevelData> _levelList;
        private List<PlatformBase> _platformBases;
        private List<BallPackBase> _ballPackBases;

        public Material groundMaterial;
        public Material pickerMaterial;

        public bool IsLevelIndexExceededLevelPrefabs(int levelIndex) => levelIndex >= _levelList.Count;
        
        public void LoadAssets()
        {
            _levelList = Resources.LoadAll<LevelData>(LEVEL_PATH).ToList();
            _platformBases = Resources.LoadAll<PlatformBase>(PLATFORM_PATH).ToList();
            _ballPackBases = Resources.LoadAll<BallPackBase>(BALLPACK_PATH).ToList();
        }

        public PlatformBase GetPlatform(PlatformType platformType)
        {
            return _platformBases?.FirstOrDefault(x => x.PlatformType == platformType);
        }

        public BallPackBase GetBallPack(BallPackType ballPackType)
        {
            return _ballPackBases?.FirstOrDefault(x => x.ballPackType == ballPackType);
        }
        
        public LevelData LoadLevel(int levelIndex)
        {
            if (!IsLevelIndexExceededLevelPrefabs(levelIndex)) return _levelList[levelIndex];
            
            var random = Random.Range(0, _levelList.Count);
            return _levelList[random];
        }
    }
}
