using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.ActivityAnalytic.View.WeekProgressCharts.Models
{
    public class WeekProgressData<T>
    {
        private T Monday { get; set; }
        private T Tuesday { get; set; }
        private T Wednesday { get; set; }
        private T Thursday { get; set; }
        private T Friday { get; set; }
        private T Saturday { get; set; }
        private T Sunday { get; set; }
        
        public void SaveTodayProgression(string playerPrefsKeyPrefix, T value)
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    PlayerPrefs.SetString($"{playerPrefsKeyPrefix}_Monday", value?.ToString());
                    break;
                case DayOfWeek.Tuesday:
                    PlayerPrefs.SetString($"{playerPrefsKeyPrefix}_Tuesday", value?.ToString());
                    break;
                case DayOfWeek.Wednesday:
                    PlayerPrefs.SetString($"{playerPrefsKeyPrefix}_Wednesday", value?.ToString());
                    break;
                case DayOfWeek.Thursday:
                    PlayerPrefs.SetString($"{playerPrefsKeyPrefix}_Thursday", value?.ToString());
                    break;
                case DayOfWeek.Friday:
                    PlayerPrefs.SetString($"{playerPrefsKeyPrefix}_Friday", value?.ToString());
                    break;
                case DayOfWeek.Saturday:
                    PlayerPrefs.SetString($"{playerPrefsKeyPrefix}_Saturday", value?.ToString());
                    break;
                case DayOfWeek.Sunday:
                    PlayerPrefs.SetString($"{playerPrefsKeyPrefix}_Sunday", value?.ToString());
                    break;
            }

            PlayerPrefs.Save();
        }
        
        public void Load(string playerPrefsKeyPrefix)
        {
            var today = (int)DateTime.Now.DayOfWeek;
            // Monday = 0, Sunday = 6
            today = (today + 6) % 7;

            var keys = new[]
            {
                $"{playerPrefsKeyPrefix}_Monday",
                $"{playerPrefsKeyPrefix}_Tuesday",
                $"{playerPrefsKeyPrefix}_Wednesday",
                $"{playerPrefsKeyPrefix}_Thursday",
                $"{playerPrefsKeyPrefix}_Friday",
                $"{playerPrefsKeyPrefix}_Saturday",
                $"{playerPrefsKeyPrefix}_Sunday"
            };

            var values = new T[7];
            for (int i = 0; i < 7; i++)
            {
                if (i <= today)
                {
                    var str = PlayerPrefs.GetString(keys[i], null);
                    values[i] = string.IsNullOrEmpty(str) ? default : (T)System.Convert.ChangeType(str, typeof(T));
                }
                else
                {
                    values[i] = default;
                }
            }

            Monday = values[0];
            Tuesday = values[1];
            Wednesday = values[2];
            Thursday = values[3];
            Friday = values[4];
            Saturday = values[5];
            Sunday = values[6];
        }
        
        public List<T> GetAll()
        {
            return new List<T>
            {
                Monday,
                Tuesday,
                Wednesday,
                Thursday,
                Friday,
                Saturday,
                Sunday
            };
        }
    }
}