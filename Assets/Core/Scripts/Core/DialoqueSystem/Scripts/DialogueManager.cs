using Core.Progression;
using Core.System;
using PlayerSystem;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Core.DialogueSystem
{
    public class DialogueManager : Manager
    {
        public static DialogueManager Instance { get; private set; }

        public Action<DialogueContainerData> OnDialogueInitialize;
        public Action<DialogueBlockData> OnSmallTalkInitialize;
        public Action<GameStage> OnDialogueFinish;
        public Action OnDialoguePanelOpenRequested;
        public Action OnDialoguePanelCloseRequested;

        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private TextMeshProUGUI _controllsText;
        [SerializeField] private TextMeshProUGUI _speakerNameText;
        [SerializeField] private Image _speakerImage;
        [Space]
        [SerializeField] private float _writingSpeed;

        private InputAction _skipAction;
        private DialogueBlockData _dialogueBlock;

        private int _index = 0;
        private bool _isWriting = false;
        private Coroutine _writingCoroutine;

        public override IEnumerator InitManager()
        {
            ClearText();
            yield return null;
        }

        public override IEnumerator SetupManager()
        {
            _skipAction = InputManager.Instance.Input.DialogueInputMap.Apply;
            _skipAction.started += _ => OnSkipAction();

            OnDialogueInitialize += StartDialogue;
            OnSmallTalkInitialize += StartSmallTalk;
            yield return null;
        }
        private void Awake()
        {
            SingletonMethod();
        }

        private void StartDialogue(DialogueContainerData dialogueContainer)
        {
            _index = 0;
            _dialogueBlock = dialogueContainer.GetDialogueBlock();
            OnDialoguePanelOpenRequested?.Invoke();
            DisplayNextLine();
        }
        private void StartSmallTalk(DialogueBlockData dialogueBlock)
        {
            _index = 0;
            _dialogueBlock = dialogueBlock;
            OnDialoguePanelOpenRequested?.Invoke();
            DisplayNextLine();
        }

        private void DisplayNextLine()
        {
            if (_index < _dialogueBlock.BlockCount)
            {
                DialogueData dialogue = _dialogueBlock.GetDialogueData(_index);
                _speakerNameText.text = dialogue.SpeakerName;
                _speakerImage.sprite = dialogue.SpeakerSprite;
                _writingCoroutine = StartCoroutine(DisplayLine(dialogue));
            }
            else
            {
                EndDialogue();
            }
        }

        private IEnumerator DisplayLine(DialogueData dialogueData)
        {
            ClearText();
            _isWriting = true;

            string textLine = dialogueData.Line;
            foreach (char c in textLine)
            {
                _dialogueText.text += c;
                yield return new WaitForSeconds(_writingSpeed);
            }

            _isWriting = false;
            _index++;
        }

        private void OnSkipAction()
        {
            if (_isWriting)
            {
                StopCoroutine(_writingCoroutine);
                _dialogueText.text = _dialogueBlock.GetDialogueData(_index).Line;
                _isWriting = false;
                _index++;
            }
            else
            {
                DisplayNextLine();
            }
        }

        private void EndDialogue()
        {
            _dialogueBlock.MarkAsTalked();
            OnDialogueFinish?.Invoke(_dialogueBlock.GetImpactAfterThisDialogue);
            OnDialoguePanelCloseRequested?.Invoke();
        }

        private void ClearText()
        {
            _dialogueText.text = string.Empty;
        }

        private void SingletonMethod()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                //DontDestroyOnLoad(gameObject);
            }
        }
    }
}