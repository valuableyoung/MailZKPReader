
using ALogic.Logic.Heplers;
using ALogic.Model.Entites.HP;
using ALogic.Model.EntityFrame;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ALogic.Logic.HP
{
    public static class EmployeeCardLogic
    {

        public static DataTable GetEmployees()
        {
            string str = @"
select
    id_kontr
    ,n_kontr_full
    ,tel3
    ,id_post
    ,b_day
    ,adress_fact
    ,id_cond
    ,id_firm
    ,id_torg
    ,last_compare
    ,PassWithCar
    ,idCondEmpl
from
    spr_kontr
where employer = 1 and id_cond <> 30
";

            var result = DBConnector.DBExecutor.SelectTable(str);
            result.Columns.Add("Age");
            result.Columns.Add("WorkTime");

            result.AsEnumerable().ToList().ForEach(p => p["Age"] = p["b_day"] != DBNull.Value ? (DateTime.Now.Year - Convert.ToDateTime(p["b_day"]).Year).ToString() + " лет" : "");
            result.AsEnumerable().ToList().Where(p => p["last_compare"] != DBNull.Value).ToList().ForEach(p => p["WorkTime"] = DateTimeHelper.ToWorkTime(Convert.ToDateTime(p["last_compare"])));

            return result;
        }

        private static void PreSaveAction(EmployeeCardEntity employee)
        {
            employee.spr_kontr.n_kontr = employee.spr_kontr.n_kontr_full == null ? "" : employee.spr_kontr.n_kontr_full;
            employee.spr_kontr.employer = 1;
            employee.spr_kontr.name_user = "";
            employee.spr_kontr.id_cond = 10;
            employee.spr_kontr.id_direct = 60;

            employee.spr_kontr.id_firm = employee.spr_kontr.id_firm.HasValue ? employee.spr_kontr.id_firm : 0;
            employee.spr_kontr.id_post = employee.spr_kontr.id_post.HasValue ? employee.spr_kontr.id_post : 0;
            employee.spr_kontr.id_torg = employee.spr_kontr.id_torg.HasValue ? employee.spr_kontr.id_torg : 0;

            if (employee.listStaffMove.Count > 0)
            {
                var lastStaff = employee.listStaffMove.OrderBy(p => p.DateS).Last();
                employee.spr_kontr.id_torg = lastStaff.idUnit;
                employee.spr_kontr.id_post = lastStaff.idPost;
                employee.spr_kontr.id_firm = lastStaff.idFirm;
            }

            if (employee.listStaffMove.Count == 0 && employee.spr_kontr.last_compare.HasValue)
            {
                var sm = new StaffMove()
                {
                    DateS = employee.spr_kontr.last_compare.Value
                      ,
                    DateE = null
                      ,
                    idFirm = int.Parse(employee.spr_kontr.id_firm.ToString())
                      ,
                    idPost = int.Parse(employee.spr_kontr.id_post.ToString())
                      ,
                    idUnit = int.Parse(employee.spr_kontr.id_torg.ToString())
                      ,
                    DbState = (int)EntityState.Added
                      ,
                    nDecree = ""
                };
                employee.listStaffMove.Add(sm);
            }

        }

        public static EmployeeCardEntity GetEmployee(int id_kontr)
        {
            return new EmployeeCardEntity(id_kontr);
        }

        public static EmployeeCardEntity Save(EmployeeCardEntity employee)
        {
            PreSaveAction(employee);
            if (isValid(employee))
            {
                employee.Save();
            }
            return employee;
        }

        private static bool isValid(EmployeeCardEntity employee)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (employee.spr_kontr.last_compare == null)
                errors.Add("last_compare", "Не зполнено поле дата приема");

            if (employee.spr_kontr.id_firm == 0 || employee.spr_kontr.id_firm == null)
                errors.Add("id_firm", "Не зполнено поле фирма");

            if (employee.spr_kontr.n_kontr_full == "")
                errors.Add("n_kontr_full", "Не зполнено поле ФИО");

            if (employee.spr_kontr.id_torg == 0 || employee.spr_kontr.id_torg == null)
                errors.Add("id_torg", "Не зполнено поле Отдел");

            if (employee.spr_kontr.inn == "")
                errors.Add("inn", "Не зполнено поле ИНН");

            if (employee.spr_kontr.id_direct == 0)
                errors.Add("id_direct", "Не зполнено поле направление");

            if (employee.spr_kontr.id_post == 0 || employee.spr_kontr.id_post == null)
                errors.Add("id_post", "Не зполнено поле должность");

            if (employee.spr_kontr.idCondEmpl == 0)
                errors.Add("idCondEmpl", "Не зполнено поле статус");

            if (employee.listChildren.FirstOrDefault(p => p.DateBirth == DateTime.MinValue) != null)
                errors.Add("DateBirth", "Не заполнена дата рождения ребенка");

            if (employee.listStaffMove.FirstOrDefault(p => p.DateS == DateTime.MinValue || p.idFirm == 0 || p.idPost == 0 || p.idUnit == 0) != null)
                errors.Add("DateS", "Заполните корректно кадровые перемещения");

            employee.ErrorList = errors;

            if (errors.Count > 0) return false;

            return true;

        }
    }
}
