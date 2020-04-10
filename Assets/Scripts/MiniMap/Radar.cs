using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GeekBrainsFPS
{
    public class Radar : MonoBehaviour
    {
        #region Fields
        
        public static List<RadarObject> RadarObjects = new List<RadarObject>();

        [SerializeField] private readonly float _mapScale = 2;

        private Transform _playerPos;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _playerPos = Camera.main.transform;
        }

        private void Update()
        {
            if (Time.frameCount % 2 == 0)
            {
                DrawRadarDots();
            }
        }

        #endregion


        #region PublicMethods

        public static void RegisterRadarObject(GameObject o, Image i)
        {
            Image image = Instantiate(i);
            RadarObjects.Add(new RadarObject(o, image));
        }
        public static void RemoveRadarObject(GameObject o)
        {
            List<RadarObject> newList = new List<RadarObject>();
            foreach (RadarObject t in RadarObjects)
            {
                if (t.Owner == o)
                {
                    Destroy(t.Icon);
                    continue;
                }
                newList.Add(t);
            }
            RadarObjects.RemoveRange(0, RadarObjects.Count);
            RadarObjects.AddRange(newList);
        }

        #endregion


        #region PrivateMethods
        
        private void DrawRadarDots() // Синхронизирует значки на миникарте с реальными объектами
        {
            foreach (RadarObject radObject in RadarObjects)
            {
                Vector3 radarPos = (radObject.Owner.transform.position -
                                    _playerPos.position);
                float distToObject = Vector3.Distance(_playerPos.position,
                    radObject.Owner.transform.position) * _mapScale;
                float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg -
                               270 - _playerPos.eulerAngles.y;
                radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
                radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
                radObject.Icon.transform.SetParent(transform);
                radObject.Icon.transform.position = new Vector3(radarPos.x,
                    radarPos.z, 0) + transform.position;
            }
        }

        #endregion
    }

    public class RadarObject
    {
        public readonly Image Icon;
        public readonly GameObject Owner;

        public RadarObject(GameObject owner, Image icon)
        {
            Owner = owner;
            Icon = icon;
        }
    }
}