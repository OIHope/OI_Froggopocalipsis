using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class AnimationCollectionDataSO : ScriptableObject
    {
        [SerializeField] private List<AnimationData> _animationDatas;
    }
}

[System.Serializable]
public struct AnimationData
{
    private AnimationClip clip;
    private string name;

    public AnimationClip GetAnimation(string animationName)
    {
        return animationName.Equals(name) ? clip : null;
    }

    public AnimationData(AnimationClip clip, string animationName)
    {
        this.clip = clip;
        this.name = animationName;
    }
}