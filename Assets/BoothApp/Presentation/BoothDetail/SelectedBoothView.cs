using BoothApp.Presentation.View;
using UnityEngine.Events;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("selectedBooth")] public SelectedBoothViewModel selectedBoothViewModel;
        public UnityEvent<SelectedBoothViewStatus> onViewStatusChange;
        
        #endregion

        #region Private Fields

        private SelectedBoothViewStatus _viewStatus = SelectedBoothViewStatus.Init;

        #endregion
        
        #region Method
        
        public void OnViewShow()
        {
            onViewShow.Invoke();
        }

        public void SetViewStatusToNormal()
        {
            ViewStatus = SelectedBoothViewStatus.Normal;
        }
        
        #endregion
    }
}