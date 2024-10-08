using Core.Progression;
using System;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class GameStageCondition : ConditionBase
    {
        [SerializeField] private GameStage _requestedGameStage;

        public override bool ConditionIsValid()
        {
            GameStage currentGameStage = GameStageManager.Instance.CurrentGameStage;

            switch (request)
            {
                case ConditionRequest.Equals:
                    return currentGameStage == _requestedGameStage;

                case ConditionRequest.MoreThan:
                    return currentGameStage > _requestedGameStage;

                case ConditionRequest.LessThan:
                    return currentGameStage < _requestedGameStage;

                case ConditionRequest.MoreOrEquals:
                    return currentGameStage >= _requestedGameStage;

                case ConditionRequest.LessOrEquals:
                    return currentGameStage <= _requestedGameStage;

                default:
                    throw new ArgumentOutOfRangeException(nameof(request), request, "Unsupported condition request type");
            }
        }
    }
}