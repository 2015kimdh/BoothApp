using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.AddItem
{
    public abstract class AddOptionWithInputField : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] protected TMP_InputField inputField;
        [SerializeField] protected SelectedBooth selectedBooth;

        #endregion

        #region Property

        public string InputFieldValue => inputField.text;

        public bool IsViability => CheckViability();
        protected abstract List<string> OriginalList { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 현재 입력된 필드의 값이 사용 가능한 조건인지 확인
        /// </summary>
        /// <returns></returns>
        private bool CheckViability()
        {
            if (OriginalList == null)
            {
                Debug.LogWarning("AddOptionWithInputField - OriginalList가 Null입니다.");
                return false;
            }

            if (inputField.text == "")
                return false;

            var result = OriginalList.Where(x => x == inputField.text).ToList();
            if (result.Count != 0)
                return false;
            return true;
        }
        
        #endregion
    }
}