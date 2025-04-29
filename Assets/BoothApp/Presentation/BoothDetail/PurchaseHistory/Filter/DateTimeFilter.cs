using System;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.Filter
{
    public class DateTimeFilter : MonoBehaviour
    {
        [SerializeField] private PurchaseHistoryViewModel viewModel;
        [SerializeField] private BoothDataPresenter presenter;
        [SerializeField] private DateTimeDropdownPicker from;
        [SerializeField] private DateTimeDropdownPicker to;
        [SerializeField] private UIButton confirmButton;
        [SerializeField] private TMP_Text ruleAnnouncement;
        private Color _errorColor = Color.red;
        private Color _correctColor = new Color(0, 0.65f, 0);

        private PurchaseHistoryFilterAttributeInfo FilterAttributeInfo =>
            viewModel.SelectedBooth.boothInformationInfo.purchaseHistoryFilterAttribute;

        #region UnityEvent

        public UnityEvent onFilterSet;

        #endregion

        private void Awake()
        {
            from.onValueChange.AddListener(CheckValid);
            to.onValueChange.AddListener(CheckValid);
            confirmButton.onClickEvent.AddListener(SetFilter);
        }

        #region Private Method

        private void CheckValid()
        {
            bool isValid = true;
            DateTime? fromDate = from.GetSelectedDate();
            DateTime? toDate = to.GetSelectedDate();

            if (fromDate != null && toDate != null)
                if (toDate.Value < fromDate.Value)
                    isValid = false;
            confirmButton.interactable = isValid;
            ruleAnnouncement.color = isValid ? _correctColor : _errorColor;
        }

        private void SetFilter()
        {
            if (from.GetSelectedDate().HasValue)
            {
                var forFrom = new DateTime(from.GetSelectedDate().Value.Year,
                    from.GetSelectedDate().Value.Month,
                    from.GetSelectedDate().Value.Day,
                    0, 0, 1);
                FilterAttributeInfo.limit1 = DateTimeUtil.DateTimeToString(forFrom);
            }
            else
            {
                FilterAttributeInfo.limit1 = "";
            }

            if (to.GetSelectedDate().HasValue)
            {
                var forTo = new DateTime(to.GetSelectedDate().Value.Year,
                    to.GetSelectedDate().Value.Month,
                    to.GetSelectedDate().Value.Day,
                    11, 59, 59);
                FilterAttributeInfo.limit2 = DateTimeUtil.DateTimeToString(forTo);
            }
            else
            {
                FilterAttributeInfo.limit2 = "";
            }

            viewModel.SelectedBooth.UpdateModifyTime();
            presenter.SaveDataAtDisk();
            onFilterSet.Invoke();
        }

        #endregion

        #region Public Method

        public void SetPreviousDateTimeToPicker()
        {
            DateTime? toFrom = FilterAttributeInfo.limit1 == ""
                ? null
                : DateTimeUtil.DateTimeStringToDateTime(FilterAttributeInfo.limit1);

            DateTime? toTo = FilterAttributeInfo.limit2 == ""
                ? null
                : DateTimeUtil.DateTimeStringToDateTime(FilterAttributeInfo.limit2);

            from.SetSpecifyValue(toFrom);
            to.SetSpecifyValue(toTo);
        }

        public void ResetLimit()
        {
            from.SetSpecifyValue(null);
            to.SetSpecifyValue(null);
        }
        
        #endregion
    }
}