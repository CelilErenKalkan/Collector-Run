using Game.PickerSystem;
using Managers.Level;
using UnityEngine;

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
            picker.Initialize();
        }

        private void Start()
        {
            _levelManager.GenerateLevel();
        }
    }
}
