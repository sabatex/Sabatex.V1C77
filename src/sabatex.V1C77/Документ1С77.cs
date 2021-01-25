using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public interface Документ1С77 : ICOMObject1C77
    {
        /// <summary>
        /// Позволяет получать и устанавливать номер документа(в виде строки).
        /// Подробнее см.в документации, глава 'Работа с Документами'
        /// </summary>
        public string НомерДок { get => GetProperty<string>("DocNum").Trim(); set => SetProperty("DocNum", value);}
        public DateTime ДатаДок { get => GetProperty<DateTime>("DocDate"); set => SetProperty("DocDate", value); }
        public double НомерСтроки { get => GetProperty<double>("LineNum"); set => SetProperty("LineNum", value); }
        public string Вид() => Method<string>("Kind");
        public string ПредставлениеВида() => Method<string>("KindPresent");
        public void УстановитьАтрибут(string attributeName, object value) => Method<object>("SetAttrib", attributeName, value);
        public T ПолучитьАтрибут<T>(string attributeName) => Method<T>("GetAttrib", attributeName);
        public double Выбран() => Method<double>("Selected");
        public double Проведен() => Method<double>("IsTransacted");
        public Документ1С77 ТекущийДокумент() => Method<Документ1С77>("CurrentDocument");
        public double Итог(string attributeName) => Method<double>("Total", attributeName);
        public double КоличествоСтрок() => Method<double>("LinesCnt");
        public double НайтиДокумент(Документ1С77 doc) => Method<double>("FindDocument", doc);
        public double НайтиПоНомеру(string numberDoc, DateTime date, string idKind = null) => Method<double>("FindByNum", numberDoc, date, idKind);
        public double ПолучитьСтрокуПоНомеруh(double num) => Method<double>("GetLineByNum", num);
        public double ВыбратьДокументы(DateTime? begin, DateTime? end) => Method<double>("SelectDocuments", begin, end);
        public double ВыбратьПодчиненныеДокументы(DateTime? begin, DateTime? end, Документ1С77 doc) => Method<double>("SelectChildDocs", begin, end, doc);
        public double ВыбратьПоЗначению(DateTime? begin, DateTime? end, string selectorName, object value) => Method<double>("SelectByValue", begin, end, selectorName, value);
        public double ВыбратьПоНомеру(string numberDoc, DateTime date, string idKind) => Method<double>("SelectByNum", numberDoc, date, idKind);
        public double ОбратныйПорядок(double? backward = null) => Method<double>("BackwardOrder", backward);
        public void УстановитьФильтр(double transacted, double noTransacted, double oper, double calculated, double account) => Method<object>("SetFilter", transacted, noTransacted, oper, calculated, account);
        public double ПолучитьДокумент() => Method<double>("GetDocument");
        public double ВыбратьСтроки() => Method<double>("SelectLines");
        public double ПолучитьСтроку() => Method<double>("GetLine");
        public string ПрефиксНомера(string prefix = null) => Method<string>("NumPrefix", prefix);
        public void УстановитьНовыйНомер(string prefix) => Method<object>("SetNewNum", prefix);
        public void НазначитьТип(string attributeName, string typeName, double? longType, double? precision) => Method<object>("SetType", attributeName, typeName, longType, precision);
        public void Записать() => Method<object>("Write");
        public void Удалить(double full = 1) => Method<object>("Delete", full);
        public double ПометкаУдаления() => Method<double>("DeleteMark");
        public void СнятьПометкуУдаления() => Method<object>("ClearDeleteMark");
        public void НоваяСтрока() => Method<object>("NewLine");
        public void УдалитьСтроку() => Method<object>("DeleteLine");
        public void УдалитьСтроки() => Method<object>("DeleteLines");
        public string ПолучитьПозицию() => Method<string>("GetPosition");

    }
}
