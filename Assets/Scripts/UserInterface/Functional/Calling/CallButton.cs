using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.SerializingModels;

namespace UserInterface.Functional.Calling
{
    public class CallButton : MonoBehaviour
    {
        [SerializeField] private Button callButton;
        [SerializeField] private float initialDelay = 0.1f;
        [SerializeField] private float delayStep = 0.1f;
        [SerializeField] private List<MoveableUIObject> objectsToCall;
        
        private bool _isCalled;
        
        private void Awake()
        {
            callButton.onClick.AddListener(CallObjects);
        }
        
        private void CallObjects()
        {
            var delay = initialDelay;
            
            if (_isCalled)
            {
                foreach (var callObject in objectsToCall)
                {
                    callObject.rectTransform.DOAnchorPos(callObject.outScreenPosition, delay);
                    delay += delayStep;
                }

                _isCalled = false;
            }
            else
            {
                foreach (var callObject in objectsToCall)
                {
                    callObject.rectTransform.DOAnchorPos(callObject.onScreenPosition, delay);
                    delay += delayStep;
                }
                _isCalled = true;
            }
        }
    }
}