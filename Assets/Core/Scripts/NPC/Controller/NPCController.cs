using UnityEngine;

namespace NPC
{
    public class NPCController : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _text;

        private IInteractor currentInteractor;

        private void Awake()
        {
            HideInteraction();
        }

        public void DisplayInteraction()
        {
            Debug.Log("Herbalist cat talk to you");
            _text.SetActive(true);
        }

        public void HideInteraction()
        {
            Debug.Log("Herbalist will miss you");
            _text.SetActive(false);
        }

        public void Interact()
        {
            Debug.Log("Herbalist greets you!");
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
        }
    }
}