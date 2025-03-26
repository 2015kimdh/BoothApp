using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.DeleteBooth
{
    public class DeleteBoothFunction : MonoBehaviour
    {
        #region UnityEvents

        public UnityEvent onDeleteBooth;

        #endregion

        #region Serialize Fields

        [SerializeField] private BoothColumnGroup boothColumnGroup;

        #endregion

        #region Private Fields

        private BoothDataPresenter _presenter;
        private List<BoothItemSelectToggle> _boothItemSelectToggle = new();

        #endregion

        #region Method

        private void Awake()
        {
            _presenter = FindObjectOfType<BoothDataPresenter>();
        }

        public void RefreshItemSelectToggle()
        {
            _boothItemSelectToggle.Clear();
            foreach (var boothItem in boothColumnGroup.boothColumns)
                _boothItemSelectToggle.Add(boothItem.GetComponentInChildren<BoothItemSelectToggle>());
        }

        public void AbleDeleteFunction(bool isOn)
        {
            RefreshItemSelectToggle();

            foreach (var toggleItem in _boothItemSelectToggle)
            {
                if (isOn)
                {
                    toggleItem.SetActiveToggle();
                }
                else
                    toggleItem.SetDisableToggle();
            }
        }

        public void DeleteSelectedBooth()
        {
            List<BoothItemSelectToggle> selected = new();
            foreach (var toggleItem in _boothItemSelectToggle)
            {
                if (toggleItem.toggleValue == true)
                    selected.Add(toggleItem);
            }

            foreach (var item in selected)
            {
                _boothItemSelectToggle.Remove(item);
                var target = _presenter.boothInfo
                    .Find(x => x.boothInformationInfo.boothName == item.itemNameValue);
                _presenter.boothInfo.Remove(target);
            }

            _presenter.SaveDataAtDisk();
            onDeleteBooth.Invoke();
        }

        #endregion
    }
}