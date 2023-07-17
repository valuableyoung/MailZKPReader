using AForm.Base;
using ALogic.Model.EntityFrame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ALogic.Logic.Base;

namespace AForm.Forms.Kontr
{
    public partial class CompanyList : AWindow
    {
        public CompanyList()
        {
            InitializeComponent();
            this.Tag = this.Name = "Список компаний";
        }

        public override void LoadData(object sender, EventArgs e)
        {
            /*var c = Context.Get(); 
             var listCompany = (from x in c.Company select x).ToList();
            tCompany.DataSource = listCompany.Count() > 0 ? listCompany.ConvertToDataTable() : null;
            */
        }

        public override void LoadControls()
        {
            tCompany.EventAdd += TCompany_EventAdd;
            tCompany.EventEdit += TCompany_EventEdit;
            tCompany.EventDelete += TCompany_EventDelete;          

            tCompany.AddColumn("idCompany", "Код");
            tCompany.AddColumn("nCompany", "Наименование");
            tCompany.AddColumn("nCity", "Город");

            tUnit.EventAdd += TUnit_EventAdd;
            tUnit.EventEdit += TUnit_EventEdit;
            tUnit.EventDelete += TUnit_EventDelete;

            tStructure.EventAdd += TStructure_EventAdd;
            tStructure.EventEdit += TStructure_EventAdd;

            tUnit.AddButton("Назначить", Properties.Resources.arrow_up, null);
        }

        private void TUnit_EventDelete(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TUnit_EventEdit(object sender, EventArgs e)
        {
            var row = tUnit.FocusedRow;
            if (row != null)
            {
                var c = Context.Get();
                int idCompanyEntity = int.Parse(row["idConmanyEntity"].ToString());
                var wind = new CompanyEdit3(idCompanyEntity);
                wind.OnChange += LoadData;
                WindowOpener.OpenWindow(wind);
            }
        }

        private void TUnit_EventAdd(object sender, EventArgs e)
        {            
            var wind = new CompanyEdit3(0);
            wind.OnChange += LoadData;
            WindowOpener.OpenWindow(wind);            
        }

        private void TCompany_EventDelete(object sender, EventArgs e)
        {
           /* var row = tCompany.FocusedRow;
            if (row != null)
            {
                var c = Context.Get();
                int idCompany = int.Parse(row["idCompany"].ToString());
                var company = (from x in c.Company where x.idCompany == idCompany select x).First();
                c.Company.DeleteObject(company);
                c.SaveChanges();
                LoadData(null, null);
            }*/
        }

        private void TCompany_EventEdit(object sender, EventArgs e)
        {
            /*var row = tCompany.FocusedRow;
            if (row != null)
            {
                int idCompany = int.Parse( row["idCompany"].ToString());
                var c = Context.Get();
                var company = (from x in c.Company where x.idCompany == idCompany select x).First();
                company.DbState = (int)EntityState.Modified;
                var wind = new CompanySimple(company);
                wind.ShowDialog();
                LoadData(null, null);
            }*/
        }

        private void TCompany_EventAdd(object sender, EventArgs e)
        {
            var c = new Company();
            c.DbState = (int)EntityState.Added;
            var wind = new CompanySimple(c);
            wind.ShowDialog();
            LoadData(null, null);
        }

        private void TStructure_EventAdd(object sender, EventArgs e)
        {
           /* var wind = new CompanyEdit3();
            WindowOpener.OpenWindow(wind);*/
        }

      
    }
}
