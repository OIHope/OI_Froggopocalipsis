using UnityEngine;

public interface IAttackableTarget
{
    public bool ThisTargetIsGoodSide { get; }
    public bool TargetIsAlive { get; }
    public Transform InstanceTransform {  get; }
}
