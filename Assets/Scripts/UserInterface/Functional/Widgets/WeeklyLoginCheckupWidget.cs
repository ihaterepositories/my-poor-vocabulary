using System;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface.Functional.Widgets
{
    public class WeeklyLoginCheckupWidget : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> dayIcons;
        [SerializeField] private Sprite activeDayIcon;
        [SerializeField] private Sprite inactiveDayIcon;
        
        private List<string> _dayKeys = new List<string>
        {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"
        };
        
        private void Start()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                ResetData();
            }
        }
        
        private void ResetData()
        {
            foreach (var dayIcon in dayIcons)
            {
                dayIcon.sprite = inactiveDayIcon;
            }

            foreach (var dayKey in _dayKeys)
            {
                PlayerPrefs.SetInt(dayKey, 0);
            }
        }
    }
}