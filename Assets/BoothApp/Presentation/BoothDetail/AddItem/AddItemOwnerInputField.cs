using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.AddItem
{
    public class AddItemOwnerInputField : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private SelectedBooth selectedBooth;

        #endregion

        #region Property

        public string InputFieldValue => inputField.text;

        public bool IsViability => CheckViability();
        private List<string> OwnerList => selectedBooth.selectedBooth.boothInformationInfo.owners;
        
        #endregion

        #region Methods

        /// <summary>
        /// 현재 입력된 필드의 값이 사용 가능한 조건인지 확인
        /// </summary>
        /// <returns></returns>
        private bool CheckViability()
        {
            if (inputField.text == "")
                return false;

            var result = OwnerList.Where(x => x == inputField.text).ToList();
            if (result.Count != 0)
                return false;
            return true;
        }
        
        #endregion
    }
}