using Constants;
using Modules.ActivityAnalytic.View.WeekProgressCharts.Abstraction;
using Modules.PersonalVocabulary;
using Modules.PersonalVocabulary.Data.Models;
using Zenject;

namespace Modules.ActivityAnalytic.View.WeekProgressCharts
{
    public class WordsCountWeekProgressChartView : WeekProgressChartView<int>
    {
        private Vocabulary _vocabulary;

        [Inject]
        private void Construct(VocabularyController vocabularyController)
        {
            _vocabulary = vocabularyController.Vocabulary;
        }
        
        protected override void SetPlayerPrefsKey()
        {
            PlayerPrefsKey = AppPlayerPrefsKeys.WordsCountThroughWeekKey;
        }

        protected override void SetTodayProgression()
        {
            TodayProgression = _vocabulary.GetWordsCount();
        }
    }
}