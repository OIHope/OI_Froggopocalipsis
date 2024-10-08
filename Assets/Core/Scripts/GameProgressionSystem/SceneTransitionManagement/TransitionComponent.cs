using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Stage
{
    public class TransitionComponent : MonoBehaviour
    {
        [SerializeField] private TransitionData _transData;

        public TransitionData TransData => _transData;
    }
}