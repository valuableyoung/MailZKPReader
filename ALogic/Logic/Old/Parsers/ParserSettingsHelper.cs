using ALogic.DBConnector;
using ALogic.Logic.SPR;
using ALogic.Logic.SPR.Old;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static ALogic.Logic.Old.Parsers.MailPriceReader;

namespace ALogic.Logic.Old.Parsers
{
    public class ParserSettingsHelper
    {
        public static void fillBrand(ComboBox cbSetBrand)
        {
            DataTable tableBrand = DBSprBrend.GetBrends();
            DataRow row = tableBrand.NewRow();
            row[0] = 0;
            row[1] = " -- Выберите бренд --";
            tableBrand.Rows.InsertAt(row, 0);
            cbSetBrand.BeginUpdate();
            cbSetBrand.DataSource = tableBrand;
            cbSetBrand.DisplayMember = "tm_name";
            cbSetBrand.ValueMember = "tm_id";
            cbSetBrand.EndUpdate();
        }


        public static void fillSupplier(ComboBox cbSupplier)
        {
            DataTable tableSupplier = DBSprSupplier.GetSuppliers();
            DataRow row = tableSupplier.NewRow();
            row[0] = 0;
            row[1] = " -- Выберите поставщика --";
            tableSupplier.Rows.InsertAt(row, 0);
            cbSupplier.DataSource = tableSupplier;
            cbSupplier.DisplayMember = "n_kontr";
            cbSupplier.ValueMember = "id_kontr";
        }

        public static void fillSkladSupplier(ComboBox idSkladSupplier,  int idsupplier)
        {
            DataTable tableSkladSupplier = DBSprSkladSupplier.GetSkladSuppliersByIdKontr(idsupplier); //GetSkladSuppliers(); /
            DataRow row = tableSkladSupplier.NewRow();
            row[0] = 0;
            row[1] = " -- Выберите Склад поставщика_ --";
            tableSkladSupplier.Rows.InsertAt(row, 0);
            idSkladSupplier.DataSource = tableSkladSupplier;
            idSkladSupplier.DisplayMember = "nSkladSupplier";
            idSkladSupplier.ValueMember = "idSkladSupplier";
        }


        // ins 21.01.22 Semenkina Добавлен параметр subject - Тема письма
        public static List<ParserSettings> GetCurrentSettingByEmail(string eMail, string subject)
        {
            DataTable dtSettings = DBParserSettings.GetParserSettingByMail(eMail, subject);  // ins 21.01.22 Semenkina Добавлен параметр subject - Тема письма
            if (dtSettings == null)
                return null;

            var result = new List<ParserSettings>();
            foreach (var row in dtSettings.AsEnumerable())
                result.Add(GetGetCurrentSettingByRow(row));
            return result;
        }

        public static List<ParserSettings> GetCurrentSettingByEmailA1(string eMail)
        {
            DataTable dtSettings = DBParserSettings.GetParserSettingByMailA1(eMail);
            if (dtSettings == null)
                return null;

            var result = new List<ParserSettings>();
            foreach (var row in dtSettings.AsEnumerable())
                result.Add(GetGetCurrentSettingByRow(row));
            return result;
        }


        public static void FillFutureLoadType(ComboBox cbLoadType)
        {
            DataTable tableSupplier = ParserFuture.GetLoadType();
            DataRow row = tableSupplier.NewRow();
            row[0] = 0;
            row[1] = " -- Выберите тип загрузки прайса со сроком действия --";
            tableSupplier.Rows.InsertAt(row, 0);
            cbLoadType.DataSource = tableSupplier;
            cbLoadType.DisplayMember = "loadname";
            cbLoadType.ValueMember = "loadtype";
        }

        public static List<ParserSettings> GetCurrentSettingByidSupplier(int idSupplier)
        {
            DataTable dtSettings = DBParserSettings.GetParserSettingByidSupplier(idSupplier);
            if (dtSettings == null)
                return null;

            var result = new List<ParserSettings>();
            foreach (var row in dtSettings.AsEnumerable())
                result.Add(GetGetCurrentSettingByRow(row));
            return result;
        }

        public static List<ParserSettings> GetALLSettingsFTP()
        {
            DataTable dtSettings = DBParserSettings.GetParserSettingFTP();
            if (dtSettings == null)
                return null;

            var result = new List<ParserSettings>();
            foreach (var row in dtSettings.AsEnumerable())
                result.Add(GetGetCurrentSettingByRow(row));
            return result;
        }





