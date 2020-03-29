using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class RPGAmmo : Ammunition
    {

        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_curDamage, Rigidbody.velocity));
            }
        }

        #endregion
    }
}