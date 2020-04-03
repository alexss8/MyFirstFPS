using UnityEngine;


namespace GeekBrainsFPS
{
    public readonly struct InfoCollision
    {
        #region Fields

        private readonly ContactPoint _contact;
        private readonly Transform _objCollision;
        private readonly Vector3 _dir;

        private readonly float _damage;

        #endregion


        #region Properties

        public ContactPoint Contact => _contact;
        public Transform ObjCollision => _objCollision;
        public Vector3 Dir => _dir;

        public float Damage => _damage;

        #endregion


        #region ClassLifeCycles

        public InfoCollision(float damage, ContactPoint contact, Transform objCollision, Vector3 dir = default)
        {
            _damage = damage;
            _dir = dir;
            _contact = contact;
            _objCollision = objCollision;
        }

        #endregion
    }
}