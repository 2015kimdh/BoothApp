using System;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail
{
    public class StatusTargetEvent : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private SelectedBoothViewStatus targetStatus;
        [SerializeField] private SelectedBoothView view;

        #endregion

        #region Event

        public UnityEvent onStatusEnter;
        public UnityEvent onStatusExit;

        #endregion

        private void Awake()
        {
            view.onViewStatusChange.AddListener(ListenStatus);
        }

        private void ListenStatus(SelectedBoothViewStatus status)
        {
            if (status == targetStatus)
                onStatusEnter.Invoke();
            else
                onStatusExit.Invoke();
        }
    }
}