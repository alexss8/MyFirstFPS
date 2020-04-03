using System;
using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class DestroyPoint : MonoBehaviour
    {
        #region PrivateData

        public event Action<GameObject> OnFinishChange = delegate (GameObject o) { };

        #endregion


        #region UnityMethods

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Bot>())
            {
                OnFinishChange.Invoke(gameObject);
            }
        }

        #endregion
    }
}