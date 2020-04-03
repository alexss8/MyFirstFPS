using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class AimController : BaseController, IInitialization
    {
        #region Fields

        private BaseAim[] _aims;

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _aims = Object.FindObjectsOfType<BaseAim>();
            AimUIText aimUiText = ServiceLocatorMonoBehaviour.GetService<AimUIText>();
            foreach (var aim in _aims)
            {
                aimUiText.AddPointsGiver(aim);
            }
        }

        #endregion
    }
}