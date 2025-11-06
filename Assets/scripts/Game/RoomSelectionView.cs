using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;

namespace Game
{
    public class RoomSelectionView : MonoBehaviour
    {
        public SelectableRoomView[] rooms;
        public Transform[] slots;
        private GameObject slotPrefab;
        private CancellationTokenSource _lifecycleCancellation = new();
        
        private void Awake()
        {
            DI.sceneScope.register(this);
        }

        private async UniTask Start()
        {
            slotPrefab = await Resources.LoadAsync<GameObject>("Prefabs/slot").ToUniTask() as GameObject;
            Lifecycle().Forget();
        }
        
        private async UniTask Lifecycle()
        {
            GenerateNewRooms();
            while (!_lifecycleCancellation.IsCancellationRequested)
            {
                var cancellationSource = new CancellationTokenSource();
                var tasks = new UniTask[rooms.Length];
                for (var i = 0; i < rooms.Length; i++)
                {
                    tasks[i] = rooms[i].WaitForSelection().AttachExternalCancellation(cancellationSource.Token);
                }

                var selected = await UniTask.WhenAny(tasks);
                cancellationSource.Cancel();

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

                await GenerateNewRooms();
            }
            
        }

        private async UniTask GenerateNewRooms()
        {
            for (var i = 0; i < rooms.Length; i++)
            {
                rooms[i] = Instantiate(slotPrefab, slots[i]).GetComponent<SelectableRoomView>();
                var randomRoom = RandomRoomGenerator.generate();
                rooms[i].RoomVM = new RoomVM(randomRoom);
            }
        }
    }
}