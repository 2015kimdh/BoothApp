using System;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.AddItem
{
    public class ItemPriceAndNameAmount : MonoBehaviour
    {
        #region Property

        public string ItemName => itemName.text;

        public int ItemPrice
        {
            get
            {
                if (itemPrice.text != "")
                    return int.Parse(itemPrice.text);
                else
                    return 0;
            }
        }

        public int ItemAmount
        {
            get
            {
                if (itemAmount.text != "")
                    return int.Parse(itemAmount.text);
                else
                    return 0;
            }
        }

        public bool IsViability => CheckViability();

        #endregion

        #region Serialize Field

        [SerializeField] private TMP_InputField itemName;
        [SerializeField] private TMP_InputField itemPrice;
        [SerializeField] private TMP_InputField itemAmount;

        #endregion

        #region Private Field

        private AddNewItemView _itemView;

        #endregion

        #region MonoBehaviourEvent

        private void Awake()
        {
            _itemView = FindObjectOfType<AddNewItemView>();
            _itemView.onViewShow.AddListener(InitInputField);
            
            itemName.onValueChanged.AddListener(delegate { SetNameValue(); });
            itemPrice.onValueChanged.AddListener(delegate { SetPriceValue(); });
            itemAmount.onValueChanged.AddListener(delegate { SetAmountValue(); });
        }

        #endregion

        #region Methods

        private void InitInputField()
        {
            itemName.text = "";
            itemPrice.text = "";
            itemAmount.text = "";
        }

        private bool CheckViability()
        {
            if (ItemName == "")
                return false;
            if (ItemPrice < 0)
                return false;
            if (ItemAmount < 0)
                return false;
            return true;
        }

        private void SetNameValue()
        {
            _itemView.itemName = ItemName;
        }
        
        private void SetPriceValue()
        {
            _itemView.itemPrice = ItemPrice;
        }
        
        private void SetAmountValue()
        {
            _itemView.itemName = ItemName;
        }
        
        #endregion
    }
}