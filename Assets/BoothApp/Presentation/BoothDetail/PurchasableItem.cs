using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace BoothApp.Presentation.BoothDetail
{
    public class PurchasableItem : MonoBehaviour
    {
        #region Public Fields

        public Image itemImage;
        public TMP_Text itemName;
        public TMP_Text remainAmount;
        public TMP_Text tryToPurchaseAmount;
        public List<string> itemTag = new();
        public string owner = "";

        #endregion

        #region Private Fields

        private SelectedBooth _selectedBooth;

        #endregion

        #region Methods

        private void Awake()
        {
            _selectedBooth = FindObjectOfType<SelectedBooth>();
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
        public void RefreshRemainAmount() =>
            remainAmount.text = _selectedBooth.selectedBooth.RemainItemAmount(itemName.text).ToString();

        #endregion
    }
}