using System.Collections.Generic;
using System.Linq;
using Extenders;
using Game;
using Game.PlatformSystem.PlatformTypes;
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
        private List<Platform> _platforms;
        private List<ObjectGroup> _objectGroups;

        public Material groundMaterial;
        public Material pickerMaterial;

        private bool IsLevelIndexExceededLevelPrefabs(int levelIndex) => levelIndex >= _levelList.Count;
        
        public void LoadAssets()
        {
            _levelList = Resources.LoadAll<LevelData>(LEVEL_PATH).ToList();
            _platforms = Resources.LoadAll<Platform>(PLATFORM_PATH).ToList();
            _objectGroups = Resources.LoadAll<ObjectGroup>(BALLPACK_PATH).ToList();
        }

        public Platform GetPlatform(PlatformType platformType)
        {
            return _platforms?.FirstOrDefault(x => x.PlatformType == platformType);
        }

        public ObjectGroup GetObjectGroup(ObjectGroupType objectGroupType)
        {
            return _objectGroups?.FirstOrDefault(x => x.objectGroupType == objectGroupType);
        }
        
        public LevelData LoadLevel(int levelIndex)
        {
            if (!IsLevelIndexExceededLevelPrefabs(levelIndex)) return _levelList[levelIndex];
            
            var random = Random.Range(0, _levelList.Count);
            return _levelList[random];
        }
    }
}
