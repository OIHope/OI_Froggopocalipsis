using Core.Language;
using UnityEngine;

namespace Core.DialogueSystem
{
    public enum SpeakerEmotion
    {
        Default, Smiling, Happy, Sad, VerySad, WTF, Suspence, Wonder, Surprised, Upset, Angry, Mad, Guilty
    }
    [CreateAssetMenu(menuName =("Dialogue System/Data/Speaker Data"))]
    public class SpeakerSpriteData : ScriptableObject
    {
        [SerializeField] private string _characterNameEn;
        [SerializeField] private string _characterNameUa;

        [SerializeField] private Sprite _defaultEmotion;
        [SerializeField] private Sprite _smilingEmotion;
        [SerializeField] private Sprite _happyEmotion;
        [SerializeField] private Sprite _sadEmotion;
        [SerializeField] private Sprite _verySadEmotion;
        [SerializeField] private Sprite _wtfEmotion;
        [SerializeField] private Sprite _suspenceEmotion;
        [SerializeField] private Sprite _wonderEmotion;
        [SerializeField] private Sprite _surprisedEmotion;
        [SerializeField] private Sprite _upsetEmotion;
        [SerializeField] private Sprite _angryEmotion;
        [SerializeField] private Sprite _madEmotion;
        [SerializeField] private Sprite _guiltyEmotion;

        public string SpeakerName
        {
            get
            {
                GameLanguage currentLanguage = LanguageManager.Instance.CurrentGameLanguage;
                return currentLanguage switch
                {
                    GameLanguage.English => _characterNameEn,
                    GameLanguage.Ukrainian => _characterNameUa,
                    _ => _characterNameEn,
                };
            }
        }
        public Sprite SpeakerSprite(SpeakerEmotion emotion)
        {
            return emotion switch
            {
                SpeakerEmotion.Default => _defaultEmotion,
                SpeakerEmotion.Smiling => _smilingEmotion,
                SpeakerEmotion.Happy => _happyEmotion,
                SpeakerEmotion.Sad => _sadEmotion,
                SpeakerEmotion.VerySad => _verySadEmotion,
                SpeakerEmotion.WTF => _wtfEmotion,
                SpeakerEmotion.Suspence => _suspenceEmotion,
                SpeakerEmotion.Wonder => _wonderEmotion,
                SpeakerEmotion.Surprised => _surprisedEmotion,
                SpeakerEmotion.Upset => _upsetEmotion,
                SpeakerEmotion.Angry => _angryEmotion,
                SpeakerEmotion.Mad => _madEmotion,
                SpeakerEmotion.Guilty => _guiltyEmotion,
                _ => _defaultEmotion,
            };
        }
    }
}