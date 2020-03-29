using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class GameController : MonoBehaviour
    {
        #region MyRegion

        private Controllers _controllers;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();
        }

        private void Update()
        {
            _controllers.Execute();
        }

        #endregion
    }
}