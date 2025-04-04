using BoothApp.Presentation.BoothDetail.BaseClass;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.EditItem
{
    public class EditItemNamePriceAmount : ItemNamePriceAmountBase
    {
        protected override void Awake()
        {
            itemView.onViewShow.AddListener(SetInputField);
            
            itemName.onValueChanged.AddListener(delegate { SetNameValue(); });
            itemPrice.onValueChanged.AddListener(delegate { SetPriceValue(); });
            itemAmount.onValueChanged.AddListener(delegate { SetAmountValue(); });
        }
        
        private void SetInputField()
        {
            itemName.text = itemView.itemName;
            itemPrice.text = itemView.itemPrice.ToString();
            itemAmount.text = itemView.itemAmount.ToString();
        }
    }
}