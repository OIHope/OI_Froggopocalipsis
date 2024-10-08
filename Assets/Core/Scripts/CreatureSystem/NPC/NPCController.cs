using Core.DialogueSystem;
using Core.Progression;
using UnityEngine;

namespace NPC
{
    public class NPCController : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueContainerData _dialogueContainerData;
        [SerializeField] private GameObject _text;

        private IInteractor currentInteractor;

        private void Awake()
        {
            HideInteraction();
        }

        public void DisplayInteraction()
        {
            _text.SetActive(true);
        }

        public void HideInteraction()
        {
            _text.SetActive(false);
        }

        public void Interact()
        {
            DialogueManager.Instance.OnDialogueInitialize?.Invoke(_dialogueContainerData);
        }
        public void AltInteract()
        {
            PlayerProgressionManager.Instance.OnOpenLevelUpMenu?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            IInteractor interactor = other.GetComponent<IInteractor>();
            if (interactor != null)
            {
                currentInteractor = interactor;
                DisplayInteraction();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (currentInteractor != null && other.GetComponent<IInteractor>() == currentInteractor)
            {
                HideInteraction();
                currentInteractor = null;
            }
        }
        private void Update()
        {
            if (currentInteractor != null && currentInteractor.RequestInteraction())
            {
                Interact();
            }
            if (currentInteractor != null && currentInteractor.RequestAlternativeInteraction())
            {
                AltInteract();
            }
        }
    }
}