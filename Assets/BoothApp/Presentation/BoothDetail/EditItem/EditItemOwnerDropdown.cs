using BoothApp.Presentation.BoothDetail.BaseClass;
using UnityEngine;
using UnityEngine.Serialization;

namespace BoothApp.Presentation.BoothDetail.EditItem
{
    public class EditItemOwnerDropdown : ItemOwnerDropdownBase
    {
        [FormerlySerializedAs("selectedItem")] [SerializeField] private SelectedItemViewModel selectedItemViewModel;
        protected override void Awake()
        {
            base.Awake();
            view.onViewShow.AddListener(Refresh);
        }

        private void Refresh()
        {
            RefreshDropdown();
            SetDropdownValue(selectedItemViewModel.selectedItem.itemInfo.owner);
        }
    }
}