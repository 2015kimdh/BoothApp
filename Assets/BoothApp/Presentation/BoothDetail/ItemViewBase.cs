using System.Collections.Generic;
using BoothApp.Presentation.Info;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoothApp.Presentation.BoothDetail
{
    public abstract class ItemViewBase : MonoBehaviour
    {
        #region Unity Event

        public UnityEvent onViewShow;
        public UnityEvent onRefresh;
        
        public UnityEvent onFail;
        public UnityEvent onSuccess;
        
        #endregion
        
        #region Property

        public BoothInfo SelectedBooth => selectedBooth.selectedBooth;
        public List<string> ItemTags => SelectedBooth.boothInformationInfo.itemTags;
        public List<string> Owners => SelectedBooth.boothInformationInfo.owners;

        #endregion
        
        #region Serialize Field

        [SerializeField] protected SelectedBooth selectedBooth;

        #endregion
        
        #region Public Field

        public string itemName = "";
        public int itemPrice = 0;
        public int itemAmount = 0;
        public Image itemImage;
        public List<string> selectedTags;
        public string owner;
        public bool namePriceAmountAbility = false;

        #endregion
        
        public void OnShowInvoke()
        {
            onViewShow.Invoke();
        }
    }
}