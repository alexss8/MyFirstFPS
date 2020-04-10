using System;
using UnityEngine;
using UnityEngine.UI;


namespace GeekBrainsFPS
{
    public sealed class HealthUIBar : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _positionOffset;

        private Camera _camera;
        private Image _bar;
        private Bot _bot;
        private Vector3 _offset;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _bar = gameObject.GetComponent<Image>();
            _camera = Camera.main;
            _offset = Vector3.up * _positionOffset;
        }

        private void LateUpdate()
        {
            transform.position = _camera.WorldToScreenPoint(_bot.transform.position + _offset);
        }

        private void OnDisable()
        {
            if (_bot != null)
            {
                _bot.OnHealthChange -= HandleHealthChanged;
            }
        }

        private void OnEnable()
        {
            if (_bot != null)
            {
                _bot.OnHealthChange += HandleHealthChanged;
            }
        }

        #endregion


        #region Methods

        public void SetBot(Bot bot)
        {
            _bot = bot;
            _bot.OnHealthChange += HandleHealthChanged;
        }

        private void HandleHealthChanged(float healthLevel)
        {
            _bar.fillAmount = healthLevel;
        }

        #endregion
    }
}