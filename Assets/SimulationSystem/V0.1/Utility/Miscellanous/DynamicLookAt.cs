using UnityEngine;
namespace SimulationSystem.V0._1.Utility.Miscellanous
{
    public class DynamicLookAt : MonoBehaviour
    {
        public GameObject ui;
        public GameObject followTransform;
        private void LateUpdate()
        {
            DynamicRotate();
        }

        private void DynamicRotate()
        {
            Vector3 direction = (followTransform.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(-direction, Vector3.up);
            transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
        }
    }
}