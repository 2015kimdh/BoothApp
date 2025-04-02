using System.Collections.Generic;
using BoothApp.Presentation.Info;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoothApp.Presentation.BoothDetail
{
    public class AddNewItemView : MonoBehaviour
    {
        #region Unity Event

        public UnityEvent onViewShow;
        public UnityEvent onRefresh;

        #endregion

        #region Property

        public BoothInfo SelectedBooth => selectedBooth.selectedBooth;
        public List<string> ItemTags => SelectedBooth.boothInformationInfo.itemTags;
        public List<string> Owners => SelectedBooth.boothInformationInfo.owners;

        #endregion

        #region Public Field

        public string itemName = "";
        public int itemPrice = 0;
        public int amount = 0;
        public Image itemImage;
        public List<string> selectedTags;
        public string owner;

        #endregion

        #region Serialize Field

        [SerializeField] private UIView view;
        [SerializeField] private SelectedBooth selectedBooth;

        #endregion

        #region Method

        private void Awake()
        {
            view.OnShowCallback.Event.AddListener(onViewShow.Invoke);
        }

        #endregion
    }
}