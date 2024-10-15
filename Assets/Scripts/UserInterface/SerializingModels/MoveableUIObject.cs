using UnityEngine;
using UnityEngine.Serialization;

namespace UserInterface.SerializingModels
{
    [System.Serializable]
    public class MoveableUIObject
    {
        public RectTransform rectTransform;
        public Vector3 onScreenPosition;
        public Vector3 outScreenPosition;
    }
}