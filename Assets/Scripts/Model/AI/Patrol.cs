using UnityEngine;
using UnityEngine.AI;


namespace GeekBrainsFPS
{
    public static class Patrol
    {
        #region Methods

        public static Vector3 GenericPoint(Transform transform)
        {
            //todo перемещение по точкам
            Vector3 result;

            var dis = Random.Range(5, 50);
            var randomPoint = Random.insideUnitSphere * dis;

            NavMesh.SamplePosition(transform.position + randomPoint,
                out var hit, dis, NavMesh.AllAreas);
            result = hit.position;

            return result;
        }

        #endregion
    }
}