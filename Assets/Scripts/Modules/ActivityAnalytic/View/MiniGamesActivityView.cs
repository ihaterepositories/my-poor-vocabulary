using Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.ActivityAnalytic.View
{
    public class MiniGamesActivityView : MonoBehaviour
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