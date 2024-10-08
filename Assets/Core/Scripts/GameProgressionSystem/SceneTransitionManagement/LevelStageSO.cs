using System.Collections.Generic;
using UnityEngine;

namespace Level.Stage
{
    [CreateAssetMenu(menuName =("Level/LevelStageList"),fileName =("StageList"))]
    public class LevelStageSO : ScriptableObject
    {
        [SerializeField] private List<GameObject> _levelStage;

        public List<GameObject> LevelStage => _levelStage;
    }
}