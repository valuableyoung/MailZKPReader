using ALogic.DBConnector;
using ALogic.Logic.SPR.Old;
using ALogic.Model.Entites.Invoice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ALogic.Logic.Old.Parsers
{
    public static class SupplierAnswer
    {
        public static string[] formats = DateTimeFormatInfo.CurrentInfo.GetAllDateTimePatterns();
        public static CultureInfo ruRU = new CultureInfo("ru-RU", false);

        /// <summary>
        /// Метод для разбора и сохранения документа
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="idKontr"></param>
        /// <param name="idTypeEdi"></param>
        /// <param name="nFile"></param>
        /// <returns></returns>
        public static bool LoadTableAns(DataTableCollection tables, int idKontr, int idTypeEdi, string nFile)
        {
            int errsfdate = 0;
            try
            {
                List<string> ls = new List<string>();
                ls = formats.ToList<string>();
                ls.Add("dd MMMM yyyy"); //добавляем форматы для своеобразных контрагентов
                formats = ls.ToArray<string>();

                ParseFile p = new ParseFile(idKontr, idTypeEdi);

                var table = tables[p.NomList];
                InvoiceBuy invoice = new InvoiceBuy();

                invoice.NFile = nFile;
                invoice.IdKontr = idKontr;
                invoice.Proform = idTypeEdi == 12 ? 1 : 0;
                invoice.idTypeSource = 1;

                DataRow rowConv = DBConvention.GetConventionZakTovByIdKontr(invoice.IdKontr);

                if (rowConv == null)
                {
                    Logger.ShowMessage("Не найден договор! Файл не обработан");
                    return false;
                }

                invoice.idFirm = int.Parse(rowConv["id_firm"].ToString());
                invoice.idConvention = int.Parse(rowConv["idConvention"].ToString());

                if (p.NomSF != "" && p.DateSF != "" && p.NomSF != "1" && p.DateSF != "1")
                {
                    int colNum = ParseFileParm.ParceNomColumn(p.NomSF.Split(':').First());
                    int rowNum = int.Parse(p.NomSF.Split(':').Last()[0].ToString()) - 1;
                    string nomSFStr = table.Rows[rowNum][colNum].ToString().Replace("'", "");

                    if (p.NomSFmask == "")
                        invoice.NomSF = nomSFStr;
                    else
                        invoice.NomSF = StringWorker.GetSubHard(nomSFStr, p.NomSFmask, "[НОМЕР С/Ф]");

                    colNum = ParseFileParm.ParceNomColumn(p.DateSF.Split(':').First());
                    rowNum = int.Parse(p.DateSF.Split(':').Last()[0].ToString()) - 1;
                    string dateSFStr = table.Rows[rowNum][colNum].ToString().Replace("'", "");

                    if (p.DateSFMask != "")
                        dateSFStr = StringWorker.GetSubHard(dateSFStr, p.DateSFMask, "[ДАТА С/Ф]");

                    //if (dateSFStr.Trim() == "" || nomSFStr.Trim() == "")
                    //{
                    //    errcnt++;
                    //    Logger.WriteErrorMessage("Контрагент ID = " + idKontr.ToString() + ". Пустая дата или номер СФ!");
                    //    return false;
                    //}

                    //invoice.DateSF = DateTime.Parse(dateSFStr);
                    //Logger.WriteErrorMessage("Пытаемся преобразовать вот эту дату: " + dateSFStr);
                    if (dateSFStr.Trim() != "")
                    {
                        DateTime sfDate;
                        //invoice.DateSF = DateTime.ParseExact(dateSFStr, formats, ruRU, DateTimeStyles.None);
                        if (DateTime.TryParseExact(dateSFStr, formats, ruRU, DateTimeStyles.None, out sfDate))
                        {
                            invoice.DateSF = sfDate;
                        }
                        else
                        {
                            if(ReplaceStrToDateTime(dateSFStr,out sfDate))
                            {
                                invoice.DateSF = sfDate;
                            }
                            else
                            {
                                Logger.WriteErrorMessage("Дата '" + dateSFStr + "' не распознана методом TryParseExact");
                                errsfdate++;
                            }
                            
                        }
                    }
                }

                DataTable ansTable = LoadTableAnswer(table, p);

                foreach (var row in ansTable.AsEnumerable())
                {
                    InvoiceBuyTov d = new InvoiceBuyTov();
                    d.LoadByRow(row);
                    if (d.Summa > 0 && d.Kol > 0 && d.Price == 0)
                        d.Price = d.Summa / d.Kol;
                    invoice.listTov.Add(d);
                }

                invoice.Save();
            }
            catch (Exception ex)
            {
                errsfdate++;
                Logger.WriteErrorMessage("Контрагент ID = " + idKontr.ToString() + " Файл: " + nFile + ". Ошибка в методе SupplierAnswer.LoadTableAns: " + ex.Message);
                Logger.ShowMessage("Контрагент ID = " + idKontr.ToString() + " Файл: " + nFile + ". Ошибка в методе SupplierAnswer.LoadTableAns: " + ex.Message);
            }
            return errsfdate == 0 ? true : false;
        }

        private static DataTable LoadTableAnswer(DataTable table, ParseFile p)
        {
            DataTable tableAns = new DataTable();
            foreach (var par in p.ListParms)
            {
                tableAns.Columns.Add(par.nColumnActual);
            }

            for (int i = p.StartRow - 1; i < table.Rows.Count; i++)
            {
                if (table.Rows[i].ItemArray.Where(q => q.ToString().Replace("\n", "").Replace(" ", "").IndexOf("Странапроисхождениятовара") != -1).Count() > 0)
                {
                    i += 2;
                    continue;
                }
                if (table.Rows[i].ItemArray.Where(q => q.ToString().Replace("\n", "").Replace(" ", "").IndexOf("Всегокоплате") != -1).Count() > 0)
                {
                    break;
                }

                var row = tableAns.NewRow();
                bool correct = true;
                foreach (var elem in p.ListParms)
                {
                    string value = table.Rows[i][elem.ColumnNumber].ToString();


                    if (elem.fPiece != 0)
                    {
                        value = value.Trim();
                        var delim = new string[1];
                        delim[0] = elem.PieceDelimiter;
                        if (elem.fPiece == 1)
                            value = value.Split(delim, StringSplitOptions.None).First();

                        if (elem.fPiece == 2)
                            value = value.Split(delim, StringSplitOptions.None).Last();
                    }

                    value = value.Replace("\"", "");

                    if (elem.type == ColType.Количество || elem.type == ColType.Цена || elem.type == ColType.Сумма)
                    {
                        if (value.IndexOf(',') < value.Length - 3 && value.IndexOf(',') >= 0)
                            value = value.Replace(",", "");
                        value = value.Replace(" ", "").Replace("руб.", "").Replace("р.", "").Replace(",", ".").Replace("<", "").Replace(">", "").Replace(((char)160).ToString(), "").Split('-').First();
                        int countPoint = value.Count(q => q == '.');
                        for (int ind = 1; ind < countPoint; ind++)
                            value = value.Remove(value.IndexOf('.'), 1);
                        if (countPoint > 0 && elem.type == ColType.Количество)
                            value = value.Remove(value.IndexOf('.'));

                        decimal test;
                        correct = correct && decimal.TryParse(value, out test);
                    }

                    if (elem.type == ColType.СтранаПроисхождения)
                    {
                        if (value.Replace("-", "") == "")
                            value = "643";
                    }

                    if (elem.type == ColType.Декларация)
                    {
                        if (value.Trim() == "11")
                            correct = false;

                        if (value == "-")
                            value = "";
                    }

                    if (elem.type == ColType.Артикул && p.IdKontr == 554998)
                    {
                        int ind = value.IndexOf('-');
                        value = value.Remove(0, ind);
                    }

                    if (elem.type == ColType.Артикул)
                    {
                        value = StringWorker.DelSymbols(value, '/', '.', '-', ' ', '\\', '|', '&', '^', '*', ';', ':', '>', '<', ',', '+', '"', '?', '=', '(', ')');
                        if (value.Trim().Length == 0)
                            correct = false;
                    }
                    if (value.Length > 1 && value[0] == ' ')
                        value = value.Remove(0, 1);

                    row[elem.nColumnActual] = value;
                }
                if (correct)
                    tableAns.Rows.Add(row);
            }
            return tableAns;
        }

        public static bool ReplaceStrToDateTime(string str, out DateTime dateConv)
        {

            bool ret = false;
            DateTime dt = default(DateTime);

          

        string[] rgxs = {
                @"\d{1,2}([/.-])\d{1,2}([/.-])\d{2,4}"
                            };

            str = str.Replace("января", ".01.").Replace("февраля", ".02.").Replace("марта", ".03.").Replace("апреля", ".04.").Replace("мая", ".05.")
                      .Replace("июня", ".06.").Replace("июля", ".07.").Replace("августа", ".08.").Replace("сентября", ".09.").Replace("октября", ".10.")
                      .Replace("ноября", ".11.").Replace("декабря", ".12.").Replace(" .", ".").Replace(". ", ".").Trim();

            

            foreach (string rs in rgxs)
            {
                Regex rgx = new Regex(rs);
                MatchCollection matches = rgx.Matches(str);
                foreach (Match m in matches)
                {
                    var dtnew = m.Value.ToString();

                    int pos1 = dtnew.IndexOf(".");

                    dtnew = dtnew.Substring(0, pos1 );
                    int dayDt;

                    if (Int32.TryParse(dtnew, out dayDt))
                    {
                        if (dayDt <= 9 )
                        {
                            dtnew = "0" + m.Value.ToString();
                        }
                        else
                        {
                            dtnew = m.Value.ToString();
                        }
                    }
                    else
                    {
                        dtnew = m.Value.ToString();
                    }
                   


                    if (DateTime.TryParseExact(dtnew, formats, ruRU, DateTimeStyles.None, out dt))
                    {
                        ret = true;

                    }
                    else
                    {
                        ret = false;


                    }

                }
            }

            
            dateConv = dt;
            return ret;
        }

    }
}

