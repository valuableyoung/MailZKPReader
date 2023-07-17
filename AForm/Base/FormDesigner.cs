using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AForm.Base
{
    public static class FormDesigner
    {
        public static void DesignGrid(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grid)
        {
            foreach (var col in grid.Columns.AsEnumerable())
            {
                col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                col.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

        }

        public static void DesignGrid(DevExpress.XtraGrid.Views.Grid.GridView grid)
        {
            foreach (var col in grid.Columns.AsEnumerable())
            {
                col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                col.AppearanceCell.Font = new System.Drawing.Font( "Arial", 10);
                col.AppearanceHeader.Font = new System.Drawing.Font("Arial", 10);
            }           

            foreach (var col in grid.Columns.AsEnumerable().Where(p => p.FieldName.ToUpper().IndexOf("DATE") != -1).ToList())
            {
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            }

            foreach (var col in grid.Columns.AsEnumerable().Where(p => p.FieldName.ToUpper().IndexOf("TIME") != -1).ToList())
            {
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            }
        }
    }
}
