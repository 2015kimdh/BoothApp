using BoothApp.Presentation.BoothDetail.BaseClass;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.EditItem
{
    public class EditItemTagDropdown : ItemTagDropdownBase
    {
        public override void RefreshOnFalse()
        {
            Refresh();
            SetAllToggleFalse();
            SetCurrentTagSelection();
            SetCurrentTagLabel();
        }

        private void SetCurrentTagSelection()
        {
            foreach (var item in view.ItemTags)
            {
                var result = _itemTagLabels.Find(x => x.label.text == item);
                if (result != null)
                    result.toggle.isOn = true;
            }
        }
    }
}