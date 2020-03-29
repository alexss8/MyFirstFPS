using System.Collections.Generic;
using UnityEngine;

namespace GeekBrainsFPS
{
    public abstract class Weapon : BaseObjectScene
    {
        #region Fields

        public Ammunition Ammunition;
        public AmmunitionType[] AmmunitionTypes = { AmmunitionType.Bullet };
        public Clip Clip;

        // Place where weapon fires from.
        [SerializeField] protected Transform _barrel;

        [SerializeField] protected float _force = 999;
        [SerializeField] protected float _rechargeTime = 0.2f;
        [SerializeField] protected int _maxCountAmmunition = 40;
        [SerializeField] protected int _minCountAmmunition = 20;
        [SerializeField] protected int _countClip = 5;

        protected ITimeRemaining _timeRemaining;

        protected bool _isReady = true;

        private Queue<Clip> _clips = new Queue<Clip>();

        #endregion


        #region Properties

        public int CountClip => _clips.Count;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _timeRemaining = new TimeRemaining(ReadyShoot, _rechargeTime);
            for (var i = 0; i <= _countClip; i++)
            {
                AddClip(new Clip { CountAmmunition = Random.Range(_minCountAmmunition, _maxCountAmmunition) });
            }

            ReloadClip();
        }

        #endregion


        #region Methods

        public virtual void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;
            var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation); //todo Pool object
            temAmmunition.AddForce(_barrel.forward * _force);
            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemaining();
        }

        public void ReloadClip()
        {
            if (CountClip <= 0) return;
            Clip = _clips.Dequeue();
        }

        protected void ReadyShoot()
        {
            _isReady = true;
        }

        protected void AddClip(Clip clip)
        {
            _clips.Enqueue(clip);
        }

        #endregion
    }
}