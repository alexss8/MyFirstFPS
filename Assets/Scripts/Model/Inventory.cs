using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class Inventory : IInitialization
    {
        #region Fields

        private Weapon[] _weapons = new Weapon[5];

        private int _currentWeaponIndex = 0;

        #endregion


        #region Properties
        
        public FlashLightModel FlashLight { get; private set; }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().
                GetComponentsInChildren<Weapon>();

            foreach (var weapon in _weapons)
            {
                weapon.SetActive(false);
            }

            FlashLight = Object.FindObjectOfType<FlashLightModel>();
            FlashLight.Switch(FlashLightActiveType.Off);
        }

        #endregion


        #region Methods

        //todo Добавить функционал

        public void RemoveWeapon(Weapon weapon)
        {

        }

        public Weapon SelectWeapon(int index)
        {
            if (_weapons.Length == 0) return null;

            if (index > _weapons.Length - 1) return _weapons[_currentWeaponIndex];

            _currentWeaponIndex = index;
            return _weapons[_currentWeaponIndex];
        }

        public Weapon GetNextWeapon()
        {
            if (_weapons.Length == 0) return null;

            _currentWeaponIndex++;
            if (_currentWeaponIndex == _weapons.Length) _currentWeaponIndex = 0;

            return _weapons[_currentWeaponIndex];
        }

        public Weapon GetPreviousWeapon()
        {
            if (_weapons.Length == 0) return null;

            _currentWeaponIndex--;
            if (_currentWeaponIndex == -1) _currentWeaponIndex = _weapons.Length - 1;

            return _weapons[_currentWeaponIndex];
        }

        #endregion
    }
}