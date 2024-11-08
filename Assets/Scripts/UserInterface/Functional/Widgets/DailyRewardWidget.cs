using System;
using Constants;
using Modules.General.Score;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Functional.Widgets
{
    public class DailyRewardWidget : MonoBehaviour
    {
        [SerializeField] private Text timeElapsedText;
        [SerializeField] private Button claimButton;
        [SerializeField] private Sprite activeClaimButtonImage;
        [SerializeField] private Sprite inactiveClaimButtonImage;

        private ScoreController _scoreController;
        private const string LastClaimDateKey = "LastClaimDate";

        [Inject]
        private void Construct(ScoreController scoreController)
        {
            _scoreController = scoreController;
        }
        
        private void Start()
        {
            claimButton.onClick.AddListener(ClaimReward);
            StartCoroutine(UpdateButtonState());
        }

        private void ClaimReward()
        {
            if (claimButton.interactable)
            {
                PlayerPrefs.SetString(LastClaimDateKey, DateTime.Now.ToString());
                PlayerPrefs.Save();
                _scoreController.AddExp(AppConstants.ExpPerDayReward);
            }
        }

        private System.Collections.IEnumerator UpdateButtonState()
        {
            while (true)
            {
                if (PlayerPrefs.HasKey(LastClaimDateKey))
                {
                    DateTime lastClaimDate = DateTime.Parse(PlayerPrefs.GetString(LastClaimDateKey));
                    TimeSpan timeSinceLastClaim = DateTime.Now - lastClaimDate;
                    if (timeSinceLastClaim.TotalDays < 1)
                    {
                        claimButton.interactable = false;
                        claimButton.image.sprite = inactiveClaimButtonImage;
                        TimeSpan timeUntilNextClaim = TimeSpan.FromDays(1) - timeSinceLastClaim;
                        timeElapsedText.text = $"next reward\n{timeUntilNextClaim.Hours}h {timeUntilNextClaim.Minutes}m {timeUntilNextClaim.Seconds}s";
                    }
                    else
                    {
                        claimButton.interactable = true;
                        claimButton.image.sprite = activeClaimButtonImage;
                        timeElapsedText.text = "50exp reward!";
                    }
                }
                else
                {
                    claimButton.interactable = true;
                    claimButton.image.sprite = activeClaimButtonImage;
                    timeElapsedText.text = "50exp reward!";
                }

                yield return new WaitForSeconds(1);
            }
        }
    }
}