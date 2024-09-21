using Core.Language;
using UnityEngine;

namespace Core.DialogueSystem
{
    [System.Serializable]
    public class DialogueData
    {
        [SerializeField] private string _englishLine;
        [SerializeField] private string _ukrainianLine;

        [SerializeField] private SpeakerSpriteData _speakerData;

        [SerializeField] private SpeakerEmotion _thisLineEmotion;

        public string Line
        {
            get
            {
                GameLanguage currentLanguage = LanguageManager.Instance.CurrentGameLanguage;
                return currentLanguage switch
                {
                    GameLanguage.English => _englishLine,
                    GameLanguage.Ukrainian => _ukrainianLine,
                    _ => _englishLine,
                };
            }
        }
        public Sprite SpeakerSprite => _speakerData.SpeakerSprite(_thisLineEmotion);
    }
}