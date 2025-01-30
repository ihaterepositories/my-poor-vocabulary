using Modules.MiniGamesCore.Interfaces;
using Modules.ScoreModule;
using UnityEngine;

namespace UserInterface.Sound
{
    public class EventSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        
        [SerializeField] private AudioClip bootSound;
        [SerializeField] private AudioClip expEarnedSound;
        [SerializeField] private AudioClip wrongAnswerSound;

        private void Start()
        {
            audioSource.PlayOneShot(bootSound);
        }

        private void OnEnable()
        {
            ScoreController.OnExpChanged += PlayExpEarnedSound;
            MiniGameController.OnWrongAnswer += PlayWrongAnswerSound;
        }
        
        private void OnDisable()
        {
            ScoreController.OnExpChanged -= PlayExpEarnedSound;
            MiniGameController.OnWrongAnswer -= PlayWrongAnswerSound;
        }

        private void PlayExpEarnedSound()
        {
            audioSource.PlayOneShot(expEarnedSound);
        }
        
        public void PlayWrongAnswerSound()
        {
            audioSource.PlayOneShot(wrongAnswerSound);
        }
    }
}