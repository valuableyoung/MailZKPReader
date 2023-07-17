using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraWaitForm;
using System.Threading;
using DevExpress.XtraLayout;
using DevExpress.XtraDataLayout;
using ALogic.Logic.Base;

namespace AForm.Base
{
    public partial class AWindow : Form
    {      
        //Progressor progress;
        public EventHandler CurrentEvent;
        public bool DataChanged;
        
        public AWindow()
        {
            InitializeComponent();
            DataChanged = false;
        }

        private void AWindow_Load(object sender, EventArgs e)
        {           
            LoadSize(this);
            NamedGrid(this);          
          
            PrivateLoadControls();
            PrivateDesignGrid(this);
            PrivateLoadData();
        }



        private void PrivateLoadData()
        {
            LoadData(this, null);
        }

        private void PrivateDesignGrid(AWindow aWindow)
        {
            DesignGrid(aWindow);
        }

        private void PrivateLoadControls()
        {
            LoadControls();
        }

        public virtual void LoadControls()
        {
            LoadAllControls(this);
        }

        public virtual void LoadData(object sender, EventArgs e)
        {
            LoadAllData(this);
        }

        public void LoadAllControls(Control p)
        {
            foreach (var control in p.Controls)
            {
                var ct = (control as Control);
                if (ct.Controls != null && ct.Controls.Count > 0)
                {
                    LoadAllControls(ct);
                }

                if (ct.GetType() == typeof(ATable))
                {
                    ATable t = (ct as ATable);
                    //t.LoadControl();
                }
            }
        }      

        public void LoadAllData(Control p)
        {
            foreach (var control in p.Controls)
            {
                var ct = (control as Control);
                if (ct.Controls != null && ct.Controls.Count > 0)
                {
                    LoadAllData(ct);
                }

                if (ct.GetType() == typeof(ATable))
                {
                    ATable t = (ct as ATable);
                    t.LoadData();
                }
            }
        }

        private void NamedGrid(Control p)
        {
            foreach (var control in p.Controls)
            {
                var ct = (control as Control);
                if (ct.Controls != null && ct.Controls.Count > 0)
                {
                    NamedGrid(ct);
                }

                if (ct.GetType() == typeof(ATable))
                {
                    ATable t = (ct as ATable);
                    t.FullName = this.Name + "_" + t.Name;                 
                }
            }
        }

        public void AcceptAllTable(Control p)
        {
            foreach (var control in p.Controls)
            {
                var ct = (control as Control);
                if (ct.Controls != null && ct.Controls.Count > 0)
                {
                    AcceptAllTable(ct);
                }

                if (ct.GetType() == typeof(ATable))
                {
                    ATable t = (ct as ATable);
                    t.AcceptEditor();
                }  
            }
        }


        private void DesignLayout(LayoutControlGroup g)
        {            
            foreach (var item in g.Items)
            {
                if (item.GetType() == typeof(LayoutControlGroup))
                {
                    (item as LayoutControlGroup).AppearanceGroup.Font = new Font("Arial", 10);
                    DesignLayout((LayoutControlGroup)item);
                }

                if (item.GetType() == typeof(LayoutControlItem))
                {                   
                    (item as LayoutControlItem).AppearanceItemCaption.Font = new Font("Arial", 10);
                }
            } 
        }

        public void AcceptLayout(LayoutControlGroup g)
        {
            foreach (var item in g.Items)
            {
                if (item.GetType() == typeof(LayoutControlGroup))
                {                  
                    DesignLayout((LayoutControlGroup)item);
                }

                if (item.GetType() == typeof(LayoutControlItem))
                {
                    var l = (item as LayoutControlItem);
                   
                }
            }
        }


        private void DesignGrid(Control p)
        {
            foreach (var control in p.Controls)
            {
                var ct = (control as Control);

                if (ct.GetType() == typeof(DataLayoutControl))
                {
                    (ct as DataLayoutControl).Appearance.Control.Font = new Font("Arial", 10);
                    DesignLayout((ct as DataLayoutControl).Root);
                }

                if (ct.Controls != null && ct.Controls.Count > 0)
                {
                    DesignGrid(ct);
                }

                if (ct.GetType() == typeof(ATable))
                {
                    ATable t = (ct as ATable);                  
                    FormDesigner.DesignGrid(t.GV);
                    t.LoadDesign();
                    t.GV.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(GV_CellValueChanged);
                }

                if (ct.GetType() == typeof(LookUpEdit))
                {
                    (ct as LookUpEdit).Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    (ct as LookUpEdit).Click += new EventHandler(AWindow_EditValueChanged);
                }

                if (ct.GetType() == typeof(DateEdit))
                {
                    (ct as DateEdit).Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    (ct as DateEdit).Click += new EventHandler(AWindow_EditValueChanged);
                }

                if (ct.GetType() == typeof(TextEdit))
                {
                    (ct as TextEdit).Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    (ct as TextEdit).Click += new EventHandler(AWindow_EditValueChanged);
                }
            }
        }

