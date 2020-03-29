using UnityEngine;
using UnityEngine.UI;


namespace GeekBrainsFPS
{
    public sealed class AimUIText : MonoBehaviour
    {
        #region Fields

        private BaseAim[] _aims;
        private Text _text;
        private int _countPoint;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _aims = FindObjectsOfType<BaseAim>();
            _text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            foreach (var aim in _aims)
            {
                aim.OnPointChange += UpdatePoint;
            }
        }

        private void OnDisable()
        {
            foreach (var aim in _aims)
            {
                aim.OnPointChange -= UpdatePoint;
            }
        }

        #endregion


        #region Methods

        private void UpdatePoint(int points)
        {
            var pointTxt = "очков";
            _countPoint += points;
            if (_countPoint >= 5) pointTxt = "очков";
            else if (_countPoint == 1) pointTxt = "очко";
            else if (_countPoint < 5) pointTxt = "очка";
            _text.text = $"Вы заработали {_countPoint} {pointTxt}";

            //todo отписаться удалить и списка
        }

        #endregion
    }
}