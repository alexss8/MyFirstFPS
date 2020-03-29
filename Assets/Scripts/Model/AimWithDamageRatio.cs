using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class AimWithDamageRatio : BaseAim
    {
        #region Fields

        [SerializeField] private float _takeDamageRatio = 0.5f;

        private bool _isDead;

        #endregion


        #region ICollision

        // todo: Make damage taking implementation.
        public override void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;
            if (Hp > 0)
            {
                Hp -= info.Damage * _takeDamageRatio;
            }

            if (Hp <= 0)
            {
                if (!TryGetComponent<Rigidbody>(out _))
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                Destroy(gameObject, _timeToDestroy);

                HandleOnPointChange();
                _isDead = true;
            }
        }

        #endregion
    }
}