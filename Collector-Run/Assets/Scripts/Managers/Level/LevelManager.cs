using System.Collections;
using System.Collections.Generic;
using Extenders;
using Game.CollectorSystem;
using Game.PlatformSystem.PlatformTypes;
using UnityEngine;
using static Extenders.Actions;

namespace Managers.Level
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private AssetManager _assetManager;
        private UIManager _uiManager;
        private PoolManager _pool;
        [SerializeField] private Collector collector;
        private List<Platform> _platforms;
        private Vector3 _pickerStartPosition;
        [HideInInspector] public int levelIndex;

        public void LoadLevel()
        {
            _assetManager = AssetManager.Instance;
            _uiManager = UIManager.Instance;
            _pool = PoolManager.Instance;
            _pickerStartPosition = new Vector3(0,0.6f,2.5f);
            levelIndex = PlayerPrefs.GetInt("Level", 1);
        }

        private void GenerateLevel()
        {
            var levelData = _assetManager.LoadLevel(levelIndex);
            var platformList = levelData.platformDatas;
            
            foreach (var platformData in platformList)
            {
                var platform = _pool.GetAvailablePlatform(platformData.platformType);
                platform.transform.position = platformData.position;

                if (platform.TryGetComponent(out CheckPoint checkPoint))
                    checkPoint.SetTarget(platformData.checkPointCount);
            }

            var objectGroupDatas = levelData.objectGroupDatas;
            foreach (var objectGroupData in objectGroupDatas)
            {
                var objectGroup = _pool.GetAvailableObjectGroup(objectGroupData.objectGroupType);
                objectGroup.transform.position = objectGroupData.position;
            }
            
            collector.transform.position = _pickerStartPosition;
            _uiManager.SetLevelText();
        }

        private void GenerateLevelInTime()
        {
            StartCoroutine(Timer());
            IEnumerator Timer()
            {
                yield return 0.1f.GetWait();
                _pool.DeactivateWholePool();
                levelIndex++;
                PlayerPrefs.SetInt("Level", levelIndex);
                GenerateLevel();
            }
        }

        private void StartLevel()
        {
            _pool.DeactivateWholePool();
            GenerateLevel();
        }

        private void OnEnable()
        {
            LEVEL_START += StartLevel;
            SUCCESS += GenerateLevelInTime;
            FAIL += StartLevel;
        }
        
        private void OnDisable()
        {
            LEVEL_START -= StartLevel;
            SUCCESS -= GenerateLevelInTime;
            FAIL -= StartLevel;
        }
    }
}
