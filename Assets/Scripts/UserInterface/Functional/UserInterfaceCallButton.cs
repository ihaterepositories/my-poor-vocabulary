using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.SerializingModels;

namespace UserInterface.Functional
{
    public class UserInterfaceCallButton : MonoBehaviour
    {
        [SerializeField] private Button callButton;
        [SerializeField] private float initialDelay = 0.1f;
        [SerializeField] private float delayStep = 0.1f;
        [SerializeField] private List<MoveableUIObject> objectsToCall;
        
        private bool _isCalled;
        
        private void Awake()
        {
            SetOutScreenPositions();
            callButton.onClick.AddListener(CallObjects);
        }

        private void OnDisable()
        {
            StopAllAnimationsOfCalledObjects();
        }

        private void SetOutScreenPositions()
        {
            foreach (var callObject in objectsToCall)
            {
                callObject.SetOutScreenPosition(new Vector3(callObject.rectTransform.anchoredPosition.x, callObject.rectTransform.anchoredPosition.y, 0f));
            }
        }
        
        private void CallObjects()
        {
            StopAllAnimationsOfCalledObjects();
            
            var delay = initialDelay;
            
            if (_isCalled)
            {
                var objectToCallReversed = objectsToCall.AsEnumerable()!.Reverse().ToList();
                foreach (var callObject in objectToCallReversed)
                {
                    callObject.rectTransform.DOAnchorPos(callObject.OutScreenPosition, delay);
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
        
        private void StopAllAnimationsOfCalledObjects()
        {
            foreach (var callObject in objectsToCall)
            {
                callObject.rectTransform.DOKill();
            }
        }
    }
}