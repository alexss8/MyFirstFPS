using UnityEngine;

namespace GeekBrainsFPS
{
    public abstract class Ammunition : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _baseDamage = 10;

        public AmmunitionType Type = AmmunitionType.Bullet;

        protected float _curDamage;

        private float _lossOfDamageAtTime = 0.2f;
        private ITimeRemaining _damageLowerTimeRemaining;
        private ITimeRemaining _destroyTimeRemaining;

        #endregion



        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _curDamage = _baseDamage;
        }

        private void Start()
        {
            _destroyTimeRemaining = new TimeRemaining(DestroyAmmunition, _timeToDestruct);
            _destroyTimeRemaining.AddTimeRemaining();

            _damageLowerTimeRemaining = new TimeRemaining(LossOfDamage, 1.0f, true);
            _damageLowerTimeRemaining.AddTimeRemaining();
        }

        #endregion


        #region Methods

        public void AddForce(Vector3 dir)
        {
            if (!Rigidbody) return;
            Rigidbody.AddForce(dir);
        }

        private void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
            if (_curDamage <= 0) DestroyAmmunition();
        }

        protected void DestroyAmmunition()
        {
            _damageLowerTimeRemaining.RemoveTimeRemaining();
            _destroyTimeRemaining.RemoveTimeRemaining();
            Destroy(gameObject);
            // Вернуть в пул
        }

        #endregion
    }
}