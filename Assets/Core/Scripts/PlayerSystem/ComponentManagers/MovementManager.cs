using UnityEngine;

namespace PlayerSystem
{
    public class MovementManager : MonoBehaviour
    {
        [SerializeField] private bool _usingRigidBody = false;
        [Space]
        [SerializeField] private CharacterController actorCC;
        [SerializeField] private Rigidbody actorRB;
        [Space]
        [SerializeField][Range(1, 100)] private int _rbMovementMultiplier;


        public void Move(Vector3 moveVector)
        {
            if (_usingRigidBody) MoveRB(moveVector);
            else MoveCC(moveVector);
        }
        public void HandleGravity(bool specialGravity)
        {
            if (_usingRigidBody) return;

            float gravity = specialGravity ? -0.5f : -9.5f;
            float groundedGravity = -0.5f;
            float usedGravityValue = actorCC.isGrounded ? groundedGravity : gravity;
            Vector3 gravityVector = Time.deltaTime * usedGravityValue * Vector3.up;
            MoveCC(gravityVector);
        }

        private void MoveCC(Vector3 moveVector)
        {
            actorCC.Move(moveVector);
        }
        private void MoveRB(Vector3 moveVector)
        {
            actorRB.AddForce(moveVector * _rbMovementMultiplier);
        }
    }
}