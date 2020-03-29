namespace GeekBrainsFPS
{
    public sealed class Wall : BaseObjectScene, ISelectObj
    {
        #region Methods

        public string GetMessage()
        {
            return Name;
        }

        #endregion
    }
}