using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace template.ui.utils.mouse
{
    public class HoverScaleBehaviour: MonoBehaviour, MouseInteractionBehaviour
    {
        public float scaleHover = 1.1f;
        public float scaleDurationSec = 0.2f;
        public Vector3 lastKnownScale = Vector3.one;

        private void Start()
        {
            lastKnownScale = transform.localScale;       
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOKill();
            transform.DOScale(lastKnownScale * scaleHover, scaleDurationSec);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOKill();
            transform.localScale = lastKnownScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Do nothing
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // Do nothing       
        }
    }
}