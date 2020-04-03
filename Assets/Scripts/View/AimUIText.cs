using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GeekBrainsFPS
{
    public sealed class AimUIText : MonoBehaviour
    {
        #region Fields

        private readonly HashSet<IPointsGiver> _pointsGivers = new HashSet<IPointsGiver>();

        private Text _text;
        private int _countPoint;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            if (_pointsGivers.Count != 0)
            {
                foreach (var pointsGiver in _pointsGivers)
                {
                    pointsGiver.OnPointChange += UpdatePoint;
                }
            }
        }

        private void OnDisable()
        {
            if (_pointsGivers.Count != 0)
            {
                foreach (var pointsGiver in _pointsGivers)
                {
                    pointsGiver.OnPointChange -= UpdatePoint;
                }
            }
        }

        #endregion


        #region Methods

        public void AddPointsGiver(IPointsGiver pointsGiver)
        {
            if (_pointsGivers.Contains(pointsGiver)) return;

            _pointsGivers.Add(pointsGiver);
            pointsGiver.OnPointChange += UpdatePoint;
        }

        private void UpdatePoint(IPointsGiver pointsGiver)
        {
            if (!_pointsGivers.Contains(pointsGiver)) return;

            var pointTxt = "очков";
            _countPoint += pointsGiver.GivePoints();
            if (_countPoint >= 5) pointTxt = "очков";
            else if (_countPoint == 1) pointTxt = "очко";
            else if (_countPoint < 5) pointTxt = "очка";
            _text.text = $"Вы заработали {_countPoint} {pointTxt}";

            pointsGiver.OnPointChange -= UpdatePoint;
            _pointsGivers.Remove(pointsGiver);
        }

        #endregion
    }
}