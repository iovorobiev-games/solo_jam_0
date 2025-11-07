using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class EnemyView : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        Vector3 originalPosition;
        
        private void Awake()
        {
            DI.sceneScope.register(this);
        }

        private void Start()
        {
            originalPosition = transform.position;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public async UniTask passDungeon()
        {
            gameObject.SetActive(true);
            var enemy = DI.sceneScope.getInstance<EnemyVM>();
            var tileplacement = DI.sceneScope.getInstance<TilePlacement>();
            var dungeon = DI.sceneScope.getInstance<DungeonVM>();
            var path = dungeon.getPathThroughDungeon();
            foreach (var waypoint in path)
            {
                var waypointWorld = tileplacement.tilemap.CellToWorld(waypoint) + Vector3.one * 0.3f;
                await transform.DOMove(waypointWorld, 0.5f);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                var room = dungeon.GetRoom(waypoint);
                var isAlive = enemy.takeDamage(room.getDamage());
                if (isAlive)
                {
                    await room.SetActive(false);
                }
                else
                {
                    await die();
                    return;
                }
            }
            
        }

        public void appear()
        {
            spriteRenderer.color = Color.white;
        }

        private async UniTask die()
        {
            await spriteRenderer.DOFade(0f, 0.5f).ToUniTask();
            transform.position = originalPosition;
            gameObject.SetActive(false);
        }
    }
}