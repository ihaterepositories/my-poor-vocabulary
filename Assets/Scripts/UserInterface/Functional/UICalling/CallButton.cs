using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.SerializingModels;

namespace UserInterface.Functional.UICalling
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
                var objectToCallReversed = objectsToCall.AsEnumerable()!.Reverse().ToList();
                foreach (var callObject in objectToCallReversed)
                {
                    callObject.rectTransform.DOAnchorPos(callObject.outScreenPosition, delay);
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