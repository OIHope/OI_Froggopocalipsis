using UnityEngine;
using BehaviourSystem;

namespace PlayerSystem
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private PlayerStateManager _stateManager;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private PlayerStatsSO _playerStats;

        [SerializeField] private GameObject _damageDealer;
        [SerializeField] private GameObject _navigationArrow;

        private PlayerControllerContext _context;

        

        private void Awake()
        {
            _inputManager.InitializeInputControls();
            _context = new PlayerControllerContext(_controller, _playerStats, _inputManager, _damageDealer, _navigationArrow);
            _stateManager.SetupStateManager(_context);
        }
    }
}