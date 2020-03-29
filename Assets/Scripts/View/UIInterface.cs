using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class UIInterface
    {
        #region Fields

        private FlashLightUIText _flashLightUIText;
        private FlashLightUIBar _flashLightUIBar;
        private WeaponUIText _weaponUIText;
        private SelectionObjMessageUI _selectionObjMessageUI;

        #endregion


        #region Properties

        public FlashLightUIText FlashLightUIText
        {
            get
            {
                if (!_flashLightUIText)
                    _flashLightUIText = Object.FindObjectOfType<FlashLightUIText>();
                return _flashLightUIText;
            }
        }

        public FlashLightUIBar FlashLightUIBar
        {
            get
            {
                if (!_flashLightUIBar)
                    _flashLightUIBar = Object.FindObjectOfType<FlashLightUIBar>();
                return _flashLightUIBar;
            }
        }

        public WeaponUIText WeaponUIText
        {
            get
            {
                if (!_weaponUIText)
                    _weaponUIText = Object.FindObjectOfType<WeaponUIText>();
                return _weaponUIText;
            }
        }

        public SelectionObjMessageUI SelectionObjMessageUI
        {
            get
            {
                if (!_selectionObjMessageUI)
                    _selectionObjMessageUI = Object.FindObjectOfType<SelectionObjMessageUI>();
                return _selectionObjMessageUI;
            }
        }

        #endregion
    }
}