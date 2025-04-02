using System;

namespace BoothApp.Presentation.BoothDetail.AddItem.Abstract
{
    [Serializable]
    public class TextFieldEmptyRule : IConditionTextRule
    {
        public bool IsConditionGood(string targetText)
        {
            return targetText != "";
        }
    }
}