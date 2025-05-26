using System;
using System.Collections.Generic;
using System.Linq;
using Modules.ActivityAnalytic.View.WeekProgressCharts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.ActivityAnalytic.View.WeekProgressCharts.Abstraction
{
    /// <summary>
    /// Parent class for all charts that show numeric data progression through the week.
    /// </summary>
    /// <typeparam name="T">T must be int, float or double</typeparam>
    public abstract class WeekProgressChartView<T> : MonoBehaviour 
    {
        [SerializeField] protected List<Image> dayBars;
        [SerializeField] protected List<TextMeshProUGUI> dayLabels;

        private WeekProgressData<T> _weekProgressData;
        protected string PlayerPrefsKey;
        protected T TodayProgression;

        private void Awake()
        {
            _weekProgressData = new WeekProgressData<T>();
            SetPlayerPrefsKey();
            SetTodayProgression();
            _weekProgressData.SaveTodayProgression(PlayerPrefsKey, TodayProgression);
        }

        private void Start()
        {
            _weekProgressData.Load(PlayerPrefsKey);
            ShowProgressionThroughWeek();
        }

        protected abstract void SetPlayerPrefsKey();
        protected abstract void SetTodayProgression();

        private void ShowProgressionThroughWeek()
        {
            var weekData = _weekProgressData.GetAll();

            // Convert all values to double
            List<double> weekDataDoubles = weekData
                .Select(x => x != null ? System.Convert.ToDouble(x) : 0d)
                .ToList();

            double maxValue = weekDataDoubles.Count > 0 ? weekDataDoubles.Max() : 0d;

            for (int i = 0; i < weekData.Count; i++)
            {
                double currentValue = weekDataDoubles[i];
                dayBars[i].fillAmount = maxValue > 0 ? (float)(currentValue / maxValue) : 0f;
                
                var today = (int)DateTime.Now.DayOfWeek;
                // Monday = 0, Sunday = 6
                today = (today + 6) % 7;

                if (i <= today)
                {
                    dayLabels[i].text = currentValue.ToString("0.#");
                }
                else
                {
                    dayLabels[i].text = "x";
                }
            }
        }
    }
}