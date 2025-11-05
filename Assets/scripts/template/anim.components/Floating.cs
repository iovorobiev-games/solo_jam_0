using System;
using DG.Tweening;
using UnityEngine;

namespace template.anim.components
{
    public class Floating : MonoBehaviour
    {
        public bool launchOnStart;
        public float amplitude = 1f;
        public float speedSec = 1f;
        public float shakeScale = 0.5f;

        private Vector3 _initPos;
        private Quaternion _initRot;
        private int hash;
        private string floatSequenceId;
        private string shakeSequenceId;
        private string floatDownId;
        private string floatUpId;
        private string shakeLeftId;
        private string shakeRightId;

        private void Start()
        {
            hash = GetHashCode();
            floatSequenceId = "float_sequence@" + hash;
            shakeSequenceId = "shake_sequence@" + hash;
            floatDownId = "float_down@" + hash;
            floatUpId = "float_up@" + hash;
            shakeLeftId = "shake_left@" + hash;
            shakeRightId = "shake_right@" + hash;
            _initPos = transform.position;
            _initRot = transform.rotation;
            if (launchOnStart)
            {
                launch();
            }
        }

        public void stop()
        {
            DOTween.Kill(floatSequenceId);
            DOTween.Kill(shakeSequenceId);
            DOTween.Kill(floatDownId);
            DOTween.Kill(floatUpId);
            DOTween.Kill(shakeLeftId);
            DOTween.Kill(shakeRightId);
            transform.position = _initPos;
            transform.rotation = _initRot;
        }
        public void launch()
        {
            transform.DOKill();
            transform.position = _initPos;
            DOTween.Sequence()
                .Append(transform.DOMoveY(transform.position.y - amplitude / 2f, speedSec / 2f)
                    .SetLoops(2, LoopType.Yoyo).SetId(floatDownId))
                .Append(transform.DOMoveY(transform.position.y + amplitude / 2f, speedSec / 2f).SetId(floatUpId)
                    .SetLoops(2, LoopType.Yoyo)).SetLoops(-1, LoopType.Restart).SetId(floatSequenceId);
            if (shakeScale > 0f)
            {
                DOTween.Sequence()
                    .Append(transform
                        .DORotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - shakeScale),
                            speedSec / 4f).SetLoops(2, LoopType.Yoyo).SetId(shakeLeftId))
                    .Append(transform
                        .DORotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + shakeScale),
                            speedSec / 4f).SetLoops(2, LoopType.Yoyo).SetId(shakeRightId))
                    .SetLoops(-1, LoopType.Restart).SetId(shakeSequenceId);
            }
        }
    }
}