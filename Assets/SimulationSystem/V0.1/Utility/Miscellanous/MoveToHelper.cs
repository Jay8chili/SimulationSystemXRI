using System.Collections;
using System.Threading;
using DG.Tweening;
using UnityEngine;
namespace SimulationSystem.V0._1.Utility.Miscellanous
{
    public class MoveToHelper : MonoBehaviour
    {
        public bool UseConstantReset;
        private int _resetDelay;
        private CancellationTokenSource _cancellation;
        private Vector3 currentvelocity;
        private float speed = 0.3f;
        private IEnumerator _lerp;
        public Transform ResetPos;
        public void ChangeParent(Transform newParentTransform)
        {
            transform.parent = newParentTransform;
        }

        public void MoveToPosition(Transform moveToTransform)
        {
            if (_lerp != null) StopCoroutine(_lerp);
            _lerp = MoveToPositionAsync(moveToTransform);
            StartCoroutine(_lerp);
        }

        private IEnumerator MoveToPositionAsync(Transform moveToTransform)
        {
            var destination = moveToTransform;
            var lerpAmount = 0f;
            transform.DOScale(destination.localScale, 1f);

            while (transform.position != destination.position)
            {
                lerpAmount = Mathf.Clamp01(lerpAmount += Time.deltaTime);
                transform.position = Vector3.SmoothDamp(transform.position, destination.position, ref currentvelocity, speed);
                transform.rotation = Quaternion.Lerp(transform.rotation, destination.rotation, lerpAmount * 0.1f);

                yield return null;
            }
        }
        public void MoveToPositionInstantly(Transform T)
        {
            transform.position = T.position;
        }
        public void MoveToPositionRotateInstantly(Transform T)
        {
            transform.SetPositionAndRotation(T.position, T.rotation);
        }
        private void LateUpdate()
        {
            if (UseConstantReset)
                MoveToPositionInstantly(ResetPos);
        }
    }
}



