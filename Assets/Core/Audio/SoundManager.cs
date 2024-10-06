using Core.System;
using System;
using System.Collections;
using UnityEngine;

namespace Core.Sound
{
    public enum MixerGroup
    {
        Master, PlayerSFX, EnemySFX, PropSFX, UI, Music
    }
    public class SoundManager : Manager
    {
        public static SoundManager Instance;

        [SerializeField] private AudioSource _audioSourceMasterSFX;
        [SerializeField] private AudioSource _audioSourcePlayerSFX;
        [SerializeField] private AudioSource _audioSourceEnemySFX;
        [SerializeField] private AudioSource _audioSourcePropSFX;
        [SerializeField] private AudioSource _audioSourceUISFX;
        [SerializeField] private AudioSource _audioSourceMusicSFX;

        private AudioSource _musicPlayer;
        private bool _hasPlayingMusic = false;


        public override IEnumerator InitManager()
        {
            yield return null;
        }

        public override IEnumerator SetupManager()
        {
            yield return null;
        }

        public void PlaySFX(AudioClip clip, Transform playAtTransform, MixerGroup mixerGroup)
        {
            AudioSource selectedAudioSource = mixerGroup switch
            {
                MixerGroup.Master => _audioSourceMasterSFX,
                MixerGroup.PlayerSFX => _audioSourcePlayerSFX,
                MixerGroup.EnemySFX => _audioSourceEnemySFX,
                MixerGroup.PropSFX => _audioSourcePropSFX,
                MixerGroup.UI => _audioSourceUISFX,
                MixerGroup.Music => _audioSourceMusicSFX,
                _ => _audioSourceMasterSFX,
            };
            AudioSource audioInstance = Instantiate(selectedAudioSource, playAtTransform.position, Quaternion.identity);
            audioInstance.clip = clip;
            audioInstance.volume = 1f;

            audioInstance.Play();
            float duration = clip.length;
            Destroy(audioInstance.gameObject, duration);
        }

        public void PlayMusic(AudioClip clip, Transform playAtTransform)
        {
            StartCoroutine(SwitchMusic(clip, playAtTransform));
        }

        private IEnumerator SwitchMusic(AudioClip clip, Transform playAtTransform)
        {
            if (_musicPlayer == null)
            {
                _musicPlayer = Instantiate(_audioSourceMusicSFX, playAtTransform.position, Quaternion.identity);
                _musicPlayer.clip = clip;
                _musicPlayer.loop = true;
                _musicPlayer.volume = 0f;
                _musicPlayer.Play();

                _hasPlayingMusic = false;
            }

            if (!_hasPlayingMusic)
            {
                yield return IncreaseMusicVolume();
                yield break;
            }

            if (_hasPlayingMusic && clip != _musicPlayer.clip)
            {
                yield return DecreaseMusicVolume();
                _musicPlayer.clip = clip;
                _musicPlayer.Play();
                yield return IncreaseMusicVolume();
                yield break;
            }
        }

        private IEnumerator IncreaseMusicVolume()
        {
            if (!_hasPlayingMusic)
            {
                while (_musicPlayer.volume < 1f)
                {
                    _musicPlayer.volume += 0.1f;
                    yield return new WaitForSeconds(0.1f);
                }
                _musicPlayer.volume = 1f;
                _hasPlayingMusic = true;
            }
        }

        private IEnumerator DecreaseMusicVolume()
        {
            if (_hasPlayingMusic)
            {
                while (_musicPlayer.volume > 0f)
                {
                    _musicPlayer.volume -= 0.1f;
                    yield return new WaitForSeconds(0.1f);
                }
                _musicPlayer.volume = 0f;
                _hasPlayingMusic = false;
            }
        }

        private void Awake()
        {
            SingletonMethod();
        }
        private void SingletonMethod()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                //DontDestroyOnLoad(gameObject);
            }
        }
    }
}