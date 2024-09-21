using UnityEngine;

namespace Core.DialogueSystem
{
    public enum SpeakerEmotion
    {
        Default, Smiling, Happy, Sad, Crying, Surprised
    }
    [CreateAssetMenu(menuName =("Dialogue System/Data/Speaker Data"))]
    public class SpeakerSpriteData : ScriptableObject
    {
        [SerializeField] private Sprite _defaultEmotion;
        [SerializeField] private Sprite _smilingEmotion;
        [SerializeField] private Sprite _happyEmotion;
        [SerializeField] private Sprite _sadEmotion;
        [SerializeField] private Sprite _cryingEmotion;
        [SerializeField] private Sprite _surprisedEmotion;

        public Sprite SpeakerSprite(SpeakerEmotion emotion)
        {
            return emotion switch
            {
                SpeakerEmotion.Default => _defaultEmotion,
                SpeakerEmotion.Smiling => _smilingEmotion,
                SpeakerEmotion.Happy => _happyEmotion,
                SpeakerEmotion.Sad => _sadEmotion,
                SpeakerEmotion.Crying => _cryingEmotion,
                SpeakerEmotion.Surprised => _surprisedEmotion,
                _ => _defaultEmotion,
            };
        }
    }
}