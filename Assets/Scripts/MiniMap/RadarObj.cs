using UnityEngine;
using UnityEngine.UI;


namespace GeekBrainsFPS
{
	public sealed class RadarObj : MonoBehaviour
	{
        #region Fields

		[SerializeField] private Image _ico;

		#endregion


		#region UnityMethods

        private void OnDisable()
        {
            Radar.RemoveRadarObject(gameObject);
        }

        private void OnEnable()
        {
            Radar.RegisterRadarObject(gameObject, _ico);
        }

		#endregion
    }
}