using System;
using UnityEngine;


namespace GeekBrainsFPS
{
    public abstract class BaseAim : BaseObjectScene, ICollision, ISelectObj, IPointsGiver
    {
        #region PrivateData

        public event Action<IPointsGiver> OnPointChange = delegate { };

        #endregion


        #region Fields

        public float Hp = 100;

        [SerializeField] protected float _timeToDestroy = 10.0f;
        [SerializeField] protected int _points = 1;

        #endregion


        #region Methods

        public int GivePoints()
        {
            return _points;
        }

        public void HandleOnPointChange()
        {
            OnPointChange.Invoke(this);
        }

        #endregion


        #region ICollision

        public abstract void OnCollision(InfoCollision info);

        #endregion


        #region ISelectObj

        public string GetMessage()
        {
            return gameObject.name;
        }

        #endregion
    }
}