using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using DG.Tweening;
using Game.data;
using Game.ui;
using template.anim.components;
using template.sprites;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class SelectableRoomView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Transform dungeonParent;
        public Floating floating;
        public Movable movable;
        private TilePlacement tilePlacement;
        private CancellationTokenSource _cancellationTokenSource = new();
        private Vector3 originalPosition;
        private DungeonVM dungeon;
        private HintUI hintUI;
        private Camera camera;
        private SpriteRenderer spriteRenderer;
        
        public TMP_Text cooldownText;
        
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
            spriteRenderer = GetComponent<SpriteRenderer>();
            camera = Camera.main == null ? FindFirstObjectByType<Camera>() : Camera.main;
            tilePlacement = DI.sceneScope.getInstance<TilePlacement>();
            dungeon = DI.sceneScope.getInstance<DungeonVM>();
            hintUI = DI.sceneScope.getInstance<HintUI>();
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
            while (!_cancellationTokenSource.IsCancellationRequested || !RoomVM.IsPlaced)
            {
                await movable.dragStart();
                DI.sceneScope.register(this, "last_selected_room");
                hintUI.hideHint().Forget();
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
                    transform.parent = dungeonParent;
                    RoomVM.IsPlaced = true;
                    movable.enabled = false;
                    placedLifecycle().Forget();
                }
                tilePlacement.highlightClosestPosition(false);
            }
        }

        private async UniTask placedLifecycle()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                await UniTask.WaitUntil(() => !RoomVM.IsActive());
                await spriteRenderer.DOColor(Color.gray, 0.5f).ToUniTask();
                cooldownText.gameObject.SetActive(true);
                while (!RoomVM.IsActive())
                {
                    var currentCooldownValue = RoomVM.currentCooldown;
                    cooldownText.text = currentCooldownValue + RTHelper.RESP;
                    await UniTask.WaitUntil(() => currentCooldownValue != RoomVM.currentCooldown);    
                }    
                cooldownText.gameObject.SetActive(false);
                await spriteRenderer.DOColor(Color.white, 0.5f).ToUniTask();
            }
        }

        public async void OnPointerEnter(PointerEventData eventData)
        {
            if (movable.isDragging)
            {
                return;
            }
            await hintUI.showHint(RoomVM, camera.ScreenToWorldPoint(eventData.position));
        }

        public async void OnPointerExit(PointerEventData eventData)
        {
            await hintUI.hideHint();
        }
    }
}