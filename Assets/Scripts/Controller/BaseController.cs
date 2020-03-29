namespace GeekBrainsFPS
{
    public abstract class BaseController
    {

        #region Fields

        protected UIInterface UIInterface;

        #endregion

        #region Properties

        public bool IsActive { get; private set; }

        #endregion


        #region ClassLifeCycles

        protected BaseController()
        {
            UIInterface = new UIInterface();
        }

        #endregion


        #region Methods

        public virtual void On()
        {
            On(null);
        }

        public virtual void On(params BaseObjectScene[] obj)
        {
            IsActive = true;
        }

        public virtual void Off()
        {
            IsActive = false;
        }

        public void Switch(params BaseObjectScene[] obj)
        {
            if (!IsActive)
            {
                On(obj);
            }
            else
            {
                Off();
            }
        }

        #endregion
    }
}