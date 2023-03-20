using System.Collections.Generic;
using System.Linq;
using Extenders;
using Game;
using Game.PlatformSystem.PlatformTypes;
using Managers.Level;
using UnityEngine;
using Random = UnityEngine.Random;
using static Extenders.Actions;

namespace Managers
{
    public class AssetManager : MonoSingleton<AssetManager>
    {
        private const string LEVEL_PATH = "Levels";
        private const string OBJECT_GROUP_PATH = "Objects";
        private const string PLATFORM_PATH = "Platforms";

        [SerializeField] private List<LevelData> _levelList;
        private List<Platform> _platforms;
        private List<ObjectGroup> _objectGroups;

        public Material groundMaterial;
        public Material collectorMaterial;

        private int _prefabIndex;

        private bool IsLevelIndexExceededLevelPrefabs(int levelIndex) => levelIndex > _levelList.Count;
        
        public void LoadAssets()
        {
            _levelList = Resources.LoadAll<LevelData>(LEVEL_PATH).ToList();
            _platforms = Resources.LoadAll<Platform>(PLATFORM_PATH).ToList();
            _objectGroups = Resources.LoadAll<ObjectGroup>(OBJECT_GROUP_PATH).ToList();
            _prefabIndex = PlayerPrefs.GetInt("PrefabIndex");
            Debug.Log(_prefabIndex);
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
            if (!IsLevelIndexExceededLevelPrefabs(levelIndex))
            {
                _prefabIndex = levelIndex - 1;
                PlayerPrefs.SetInt("PrefabIndex", _prefabIndex);
            }
            
            return _levelList[_prefabIndex];
        }

        private void OnLevelEnd(bool isSuccess)
        {
            if (isSuccess && IsLevelIndexExceededLevelPrefabs(LevelManager.Instance.levelIndex + 1))
            {
                _prefabIndex = Random.Range(0, _levelList.Count);
                PlayerPrefs.SetInt("PrefabIndex", _prefabIndex);
            }
        }
        
        private void OnEnable()
        {
            LEVEL_END += OnLevelEnd;
        }
        
        private void OnDisable()
        {
            LEVEL_END -= OnLevelEnd;
        }
    }
}
