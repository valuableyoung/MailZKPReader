using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AForm.Forms.Old.BuySalePlan
{
    public interface ITableForm
    {
        void InitControls();
        void AcceptEditor();
        void LoadData();
        void SaveData();
        void DelData();
        void AddRow();
        void OnKeyEnter();
        void SaveTableXml();
        void LoadTableXml();       
    }
}
