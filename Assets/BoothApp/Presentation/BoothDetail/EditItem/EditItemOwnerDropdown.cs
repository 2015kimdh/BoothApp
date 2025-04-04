using BoothApp.Presentation.BoothDetail.BaseClass;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.EditItem
{
    public class EditItemOwnerDropdown : ItemOwnerDropdownBase
    {
        [SerializeField] private SelectedItem selectedItem;
        protected override void Awake()
        {
            base.Awake();
            view.onViewShow.AddListener(Refresh);
        }

        private void Refresh()
        {
            RefreshDropdown();
            SetDropdownValue(selectedItem.selectedItem.itemInfo.owner);
        }
    }
}