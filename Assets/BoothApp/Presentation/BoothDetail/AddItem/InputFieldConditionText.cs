using System;
using System.Collections.Generic;
using BoothApp.Presentation.BoothDetail.AddItem.Abstract;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.AddItem
{
    [Serializable]
    public class InputFieldConditionText : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private TMP_InputField inputField;

        [SerializeField] private List<TMP_Text> conditionTexts;

        [SerializeReference,SubclassSelector] private List<IConditionTextRule> rules;
        
        #endregion

        #region Private Fields

        private Color _red = Color.red;
        private Color _green = Color.green;

        #endregion

        #region MonoBehaviour Events

        private void Awake()
        {
            inputField.onValueChanged.AddListener(x => IsConditionGood(x));
        }
        
        #endregion

        #region Method
        
        /// <summary>
        /// 조건에 따른 텍스트 색 변경
        /// 0번은 0번끼리 매칭. 숫자 따라
        /// </summary>
        /// <returns>조건 탐색의 결과</returns>
        public bool IsConditionGood(string targetText)
        {
            bool result = true;
            for (int i = 0; i < conditionTexts.Count; i++)
            {
                if(rules[i].IsConditionGood(targetText))
                    SetColor(conditionTexts[i], _green);
                else
                {
                    SetColor(conditionTexts[i], _red);
                    result = false;
                }
            }

            return result;
        }

        protected void SetColor(TMP_Text target, Color color)
        {
            target.color = color;
        }

        #endregion
    }
}