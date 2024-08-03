using UnityEngine;
using BehaviourSystem;

namespace PlayerSystem
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Managers")]
        [Space]
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private PlayerStateManager _stateManager;
        [SerializeField] private PlayerCooldownManager _cooldownManager;
        [SerializeField] private DamageDealerManager _damageDealerManager;
        [Space]
        [Header("Components")]
        [Space]
        [SerializeField] private CharacterController _controller;
        [SerializeField] private PlayerStatsSO _playerStats;
        [Space]
        [Header("Game Objects")]
        [Space]
        [SerializeField] private GameObject _damageDealerPivot;
        [SerializeField] private GameObject _navigationArrow;

        private PlayerControllerContext _context;

        

        private void Awake()
        {
            _inputManager.InitializeInputControls();
            _context = new PlayerControllerContext(_controller, _playerStats, _inputManager, 
                _damageDealerPivot, _navigationArrow, _cooldownManager);
            _damageDealerPivot.SetActive(false);
            _stateManager.SetupStateManager(_context);
        }
    }
}