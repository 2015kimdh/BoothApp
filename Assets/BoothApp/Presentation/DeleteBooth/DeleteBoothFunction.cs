using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.DeleteBooth
{
    public class DeleteBoothFunction : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private BoothColumnGroup boothColumnGroup;

        #endregion

        #region Private Fields

        private List<BoothItemSelectToggle> _boothItemSelectToggle = new();

        #endregion

        #region Method

        public void RefreshItemSelectToggle()
        {
            _boothItemSelectToggle.Clear();
            foreach (var boothItem in boothColumnGroup.boothColumns)
                _boothItemSelectToggle.Add(boothItem.GetComponentInChildren<BoothItemSelectToggle>());
        }

        public void AbleDeleteFunction(bool isOn)
        {
            if (isOn)
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
                if (toggleItem.toggleValue)
                    selected.Add(toggleItem);
            }
            
            

        }
        
        #endregion
    }
}