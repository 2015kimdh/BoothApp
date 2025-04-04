using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.BaseClass
{
    public abstract class ItemOwnerDropdownBase : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] protected TMP_Dropdown dropdown;
        [SerializeField] protected ItemViewBase view;

        #endregion

        #region Private Field

        protected const string DefaultOwner = "미설정";

        #endregion

        #region Methods

        protected virtual void Awake()
        {
            dropdown.onValueChanged.AddListener(delegate { SetValue(); });
        }

        public void RefreshDropdown()
        {
            dropdown.ClearOptions();
            List<TMP_Dropdown.OptionData> optionData = new();

            // 기본 선택지를 0번 인덱스에 설정
            AddDropdownOption(optionData, DefaultOwner);

            foreach (var owner in view.Owners)
                AddDropdownOption(optionData, owner);

            dropdown.AddOptions(optionData);
        }

        public void SetDropdownValue(string owner)
        {
            for (int i = 0; i < dropdown.options.Count; i++)
            {
                if (dropdown.options[i].text == owner)
                {
                    dropdown.value = i;
                    return;
                }
            }

            dropdown.value = 0;
        }

        protected void AddDropdownOption(List<TMP_Dropdown.OptionData> optionData, string optionName)
        {
            TMP_Dropdown.OptionData newOptionData = new()
            {
                text = optionName
            };
            optionData.Add(newOptionData);
        }

        protected void SetValue()
        {
            if (dropdown.value == 0)
                view.owner = "";
            else
                view.owner = dropdown.options[dropdown.value].text;
        }

        #endregion
    }
}