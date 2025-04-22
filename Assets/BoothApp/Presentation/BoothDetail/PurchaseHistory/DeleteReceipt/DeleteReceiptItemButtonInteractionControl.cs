using BoothApp.Presentation.BoothDetail.DeleteItem;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.DeleteReceipt
{
    public class DeleteReceiptItemButtonInteractionControl : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private UIButton uiButton;

        [SerializeField] private DeleteSelectedReceiptViewModel viewModel;

        #endregion

        #region Method

        private void Awake()
        {
            viewModel.onSelectReceipt.AddListener(SetUIButtonInteractable);
        }

        private void SetUIButtonInteractable()
        {
            if (viewModel.SelectedReceiptCount == 0)
                uiButton.interactable = false;
            else
                uiButton.interactable = true;
        }

        #endregion
    }
}