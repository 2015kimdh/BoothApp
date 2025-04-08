using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace BoothApp.Presentation.BoothDetail
{
    public class PurchasableItem : MonoBehaviour
    {
        #region Property

        public int TryToPurchaseAmount
        {
            get => _tryToPurchaseAmount;
            set
            {
                if (value <= _originalAmount)
                {
                    _tryToPurchaseAmount = value;
                    purchaseAmountText.text = _tryToPurchaseAmount.ToString();
                }
            }
        }

        public int OriginalAmount
        {
            get => _originalAmount;
            set
            {
                _originalAmount = value;
                originalAmount.text = _originalAmount.ToString();
            }
        }

        #endregion

        #region Public Fields

        public Image itemImage;
        public TMP_Text itemName;
        public TMP_Text originalAmount;
        public TMP_Text purchaseAmountText;
        public List<string> itemTag = new();
        public string owner = "";
        public string hash = "";

        #endregion

        #region Private Fields

        private SelectedBoothViewModel _selectedBoothViewModel;
        private int _tryToPurchaseAmount = 0;
        private int _originalAmount = 0;

        #endregion

        #region Methods

        private void Awake()
        {
            _selectedBoothViewModel = FindObjectOfType<SelectedBoothViewModel>();
        }
        
        /// <summary>
        /// 생성한 아이템 표시 항목에 설정하는 세부 항목들
        /// </summary>
        /// <param name="image">품목 이미지. 없으면 공란</param>
        /// <param name="item">아이템 품명</param>
        /// <param name="itemTags">아이템을 구분할 태그</param>
        /// <param name="itemOwner">아이템 소유자</param>
        public void SetPurchasableItem(Sprite image, string item, List<string> itemTags, string itemOwner)
        {
            itemImage.sprite = image;
            itemName.text = item;
            itemTag = itemTags;
            owner = itemOwner;
        }

        /// <summary>
        /// 남은 개수 최신화
        /// </summary>
        public void RefreshOriginalAmount() =>
            OriginalAmount = _selectedBoothViewModel.selectedBooth.GetOriginalItemAmount(hash);
        
        #endregion
    }
}