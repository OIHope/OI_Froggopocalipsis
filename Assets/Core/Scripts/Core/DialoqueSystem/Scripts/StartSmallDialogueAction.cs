using ActionExecuteSystem;
using UnityEngine;

namespace Core.DialogueSystem
{
    public class StartSmallDialogueAction : ActionBase
    {
        [SerializeField] private DialogueBlockData dialogueBlock;
        protected override void ActionToPerform()
        {
            Debug.Log("Starting small talk...");
            DialogueManager.Instance.OnSmallTalkInitialize?.Invoke(dialogueBlock);
        }
    }
}