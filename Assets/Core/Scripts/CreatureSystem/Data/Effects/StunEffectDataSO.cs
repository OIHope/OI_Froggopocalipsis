using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = ("Stun Effect DataSO"), menuName = ("Data/Effect/Stun Effect Data"))]
    public class StunEffectDataSO : EffectDataSO
    {
        public override void ApplyEffect()
        {
            Debug.LogWarning("Effect is applied!");
        }
    }
}