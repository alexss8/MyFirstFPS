using System.Collections.Generic;
using UnityEngine;


namespace GeekBrainsFPS
{
    class HealthBarController : BaseController, IInitialization, IExecute
    {
        #region Fields

        private HealthUIBar _healthBarPrefab;
        private Transform _transform;

        private Dictionary<Bot, HealthUIBar> _healthUIBars = new Dictionary<Bot, HealthUIBar>();
       
        private bool _isBotVisible;
        private bool _isHealthBarActive;
        
        #endregion


        #region Methods

        public override void On()
        {
            if (IsActive) return;
            base.On();
            Bot.OnBotEnabled += AddHealthBar;
            Bot.OnBotDisabled += RemoveHealthBar;
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            Bot.OnBotEnabled -= AddHealthBar;
            Bot.OnBotDisabled -= RemoveHealthBar;

            foreach (var item in _healthUIBars)
            {
                if (item.Key != null)
                {
                    item.Key.OnDieChange -= RemoveHealthBar;
                }
            }
        }

        private void AddHealthBar(Bot bot)
        {
            if (!_healthUIBars.ContainsKey(bot))
            {
                var healthBar = Object.Instantiate(_healthBarPrefab, _transform);
                _healthUIBars.Add(bot, healthBar);
                // todo: Unification to health interface is needed.
                healthBar.SetBot(bot);
                bot.OnDieChange += RemoveHealthBar;
            }
        }

        private void RemoveHealthBar(Bot bot)
        {
            if (_healthUIBars.ContainsKey(bot))
            {
                if (_healthUIBars[bot] != null)
                {
                    Object.Destroy(_healthUIBars[bot].gameObject);
                }
                _healthUIBars.Remove(bot);
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _transform = ServiceLocatorMonoBehaviour.GetService<Canvas>().transform;
            _healthBarPrefab = ServiceLocatorMonoBehaviour.GetService<Reference>().HealthUIBar;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            foreach (var item in _healthUIBars)
            {
                _isBotVisible = item.Key.IsVisibleToCameraMain();
                _isHealthBarActive = item.Value.gameObject.activeInHierarchy;
                if (!_isBotVisible && _isHealthBarActive)
                {
                    item.Value.gameObject.SetActive(false);
                }
                else if (_isBotVisible && !_isHealthBarActive)
                {
                    item.Value.gameObject.SetActive(true);
                }
            }
        }

        #endregion
    }
}