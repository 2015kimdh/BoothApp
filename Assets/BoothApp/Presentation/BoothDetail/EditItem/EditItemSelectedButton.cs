using BoothApp.Presentation.View;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.EditItem
{
    public class EditItemSelectedButton : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private UIButton button;
        [SerializeField] private PurchasableItem purchasableItem;

        #endregion

        #region Private Field

        private SelectedBoothView _view;
        private SelectedItemViewModel _selectedItemViewModel;
        private const SelectedBoothViewStatus TargetStatus = SelectedBoothViewStatus.Modify;

        #endregion

        #region Method

        private void Awake()
        {
            _selectedItemViewModel = FindObjectOfType<SelectedItemViewModel>();
            _view = ViewHub.Views.Find(x => x.GetType() == typeof(SelectedBoothView)) as SelectedBoothView;
            button.onClickEvent.AddListener(SetMyData);
        }

        private void SetMyData()
        {
            if (_view.ViewStatus != TargetStatus)
                return;
            _selectedItemViewModel.SetSelectedItem(purchasableItem.hash);
        }

        #endregion
    }
}