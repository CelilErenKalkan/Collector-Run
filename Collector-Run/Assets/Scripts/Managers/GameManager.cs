using Game.CollectorSystem;
using Managers.Level;
using UnityEngine;
using static Extenders.Actions;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private LevelManager _levelManager;
        private AssetManager _assetManager;
        [SerializeField] private Collector collector;
        
        private void Awake()
        {
            _assetManager = AssetManager.Instance;
            _levelManager = LevelManager.Instance;

            _assetManager.LoadAssets();
            _levelManager.LoadLevel();
        }

        private void OnLevelStart()
        {
            collector.gameObject.SetActive(true);
            collector.Initialize();
        }

        private void OnEnable()
        {
            LEVEL_START += OnLevelStart;
        }
        
        private void OnDisable()
        {
            LEVEL_START -= OnLevelStart;
        }
    }
}