using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using Game.data;
using UnityEngine;

namespace Game
{
    public class RoomSelectionView : MonoBehaviour
    {
        public Transform dungeonParent;
        public RoomVM[] selectableRoomsArray = new RoomVM[3];
        public SelectableRoomView[] rooms;
        public Transform[] slots;
        private GameObject slotPrefab;
        private CancellationTokenSource _lifecycleCancellation = new();
        private Player player;

        public RoomVM CurrentSelected
        {
            get;
            private set;
        }
        
        private async void Awake()
        {
            DI.sceneScope.register(this);
            player = DI.sceneScope.getInstance<Player>();
            slotPrefab = await Resources.LoadAsync<GameObject>("Prefabs/slot").ToUniTask() as GameObject;
        }
        public async UniTask Lifecycle()
        {
           gameObject.SetActive(true);
           generateRooms();
            while (player.hasBudgetFor(1))
            {
                await GenerateNewRooms();
                var cancellationSource = new CancellationTokenSource();
                var tasks = new UniTask[rooms.Length];
                for (var i = 0; i < rooms.Length; i++)
                {
                    tasks[i] = rooms[i].WaitForSelection().AttachExternalCancellation(cancellationSource.Token);
                }

                var selected = await UniTask.WhenAny(tasks);
                cancellationSource.Cancel();
                generateRooms();
                player.spend(rooms[selected].RoomVM.getCost());
                tasks = new UniTask[rooms.Length - 1];
                var j = 0;
                for (var i = 0; i < rooms.Length; i++)
                {
                    if (i == selected)
                    {
                        continue;
                    }

                    tasks[j] = rooms[i].hide();
                    j++;
                }
                await UniTask.WhenAll(tasks);
                for (var i = 0; i < rooms.Length; i++)
                {
                    if (i == selected)
                    {
                        continue;
                    }
                    await rooms[i].remove();
                }
            }
        }

        public async UniTask hide()
        {
            gameObject.SetActive(false);
        }

        private void generateRooms()
        {
            for (var i = 0; i < selectableRoomsArray.Length; i++)
            {
                selectableRoomsArray[i] = new RoomVM(RandomRoomGenerator.generate());
            }
        }

        private async UniTask GenerateNewRooms()
        {
            for (var i = 0; i < rooms.Length; i++)
            {
                rooms[i] = Instantiate(slotPrefab, slots[i]).GetComponent<SelectableRoomView>();
                rooms[i].dungeonParent = dungeonParent;
                var randomRoom = selectableRoomsArray[i];
                rooms[i].RoomVM = randomRoom;
            }
        }
    }
}