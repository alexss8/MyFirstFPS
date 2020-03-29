using UnityEngine;
using UnityEngine.UI;


namespace GeekBrainsFPS
{
    public sealed class FlashLightUIBar : MonoBehaviour
    {
        #region Fields

        private Image _bar;

        #endregion


        #region Properties

        public float FillAmount
        {
            set => _bar.fillAmount = value;
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _bar = GetComponent<Image>();
            _bar.fillAmount = 1.0f;
        }

        #endregion


        #region Methods

        public void SetActive(bool value)
        {
            _bar.gameObject.SetActive(value);
        }

        public void SetColor(Color col)
        {
            _bar.color = col;
        }

        #endregion
    }
}