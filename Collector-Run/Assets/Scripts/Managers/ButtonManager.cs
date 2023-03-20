using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ButtonManager : MonoBehaviour
    {
        [Header("PANELS:")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button nextLevelButton, retryButton;

        private UIManager _uiManager;

        // Start is called before the first frame update
        private void Start()
        {
            _uiManager = UIManager.Instance;
            
            playButton.onClick.AddListener(_uiManager.PlayButton);
            nextLevelButton.onClick.AddListener(_uiManager.NextLevelButton);
            retryButton.onClick.AddListener(_uiManager.RetryButton);
        }
    }
}
