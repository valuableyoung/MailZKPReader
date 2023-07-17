using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Old.Parsers
{
    public static class DBParserSettings
    {
        public static DataRow GetParserSettingById(int ParsetSettingId)
        {
            SqlParameter parParsetSettingId = new SqlParameter("idParserSettings", ParsetSettingId);
            return DBConnector.DBExecutor.SelectRow("select * from ParserSettings (nolock) where idParserSettings = @idParserSettings", parParsetSettingId);
        }

        public static DataTable GetParserSettingByMail(string eMail, string subject)
        {
            SqlParameter pareMail = new SqlParameter("eMail", eMail);
            //change 21.01.22 Semenkina Для определения параметров парсинга добавляем Маску файла(темы), по ней определяем  Склад поставщика
            //return DBConnector.DBExecutor.SelectTable("select * from ParserSettings where Mail = @email", pareMail);

            //var testsql = "select p.* from ParserSettings p , sskladsupplier s where p.idSkladSupplier = s.idskladsupplier and p.Mail = @email and  ( '" + subject + "' like isnull(s.nPriceMask,'') or len(isnull(s.nPriceMask,'')) = 0) ";

            if (subject == null)
                { subject = ""; }

            return DBConnector.DBExecutor.SelectTable("select p.* from ParserSettings p (nolock), sskladsupplier s (nolock) where p.idSkladSupplier = s.idskladsupplier and p.Mail = @email and  ( '" + subject + "' like isnull(s.nPriceMask,'') or len(isnull(s.nPriceMask,'')) = 0) ", pareMail);
            //end 21.01.22 Semenkina
        }

        public static DataTable GetParserSettingByMailA1(string eMail)
        {
            SqlParameter pareMail = new SqlParameter("eMail", eMail);

            return DBConnector.DBExecutor.SelectTable("select * from ParserSettings (nolock) p where p.fCompetitorType=2 and p.Mail = @email ", pareMail);
        }


        public static DataTable GetParserSettingCollumn(int idParserSettings)
        {
            SqlParameter pareidParserSettings = new SqlParameter("idParserSettings", idParserSettings);
            return DBConnector.DBExecutor.SelectTable("select * from ParserSettingsColumn (nolock) where idParserSettings = @idParserSettings", pareidParserSettings);
        }

        public static DataTable GetParserConditions()
        {
            return DBConnector.DBExecutor.SelectTable("select idTypeParserSettingsColumn as id, nTypeParserSettingsColumn as name from sTypeParserSettingsColumn (nolock)");
        }

        public static DataTable GetParserSettingByidSupplier(int idSupplier)
        {
            SqlParameter paridSupplier = new SqlParameter("idSupplier", idSupplier);
            var result = DBConnector.DBExecutor.SelectTable("select idParserSettings, nParserSettings, fPriceWithDateActual from ParserSettings (nolock) where idSupplier = @idSupplier", paridSupplier);
            var row = result.NewRow();
            row["idParserSettings"] = 0;
            row["nParserSettings"] = "--Выберите настройку--";
            row["fPriceWithDateActual"] = 0;

            result.Rows.InsertAt(row, 0);
            return result;
        }

        public static DataRow GetLogForKontr(int idSupplier)
        {
            SqlParameter paridSupplier = new SqlParameter("idSupplier", idSupplier);
            var sql = "select * from logParserPriceLoad (nolock) where idSupplier = @idSupplier and idLog = (select max(idLog) from logParserPriceLoad (nolock) where idSupplier = @idSupplier)";
            return DBConnector.DBExecutor.SelectRow(sql, paridSupplier);
        }

        public static string GetLogForKontr(int idSupplier, int idpf)
        {
            SqlParameter paridSupplier = new SqlParameter("idSupplier", idSupplier);
            SqlParameter paridpf = new SqlParameter("idpf", idpf);
            var sql = @"
                        select * from logParserPriceLoad (nolock)
                        where idLog = (
	                        select MAX(idLog) from logParserPriceLoad 
	                        where idSupplier = @idSupplier
	                        and idpf = @idpf)
                        ";

            var row = DBConnector.DBExecutor.SelectRow(sql, paridSupplier, paridpf);

            var str = "Количество строк в файле: " + row["kolInFile"].ToString() + '\n';
            str += "Загружено в базу данных: " + row["kolInBase"].ToString() + '\n';
            str += "Не распознан бренд: " + row["kolNotBrend"].ToString() + '\n';
            str += "Не распознан артикул: " + row["kolNotArt"].ToString() + '\n';
            str += "Не корректная цена: " + row["kolBadPrice"].ToString() + '\n';
            str += "Некорректное количество: " + row["kolBadKol"].ToString() + '\n';
            str += "Строк загружено: " + row["kolInRTK"].ToString() + '\n';
            return str;
        }

        public static DataTable GetParserSettingFTP()
        {
            return DBConnector.DBExecutor.SelectTable("select * from ParserSettings (nolock) where idTypeSource = 2");
        }

        public static DataTable GetAllParserSettings()
        {
            string str = "select ";
            str += "       idParserSettings  ";
            str += "      ,nParserSettings";
            str += "      ,ks.n_kontr as nSupplier";
            str += "      ,kk.n_kontr as nSource";
            str += "      ,cast(isnull(fPriceOnline, 0) as bit) as fPriceOnline";
            str += "      ,cast(isnull(fArmRtk, 0) as bit) as fArmRtk";
            str += "      ,cast(isnull(fPriceCompetitor, 0) as bit) as fPriceCompetitor";
            str += "      ,cast(isnull(fCorrectCoeff, 0) as bit) as fCorrectCoeff";
            str += "      ,cast(isnull(fAutoload, 0) as bit) as fAutoload	";
            str += "     from ParserSettings p (nolock)";
            str += "        inner join spr_kontr ks on p.idSupplier = ks.id_kontr ";
            str += "        inner join spr_kontr kk on p.idSource = kk.id_kontr ";




            return DBConnector.DBExecutor.SelectTable(str);
        }

        public static void DeleteParserSetting(int idSetting)
        {
            SqlParameter paridSetting = new SqlParameter("idSetting", idSetting);
            var sql = "delete from ParserSettings where idParserSettings = @idSetting";
            DBConnector.DBExecutor.ExecuteQuery(sql, paridSetting);
        }
    }

    public class PSettings: object
    {
        public string m_Name { get; set; }
	    public int m_Value { get; set; }
        public int m_Tag { get; set; }

        public PSettings(string name, int in_value, int tag)
        {
            m_Name = name;
			m_Value = in_value;
            m_Tag = tag;
        }
    }
}
