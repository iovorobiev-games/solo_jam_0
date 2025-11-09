using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public GameObject gameEndScreen;
        public TMP_Text gameEndText;
        
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
            var budget = new[] { 5, 5, 5, 5 };
            var strength = new[] { 5, 10, 15, 20 };
            while (true)
            {
                bool gameOver = false;
                gameEndScreen.SetActive(false);
                for (int i = 0; i < strength.Length; i++)
                {
                    player.Budget = budget[i];
                    enemy.appear();
                    dungeon.tick();
                    dungeon.retriggerSkills();
                    await roomSelectionView.Lifecycle();
                    await roomSelectionView.hide();
                    var enemyData = new EnemyVM
                    {
                        HitPoints = strength[i]
                    };
                    DI.sceneScope.register(enemyData);
                    await enemy.passDungeon();
                    if (enemyData.HitPoints > 0)
                    {
                        gameOver = true;
                        break;
                    }
                }
                gameEndScreen.SetActive(true);
                if (gameOver)
                {
                    gameEndText.text = "Game over";
                }
                else
                {
                    gameEndText.text = "You won";
                }
                await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.R));
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }
    }
}