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
            ServiceLocator.SetService(new HealthBarController());
            ServiceLocator.SetService(new BotController());
            ServiceLocator.SetService(new AimController());
            ServiceLocator.SetService(new SaveDataRepository());

            // Forming IExecute controllers.
            _executeControllers = new IExecute[7];
            _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();
            _executeControllers[1] = ServiceLocator.Resolve<PlayerController>();
            _executeControllers[2] = ServiceLocator.Resolve<FlashLightController>();
            _executeControllers[3] = ServiceLocator.Resolve<InputController>();
            _executeControllers[4] = ServiceLocator.Resolve<SelectionController>();
            _executeControllers[5] = ServiceLocator.Resolve<HealthBarController>();
            _executeControllers[6] = ServiceLocator.Resolve<BotController>();
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            // Initialization stage.
            ServiceLocator.Resolve<HealthBarController>().Initialization();
            ServiceLocator.Resolve<HealthBarController>().On();
            ServiceLocator.Resolve<BotController>().Initialization();

            ServiceLocator.Resolve<FlashLightController>().Initialization();
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