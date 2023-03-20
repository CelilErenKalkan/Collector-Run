using Game.PickerSystem;
using Managers.Level;
using UnityEngine;
using static Extenders.Actions;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private LevelManager _levelManager;
        private AssetManager _assetManager;
        [SerializeField] private Picker picker;
        
        private void Awake()
        {
            _assetManager = AssetManager.Instance;
            _levelManager = LevelManager.Instance;

            _assetManager.LoadAssets();
            _levelManager.LoadLevel();
        }

        private void OnLevelStart()
        {
            picker.gameObject.SetActive(true);
            picker.Initialize();
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