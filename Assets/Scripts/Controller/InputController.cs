using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class InputController : BaseController, IExecute
    {
        #region Fields

        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _savePlayer = KeyCode.F5;
        private KeyCode _loadPlayer = KeyCode.F9;

        private int _mouseButton = (int)MouseButton.LeftButton;
        private float _mouseScrollDeltaY;

        #endregion


        #region ClassLifeCycles

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Выбор оружия
        /// </summary>
        /// <param name="i">Номер оружия</param>
        private void SelectWeapon(int i)
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            var tempWeapon = ServiceLocator.Resolve<Inventory>().SelectWeapon(i);
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            }
        }

        private void SelectWeaponByScroll(float mouseScrollDeltaY)
        {
            ServiceLocator.Resolve<WeaponController>().Off();

            var inventory = ServiceLocator.Resolve<Inventory>();
            Weapon tempWeapon = null;
            if (mouseScrollDeltaY > 0.0f)
            {
                tempWeapon = inventory.GetNextWeapon();
            }
            else if (mouseScrollDeltaY < 0.0f)
            {
                tempWeapon = inventory.GetPreviousWeapon();
            }

            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            }
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;

            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch(ServiceLocator.Resolve<Inventory>().FlashLight);
            }

            _mouseScrollDeltaY = Input.mouseScrollDelta.y;
            if (Mathf.Abs(_mouseScrollDeltaY) > Mathf.Epsilon ) SelectWeaponByScroll(_mouseScrollDeltaY);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectWeapon(1);
            }

            if (Input.GetMouseButton(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Fire();
                }
            }

            if (Input.GetKeyDown(_cancel))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            }

            if (Input.GetKeyDown(_reloadClip))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().ReloadClip();
                }
            }

            if (Input.GetKeyDown(_savePlayer))
            {
                ServiceLocator.Resolve<SaveDataRepository>().Save();
            }

            if (Input.GetKeyDown(_loadPlayer))
            {
                ServiceLocator.Resolve<SaveDataRepository>().Load();
            }
        }

        #endregion
    }
}