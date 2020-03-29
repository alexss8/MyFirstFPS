using UnityEngine;


namespace GeekBrainsFPS
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        #region Fields

        [HideInInspector] public Rigidbody Rigidbody;
        [HideInInspector] public Transform Transform;

        private Color _color;

        private int _layer;
        private bool _isVisible;

        #endregion


        #region Properties

        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        public int Layer
		{
			get => _layer;

			set
			{
				_layer = value;
				AskLayer(transform, value);
			}
		}

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                AskColor(transform, _color);
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                RendererSetActive(transform);
                if (transform.childCount <= 0) return;
                foreach (Transform t in transform)
                {
                    RendererSetActive(t);
                }
            }
        }

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = transform;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Выключает физику у объекта и его детей
        /// </summary>
        public void DisableRigidBody()
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = true;
            }
        }

        /// <summary>
        /// Включает физику у объекта и его детей
        /// </summary>
        public void EnableRigidBody(float force)
        {
            EnableRigidBody();
            Rigidbody.AddForce(transform.forward * force);
        }

        /// <summary>
        /// Включает физику у объекта и его детей
        /// </summary>
        public void EnableRigidBody()
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = false;
            }
        }

        /// <summary>
        /// Замораживает или размораживает физическую трансформацию объекта
        /// </summary>
        /// <param name="rigidbodyConstraints">Трансформация которую нужно заморозить</param>
        public void ConstraintsRigidBody(RigidbodyConstraints rigidbodyConstraints)
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.constraints = rigidbodyConstraints;
            }
        }

        public void SetActive(bool value)
        {
            IsVisible = value;
            if (TryGetComponent<Collider>(out var component))
            {
                component.enabled = value;
            }
        }

        /// <summary>
        /// Выставляет слой себе и всем вложенным объектам в независимости от уровня вложенности
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="lvl">Слой</param>
        private void AskLayer(Transform obj, int layer)
        {
            obj.gameObject.layer = layer;
            if (obj.childCount <= 0) return;

            foreach (Transform child in obj)
            {
                AskLayer(child, layer);
            }
        }

        private void RendererSetActive(Transform renderer)
        {
            if (renderer.gameObject.TryGetComponent<Renderer>(out var component))
            {
                component.enabled = _isVisible;
            }
        }

        private void AskColor(Transform obj, Color color)
        {
            foreach (var curMaterial in obj.GetComponent<Renderer>().materials)
            {
                curMaterial.color = color;
            }
            if (obj.childCount <= 0) return;
            foreach (Transform d in obj)
            {
                AskColor(d, color);
            }
        }

        #endregion
    }
}