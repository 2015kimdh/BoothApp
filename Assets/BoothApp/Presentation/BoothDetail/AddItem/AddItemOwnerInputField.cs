using System.Collections.Generic;

namespace BoothApp.Presentation.BoothDetail.AddItem
{
    public class AddItemOwnerInputField : AddOptionWithInputField
    {
        #region Property
        protected override List<string> OriginalList 
            => selectedBooth.selectedBooth.boothInformationInfo.owners;
        
        #endregion
    }
}