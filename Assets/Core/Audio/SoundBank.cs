using System.Collections.Generic;
using UnityEngine;

namespace Core.Sound
{
    [CreateAssetMenu(menuName =("Sound/Sound Bank"))]
    public class SoundBank : ScriptableObject
    {
        [SerializeField] private List<AudioClip> _sfxList;

        public AudioClip GetAudioFromBank
        {
            get
            {
                if (_sfxList.Count == 1) return _sfxList[0];

                int randomIndex = Random.Range(0, _sfxList.Count);
                return _sfxList[randomIndex];
            }
        }
    }
}