using DG.Tweening;
using UnityEngine;

namespace Modules.General.Navigation.Effects
{
    public class FogEffect : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Start()
        {
            Decrease(.5f);
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