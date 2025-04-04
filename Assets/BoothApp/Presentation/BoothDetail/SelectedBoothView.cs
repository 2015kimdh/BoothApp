using BoothApp.Presentation.View;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail
{
    public class SelectedBoothView : ViewBase
    {
        #region Property

        public SelectedBoothViewStatus ViewStatus
        {
            get => _viewStatus;
            set
            {
                if (_viewStatus != value)
                {
                    _viewStatus = value;
                    onViewStatusChange.Invoke(_viewStatus);
                }
            }
        }

        #endregion
        
        #region Public Fields

        public UnityEvent onViewShow;
        public SelectedBooth selectedBooth;
        public UnityEvent<SelectedBoothViewStatus> onViewStatusChange;
        
        #endregion

        #region Private Fields

        private SelectedBoothViewStatus _viewStatus = SelectedBoothViewStatus.Normal;

        #endregion
        
        #region Method

        public void OnViewShow()
        {
            onViewShow.Invoke();
        }
        
        #endregion
    }
}