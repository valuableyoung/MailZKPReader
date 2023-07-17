using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ALogic.Logic.SPR
{
    public static class ValuteLogic
    {
        public static ValuteCur GetCurCBR()
        {
            var result = new ValuteCur();

            try
            {
                //Инициализируем объекта типа XmlTextReader и
                //загружаем XML документ с сайта центрального банка
                XmlTextReader reader = new XmlTextReader("http://www.cbr.ru/scripts/XML_daily.asp");

                //В эти переменные будем сохранять куски XML
                //с определенными валютами (Euro, USD)
                string USDXml = "";
                string EuroXML = "";
                string UanXML = "";

                //Перебираем все узлы в загруженном документе
                while (reader.Read())
                {
                    //Проверяем тип текущего узла
                    switch (reader.NodeType)
                    {
                        //Если этого элемент Valute, то начинаем анализировать атрибуты
                        case XmlNodeType.Element:

                            if (reader.Name == "Valute")
                            {
                                if (reader.HasAttributes)
                                {
                                    //Метод передвигает указатель к следующему атрибуту
                                    while (reader.MoveToNextAttribute())
                                    {
                                        if (reader.Name == "ID")
                                        {
                                            //Если значение атрибута равно R01235, то перед нами информация о курсе доллара
                                            if (reader.Value == "R01235")
                                            {
                                                //Возвращаемся к элементу, содержащий текущий узел атрибута
                                                reader.MoveToElement();
                                                //Считываем содержимое дочерних узлом
                                                USDXml = reader.ReadOuterXml();
                                            }
                                        }

                                        //Аналогичную процедуру делаем для ЕВРО
                                        if (reader.Name == "ID")
                                        {
                                            if (reader.Value == "R01239")
                                            {
                                                reader.MoveToElement();
                                                EuroXML = reader.ReadOuterXml();
                                            }
                                        }

                                        //Аналогичную процедуру делаем для Uan
                                        if (reader.Name == "ID")
                                        {
                                            if (reader.Value == "R01820")
                                            {
                                                reader.MoveToElement();
                                                UanXML = reader.ReadOuterXml();
                                            }
                                        }
                                    }
                                }
                            }

                            break;
                    }
                }

                //Из выдернутых кусков XML кода создаем новые XML документы
                XmlDocument usdXmlDocument = new XmlDocument();
                usdXmlDocument.LoadXml(USDXml);
                XmlDocument euroXmlDocument = new XmlDocument();
                euroXmlDocument.LoadXml(EuroXML);
                XmlDocument uanXmlDocument = new XmlDocument();
                uanXmlDocument.LoadXml(UanXML);
                //Метод возвращает узел, соответствующий выражению XPath
                XmlNode xmlNode = usdXmlDocument.SelectSingleNode("Valute/Value");
                //Считываем значение и конвертируем в decimal. Курс валют получен


                result.USD = decimal.Parse(xmlNode.InnerText.Replace(",", "."));
                xmlNode = euroXmlDocument.SelectSingleNode("Valute/Value");
                result.EURO = decimal.Parse(xmlNode.InnerText.Replace(",", "."));
                xmlNode = uanXmlDocument.SelectSingleNode("Valute/Value");
                result.JPY = decimal.Parse(xmlNode.InnerText.Replace(",", ".")) / 100;

            }
            catch(Exception ex)
            {
                string x = ex.Message;
            }
            return result;
        }

    }

    public class ValuteCur
    {
        public decimal USD { get; set; }
        public decimal EURO { get; set; }
        public decimal JPY { get; set; }

        public ValuteCur()
        {
            USD = 0;
            EURO = 0;
            JPY = 0;
        }
    }
}

