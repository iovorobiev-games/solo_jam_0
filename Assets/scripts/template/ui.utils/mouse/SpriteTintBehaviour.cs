using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace template.ui.utils.mouse
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteTintBehaviour: MonoBehaviour, MouseInteractionBehaviour
    {
        public SpriteRenderer spriteRenderer;

        public Color defaultTint = Color.white;
        public Color hoverTint = Color.clear;
        public Color mouseDownTint = Color.clear;

        private bool isMouseDown;
        private bool isMouseOver;
        private void Start()
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOver = true;
            if (hoverTint != Color.clear && spriteRenderer != null)
            {
                spriteRenderer.color = hoverTint;
            }        
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMouseOver = false;
            spriteRenderer.color = (isMouseDown && mouseDownTint != Color.clear)? mouseDownTint : defaultTint;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isMouseDown = true;

            if (mouseDownTint != Color.clear && spriteRenderer != null)
            {
                spriteRenderer.color = mouseDownTint;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = (isMouseOver && hoverTint != Color.clear)? hoverTint : defaultTint;    
            }
        }
    }
}