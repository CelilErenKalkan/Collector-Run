using System.Collections;
using System.Collections.Generic;
using Bases;
using Extenders;
using Game.PlatformSystem;
using UnityEngine;
using static Extenders.Actions;

namespace Managers.Level
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private PoolManager _pool;
        [SerializeField] private PickerBase pickerBase;
        private List<PlatformBase> _platformBases;
        private int _levelIndex;
        private Vector3 _pickerStartPosition;

        public void LoadLevel()
        {
            _pool = PoolManager.Instance;
            _pickerStartPosition = new Vector3(0,0.6f,2.5f);
            _levelIndex = PlayerPrefs.GetInt("Level", 1);
        }

        public void GenerateLevel()
        {
            var levelData = AssetManager.Instance.LoadLevel(_levelIndex);
            var platformList = levelData.platformDatas;
            foreach (var platformData in platformList)
            {
                var platform = _pool.GetAvailablePlatform(platformData.platformType);
                platform.transform.position = platformData.position;
                
                if (platform.PlatformType == PlatformType.CHECKPOINT)
                    platform.GetComponent<CheckPoint>()?.SetTarget(platformData.checkPointCount);
                
            }

            var ballPacks = levelData.ballPackDatas;
            foreach (var ballPack in ballPacks)
            {
                var ball = _pool.GetAvailableBallPack(ballPack.ballPackType);
                ball.transform.position = ballPack.position;
            }
            
            pickerBase.transform.position = _pickerStartPosition;
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
            Success += GenerateLevelInTime;
            Fail += RestartLevel;
        }
        
        private void OnDisable()
        {
            Success -= GenerateLevelInTime;
            Fail -= RestartLevel;
        }
    }
}
