using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private FlashLightModel _flashLightModel;

        #endregion


        #region Methods

        public override void On(params BaseObjectScene[] flashLight)
        {
            if (IsActive) return;

            if (flashLight.Length > 0) _flashLightModel = flashLight[0] as FlashLightModel;
            if (_flashLightModel == null) return;

            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            UIInterface.FlashLightUIText.Text = _flashLightModel.BatteryChargeCurrent;

            base.On(_flashLightModel);

            _flashLightModel.Switch(FlashLightActiveType.On);
            UIInterface.FlashLightUIBar.SetColor(Color.green);

            UIInterface.FlashLightUIText.SetActive(true);
            UIInterface.FlashLightUIBar.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            UIInterface.FlashLightUIText.SetActive(false);
            UIInterface.FlashLightUIBar.SetActive(false);
            _flashLightModel.Switch(FlashLightActiveType.Off);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive)
            {
                if(_flashLightModel == null) return;
                
                if (_flashLightModel.RechargeBattery())
                {
                    UIInterface.FlashLightUIText.Text = _flashLightModel.BatteryChargeCurrent;
                    UIInterface.FlashLightUIBar.FillAmount = _flashLightModel.Charge;

                    if (!_flashLightModel.LowBattery())
                    {
                        UIInterface.FlashLightUIBar.SetColor(Color.green);
                    }
                }
                return;
            }

            if (_flashLightModel.EditBatteryCharge())
            {
                UIInterface.FlashLightUIText.Text = _flashLightModel.BatteryChargeCurrent;
                UIInterface.FlashLightUIBar.FillAmount = _flashLightModel.Charge;

                _flashLightModel.Rotation();

                if (_flashLightModel.LowBattery())
                {
                    UIInterface.FlashLightUIBar.SetColor(Color.red);
                }
            }
            else
            {
                Off();
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            UIInterface.FlashLightUIText.SetActive(false);
            UIInterface.FlashLightUIBar.SetActive(false);
        }

        #endregion
    }
}