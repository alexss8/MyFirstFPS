using System;
using UnityEngine;
using static UnityEngine.Random;


namespace GeekBrainsFPS
{
    public sealed class FlashLightModel : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _speed = 11;
        [SerializeField] private float _batteryChargeMax;
        [SerializeField] private float _intensity = 1.5f;

        private Light _light;
        private Transform _goFollow;
        private Vector3 _vecOffset;

        private float _share;
        private float _currentIntensity => _intensity * Charge;

        #endregion


        #region Properties

        public float Charge => BatteryChargeCurrent / _batteryChargeMax;
        public float BatteryChargeCurrent { get; private set; }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();

            _light = GetComponent<Light>();
            _light.enabled = false;
            _light.intensity = _intensity;

            BatteryChargeCurrent = _batteryChargeMax;
            _share = _batteryChargeMax / 4.0f;

            _goFollow = Camera.main.transform;

            _vecOffset = Transform.position - _goFollow.position;
        }

        #endregion


        #region Methods

        public void Switch(FlashLightActiveType value)
        {
            switch (value)
            {
                case FlashLightActiveType.On:
                    _light.enabled = true;
                    Transform.position = _goFollow.position + _vecOffset;
                    Transform.rotation = _goFollow.rotation;
                    break;
                case FlashLightActiveType.Off:
                    _light.enabled = false;
                    break;
                case FlashLightActiveType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void Rotation()
        {
            Transform.position = _goFollow.position + _vecOffset;
            Transform.rotation = Quaternion.Lerp(Transform.rotation,
                _goFollow.rotation, _speed * Time.deltaTime);
        }

        public bool EditBatteryCharge()
        {
            if (BatteryChargeCurrent > 0)
            {
                BatteryChargeCurrent -= Time.deltaTime;

                if (BatteryChargeCurrent < _share)
                {
                    _light.intensity = _currentIntensity;
                    _light.enabled = Range(0, 100) >= Range(0, 10);
                }
                else
                {
                    _light.intensity = _intensity;
                }
                return true;
            }

            return false;
        }

        public bool LowBattery()
        {
            return BatteryChargeCurrent <= _share;
        }

        public bool RechargeBattery()
        {
            if (BatteryChargeCurrent < _batteryChargeMax)
            {
                BatteryChargeCurrent += Time.deltaTime;
                return true;
            }

            return false;
        }

        #endregion
    }
}