        public static ParserSettings GetCurrentSettingById(int ParserSettingsId)
        {
            DataRow row = DBParserSettings.GetParserSettingById(ParserSettingsId);
            if (row == null)
                return null;

            return GetGetCurrentSettingByRow(row);
        }





        public static ParserSettings GetGetCurrentSettingByRow(DataRow row)
        {
            ParserSettings result = new ParserSettings();

            result.idParserSettings = int.Parse(row["idParserSettings"].ToString());
            result.nParserSettings = row["nParserSettings"].ToString();
            result.idSupplier = int.Parse(row["idSupplier"].ToString());
            result.idSource = row["idSource"] == DBNull.Value ? 0 : int.Parse(row["idSource"].ToString());
            result.fArmRtk = row["fArmRtk"] == DBNull.Value ? 0 : int.Parse(row["fArmRtk"].ToString());
            result.fPriceOnline = row["fPriceOnline"] == DBNull.Value ? 0 : int.Parse(row["fPriceOnline"].ToString());
            result.fPriceCompetitor = row["fPriceCompetitor"] == DBNull.Value ? 0 : int.Parse(row["fPriceCompetitor"].ToString());
            result.fCorrectCoeff = row["fCorrectCoeff"] == DBNull.Value ? 0 : int.Parse(row["fCorrectCoeff"].ToString());
            result.fAutoload = row["fAutoload"] == DBNull.Value ? 0 : int.Parse(row["fAutoload"].ToString());
            result.idTypeSource = row["idTypeSource"] == DBNull.Value ? 0 : int.Parse(row["idTypeSource"].ToString());
            result.Mail = row["Mail"] == DBNull.Value ? "" : row["Mail"].ToString();
            result.MailFileMask = row["MailFileMask"] == DBNull.Value ? "" : row["MailFileMask"].ToString();
            result.FtpLogin = row["FtpLogin"] == DBNull.Value ? "" : row["FtpLogin"].ToString();
            result.FtpPass = row["FtpPass"] == DBNull.Value ? "" : row["FtpPass"].ToString();
            result.FtpServer = row["FtpServer"] == DBNull.Value ? "" : row["FtpServer"].ToString();
            result.StartRow = row["StartRow"] == DBNull.Value ? 1 : int.Parse(row["StartRow"].ToString());
            result.fNds = row["fNds"] == DBNull.Value ? 0 : int.Parse(row["fNds"].ToString());
            result.PricePercent = row["PricePercent"] == DBNull.Value ? 0 : int.Parse(row["PricePercent"].ToString());
            result.fBrend = row["fBrend"] == DBNull.Value ? 0 : int.Parse(row["fBrend"].ToString());
            result.nBrend = row["nBrend"] == DBNull.Value ? "" : row["nBrend"].ToString();
            result.fAllList = row["fAllList"] == DBNull.Value ? 0 : int.Parse(row["fAllList"].ToString());
            result.fHardLoad = row["fHardLoad"] == DBNull.Value ? 0 : int.Parse(row["fHardLoad"].ToString());
            result.Separator = row["Separator"] == DBNull.Value ? "" : row["Separator"].ToString();
            result.fMarketDiscount = row["fMarketDiscount"] == DBNull.Value ? 0 : int.Parse(row["fMarketDiscount"].ToString());
            result.IdCur = row["IdCur"] == DBNull.Value ? 0 : int.Parse(row["IdCur"].ToString());
            result.fDelOldPrice = row["fDelOldPrice"] == DBNull.Value ? -1 : int.Parse(row["fDelOldPrice"].ToString());
            result.KolDayActual = row["KolDayActual"] == DBNull.Value ? 0 : int.Parse(row["KolDayActual"].ToString());
            result.fOvveridePerPrice = row["fOvveridePerPrice"] == DBNull.Value ? 0 : int.Parse(row["fOvveridePerPrice"].ToString());
            result.idSkladSupplier =  int.Parse(row["idSkladSupplier"].ToString());
            result.fCompetitorType = int.Parse(row["fCompetitorType"].ToString());

            result.parserSettingsColumns = GetParserSettingsCollumn(result.idParserSettings);
            return result;
        }

