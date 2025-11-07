using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private async UniTask Start()
        {
            await UniTask.WaitForFixedUpdate();
            GameLoop().Forget();
        }

        private async UniTask GameLoop()
        {
            var player = DI.sceneScope.getInstance<Player>();
            var enemy = DI.sceneScope.getInstance<EnemyView>();
            var roomSelectionView = DI.sceneScope.getInstance<RoomSelectionView>();
            var dungeon = DI.sceneScope.getInstance<DungeonVM>();
            while (true)
            {
                player.setBudget(5);
                enemy.appear();
                dungeon.tick();
                await roomSelectionView.Lifecycle();
                await roomSelectionView.hide();
                var enemyData = new EnemyVM
                {
                    HitPoints = 5
                };
                DI.sceneScope.register(enemyData);
                await enemy.passDungeon();
                if (enemyData.HitPoints > 0)
                {
                    break;
                }
            }
            Debug.Log("Game over, lol");
        }
    }
}