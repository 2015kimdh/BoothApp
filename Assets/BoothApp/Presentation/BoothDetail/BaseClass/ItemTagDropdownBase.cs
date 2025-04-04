using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.BoothDetail.AddItem;
using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.BaseClass
{
    public abstract class ItemTagDropdownBase : MonoBehaviour
    {
        #region Property

        public List<string> SelectedTags => GetSelectedTags();

        #endregion

        #region Serialize Field

        [SerializeField] protected ItemViewBase view;

        [SerializeField] protected GameObject selectionPanel;
        [SerializeField] protected GameObject itemTagPrefab;
        [SerializeField] protected GameObject parentObject;
        [SerializeField] protected UIButton dropDownButton;
        [SerializeField] protected TMP_Text currentTags;

        #endregion

        #region Private Field

        protected List<ItemTagLabel> _itemTagLabels = new();

        protected List<string> OriginalTagList => view.ItemTags;

        #endregion

        #region MonoBehaviourEvent

        private void Awake()
        {
            dropDownButton.onClickEvent.AddListener(ShowPanel);
        }

        #endregion

        #region Public Method

        public void Refresh()
        {
            MakeOptions();
        }

        public virtual void RefreshOnFalse()
        {
            Refresh();
            SetAllToggleFalse();
            SetCurrentTagLabel();
        }

        #endregion

        #region Private Methods

        private void ShowPanel()
        {
            selectionPanel.SetActive(!selectionPanel.activeInHierarchy);
        }

        private List<string> GetSelectedTags()
        {
            var result = _itemTagLabels.Where(x => x.toggle.isOn == true).ToList();

            List<string> resultList = new();
            foreach (var item in result)
                resultList.Add(item.label.text);
            return resultList;
        }

        private void MakeOptions()
        {
            List<string> instantiatedList = new();
            foreach (var item in _itemTagLabels)
                instantiatedList.Add(item.label.text);

            var except = instantiatedList.Except(OriginalTagList).ToList();

            for (int i = 0; i < except.Count; i++)
            {
                var target = _itemTagLabels.Find(x => x.label.text == except[i]);
                _itemTagLabels.Remove(target);
                Destroy(target.gameObject);
            }

            var needToMake = OriginalTagList.Except(instantiatedList).ToList();

            for (int i = 0; i < needToMake.Count; i++)
            {
                var newItem = Instantiate(itemTagPrefab, parentObject.transform).GetComponent<ItemTagLabel>();
                newItem.label.text = needToMake[i];
                newItem.toggle.isOn = false;
                newItem.toggle.onValueChanged.AddListener(delegate { SetTagData(); });
                newItem.toggle.onValueChanged.AddListener(delegate { SetCurrentTagLabel(); });
                _itemTagLabels.Add(newItem);
            }
        }

        protected void SetAllToggleFalse()
        {
            foreach (var item in _itemTagLabels)
                item.toggle.isOn = false;
        }

        private void SetTagData()
        {
            List<string> selectedTags = new();
            foreach (var item in _itemTagLabels)
            {
                if (item.toggle.isOn)
                    selectedTags.Add(item.label.text);
            }

            view.selectedTags = selectedTags;
        }

        protected void SetCurrentTagLabel()
        {
            currentTags.text = "미설정";
            for (int i = 0; i < view.selectedTags.Count; i++)
            {
                if(i == 0)
                    currentTags.text = view.selectedTags[i];
                else
                    currentTags.text = currentTags.text + "/" + view.selectedTags[i];
            }
        }

        #endregion
    }
}