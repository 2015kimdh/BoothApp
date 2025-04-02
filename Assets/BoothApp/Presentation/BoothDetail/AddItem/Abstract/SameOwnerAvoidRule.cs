using System;
using System.Linq;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.AddItem.Abstract
{
    [Serializable]
    public class SameOwnerAvoidRule : IConditionTextRule
    {
        private SelectedBooth _selectedBooth;
        public bool IsConditionGood(string targetText)
        {
            if (_selectedBooth == null)
                _selectedBooth = GameObject.FindObjectOfType<SelectedBooth>();
            return CheckIsUnique(targetText);
        }

        private bool CheckIsUnique(string targetString)
        {
            var result = _selectedBooth.selectedBooth.boothInformationInfo.owners.Where(x=> x == targetString);
            if (!result.Any())
                return true;
            return false;
        }
    }
}