        void AWindow_EditValueChanged(object sender, EventArgs e)
        {
            DataChanged = true;
        }

        void GV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataChanged = true;
        }      

        private void LoadSize(Control p)
        {
            foreach (var control in p.Controls)
            {    
                var ct = (control as Control);

                if (ct.Controls != null && ct.Controls.Count > 0)
                {
                    LoadSize(ct);
                }

                if (control.GetType() == typeof(Splitter))
                {
                    var splitter = (control as Splitter);
                    int pos = ConrtolPositionLogic.GetControlPosition( this.Name + "_"+ splitter.Name);
                    splitter.SplitPosition = pos <= 0 ? splitter.SplitPosition : pos; 
                }

                if (control.GetType() == typeof(SplitterControl))
                {
                    var splitter = (control as SplitterControl);
                    int pos = ConrtolPositionLogic.GetControlPosition(this.Name + "_" + splitter.Name);
                    splitter.SplitPosition = pos <= 0 ? splitter.SplitPosition : pos; 
                }
            }
        }

        private void SaveSize(Control p)
        {
            foreach (var control in p.Controls)
            {
                var ct = (control as Control);
                if (ct.Controls != null && ct.Controls.Count > 0)
                {
                    SaveSize(ct);
                }

                if (control.GetType() == typeof(Splitter))
                {
                    var splitter = (control as Splitter);
                    ConrtolPositionLogic.SaveControlPosition(this.Name + "_" + splitter.Name, splitter.SplitPosition);
                }

                if (control.GetType() == typeof(SplitterControl))
                {
                    var splitter = (control as SplitterControl);
                    ConrtolPositionLogic.SaveControlPosition(this.Name + "_" + splitter.Name, splitter.SplitPosition);
                }
            }
        }

        public virtual void AWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSize(this);
            if (DataChanged)
            {
                if (MessageBox.Show("Обнаружены не сохраненные данные, выйти?", "Предупреждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    e.Cancel = true;
            }
        }

        public void ExecuteAction(EventHandler e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (!bwMain.IsBusy)
            {
                CurrentEvent = e;
                //ppLoad.Visible = true;
                SetAllEnabled(false);
                bwMain.RunWorkerAsync();
            }
            this.Cursor = Cursors.Default;
        }

        private void SetAllEnabled(bool p)
        {
            //throw new NotImplementedException();
        }

        private void bwMain_DoWork(object sender, DoWorkEventArgs e)
        {
            if (CurrentEvent != null)
            {              
                CurrentEvent(this, null);               
            }  
        }

        private void bwMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //ppLoad.Visible = false;
            CurrentEvent = null;
            SetAllEnabled(true);
        }

       

       /* public IParControl GetControlByName(string nControl, Control p)
        {
            foreach (var control in p.Controls)
            {
                var ct = (control as Control);
                if (ct.Name == nControl)
                    return ct as IParControl;

                if (ct.Controls != null && ct.Controls.Count > 0)
                {
                    foreach (var ctCont in ct.Controls)
                    {
                        var foudControl = GetControlByName(nControl, ctCont as Control);
                        if (foudControl != null)
                            return foudControl;
                    }
                }               
            }
            return null;
        }*/

        protected void SetGotFocusColorWhite(DevExpress.XtraDataLayout.DataLayoutControl dlc)
        {
            foreach (var c in dlc.Controls)
            {
                var ct = (c as BaseEdit);
                if (ct != null)
                    ct.GotFocus += new EventHandler(ct_GotFocus);
            }
        }

        protected void ct_GotFocus(object sender, EventArgs e)
        {
            (sender as BaseEdit).BackColor = Color.White;
        }

        protected void SetErrComponentRed(DataLayoutControl dlc, List<string> list)
        {
            foreach (var c in dlc.Controls)
            {
                var le = (c as BaseEdit);
                if (le != null && le.DataBindings.Count > 0 && list.IndexOf(le.DataBindings[0].BindingMemberInfo.BindingField) != -1)
                {
                    le.BackColor = Color.FromArgb(255, 99, 71);
                }
            }
        }

        protected void SetTableEditAll(ATable tMain, bool p)
        {
            foreach (var col in tMain.GV.Columns.ToList())
            {
                col.OptionsColumn.AllowEdit = p;
            }
        }
    }
}
