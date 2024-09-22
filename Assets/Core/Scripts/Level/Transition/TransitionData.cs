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
        [SerializeField] private Transform _customPoint1;
        [SerializeField] private Transform _customPoint2;
        [SerializeField] private Transform _customPoint3;

        public Vector3 StartPointPos => _startPoint.position;
        public Vector3 EndPointPos => _endPoint.position;
        public Vector3 CustomPointPos1 => _customPoint1.position;
        public Vector3 CustomPointPos2 => _customPoint2.position;
        public Vector3 CustomPointPos3 => _customPoint3.position;


        public void ToggleTransitionZones(bool toggle)
        {
            _nextStageBridge.SetActive(toggle);
            _previousStageBridge.SetActive(toggle);
        }
    }
}