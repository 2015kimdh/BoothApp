using System;
using BoothApp.Presentation.View;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail
{
    public class ViewStatusChangeButton : MonoBehaviour
    {
        [SerializeField] private SelectedBoothViewStatus targetStatus;
        [SerializeField] private UIButton button;
        private SelectedBoothView view;

        private void Start()
        {
            view = ViewHub.Views.Find(x => x.GetType() == typeof(SelectedBoothView)) as SelectedBoothView;
            button.onClickEvent.AddListener(SetStatus);
        }

        private void SetStatus()
        {
            if (targetStatus == view.ViewStatus)
                view.ViewStatus = SelectedBoothViewStatus.Normal;
            else
                view.ViewStatus = targetStatus;
        }
    }
}