using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ALogic.Logic.SPR.Old;
using ALogic.Logic.Heplers;
using System.Windows.Forms;

namespace ALogic.Logic.Old.BuySalePlan
{
    public class SalePlanEntity
    {       
        DataTable _deletedData;
        DataTable _data = new DataTable();
        int _year;

        public DataTable LoadData(object Year, bool onlyEntered = false)
        {
            if (Year == null || Year.ToString() == "")
                return null;

            _year = int.Parse(Year.ToString());

            _data = SalePlanLogic.GetSalePlan(_year, onlyEntered);
            _deletedData = _data.Clone();
           
            return _data;          
        }



        public DataTable LoadDataFromFile(string filepath)
        {
            var datatable = FileReader.Read(filepath);
            DataTable table = datatable[0];

            try
            {
                _data = LoadData(_year).Clone();

                foreach (DataRow item in table.Rows)
                {
                    DataRow row = _data.NewRow();
                    row["idbrend"] = DBSprBrend.getBrandIDByName(item[0].ToString());
                    row["idSegm"] = DBSprSegment.getIDSegmentByName(item[1].ToString());
                    //row["idRegion"] = DBRegion.getRegionIDByName(item[2].ToString());
                    row["type"] = item[2].ToString();
                    double Marga = 0; double.TryParse(item[3].ToString(), out Marga);
                    row["Marga"] = Marga;
                    // double naklRasx = 0; double.TryParse(item[4].ToString(), out naklRasx);
                    // row["naklRasx"] = naklRasx;
                    for (int i = 1; i < 13; i++)
                    {
                        //double k = 0; double.TryParse(item[i + 4].ToString(), out k);
                        double k = 0; double.TryParse(item[3].ToString(), out k);
                        row["k" + i.ToString()] = k;
                    }

                    _data.Rows.Add(row);
                }

                return _data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки файла: " + ex.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public bool TableChanged()
        {

            var dat = _data.GetChanges(DataRowState.Modified);
            if (dat != null && dat.Rows.Count > 0) return true;


            return false;
        }

        public void SaveData()
        {
            if ( _year == 0 )
                return;

            var newData = _data.AsEnumerable().Where(p => p.RowState == DataRowState.Added || p.RowState == DataRowState.Modified).ToList();

            int maxcount = newData.Count();
            int count = 0;

            foreach (var row in newData)
            {
                SalePlanLogic.SaveData(row, _year);
                count++;               
            }

            foreach (var row in _deletedData.AsEnumerable())
            {
                SalePlanLogic.DelData(row, _year);
            }

        }

        public void DelData(DataRow dataRow)
        {
            _deletedData.Rows.Add(dataRow.ItemArray);
        }

        public void FillNewRow(DataRow dataRow)
        {
            dataRow["idBrend"] = 0;
            dataRow["idRegion"] = 0;
            dataRow["idSegm"] = 0;
            dataRow["marga"] = 0;
            for (int i = 1; i < 13; i++)
                dataRow["k" + i.ToString()] = 0;
        }

        public void AcceptChanges()
        {
            _data.AcceptChanges();
        }
    }
}
