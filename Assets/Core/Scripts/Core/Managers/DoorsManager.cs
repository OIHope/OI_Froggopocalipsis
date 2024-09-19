using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class DoorsManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _doorList;

        public void OpenDoors()
        {
            foreach (GameObject door in _doorList)
            {
                door.SetActive(false);
            }
        }
        public void CloseDoors()
        {
            foreach (GameObject door in _doorList)
            {
                door.SetActive(true);
            }
        }
    }
}