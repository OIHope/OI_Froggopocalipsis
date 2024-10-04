using Core.Language;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class ChangeLanguageAction : ActionBase
    {
        [SerializeField] private GameLanguage _language;
        protected override void ActionToPerform()
        {
            LanguageManager.Instance.OnRequestToChangeGameLanguage?.Invoke(_language);
        }
    }
}