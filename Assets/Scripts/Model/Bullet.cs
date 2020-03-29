using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class Bullet : Ammunition
    {
        #region Fields

        [SerializeField] private float _ricochetImpulse = 5.0f;
        [SerializeField] private float _ricochetDamageLoss = 2.0f;
        [SerializeField] private int _countOfRicochetsMax = 3;

        private int _countOfRicochets = 0;

        #endregion


        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_curDamage, Rigidbody.velocity));
            }

            if (_countOfRicochets < _countOfRicochetsMax)
            {
                _countOfRicochets++;
                _curDamage -= _ricochetDamageLoss;
                if (_curDamage > 0)
                {
                    Rigidbody.AddForce(collision.contacts[0].normal * _ricochetImpulse, ForceMode.Impulse);
                }
                else
                {
                    DestroyAmmunition();
                }
            }
            else
            {
                DestroyAmmunition();
            }
        }

        #endregion
    }
}