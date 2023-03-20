using TMPro;
using UnityEngine;

namespace Extenders
{
    public class CanvasHelper : MonoSingleton<CanvasHelper>
    {
        [Header("Game Panels")]
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject successPanel;
        [SerializeField] private GameObject failPanel;

        [Header("Texts")]
        [SerializeField] private TMP_Text levelText;
        
        public void SetPanel(CanvasPanel panel, bool state)
        {
            switch (panel)
            {
                case CanvasPanel.Start:
                    startPanel.SetActive(state);
                    return;
                case CanvasPanel.Success:
                    successPanel.SetActive(state);
                    return;
                case CanvasPanel.Fail:
                    failPanel.SetActive(state);
                    return;
                default:
                    return;
            }
        }
        
        public void SetLevelText(int levelNo)
        {
            levelText.text = "LEVEL " + levelNo;
        }
    }

    public enum CanvasPanel
    {
        Start,
        Success,
        Fail
    }
}