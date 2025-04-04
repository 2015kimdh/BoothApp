using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace BoothApp.Presentation.BoothDetail.BaseClass
{
    public abstract class ItemNamePriceAmountBase : MonoBehaviour
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

        [SerializeField] protected TMP_InputField itemName;
        [SerializeField] protected TMP_InputField itemPrice;
        [SerializeField] protected TMP_InputField itemAmount;

        #endregion

        #region Private Field

        [SerializeField]
        protected ItemViewBase itemView;

        #endregion

        #region MonoBehaviourEvent

        protected virtual void Awake()
        {
            itemView.onViewShow.AddListener(InitInputField);
            
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
            if (ItemAmount < 1)
                return false;
            return true;
        }

        private void SetNameValue()
        {
            itemView.itemName = ItemName;
            itemView.namePriceAmountAbility = IsViability;
        }
        
        private void SetPriceValue()
        {
            itemView.itemPrice = ItemPrice;
            itemView.namePriceAmountAbility = IsViability;
        }
        
        private void SetAmountValue()
        {
            itemView.itemAmount = ItemAmount;
            itemView.namePriceAmountAbility = IsViability;
        }
        
        #endregion
    }
}