using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UserInterface.Functional.ScenesLoading.Effects
{
    public class FogEffect : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Start()
        {
            Decrease(1f);
        }

        public void Increase(float duration)
        {
            spriteRenderer.DOFade(1f, duration);
        }

        public void Decrease(float duration)
        {
            spriteRenderer.DOFade(0f, duration);
        }
    }
}