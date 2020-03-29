using System;
using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class SelectionController : BaseController, IExecute
    {
        #region Fields

        private GameObject _dedicatedObj;
        private ISelectObj _selectedObj;

        private bool _nullString;
        private bool _isSelectedObj;

        private readonly Camera _mainCamera;
        private readonly Vector2 _center;

        private readonly float _dedicateDistance = 20.0f;

        #endregion


        #region ClassLifeCycles

        public SelectionController()
        {
            _mainCamera = Camera.main;
            _center = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(_center),
                out var hit, _dedicateDistance))
            {
                SelectObject(hit.collider.gameObject);
                _nullString = false;
            }
            else if (!_nullString)
            {
                UIInterface.SelectionObjMessageUI.Text = String.Empty;
                _nullString = true;
                _dedicatedObj = null;
                _isSelectedObj = false;
            }
            if (_isSelectedObj)
            {
                // Действие над объектом

                switch (_selectedObj)
                {
                    case Weapon aim:

                        // в инвентарь


                        //Inventory.AddWeapon(aim);
                        break;
                    case Wall wall:
                        break;
                }
            }
        }

        #endregion


        #region Methods

        private void SelectObject(GameObject obj)
        {
            if (obj == _dedicatedObj) return;
            _selectedObj = obj.GetComponent<ISelectObj>();
            if (_selectedObj != null)
            {
                UIInterface.SelectionObjMessageUI.Text = _selectedObj.GetMessage();
                _isSelectedObj = true;
            }
            else
            {
                UIInterface.SelectionObjMessageUI.Text = String.Empty;
                _isSelectedObj = false;
            }
            _dedicatedObj = obj;
        }

        #endregion
    }
}
