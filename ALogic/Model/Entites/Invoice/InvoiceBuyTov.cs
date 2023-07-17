using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ALogic.Model.Entites.Invoice
{
    public class InvoiceBuyTov
    {
        public string IdArt { get; set; }
        public int Kol { get; set; }
        public decimal Price { get; set; }
        public decimal Summa { get; set; }
        public int MadeIn { get; set; }
        public string Declaration { get; set; }
        public string Brend { get; set; }

        public void LoadByRow(DataRow row)
        {
            IdArt = row.Table.Columns.IndexOf("IdTovOEM") == -1 ? "" : row["IdTovOEM"] == DBNull.Value ? "" : row["IdTovOEM"].ToString();
            Brend = row.Table.Columns.IndexOf("brend") == -1 ? "" : row["brend"] == DBNull.Value ? "" : row["brend"].ToString();
            Kol = row.Table.Columns.IndexOf("Kol") == -1 ? 0 : row["Kol"] == DBNull.Value ? 0 : int.Parse(row["Kol"].ToString());
            Price = row.Table.Columns.IndexOf("PriceCur") == -1 ? 0 : row["PriceCur"] == DBNull.Value ? 0 : decimal.Parse(row["PriceCur"].ToString());
            Summa = row.Table.Columns.IndexOf("SummaCur") == -1 ? 0 : row["SummaCur"] == DBNull.Value ? 0 : decimal.Parse(row["SummaCur"].ToString());
            MadeIn = row.Table.Columns.IndexOf("MadeIn") == -1 ? 0 : row["MadeIn"] == DBNull.Value || row["MadeIn"].ToString().Replace("-", "").Trim().Length == 0 ? 0 : int.Parse(row["MadeIn"].ToString());
            Declaration = row.Table.Columns.IndexOf("Declaration") == -1 ? "" : row["Declaration"] == DBNull.Value ? "" : row["Declaration"].ToString();
        }
    }
}
