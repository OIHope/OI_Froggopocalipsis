using System.Collections;
using UnityEngine;

namespace Core.System
{
    public abstract class Manager : MonoBehaviour
    {
        public abstract IEnumerator InitManager();
        public abstract IEnumerator SetupManager();
    }
}