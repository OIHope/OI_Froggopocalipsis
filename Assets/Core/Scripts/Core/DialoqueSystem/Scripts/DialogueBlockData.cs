using Core.Progression;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DialogueSystem
{
    [CreateAssetMenu(menuName = ("Dialogue System/Blocks/Dialogue Block"))]
    public class DialogueBlockData : ScriptableObject
    {
        [SerializeField] private bool _alreadyTalked = false;
        [Space(10)]
        [SerializeField] private List<DialogueData> _dialogueBlock;
        [Space]
        [SerializeField] private List<DialogueData> _recapBlock;
        [Space(5)]
        [SerializeField] private bool _hasImpactOnProgression = false;
        [SerializeField] private GameStage _stageUpgrageAfterThisDialogue;

        public DialogueData GetDialogueData(int index)
        {
            if (!_alreadyTalked) return _dialogueBlock[index];
            return _recapBlock[index];
        }
        public int BlockCount => _alreadyTalked ? _recapBlock.Count : _dialogueBlock.Count;
        public void MarkAsTalked() => _alreadyTalked = true;
        public void ResetDialogue() => _alreadyTalked = false;
        public GameStage GetImpactAfterThisDialogue => _hasImpactOnProgression ?
                    _stageUpgrageAfterThisDialogue :
                    GameStageManager.Instance.CurrentGameStage;
    }
}