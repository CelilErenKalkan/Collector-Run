using System.Collections;
using System.Collections.Generic;
using Extenders;
using Game.PickerSystem;
using Game.PlatformSystem.PlatformTypes;
using UnityEngine;
using static Extenders.Actions;

namespace Managers.Level
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private AssetManager _assetManager;
        private PoolManager _pool;
        [SerializeField] private Picker picker;
        private List<Platform> _platforms;
        private Vector3 _pickerStartPosition;
        private int _levelIndex;

        public void LoadLevel()
        {
            _assetManager = AssetManager.Instance;
            _pool = PoolManager.Instance;
            _pickerStartPosition = new Vector3(0,0.6f,2.5f);
            _levelIndex = PlayerPrefs.GetInt("Level", 1);
        }

        public void GenerateLevel()
        {
            var levelData = _assetManager.LoadLevel(_levelIndex);
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
            
            picker.transform.position = _pickerStartPosition;
        }

        private void GenerateLevelInTime()
        {
            StartCoroutine(Timer());
            IEnumerator Timer()
            {
                yield return 2.0f.GetWait();
                _pool.DeactivateWholePool();
                _levelIndex++;
                PlayerPrefs.SetInt("Level", _levelIndex);
                GenerateLevel();
            }
        }

        private void RestartLevel()
        {
            _pool.DeactivateWholePool();
            GenerateLevel();
        }

        private void OnEnable()
        {
            SUCCESS += GenerateLevelInTime;
            FAIL += RestartLevel;
        }
        
        private void OnDisable()
        {
            SUCCESS -= GenerateLevelInTime;
            FAIL -= RestartLevel;
        }
    }
}