        private static List<ParserSettingsColumn> GetParserSettingsCollumn(int idParserSettings)
        {
            DataTable dtSettingsCollumn = DBParserSettings.GetParserSettingCollumn(idParserSettings);
            if (dtSettingsCollumn == null || dtSettingsCollumn.Rows.Count == 0)
                return new List<ParserSettingsColumn>();


            //var sheetN = from n in dtSettingsCollumn.AsEnumerable() group n by n[""] select new { Count = n };


            var result = new List<ParserSettingsColumn>();

            foreach (var row in dtSettingsCollumn.AsEnumerable())
            {
                var elem = new ParserSettingsColumn();
                elem.id = int.Parse(row["id"].ToString());
                elem.idParserSettings = int.Parse(row["idParserSettings"].ToString());
                elem.idTypeParserSettingsColumn = int.Parse(row["idTypeParserSettingsColumn"].ToString());
                elem.ColumnNumber = int.Parse(row["ColumnNumber"].ToString());
                elem.sheetN = int.Parse(row["sheetN"].ToString());

                result.Add(elem);
            }
            return result;
        }        

        public static void SaveCurrentSetting(ParserSettings currentSetting, int fPriceWithDateActual)
        {
            try
            {
                string insertsql =
                    @"INSERT INTO 
                        dbo.ParserSettings(
                            nParserSettings,
                            idSupplier,
                            idSource,
                            fPriceOnline,
                            fArmRtk,
                            fPriceCompetitor,
                            fCorrectCoeff,
                            fAutoload,
                            idTypeSource,
                            Mail,
                            MailFileMask,
                            FtpLogin,
                            FtpPass,
                            FtpServer,
                            StartRow,
                            fNds,
                            PricePercent,
                            fBrend,
                            nBrend,
                            fAllList,
                            fHardLoad,
                            Separator,
                            fMarketDiscount,
                            idCur,
                            fDelOldPrice,
                            KolDayActual,
                            fOvveridePerPrice,
                            idSkladSupplier,
                            fPriceWithDateActual,
                            fCompetitorType
                            )
                output INSERTED.idParserSettings
                VALUES(
                            @nParserSettings,
                            @idSupplier,
                            @idSource,
                            @fPriceOnline,
                            @fArmRtk,
                            @fPriceCompetitor,
                            @fCorrectCoeff,
                            @fAutoload,
                            @idTypeSource,
                            @Mail,
                            @MailFileMask,
                            @FtpLogin,
                            @FtpPass,
                            @FtpServer,
                            @StartRow,
                            @fNds,
                            @PricePercent,
                            @fBrend,
                            @nBrend,
                            @fAllList,
                            @fHardLoad,
                            @Separator,
                            @fMarketDiscount,
                            @idCur,
                            @fDelOldPrice,
                            @KolDayActual,
                            @fOvveridePerPrice,
                            @idSkladSupplier,
                            @fPriceWithDateActual,
                            @fCompetitorType
                        )";

                string sqlupdate =
                    @"UPDATE dbo.ParserSettings
                        SET 
                            nParserSettings=@nParserSettings,
                            idSupplier=@idSupplier,
                            idSource=@idSource,
                            fPriceOnline=@fPriceOnline,
                            fArmRtk=@fArmRtk,
                            fPriceCompetitor=@fPriceCompetitor,
                            fCorrectCoeff=@fCorrectCoeff,
                            fAutoload=@fAutoload,
                            idTypeSource=@idTypeSource,
                            Mail=@Mail,
                            MailFileMask=@MailFileMask,
                            FtpLogin=@FtpLogin,
                            FtpPass=@FtpPass,
                            FtpServer=@FtpServer,
                            StartRow=@StartRow,
                            fNds=@fNds,
                            PricePercent=@PricePercent,
                            fBrend=@fBrend,
                            nBrend=@nBrend,
                            fAllList=@fAllList,
                            fHardLoad=@fHardLoad,
                            Separator=@Separator,
                            fMarketDiscount=@fMarketDiscount,
                            idCur=@idCur,
                            fDelOldPrice=@fDelOldPrice,
                            KolDayActual=@KolDayActual,
                            fOvveridePerPrice=@fOvveridePerPrice,
                            idSkladSupplier = @idSkladSupplier,
                            fPriceWithDateActual = @fPriceWithDateActual,
                            fCompetitorType = @fCompetitorType
                            where
                            idParserSettings = @idParserSettings";

                string sql = currentSetting.idParserSettings == 0 ? insertsql : sqlupdate;
                var listPar = new List<SqlParameter>();

                listPar.Add(new SqlParameter("nParserSettings", currentSetting.nParserSettings ?? Convert.DBNull));
                listPar.Add(new SqlParameter("idSupplier", currentSetting.idSupplier));
                listPar.Add(new SqlParameter("idSource", currentSetting.idSource));
                listPar.Add(new SqlParameter("fPriceOnline", currentSetting.fPriceOnline));
                listPar.Add(new SqlParameter("fArmRtk", currentSetting.fArmRtk));
                listPar.Add(new SqlParameter("fPriceCompetitor", currentSetting.fPriceCompetitor));
                listPar.Add(new SqlParameter("fCorrectCoeff", currentSetting.fCorrectCoeff));
                listPar.Add(new SqlParameter("fAutoload", currentSetting.fAutoload));
                listPar.Add(new SqlParameter("idTypeSource", currentSetting.idTypeSource));
                listPar.Add(new SqlParameter("Mail", currentSetting.Mail ?? Convert.DBNull));
                listPar.Add(new SqlParameter("MailFileMask", currentSetting.MailFileMask ?? Convert.DBNull));
                listPar.Add(new SqlParameter("FtpLogin", currentSetting.FtpLogin ?? Convert.DBNull));
                listPar.Add(new SqlParameter("FtpPass", currentSetting.FtpPass ?? Convert.DBNull));
                listPar.Add(new SqlParameter("FtpServer", currentSetting.FtpServer ?? Convert.DBNull));
                listPar.Add(new SqlParameter("StartRow", currentSetting.StartRow));
                listPar.Add(new SqlParameter("fNds", currentSetting.fNds));
                listPar.Add(new SqlParameter("PricePercent", currentSetting.PricePercent));
                listPar.Add(new SqlParameter("fBrend", currentSetting.fBrend));
                listPar.Add(new SqlParameter("nBrend", currentSetting.nBrend ?? Convert.DBNull));
                listPar.Add(new SqlParameter("fAllList", currentSetting.fAllList));
                listPar.Add(new SqlParameter("fHardLoad", currentSetting.fHardLoad));
                listPar.Add(new SqlParameter("Separator", currentSetting.Separator ?? Convert.DBNull));
                listPar.Add(new SqlParameter("fMarketDiscount", currentSetting.fMarketDiscount));
                listPar.Add(new SqlParameter("idCur", currentSetting.IdCur));
                listPar.Add(new SqlParameter("fDelOldPrice", currentSetting.fDelOldPrice));
                listPar.Add(new SqlParameter("KolDayActual", currentSetting.KolDayActual));
                listPar.Add(new SqlParameter("fOvveridePerPrice", currentSetting.fOvveridePerPrice));
                listPar.Add(new SqlParameter("idSkladSupplier", currentSetting.idSkladSupplier));
                listPar.Add(new SqlParameter("fPriceWithDateActual", fPriceWithDateActual));
                listPar.Add(new SqlParameter("fCompetitorType", currentSetting.fCompetitorType));

                if (currentSetting.idParserSettings != 0)
                {
                    listPar.Add(new SqlParameter("idParserSettings", currentSetting.idParserSettings));
                    DBConnector.DBExecutor.ExecuteQuery(sql, listPar.ToArray());
                }
                else
                {
                    currentSetting.idParserSettings = int.Parse(DBConnector.DBExecutor.SelectSchalar(sql, listPar.ToArray()).ToString());
                }

                var parId = new SqlParameter("idParserSettings", currentSetting.idParserSettings);
                DBConnector.DBExecutor.ExecuteQuery("delete dbo.ParserSettingsColumn where idParserSettings = @idParserSettings", parId);

                foreach (ParserSettingsColumn item in currentSetting.parserSettingsColumns)
                {
                    string sqlinsertSettingColumn = "insert into ParserSettingsColumn(idParserSettings, idTypeParserSettingsColumn, ColumnNumber, sheetN) values(@idParserSettings, @idTypeParserSettingsColumn, @ColumnNumber, @sheetN)";
                    var par1 = new SqlParameter("idParserSettings", currentSetting.idParserSettings);
                    var par2 = new SqlParameter("idTypeParserSettingsColumn", item.idTypeParserSettingsColumn);
                    var par3 = new SqlParameter("ColumnNumber", item.ColumnNumber);
                    var par4 = new SqlParameter("sheetN", item.sheetN);

                    DBConnector.DBExecutor.ExecuteQuery(sqlinsertSettingColumn, par1, par2, par3, par4);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static bool ValidateColumnCondition(List<ParserSettingsColumn> columnConditions, string column, ref string strcondition)
        {
            bool valid = false;
            if (columnConditions.Count > 0)
            {
                string sselectedID = String.Join(",", columnConditions.Select(x => x.idTypeParserSettingsColumn).ToArray());
                string validatesq = "select nTypeParserSettingsColumn from sTypeParserSettingsColumn (nolock) where " + column + " = 1 and idTypeParserSettingsColumn not in (" + sselectedID + ")";

                DataTable dtResult = DBConnector.DBExecutor.SelectTable(validatesq);

                List<string> listresult = new List<string>();
                foreach (var row in dtResult.AsEnumerable())
                {
                    listresult.Add(row["nTypeParserSettingsColumn"].ToString());
                }

                if (listresult.Count == 0) valid = true;
                strcondition = string.Join(",\n", listresult.ToArray());
                return valid;
            }
            else
            {
                string validatesq = "select nTypeParserSettingsColumn from sTypeParserSettingsColumn (nolock) where " + column + " = 1";
                DataTable dtResult = DBConnector.DBExecutor.SelectTable(validatesq);

                List<string> listresult = new List<string>();
                foreach (var row in dtResult.AsEnumerable())
                {
                    listresult.Add(row["nTypeParserSettingsColumn"].ToString());
                }
                if (listresult.Count == 0) valid = true;
                strcondition = string.Join(",\n", listresult.ToArray());
                return valid;
            }

        }

        public static string GetHeadStrParserSaveData(ParserSettings settings, int UserId, int sheetn)
        {
            DBDate.Date = DateTime.Now.Date;
            string result = "";
            //ins 26.01.22 Semenkina Добавляем сохранение Склада поставщика   settings.idSkladSupplier.ToString()
            if (settings.fCompetitorType == 1)
            {
                result = " insert into PriceOnlineTemp (idUser, idKontr, idskladsupplier, DatePrice,idCur,idTm,nTov,";
            }
            if (settings.fCompetitorType == 2)
            {
                result = " insert into PriceOnlineTempA1 (idUser, idKontr, idskladsupplier, DatePrice,idCur,idTm,nTov,";
            }
            
            foreach (var elem in settings.parserSettingsColumns.Where(p => p.GetColumnName() != "" && p.TypeColumn != TypeParserSettingsColumn.Бренд && p.sheetN == sheetn))
                result += elem.GetColumnName() + ',';
            //result += "brend)";
            result += "brend, nBrandSupplier)";
            result += " values (" + "'" + UserId.ToString() + "'" + "," + settings.idSupplier.ToString() + ", " + settings.idSkladSupplier.ToString() + ",'" + DBDate.StrDate + "'," + settings.IdCur.ToString() + ",0,'',";
            return result;
        }

        public static string GetHeadStrParserSaveDataFuture(ParserSettings settings, int UserId, int sheetn, DateTime date)
        {
            DBDate.Date = DateTime.Now.Date;
            //ins 27.01.22 Semenkina Добавлен Склад поставщика idskladsupplier
            
            //upd 01.03.2023 убраны лишние поля в запросе для прайса со сроком действия
            ////string result = " insert into PriceOnlineTemp (idUser, idKontr, idskladsupplier, DatePrice,idCur,idTm,nTov,idParserSettings,dateFuture,";
            string result = " insert into PriceOnlineTemp (idUser, idKontr, idskladsupplier, DatePrice,idCur,idTm,nTov,";

            foreach (var elem in settings.parserSettingsColumns.Where(p => p.GetColumnName() != "" && p.TypeColumn != TypeParserSettingsColumn.Бренд && p.sheetN == sheetn))
                result += elem.GetColumnName() + ',';
            //result += "brend)";
            result += "brend, nBrandSupplier)";
            result += " values (" + "'" + UserId.ToString() + "'" + "," + settings.idSupplier.ToString() + "," + settings.idSkladSupplier.ToString() + ",'" + DBDate.StrDate + "'," + settings.IdCur.ToString() + ",0,'',";

            DBDate.Date = date;

            //upd 01.03.2023 убраны лишние поля в запросе для прайса со сроком действия
            //result += settings.idParserSettings.ToString() + ",'" + DBDate.StrDate + "',";
            return result;
        }


        public static void fillCurrency(ComboBox idCur)
        {
            DataTable tableBrand = DBSprCur.GetAllSprCur();
            idCur.DataSource = tableBrand;
            idCur.DisplayMember = "n_curr";
            idCur.ValueMember = "id_cur";
        }
    }
}
