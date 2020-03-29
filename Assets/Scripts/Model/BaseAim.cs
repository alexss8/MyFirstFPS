using System;
using UnityEngine;


namespace GeekBrainsFPS
{
    public abstract class BaseAim : BaseObjectScene, ICollision, ISelectObj
    {
        #region PrivateData

        public event Action<int> OnPointChange = delegate { };

        #endregion


        #region Fields

        public float Hp = 100;

        [SerializeField] protected float _timeToDestroy = 10.0f;
        [SerializeField] protected int _points = 1;

        #endregion


        #region Methods

        public void HandleOnPointChange()
        {
            OnPointChange.Invoke(_points);
        }

        #endregion


        #region ICollision

        public abstract void CollisionEnter(InfoCollision info);

        #endregion


        #region ISelectObj

        public string GetMessage()
        {
            return gameObject.name;
        }

        #endregion
    }
}