using Constants;
using Modules.ActivityAnalytic.View.WeekProgressCharts.Abstraction;
using UnityEngine;

namespace Modules.ActivityAnalytic.View.WeekProgressCharts
{
    public class GamesPlayedWeekProgressChartView : WeekProgressChartView<int>   
    {
        protected override void SetPlayerPrefsKey()
        {
            PlayerPrefsKey = AppPlayerPrefsKeys.GamesPlayedThroughWeekKey;
        }

        protected override void SetTodayProgression()
        {
            TodayProgression = PlayerPrefs.GetInt(AppPlayerPrefsKeys.PlayedMiniGamesCountKey, 0);
        }
    }
}