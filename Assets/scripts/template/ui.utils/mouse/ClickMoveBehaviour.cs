using UnityEngine;
using UnityEngine.EventSystems;

namespace template.ui.utils.mouse
{
    public class ClickMoveBehaviour : MonoBehaviour, MouseInteractionBehaviour
    {
        public Vector3 deltaMoveOnClick = Vector3.zero;
        private Vector3 originalPos;
        public void OnPointerEnter(PointerEventData eventData)
        {
            // Do Nothing
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Do Nothing
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            originalPos = transform.position;
            if (deltaMoveOnClick != Vector3.zero)
            {
                transform.position += deltaMoveOnClick;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.position = originalPos;
        }
    }
}