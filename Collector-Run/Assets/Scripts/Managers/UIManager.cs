using Extenders;
using Managers.Level;
using static Extenders.Actions;

namespace Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private LevelManager _levelManager;
        private CanvasHelper _canvasHelper;

        private void Start()
        {
            _levelManager = LevelManager.Instance;
            if (TryGetComponent<CanvasHelper>(out var helper)) _canvasHelper = helper;
        }

        public void PlayButton()
        {
            _canvasHelper.SetPanel(CanvasPanel.Start, false);
            _canvasHelper.SetLevelText(_levelManager.levelIndex);
            LEVEL_START?.Invoke();
        }

        public void NextLevelButton()
        {
            _canvasHelper.SetPanel(CanvasPanel.Success, false);
            SUCCESS?.Invoke();
        }

        public void RetryButton()
        {
            _canvasHelper.SetPanel(CanvasPanel.Fail, false);
            FAIL?.Invoke();
        }

        private void OnLevelEnd(bool isSuccess)
        {
            _canvasHelper.SetPanel(isSuccess ? CanvasPanel.Success : CanvasPanel.Fail, true);
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