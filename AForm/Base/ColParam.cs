using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;

namespace AForm.Base
{
    public class ColParam
    {        
    
        public bool fSummary { get; set; }
        public bool fGroupSummary { get; set; }
        public int AfterPoint { get; set; }
        public string Band { get; set; }
        public RepositoryItem Repository { get; set; }
        public SummaryItemType SummaryItemType { get; set; }
        public DevExpress.Utils.HorzAlignment HorzAlignment { get; set; }        
        public bool fReadOnly { get; set; }

        public ColParam()
        {
            fSummary = false;
            fGroupSummary = false;
            AfterPoint = -1;
            Band = "";
            Repository = new RepositoryItemMemoEdit
            {
                WordWrap = true,
                AutoHeight = true
            };
            SummaryItemType = DevExpress.Data.SummaryItemType.Sum;
            HorzAlignment = DevExpress.Utils.HorzAlignment.Default;
            fReadOnly = false;
        }
    }
}
