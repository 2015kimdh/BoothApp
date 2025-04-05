using BoothApp.Presentation.View;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.Button
{
    public class ViewStatusChangeButton : MonoBehaviour
    {
        [SerializeField] private SelectedBoothViewStatus targetStatus;
        [SerializeField] private UIButton button;
        private SelectedBoothView _view;

        private void Start()
        {
            _view = ViewHub.Views.Find(x => x.GetType() == typeof(SelectedBoothView)) as SelectedBoothView;
            button.onClickEvent.AddListener(SetStatus);
        }

        private void SetStatus()
        {
            if (targetStatus == _view.ViewStatus)
                _view.ViewStatus = SelectedBoothViewStatus.Normal;
            else
                _view.ViewStatus = targetStatus;
        }
    }
}