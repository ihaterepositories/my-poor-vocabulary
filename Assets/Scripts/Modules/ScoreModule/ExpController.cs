using System;
using Constants;
using UnityEngine;

namespace Modules.ScoreModule
{
    public class ExpController : MonoBehaviour
    {
        private int _exp;
        private readonly int _levelSplitExpCount = AppConstants.LevelSplitExpCount;
        
        public static event Action OnExpChanged;

        private void Start()
        {
            _exp = PlayerPrefs.GetInt("Exp");
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("Exp", _exp);
            PlayerPrefs.Save();
        }

        public int GetLevel() => _exp / _levelSplitExpCount;
        public int GetExpCountToReachNewLevel() => _levelSplitExpCount - (_exp % _levelSplitExpCount);
        public int GetCurrentLevelExp() => _exp % _levelSplitExpCount;

        public void AddExp(int count)
        {
            _exp += count;
            OnExpChanged?.Invoke();
        }
    }
}