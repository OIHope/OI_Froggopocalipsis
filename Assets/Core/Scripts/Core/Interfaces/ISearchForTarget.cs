using Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISearchForTarget
{
    public DetectTargetComponent TargetDetector { get; }
    public bool CheckTargetIsClose(Transform targetTransform, float triggerDistance);
    public void StartSearching();
    public void StopSearching(Transform targetTransform);
}
