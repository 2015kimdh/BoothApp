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

        public Image itemImage;

        #endregion

        #region Serialize Field

        [SerializeField] private UIView view;
        [SerializeField]
        private SelectedBooth selectedBooth;

        #endregion

        private void Awake()
        {
            view.OnShowCallback.Event.AddListener(onViewShow.Invoke);
        }
    }
}