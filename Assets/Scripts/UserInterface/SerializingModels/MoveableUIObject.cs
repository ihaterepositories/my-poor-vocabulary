using UnityEngine;

namespace UserInterface.SerializingModels
{
    [System.Serializable]
    public class MoveableUIObject
    {
        public RectTransform rectTransform;
        public Vector3 onScreenPosition;
        
        private Vector3 _outScreenPosition = Vector3.zero;
        
        public void SetOutScreenPosition(Vector3 outScreenPosition)
        {
            _outScreenPosition = outScreenPosition;
        }

        public Vector3 OutScreenPosition => _outScreenPosition;
    }
}