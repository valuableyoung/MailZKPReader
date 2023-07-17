using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AForm.Base
{
    /// <summary>
    /// Перечисление действий пользователей. Имеет тот же текст и ИД что и таблица sWE. 
    /// Если необходимо добавить новое действие делаем следующее
    /// 1. Добавляем новую стороку в sWE
    /// 2. Добавляем запись сюда с таким же ИД
    /// </summary>
    public enum EWindowAction
    {
         Обработать_расхождения = 77
        ,Принять_по_новому = 78
        ,Выбрать_все = 55
        ,Отменить_выбор = 56 
        ,Выбрать = 1
       
    }
}
