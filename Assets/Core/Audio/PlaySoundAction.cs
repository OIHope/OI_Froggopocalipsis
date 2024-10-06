using Core.Sound;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class PlaySoundAction : ActionBase
    {
        [SerializeField] private SoundBank _soundBank;
        [SerializeField] private MixerGroup _mixerGroup;
        [SerializeField] private bool _isMusic = false;
        protected override void ActionToPerform()
        {
            if (_isMusic)
            {
                SoundManager.Instance.PlayMusic(_soundBank.GetAudioFromBank, this.transform);
                return;
            }

            SoundManager.Instance.PlaySFX(_soundBank.GetAudioFromBank, this.transform, _mixerGroup);
        }
    }
}