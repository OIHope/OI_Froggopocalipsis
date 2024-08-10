using UnityEngine;

namespace Components
{
    public class MovementComponent : ComponentBase
    {
        private Rigidbody _actorRB;

        public void Move(Vector3 moveVector)
        {
            _actorRB.MovePosition(_actorRB.position + moveVector);
        }

        public MovementComponent(Rigidbody actorRB)
        {
            _actorRB = actorRB;
        }
        public override void UpdateComponent() { }
    }
}