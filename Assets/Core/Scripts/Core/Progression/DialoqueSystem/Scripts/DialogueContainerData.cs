using Core.Progression;
using UnityEngine;

namespace Core.DialogueSystem
{
    [CreateAssetMenu(menuName = ("Dialogue System/Blocks/Full Container"))]
    public class DialogueContainerData : ScriptableObject
    {
        [SerializeField] private DialogueBlockData _introDialogueData;
        [SerializeField] private DialogueBlockData _arrivalDialogueData;
        [Space]
        [SerializeField] private DialogueBlockData _fieldEnterDialogueData;
        [SerializeField] private DialogueBlockData _fieldDeathDialogueData;
        [SerializeField] private DialogueBlockData _fieldCompleteDialogueData;
        [Space]
        [SerializeField] private DialogueBlockData _swampEnterDialogueData;
        [SerializeField] private DialogueBlockData _swampDeathDialogueData;
        [SerializeField] private DialogueBlockData _swampCompleteDialogueData;
        [Space]
        [SerializeField] private DialogueBlockData _forestEnterDialogueData;
        [SerializeField] private DialogueBlockData _forestDeathDialogueData;
        [SerializeField] private DialogueBlockData _forestCompleteDialogueData;
        [Space]
        [SerializeField] private DialogueBlockData _outroDialogueData;
        [Space]
        [SerializeField] private DialogueBlockData _placeholderDialogueData;


        public DialogueBlockData GetDialogueBlock()
        {
            GameStage gameStage = GameStageManager.Instance.CurrentGameStage;
            return gameStage switch
            {
                GameStage.Intro => _introDialogueData,
                GameStage.Arrival => _arrivalDialogueData,
                GameStage.FieldEnter => _fieldEnterDialogueData,
                GameStage.FieldDeath => _fieldDeathDialogueData,
                GameStage.FieldComplete => _fieldCompleteDialogueData,
                GameStage.SwampEnter => _swampEnterDialogueData,
                GameStage.SwampDeath => _swampDeathDialogueData,
                GameStage.SwampComplete => _swampCompleteDialogueData,
                GameStage.ForestEnter => _forestEnterDialogueData,
                GameStage.ForestDeath => _forestDeathDialogueData,
                GameStage.ForestComplete => _forestCompleteDialogueData,
                GameStage.Outro => _outroDialogueData,
                _ => _placeholderDialogueData
            };
        }
    }
}