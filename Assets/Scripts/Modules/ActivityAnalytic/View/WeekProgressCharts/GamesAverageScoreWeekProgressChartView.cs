using Constants;
using Modules.ActivityAnalytic.View.WeekProgressCharts.Abstraction;
using UnityEngine;

namespace Modules.ActivityAnalytic.View.WeekProgressCharts
{
    public class GamesAverageScoreWeekProgressChartView : WeekProgressChartView<float>
    {
        protected override void SetPlayerPrefsKey()
        {
            PlayerPrefsKey = AppPlayerPrefsKeys.AverageGameScoreThroughWeekKey;
        }

        protected override void SetTodayProgression()
        {
            TodayProgression = PlayerPrefs.GetFloat(AppPlayerPrefsKeys.AverageMiniGamesScoreKey, 0);
        }
    }
}