using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private async UniTask Start()
        {
            await UniTask.WaitForEndOfFrame();
            GameLoop().Forget();
        }

        private async UniTask GameLoop()
        {
            
        }
    }
}