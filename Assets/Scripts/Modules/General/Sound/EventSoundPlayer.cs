using System;
using Modules.VocabularyModule.Data.Input;
using UnityEngine;

namespace Modules.General.Sound
{
    public class EventSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        
        [SerializeField] private AudioClip expEarnedSound;
        [SerializeField] private AudioClip wrongAnswerSound;

        private void OnEnable()
        {
            WordAddController.OnWordAdded += PlayExpEarnedSound;
        }
        
        private void OnDisable()
        {
            WordAddController.OnWordAdded -= PlayExpEarnedSound;
        }

        private void PlayExpEarnedSound()
        {
            audioSource.PlayOneShot(expEarnedSound);
        }

        private void PlayWrongAnswerSound()
        {
            audioSource.PlayOneShot(wrongAnswerSound);
        }
    }
}