using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class Bomb : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _damage = 10.0f;
        [SerializeField] private float _bombRadius = 3.0f;
        [SerializeField] private float power = 5.0f;

        #endregion


        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent<ICollision>(out _))
                return;

            var explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, _bombRadius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, _bombRadius, 3.0F);

                if (hit.gameObject.TryGetComponent<ICollision>(out var tempObjCollision))
                {
                    //todo: contactPoint.
                    tempObjCollision.OnCollision(new InfoCollision(_damage, new ContactPoint(), hit.transform,
                         explosionPos));
                }
            }
            Destroy(gameObject);
        }

        #endregion
    }
}