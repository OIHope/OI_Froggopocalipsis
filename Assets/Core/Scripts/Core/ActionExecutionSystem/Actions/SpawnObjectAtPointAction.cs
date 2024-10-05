using UnityEngine;

namespace ActionExecuteSystem
{
    public class SpawnObjectAtPointAction : ActionBase
    {
        [SerializeField] private GameObject _spawnObject;
        [SerializeField] private Transform _spawnPointTransform;
        [SerializeField] private bool _makeParent = false;
        protected override void ActionToPerform()
        {
            if (_makeParent)
            {
                Instantiate(_spawnObject, _spawnPointTransform.position, Quaternion.identity, _spawnPointTransform);
            }
            else
            {
                Instantiate(_spawnObject, _spawnPointTransform.position, Quaternion.identity);
            }
            
        }
    }
}