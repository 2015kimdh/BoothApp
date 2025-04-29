using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.Filter
{
    public class DateTimeDropdownPicker : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private TMP_Dropdown yearDropdown;
        [SerializeField] private TMP_Dropdown monthDropdown;
        [SerializeField] private TMP_Dropdown dayDropdown;
        [SerializeField] private TextMeshProUGUI selectedDateText;

        #endregion

        #region Unity Event

        public UnityEvent onValueChange;

        #endregion

        #region Property

        public DateTime? SelectedDateTime => GetSelectedDate();

        #endregion

        #region Private Fields

        private int _startYear = 2024;
        private int _endYear = DateTime.Now.Year + 20;

        #endregion

        #region MonoBehaviour Event

        private void Awake()
        {
            SetupDropdowns();
            UpdateSelectedDateText();
        }

        #endregion

        #region Public Method

        public void SetSpecifyValue(DateTime? input)
        {
            if (input == null)
            {
                yearDropdown.value = 0;
                monthDropdown.value = 0;
                dayDropdown.value = 0;
                yearDropdown.RefreshShownValue();
                monthDropdown.RefreshShownValue();
                dayDropdown.RefreshShownValue();
                return;
            }
            
            string year = input.Value.Year.ToString();
            string month = input.Value.Month.ToString("D2");
            string day = input.Value.Day.ToString("D2");
            
            yearDropdown.value = yearDropdown.options.FindIndex(x => x.text == year);
            monthDropdown.value = monthDropdown.options.FindIndex(x => x.text == month);
            dayDropdown.value = dayDropdown.options.FindIndex(x => x.text == day);
            
            yearDropdown.RefreshShownValue();
            monthDropdown.RefreshShownValue();
            dayDropdown.RefreshShownValue();
        }

        #endregion
        
        #region Private Method

        private void SetupDropdowns()
        {
            PopulateYearDropdown();
            monthDropdown.ClearOptions();
            dayDropdown.ClearOptions();

            // 초기 상태
            monthDropdown.interactable = false;
            dayDropdown.interactable = false;

            yearDropdown.onValueChanged.AddListener(OnYearChanged);
            yearDropdown.onValueChanged.AddListener(delegate { EventInvoke(); });
            monthDropdown.onValueChanged.AddListener(OnMonthChanged);
            monthDropdown.onValueChanged.AddListener(delegate { EventInvoke(); });
            dayDropdown.onValueChanged.AddListener(delegate { UpdateSelectedDateText(); });
            dayDropdown.onValueChanged.AddListener(delegate { EventInvoke(); });
        }

        private void EventInvoke()
        {
            onValueChange.Invoke();
        }

        private void PopulateYearDropdown()
        {
            List<string> years = new List<string> { "미설정" };
            yearDropdown.ClearOptions();
            for (int year = _startYear; year <= _endYear; year++)
                years.Add(year.ToString());

            yearDropdown.AddOptions(years);
        }

        private void UpdateMonthDropdown()
        {
            List<string> months = new();
            for (int month = 1; month <= 12; month++)
            {
                months.Add(month.ToString("D2"));
            }

            monthDropdown.ClearOptions();
            monthDropdown.AddOptions(months);
        }

        private void UpdateDayDropdown(int year, int month)
        {
            List<string> days = new();
            int dayCount = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= dayCount; day++)
            {
                days.Add(day.ToString("D2"));
            }

            dayDropdown.ClearOptions();
            dayDropdown.AddOptions(days);
        }

        private void OnYearChanged(int index)
        {
            bool valid = index != 0;

            if (valid)
            {
                UpdateMonthDropdown();
                monthDropdown.interactable = true;
            }
            else
            {
                monthDropdown.ClearOptions();
                monthDropdown.interactable = false;
            }

            // 리셋
            dayDropdown.ClearOptions();
            dayDropdown.interactable = false;
            UpdateSelectedDateText();
        }

        private void OnMonthChanged(int index)
        {
            bool valid = yearDropdown.value != 0;

            if (valid)
            {
                int selectedYear = int.Parse(yearDropdown.options[yearDropdown.value].text);
                int selectedMonth = int.Parse(monthDropdown.options[monthDropdown.value].text);
                UpdateDayDropdown(selectedYear, selectedMonth);
                dayDropdown.interactable = true;
            }
            else
            {
                dayDropdown.ClearOptions();
                dayDropdown.interactable = false;
            }

            UpdateSelectedDateText();
        }

        private void UpdateSelectedDateText()
        {
            if (yearDropdown.value == 0)
            {
                selectedDateText.text = "선택된 날짜 없음";
                return;
            }

            int year = int.Parse(yearDropdown.options[yearDropdown.value].text);
            int month = 0;
            int day = 0;
            if (monthDropdown.options.Count != 0)
                month = int.Parse(monthDropdown.options[monthDropdown.value].text);
            if (dayDropdown.options.Count != 0)
                day = int.Parse(dayDropdown.options[dayDropdown.value].text);

            selectedDateText.text = $"{year}-{month:D2}-{day:D2}";
        }

        public DateTime? GetSelectedDate()
        {
            if (yearDropdown.value == 0)
                return null;

            int month = 1;
            int day = 1;
            int year = int.Parse(yearDropdown.options[yearDropdown.value].text);
            if (monthDropdown.options.Count != 0)
                month = int.Parse(monthDropdown.options[monthDropdown.value].text);
            if (dayDropdown.options.Count != 0)
                day = int.Parse(dayDropdown.options[dayDropdown.value].text);

            return new DateTime(year, month, day);
        }

        #endregion
    }
}