using UnityEngine;

public interface IAttackableTarget
{
    public bool TargetIsAlive { get; }
    public Transform InstanceTransform {  get; }
}
