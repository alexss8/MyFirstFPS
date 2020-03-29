using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class Aim : BaseAim
    {
        #region Fields

        private bool _isDead;

        #endregion


        #region ICollision

        public override void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;
            if (Hp > 0)
            {
                Hp -= info.Damage;
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