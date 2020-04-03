using System.Collections.Generic;
using UnityEngine;


namespace GeekBrainsFPS
{
    public sealed class BotController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private readonly int _countBot = 3;
        private readonly HashSet<Bot> _botList = new HashSet<Bot>();

        #endregion


        #region Methods

        private void AddBotToList(Bot bot)
        {
            if (!_botList.Contains(bot))
            {
                _botList.Add(bot);
                bot.OnDieChange += RemoveBotFromList;
            }
        }

        private void RemoveBotFromList(Bot bot)
        {
            if (!_botList.Contains(bot))
            {
                return;
            }

            bot.OnDieChange -= RemoveBotFromList;
            _botList.Remove(bot);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            foreach (var bot in _botList) bot.Execute();
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            for (var index = 0; index < _countBot; index++)
            {
                var characterTransform = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
                var tempBot = Object.Instantiate(ServiceLocatorMonoBehaviour.GetService<Reference>().Bot,
                    Patrol.GenericPoint(characterTransform),
                    Quaternion.identity);

                tempBot.Agent.avoidancePriority = index;
                tempBot.Target = characterTransform;
                //todo разных противников
                AddBotToList(tempBot);

                var aimUIText = ServiceLocatorMonoBehaviour.GetService<AimUIText>();
                aimUIText.AddPointsGiver(tempBot);
            }
        }

        #endregion
    }
}