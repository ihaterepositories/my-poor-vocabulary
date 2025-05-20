using Constants;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Functional.Widgets
{
    public class MiniGamesActivityInfoWidget : MonoBehaviour
    {
        [SerializeField] private Text widgetText;
        
        private void Start()
        {
            ShowMiniGamesActivityInfo();
        }

        private void ShowMiniGamesActivityInfo()
        {
            widgetText.text = 
                $"Played Games Count: {PlayerPrefs.GetInt(AppPlayerPrefsKeys.PlayedMiniGamesCountKey, 0)}\n" +
                $"Average Score: {PlayerPrefs.GetFloat(AppPlayerPrefsKeys.AverageMiniGamesScoreKey, 0)}%\n" +
                $"Last Played Game Score: {PlayerPrefs.GetInt(AppPlayerPrefsKeys.LastPlayedMiniGameScoreKey, 0)}%";
        }
    }
}