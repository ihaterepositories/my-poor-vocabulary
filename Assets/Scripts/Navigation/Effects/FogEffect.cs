using DG.Tweening;
using UnityEngine;

namespace Navigation.Effects
{
    public class FogEffect : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Start()
        {
            Decrease(.5f);
        }

        private void OnDisable()
        {
            spriteRenderer.DOKill();
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