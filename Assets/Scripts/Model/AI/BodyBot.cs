using System;
using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class BodyBot : MonoBehaviour, ICollision
    {
        #region PrivateData

        public event Action<InfoCollision> OnApplyDamageChange;

        #endregion


        #region ICollision

        public void OnCollision(InfoCollision info)
        {
            OnApplyDamageChange?.Invoke(info);
        }

        #endregion
    }
}