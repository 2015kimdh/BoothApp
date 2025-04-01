using System;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.AddItem
{
    public class ItemPriceAndName : MonoBehaviour
    {
        #region Property

        public string ItemName => itemName.text;
        public int ItemPrice => int.Parse(itemPrice.text);

        public bool IsViability => CheckViability();

        #endregion

        #region Serialize Field

        [SerializeField] private TMP_InputField itemName;
        [SerializeField] private TMP_InputField itemPrice;

        #endregion

        #region Private Field

        private AddNewItemView _itemView;

        #endregion

        #region MonoBehaviourEvent

        private void Awake()
        {
            _itemView = FindObjectOfType<AddNewItemView>();
        }

        #endregion
        
        #region Methods
        
        private void InitInputField()
        {
            itemName.text = "";
            itemPrice.text = "";
        }

        private bool CheckViability()
        {
            if (ItemName == "")
                return false;
            if (ItemPrice < 0)
                return false;
            return true;
        }
        
        #endregion
    }
}