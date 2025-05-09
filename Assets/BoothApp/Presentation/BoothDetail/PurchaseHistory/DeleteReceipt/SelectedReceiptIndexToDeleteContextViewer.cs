using System;
using BoothApp.Utility;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.DeleteReceipt
{
    public class SelectedReceiptIndexToDeleteContextViewer : MonoBehaviour
    {
        [SerializeField] private DeleteSelectedReceiptViewModel viewModel;
        [SerializeField] private TMP_Text text;

        private void Awake()
        {
            viewModel.onSelectReceipt.AddListener(SetText);
        }
        
        private void SetText()
        {
            string contents = "";
            
            for (int i = 0; i < viewModel.SelectedReceiptCount; i++)
            {
                if (i != 0)
                    contents += "<br>";
                contents += DateTimeUtil.DateTimeStringForPurchaseHistoryItem(viewModel.SelectedReceipt[i].purchasedAt);
            }

            text.text = contents;
        }
    }
}