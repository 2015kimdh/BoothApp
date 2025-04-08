using System.Collections.Generic;
using System.Linq;
using BoothApp.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace BoothApp.Presentation.BoothDetail.AddItem
{
    public abstract class AddOptionWithInputField : MonoBehaviour
    {
        #region Unity Event

        public UnityEvent onAddSuccess;

        #endregion

        #region Serialize Fields

        [SerializeField] protected TMP_InputField inputField;
        [FormerlySerializedAs("selectedBooth")] [SerializeField] protected SelectedBoothViewModel selectedBoothViewModel;

        #endregion

        #region Property

        public string InputFieldValue => inputField.text;

        public bool IsViability => CheckViability();
        protected abstract List<string> OriginalList { get; }

        #endregion

        #region Methods

        public void AddItemInOriginal()
        {
            if (CheckViability())
            {
                OriginalList.Add(inputField.text);
                selectedBoothViewModel.selectedBooth.boothInformationInfo.modifyAt = DateTimeUtil.DateTimeNowToString();
                InitField();
                onAddSuccess.Invoke();
            }
        }

        public void InitField()
        {
            inputField.text = "";
        }


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