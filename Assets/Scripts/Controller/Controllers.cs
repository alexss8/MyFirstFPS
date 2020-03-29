using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class Controllers : IInitialization, IExecute
    {
        #region Fields

        private readonly IExecute[] _executeControllers;

        #endregion


        #region ClassLifeCycles

        public Controllers()
        {
            ServiceLocator.SetService(new Inventory());

            IMotor motor = new UnitMotor(ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new SelectionController());
            ServiceLocator.SetService(new TimeRemainingController());

            _executeControllers = new IExecute[5];

            _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();
            _executeControllers[1] = ServiceLocator.Resolve<PlayerController>();
            _executeControllers[2] = ServiceLocator.Resolve<FlashLightController>();
            _executeControllers[3] = ServiceLocator.Resolve<InputController>();
            _executeControllers[4] = ServiceLocator.Resolve<SelectionController>();
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }

            ServiceLocator.Resolve<PlayerController>().On();
            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<SelectionController>().On();

            ServiceLocator.Resolve<Inventory>().Initialization();
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            for (var i = 0; i < _executeControllers.Length; i++)
            {
                _executeControllers[i].Execute();
            }
        }

        #endregion
    }
}