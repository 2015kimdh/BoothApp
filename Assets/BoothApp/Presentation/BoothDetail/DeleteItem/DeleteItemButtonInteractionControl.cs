using System;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.DeleteItem
{
    public class DeleteItemButtonInteractionControl : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private UIButton uiButton;

        [SerializeField] private DeleteSelectedItemViewModel viewModel;

        #endregion

        #region Method

        private void Awake()
        {
            viewModel.onSelectItem.AddListener(SetUIButtonInteractable);
        }

        private void SetUIButtonInteractable()
        {
            if (viewModel.SelectedItemCount == 0)
                uiButton.interactable = false;
            else
                uiButton.interactable = true;
        }

        #endregion
    }
}