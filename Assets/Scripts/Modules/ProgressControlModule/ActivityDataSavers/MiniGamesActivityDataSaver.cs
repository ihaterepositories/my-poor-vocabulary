using Constants;
using Modules.MiniGamesCore.Abstraction;
using UnityEngine;

namespace Modules.ProgressControlModule.ActivityDataSavers
{
    public class MiniGamesActivityDataSaver : MonoBehaviour
    {
        private void OnEnable()
        {
            MiniGameController.OnGameEndWithScore += SaveMiniGamesActivityData;
        }
        
        private void OnDisable()
        {
            MiniGameController.OnGameEndWithScore -= SaveMiniGamesActivityData;
        }

        private void SaveMiniGamesActivityData(int lastPlayedGameScore)
        {
            int newTotalScore = PlayerPrefs.GetInt(AppPlayerPrefsKeys.TotalMiniGamesScoreKey, 0) + lastPlayedGameScore;
            int newPlayedGamesCount = PlayerPrefs.GetInt(AppPlayerPrefsKeys.PlayedMiniGamesCountKey, 0) + 1;
            float newAverageScore = (float)newTotalScore / newPlayedGamesCount;
            
            PlayerPrefs.SetInt(AppPlayerPrefsKeys.TotalMiniGamesScoreKey, newTotalScore);
            PlayerPrefs.SetInt(AppPlayerPrefsKeys.PlayedMiniGamesCountKey, newPlayedGamesCount);
            PlayerPrefs.SetFloat(AppPlayerPrefsKeys.AverageMiniGamesScoreKey, newAverageScore);
        }
    }
}