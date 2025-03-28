using System;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail
{
    public class SelectedBoothView : MonoBehaviour
    {
        #region Property

        public SelectedBoothViewStatus viewStatus
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

        private void Start()
        {
            
        }

        public void OnViewShow()
        {
            onViewShow.Invoke();
        }
        
        #endregion
    }
}