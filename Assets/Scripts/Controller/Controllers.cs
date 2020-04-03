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
            // Forming ServiceLocator data.
            IMotor motor = new UnitMotor(ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new Inventory());
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new SelectionController());
            ServiceLocator.SetService(new TimeRemainingController());
            ServiceLocator.SetService(new BotController());
            ServiceLocator.SetService(new AimController());

            // Forming IExecute controllers.
            _executeControllers = new IExecute[6];
            _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();
            _executeControllers[1] = ServiceLocator.Resolve<PlayerController>();
            _executeControllers[2] = ServiceLocator.Resolve<FlashLightController>();
            _executeControllers[3] = ServiceLocator.Resolve<InputController>();
            _executeControllers[4] = ServiceLocator.Resolve<SelectionController>();
            _executeControllers[5] = ServiceLocator.Resolve<BotController>();
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            // Initialization stage.
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }
            ServiceLocator.Resolve<Inventory>().Initialization();
            ServiceLocator.Resolve<AimController>().Initialization();

            // Turning on stage.
            ServiceLocator.Resolve<PlayerController>().On();
            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<SelectionController>().On();
            ServiceLocator.Resolve<BotController>().On();
            ServiceLocator.Resolve<AimController>().On();
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