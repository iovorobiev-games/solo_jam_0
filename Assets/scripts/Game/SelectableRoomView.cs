using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using DG.Tweening;
using Game.data;
using template.anim.components;
using template.sprites;
using UnityEngine;

namespace Game
{
    public class SelectableRoomView : MonoBehaviour
    {
        public Floating floating;
        public Movable movable;
        private TilePlacement tilePlacement;
        private CancellationTokenSource _cancellationTokenSource = new();
        private Vector3 originalPosition;
        private DungeonVM dungeon;

        private RoomVM field;
        public RoomVM RoomVM
        {
            get => field;
            set
            {
                field = value; 
                var spriteSheet = SpriteLoader.load("Sprites", RoomDB.spritesheetpath);
                GetComponent<SpriteRenderer>().sprite = spriteSheet.getAtSuffix(RoomVM.Room.spritePath);
            }
        }

        private async UniTask Start()
        {
            originalPosition = transform.position;

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
            dungeon = DI.sceneScope.getInstance<DungeonVM>();
            controlBehaviour().Forget();
        }
        
        public async UniTask WaitForSelection()
        {
            await movable.dragEnd().AttachExternalCancellation(_cancellationTokenSource.Token);
        }

        public async UniTask hide()
        {
            var material = GetComponent<Renderer>().material;
            await material.DOFloat(0f, "_Progress", 1f).ToUniTask();
        }

        public async UniTask remove()
        {
            _cancellationTokenSource.Cancel();
            Destroy(gameObject);
        }
        
        private async UniTask controlBehaviour()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                await movable.dragStart();
                floating.stop();
                tilePlacement.highlightClosestPosition(true);
                var (isFinished, _) = await UniTask.WhenAny(movable.dragEnd(), movable.dragCancel());
                if (!isFinished)
                {
                    await transform.DOMove(originalPosition, 0.5f).ToUniTask();
                    floating.launch();
                }
                else
                {
                    transform.position = tilePlacement.GetClosestTilePosition(transform.position);
                    dungeon.AddRoom(RoomVM, tilePlacement.WorldToCell(transform.position));
                }
                tilePlacement.highlightClosestPosition(false);
            }
            
        }
    }
}