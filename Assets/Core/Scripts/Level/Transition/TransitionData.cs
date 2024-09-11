using UnityEngine;

namespace Level.Stage
{
    public class TransitionData : MonoBehaviour
    {
        [Header("Defies trigger zone to enter next level stage")]
        [SerializeField] private GameObject _nextStageBridge;
        [Header("Defies trigger zone to enter previous level stage")]
        [SerializeField] private GameObject _previousStageBridge;
        [Space(10)]
        [Header("Defies transform point to place player when enter stage")]
        [Header("Start point is selected when go throung NextStageBridge")]
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;

        public Vector3 StartPointPos => _startPoint.position;
        public Vector3 EndPointPos => _endPoint.position;

        public void ToggleTransitionZones(bool toggle)
        {
            _nextStageBridge.SetActive(toggle);
            _previousStageBridge.SetActive(toggle);
        }
    }
}