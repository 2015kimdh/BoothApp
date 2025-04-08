using System;
using System.Linq;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.AddItem.Abstract
{
    [Serializable]
    public class SameTagAvoidRule : IConditionTextRule
    {
        private SelectedBoothViewModel _selectedBoothViewModel;
        public bool IsConditionGood(string targetText)
        {
            if (_selectedBoothViewModel == null)
                _selectedBoothViewModel = GameObject.FindObjectOfType<SelectedBoothViewModel>();
            return CheckIsUnique(targetText);
        }

        private bool CheckIsUnique(string targetString)
        {
            var result = _selectedBoothViewModel.selectedBooth.boothInformationInfo.itemTags.Where(x=> x == targetString);
            if (!result.Any())
                return true;
            return false;
        }
    }
}