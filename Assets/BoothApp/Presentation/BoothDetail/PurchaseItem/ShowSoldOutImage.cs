using System;
using BoothApp.Presentation.View;
using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation.BoothDetail.PurchaseItem
{
    /// <summary>
    /// 품절 이미지 보여주는 기능
    /// </summary>
    public class ShowSoldOutImage : MonoBehaviour
    {
        [SerializeField] private Image soldOutImage;
        [SerializeField] private PurchasableItem purchasableItem;
        private SelectedBoothView _view;
        private PurchasableItemGroup _group;

        private void Awake()
        {
            _view = ViewHub.Views.Find(x => x.GetType() == typeof(SelectedBoothView)) as SelectedBoothView;
            _group = FindObjectOfType<PurchasableItemGroup>();
            _view.onViewStatusChange.AddListener(ChangeImageStatus);
            _group.onRefresh.AddListener(ChangeImageStatus);
        }

        private void OnDestroy()
        {
            _view.onViewStatusChange.RemoveListener(ChangeImageStatus);
            _group.onRefresh.RemoveListener(ChangeImageStatus);
        }

        private void ChangeImageStatus(SelectedBoothViewStatus status)
        {
            if (status != SelectedBoothViewStatus.Purchase && status != SelectedBoothViewStatus.Normal)
                soldOutImage.gameObject.SetActive(false);
            else
            {
                soldOutImage.gameObject.SetActive(int.Parse(purchasableItem.originalAmount.text) == 0);
            }
        }

        private void ChangeImageStatus()
        {
            if (_view.ViewStatus != SelectedBoothViewStatus.Purchase && _view.ViewStatus != SelectedBoothViewStatus.Normal)
            {
                ChangeImageStatus(_view.ViewStatus);
                return;
            }

            soldOutImage.gameObject.SetActive(int.Parse(purchasableItem.originalAmount.text) == 0);
        }
    }
}