using System;
using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class HeadBot : MonoBehaviour, ICollision
    {
        public event Action<InfoCollision> OnApplyDamageChange;
        public void OnCollision(InfoCollision info)
        {
            OnApplyDamageChange?.Invoke(new InfoCollision(info.Damage * 500,
                info.Contact, info.ObjCollision, info.Dir));
        }
    }
}