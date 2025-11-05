using System;
using System.Collections.Generic;
using System.Numerics;
using Cysharp.Threading.Tasks;
using template.ui.utils.mouse;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector3 = UnityEngine.Vector3;

namespace utils
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AwaitableClickHandlerImpl: MonoBehaviour, IAwaitablePointerClickHandler
    {
        private AwaitableClickListener<GameObject> _listener = new();
        
        private async UniTask Start()
        {
            // Wait for all other components to initialize
            await UniTask.WaitForEndOfFrame();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _listener.notifyClick(gameObject);
        }

        public async UniTask onClickAwaitable()
        {
            await _listener.awaitClick();
        }
    }
}