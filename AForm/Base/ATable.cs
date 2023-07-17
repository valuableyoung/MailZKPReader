using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.IO;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using System.Data.SqlClient;
using ALogic.Logic.SPR;
using ALogic.DBConnector;
using ALogic.Logic.Base;
using AForm.Properties;

namespace AForm.Base
{
    public partial class ATable : UserControl
    {
        public ATable()
        {
            InitializeComponent();

            _ButtonAdd = true;
            _ButtonDelete = true;
            _ButtonDesign = true;
            _ButtonEdit = false;
            _ButtonFound = true;
            _ButtonLoad = true;
            _ButtonSave = true;
            _ButtonSaveAs = false;
            _HideMenu = false;          
            _Banded = false;
        }              

        public GridView GV { get { return _Banded ? gvBanded : gvTable; } }
        public GridControl GC { get { return gcMain; } }

        public DataTable DataSource 
        { 
            get
            { 
                return (gcMain.DataSource as DataTable); 
            } 
            set 
            { 
                gcMain.DataSource = value;  
                if (value != null)
                    (gcMain.DataSource as DataTable).AcceptChanges(); 
            } 
        }    

        [DefaultValue(false)]
        public bool _HideMenu { get; set; }
        [DefaultValue(true)]
        public bool _ButtonFound { get; set; }
        [DefaultValue(true)]
        public bool _ButtonDesign { get; set; }
        [DefaultValue(true)]
        public bool _ButtonAdd { get; set; }
        [DefaultValue(false)]
        public bool _ButtonEdit { get; set; }
        [DefaultValue(true)]
        public bool _ButtonDelete { get; set; }
        [DefaultValue(true)]
        public bool _ButtonSave { get; set; }
        [DefaultValue(false)]
        public bool _ButtonSaveAs { get; set; }
        [DefaultValue(true)]
        public bool _ButtonLoad { get; set; }      
         
        [DefaultValue(false)]
        public bool _Banded { get; set; }

        public event EventHandler EventAdd;
        public event EventHandler EventEdit;
        public event EventHandler EventDelete;
        public event EventHandler EventSave;       
        public event EventHandler EventLoad;
        public event EventHandler EventAccept;
        public event EventHandler ChangeDataEvent;

        public string FullName { get; set; }   

        private void bGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           if (bGroup.Down)
               GV.OptionsView.ShowGroupPanel = true;
            else
               GV.OptionsView.ShowGroupPanel = false;
        }

