using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using template.anim.components;
using UnityEngine;

namespace Game
{
    public class SelectableRoomView : MonoBehaviour
    {
        public Floating floating;
        public Movable movable;
        private TilePlacement tilePlacement;

        private async UniTask Start()
        {
            await UniTask.WaitForEndOfFrame();
            if (floating == null)
            {
                floating = GetComponent<Floating>();
                
            }

            if (movable == null)
            {
                movable = GetComponent<Movable>();
            }

            tilePlacement = DI.sceneScope.getInstance<TilePlacement>();
            controlBehaviour().Forget();
        }

        private async UniTask controlBehaviour()
        {
            await movable.dragStart();
            floating.stop();
            tilePlacement.highlightClosestPosition(true);
            await movable.dragEnd();
            tilePlacement.highlightClosestPosition(false);
            transform.position = tilePlacement.GetClosestTilePosition(transform.position);
        }
    }
}