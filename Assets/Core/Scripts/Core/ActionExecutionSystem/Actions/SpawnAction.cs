using UnityEngine;

namespace ActionExecuteSystem
{
    public class SpawnAction : ActionBase
    {
        [SerializeField] private GameObject _thingToSpawn;

        protected override void ActionToPerform()
        {
            Instantiate(_thingToSpawn, transform.position, Quaternion.identity, this.transform);
        }

        private void CleanUp()
        {
            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnDisable()
        {
            CleanUp();
        }
    }
}