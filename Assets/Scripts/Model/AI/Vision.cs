using UnityEngine;


namespace GeekBrainsFPS
{
    [System.Serializable]
    public sealed class Vision
    {
        #region Fields

        public float ActiveDis = 10;
        public float ActiveAng = 35;

        #endregion


        #region Methods

        public bool VisionM(Transform bot, Transform target)
        {
            return Distance(bot, target) && Angle(bot, target) && !CheckBlocked(bot, target);
        }

        private bool CheckBlocked(Transform bot, Transform target)
        {
            if (!Physics.Linecast(bot.position, target.position, out var hit)) return true;
            return hit.transform != target;
        }

        private bool Angle(Transform bot, Transform target)
        {
            var angle = Vector3.Angle(bot.forward, target.position - bot.position);
            return angle <= ActiveAng;
        }

        private bool Distance(Transform bot, Transform target)
        {
            return (bot.position - target.position).sqrMagnitude <= ActiveDis * ActiveDis;
        }

        #endregion
    }
}