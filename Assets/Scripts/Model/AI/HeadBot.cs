using System;
using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class HeadBot : MonoBehaviour, ICollision
    {
        #region PrivateData

        public event Action<InfoCollision> OnApplyDamageChange;

        #endregion


        #region ICollision

        public void OnCollision(InfoCollision info)
        {
            OnApplyDamageChange?.Invoke(new InfoCollision(info.Damage * 500,
                info.Contact, info.ObjCollision, info.Dir));
        }

        #endregion
    }
}