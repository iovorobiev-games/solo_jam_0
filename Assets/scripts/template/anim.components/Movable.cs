using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace template.anim.components
{
    [RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
    public class Movable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IDragHandler
    {
        public bool holdToDrag = true;

        public bool isDragging
        {
            get;
            set;
        }
        private Camera mainCamera;
        private Vector3 offset;

        private List<UniTaskCompletionSource<Collider2D[]>> dragEndSource = new();
        private List<UniTaskCompletionSource> dragCancelSource = new();
        private List<UniTaskCompletionSource> dragStartSource = new();
        
        private HashSet<Collider2D> dragEndSet = new();
        
        private void Start()
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                mainCamera = FindObjectOfType<Camera>();
            }
        }

        private void Update()
        {
            if (holdToDrag)
            {
                return;
            }

            UpdatePositionOnDrag(mainCamera.ScreenToWorldPoint(Input.mousePosition));
        }

        public UniTask dragStart()
        {
            var uniTaskCompletionSource = new UniTaskCompletionSource();
            dragStartSource.Add(uniTaskCompletionSource); ;
            return uniTaskCompletionSource.Task;
        }
      

        public async UniTask<Collider2D[]> dragEnd()
        {
            var dragEndTask = new UniTaskCompletionSource<Collider2D[]>();
            dragEndSource.Add(dragEndTask);
            return await dragEndTask.Task;
        }
        
        public async UniTask dragCancel()
        {
            var dragCancelTask = new UniTaskCompletionSource();
            dragCancelSource.Add(dragCancelTask);
            await dragCancelTask.Task;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (holdToDrag)
            {
                StartDragging(eventData);
            }
        }

        private void StartDragging(PointerEventData eventData)
        {
            isDragging = true;

            // Calculate offset between object position and mouse position
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(eventData.position);
            worldPosition.z = transform.position.z; // Keep original Z position
            offset = transform.position - worldPosition;

            foreach (var taskSource in dragStartSource)
            {
                taskSource.TrySetResult();
            }
            dragStartSource.Clear();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (holdToDrag)
            {
                DragEnd();
            }
        }

        private void DragEnd()
        {
            if (isDragging)
            {
                foreach (var task in dragEndSource)
                {
                    task?.TrySetResult(dragEndSet.ToArray());
                }
                dragEndSource.Clear();
                dragEndSet.Clear();
            }

            isDragging = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isDragging)
            {
                dragEndSet.Add(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!isDragging) return;
            dragEndSet.Remove(other);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (holdToDrag)
            {
                UpdatePositionOnDrag(mainCamera.ScreenToWorldPoint(eventData.position));
            }
        }

        private void UpdatePositionOnDrag(Vector3 worldPosition)
        {
            if (!isDragging) return;
            worldPosition.z = transform.position.z; // Keep original Z position
            transform.position = worldPosition + offset;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (holdToDrag) return;
            if (isDragging)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    DragEnd();
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    CancelDrag();
                }
            }
            else
            {
                StartDragging(eventData);
            }
        }

        private void CancelDrag()
        {
            if (isDragging)
            {
                foreach (var task in dragCancelSource)
                {
                    task?.TrySetResult();
                }
                dragCancelSource.Clear();
            }

            isDragging = false;
        }
    }
}