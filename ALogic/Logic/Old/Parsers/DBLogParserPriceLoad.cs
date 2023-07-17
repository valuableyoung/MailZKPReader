using ALogic.DBConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Old.Parsers
{
    public class DBLogParserPriceLoad
    {

        public static DataTable getlogParserPrice()
        {
            return DBExecutor.SelectTable(@"
   						select
                        l.idLog as [id],
                        k.n_kontr as [Поставщик],
                        l.DateLoad as [Дата загрузки],
                        l.kolInFile as [Количество строк в файле],
                        l.kolInBase as [Загружено в базу],
                        l.kolNotBrend as [Неапознанные Бренды],
                        l.kolNotArt as [Неапознанные артикулы],
                        l.kolBadPrice as [Неапознанные цены],
                        l.kolBadKol as [Неапознанное количество],
                        ISNULL(l.kolInPriceOnline,0) as [В Заказной товар],
                        ISNULL(l.kolInRTK,0) as [В РТК],
                        ISNULL(l.kolInCompetitor,0) as [В Ценообразование],
                        ISNULL(l.kolInCorrKoeff, 0) as [В Корректировочных коэффициентах],
						notLoadedBrand as [Незагруженные бренды]
                        from logParserPriceLoad l
                        join spr_kontr k on l.idSupplier=k.id_kontr

                        order by id desc
                        ");
        }
    }
}