        private void bFilter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bFilter.Down)
                GV.OptionsView.ShowAutoFilterRow = true;
            else
                GV.OptionsView.ShowAutoFilterRow = false;
        }

        private void bFound_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bFound.Down)
                GV.OptionsFind.AlwaysVisible = true;
            else
                GV.OptionsFind.AlwaysVisible = false;
        }

        private void ATable_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                barMain.Visible = !_HideMenu;

                bAdd.Visibility = _ButtonAdd ? BarItemVisibility.Always : BarItemVisibility.Never;
                bDel.Visibility = _ButtonDelete ? BarItemVisibility.Always : BarItemVisibility.Never;
                bEdit.Visibility = _ButtonEdit ? BarItemVisibility.Always : BarItemVisibility.Never;
                bSave.Visibility = _ButtonSave ? BarItemVisibility.Always : BarItemVisibility.Never;
                bSaveAs.Visibility = _ButtonSaveAs ? BarItemVisibility.Always : BarItemVisibility.Never;
                bLoad.Visibility = _ButtonLoad ? BarItemVisibility.Always : BarItemVisibility.Never;

                bDesign.Visibility = _ButtonDesign ? BarItemVisibility.Always : BarItemVisibility.Never;
                bFilter.Visibility = bFound.Visibility = bGroup.Visibility = _ButtonFound ? BarItemVisibility.Always : BarItemVisibility.Never;

                gcMain.MainView = GV;
                
            }         
        }

        private void bDesignSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            gvTable.SaveLayoutToStream(stream);
            DBTableDesign.saveProperties(FullName, Encoding.UTF8.GetString(stream.ToArray()), User.Current.IdUser);        
        }

        private void bSaveDesignDeffault_ItemClick(object sender, ItemClickEventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            gvTable.SaveLayoutToStream(stream);
            DBTableDesign.saveDefaultProperties(FullName, Encoding.UTF8.GetString(stream.ToArray()));
        }

        private void bDesignLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadDesign();          
        }

        public void LoadDesign()
        {
            MemoryStream stream = TableDesignLogic.GetDesign(FullName);
            if (stream != null)
                gvTable.RestoreLayoutFromStream(stream);
        }

        private void bDesignLoadDefault_ItemClick(object sender, ItemClickEventArgs e)
        {
            MemoryStream stream = TableDesignLogic.GetDesignDefault(FullName);
            if (stream != null)
                gvTable.RestoreLayoutFromStream(stream);
        }      
        
        private void bAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            AcceptEditor();

            if (EventAdd != null)
            {
                Cursor = System.Windows.Forms.Cursors.WaitCursor;
                EventAdd(this, null);
                Cursor = System.Windows.Forms.Cursors.Arrow;
            }
            else
                GV.AddNewRow();

            GC.RefreshDataSource();
            GV.RefreshData();

            GC.Update();
            GC.Refresh();
        }

        private void bEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (EventEdit != null)
            {
                Cursor = System.Windows.Forms.Cursors.WaitCursor;
                EventEdit(this, null);
                Cursor = System.Windows.Forms.Cursors.Arrow;
            }     
        }

        private void bDel_ItemClick(object sender, ItemClickEventArgs e)
        {          
            if (EventDelete != null)
                EventDelete(this, null);
            else  
                if (GV.GetFocusedDataRow() != null)
                     GV.GetFocusedDataRow().Delete();
        }

        private void bSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (EventSave != null)
                EventSave(this, null);          
        }  

        private void bSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (sdMain.ShowDialog() == DialogResult.OK)
            {               
                var ext = sdMain.FileName.Split('.').Last().ToUpper();
                if (ext == "XLS")
                {
                    gcMain.ExportToXls(sdMain.FileName);
                }

                if (ext == "XLSX")
                {
                    gcMain.ExportToXlsx(sdMain.FileName);
                }

                if (ext == "PDF")
                {
                    gcMain.ExportToPdf(sdMain.FileName);
                }

                if (ext == "TXT")
                {
                    gcMain.ExportToText(sdMain.FileName);
                }
            }
        }

        private void bLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            int tempHandle = GV.FocusedRowHandle;
            if (EventLoad != null)
            {
                 EventLoad(this, null);
            }
        
            GV.FocusedRowHandle = tempHandle;
            if (ChangeDataEvent != null)
                ChangeDataEvent(this, null);
        }      

        public void AddButton(string text, Bitmap img, ItemClickEventHandler e, bool toEnd = false)
        {
            BarButtonItem newButton = new BarButtonItem(bar, text);
            newButton.ImageOptions.Image = img;
            newButton.ItemClick += e;         

            newButton.Visibility = BarItemVisibility.Always;
            if (toEnd)
                barMain.LinksPersistInfo.Add(new LinkPersistInfo(newButton));
            else
                barMain.AddItem(newButton);
        }

  

        public void AddBand( string name, string text )
        {
            if (_Banded)
            {
                var band = new GridBand();
                band.Name = name;
                band.Caption = text;
                gvBanded.Bands.Add(band);
            }
        }


        #region Разные варианты добавления столбца

        private void AddColumn(GridColumn col)
        {
            gvTable.OptionsView.RowAutoHeight = true;
            gvTable.OptionsView.ColumnAutoWidth = false;
            gvTable.Columns.Add(col);
            col.Visible = true;           
        }

        private void AddColumn(BandedGridColumn col)
        {
            gvBanded.OptionsView.RowAutoHeight = true;
            gvBanded.OptionsView.ColumnAutoWidth = false;
            gvBanded.Columns.Add(col);
            col.Visible = true;           
        }

        public void AddColumn(string fieldName, string text)
        {
            var column = new GridColumn();
            column.Name = fieldName;
            column.FieldName = fieldName;
            column.Caption = text;
            RepositoryItemMemoEdit colmemo = new RepositoryItemMemoEdit
            {
                AutoHeight = true,
                WordWrap = true
            };
            column.ColumnEdit = colmemo;
            
            AddColumn(column);
        }

        public void AddColumn(string fieldName, string text, ColParam par, string ToolTip = "")
        {
            GridColumn column = par.Band.Trim().Length > 0 ? new BandedGridColumn() : new GridColumn();
            column.Name = fieldName;
            column.FieldName = fieldName;
            column.Caption = text;
            column.OptionsColumn.AllowEdit = !par.fReadOnly;

            RepositoryItemMemoEdit colmemo = new RepositoryItemMemoEdit
            {
                AutoHeight = true,
                WordWrap = true
            };

            column.ColumnEdit = colmemo;
            

            if (ToolTip != "") column.ToolTip = ToolTip;


            if (par.Band.Trim().Length > 0)
            {
                var band = gvBanded.Bands.FirstOrDefault(p => p.Name == par.Band);
                if (band != null)
                    band.Columns.Add((BandedGridColumn)column);
            }

            if (par.Repository != null)
                column.ColumnEdit = par.Repository;          
            
            if (par.AfterPoint >= 0)
            {
                column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                column.DisplayFormat.FormatString = "{0:n" + par.AfterPoint.ToString() + "}";  
                
                //column.Form
            }

            AddColumn(column);
            column.AppearanceCell.TextOptions.HAlignment = par.HorzAlignment;

            if (par.fGroupSummary)
            {
                GV.GroupSummary.Add(par.SummaryItemType, fieldName, GV.Columns[fieldName], column.DisplayFormat.FormatString);
            }

            if (par.fSummary)
            {
                GV.Columns[fieldName].Summary.Add(par.SummaryItemType);
                GV.Columns[fieldName].Summary[0].DisplayFormat = column.DisplayFormat.FormatString;
            }
        }
     

     

        #endregion
        
        public DataRow FocusedRow
        {
            get
            {
                return GV.GetFocusedDataRow();  
            }
        }

        public object FocusedObject
        {
            get
            {
                return GV.GetFocusedRow();
            }
        }

        public int FocusedRowNumber
        {
            get
            {
                 return GV.FocusedRowHandle;                
            }

            set
            {
                if (GV.RowCount >= value)
                    GV.FocusedRowHandle = GV.RowCount - 1;
                else
                    if (value < 0)
                        GV.FocusedRowHandle = 0;
                    else
                        GV.FocusedRowHandle = value;
            }
        }

        private void gvTable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                AcceptEditor();
            }
        }       

        private void gvBanded_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                AcceptEditor();
            }
        }

        public void AcceptEditor()
        {
            var gvMain = GV;
            var row = gvMain.GetFocusedDataRow();
            if (row == null)
                return;
            if (gvMain.ActiveEditor == null)
                return;
            if (row.Table.Columns.Contains(gvMain.FocusedColumn.FieldName))
            {
                try
                {
                    row[gvMain.FocusedColumn.FieldName] = gvMain.ActiveEditor.EditValue;
                    gvMain.UpdateCurrentRow();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }

            if (EventAccept != null)
                EventAccept(this, null);
        }  

     

        private void gvTable_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (ChangeDataEvent != null)
                ChangeDataEvent(this, null);
        }

        private void gvTable_DoubleClick(object sender, EventArgs e)
        {
            if (EventEdit != null && !_ToCheck)
            {
                Cursor = System.Windows.Forms.Cursors.WaitCursor;
                EventEdit(this, null);
                Cursor = System.Windows.Forms.Cursors.Arrow;                
            }
        }  

        private void gvTable_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            AcceptEditor();
        }

        private bool _ToCheck;
        public void SetTableCheck()
        {
            _ToCheck = true;
        }

        private void gcMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
