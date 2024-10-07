using Core.Sound;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class PlaySoundAction : ActionBase
    {
        [SerializeField][Range(0,1f)] private float _volume = 1f;
        [SerializeField] private SoundBank _soundBank;
        [SerializeField] private MixerGroup _mixerGroup;
        [SerializeField] private bool _isMusic = false;
        [SerializeField] private bool _looped = false;
        protected override void ActionToPerform()
        {
            if (_isMusic)
            {
                SoundManager.Instance.PlayMusic(_soundBank.GetAudioFromBank, this.transform);
                return;
            }

            SoundManager.Instance.PlaySFX(_soundBank.GetAudioFromBank, this.transform, _mixerGroup, _looped, _volume);
        }
    }
}