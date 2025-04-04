using BoothApp.Presentation.BoothDetail.BaseClass;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.EditItem
{
    public class EditItemTagDropdown : ItemTagDropdownBase
    {
        public override void RefreshOnFalse()
        {
            setDataFlag = false;
            Refresh();
            SetAllToggleFalse();
            setDataFlag = true;
            SetCurrentTagSelection();
            SetCurrentTagLabel();
        }

        private void SetCurrentTagSelection()
        {
            foreach (var item in view.selectedTags)
            {
                var result = _itemTagLabels.Find(x => x.label.text == item);
                if (result != null)
                    result.toggle.isOn = true;
                else
                    result.toggle.isOn = false;

            }
        }
    }
}