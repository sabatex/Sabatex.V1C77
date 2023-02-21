// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
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
        string НомерДок { get; set; }
        DateTime ДатаДок { get; set; }
        double НомерСтроки { get; set; }
        string Вид();
        string ПредставлениеВида();
        void УстановитьАтрибут(string attributeName, object value);
        T ПолучитьАтрибут<T>(string attributeName);
        double Выбран();
        double Проведен();
        Документ1С77 ТекущийДокумент();
        double Итог(string attributeName);
        double КоличествоСтрок();
        double НайтиДокумент(Документ1С77 doc);
        double НайтиПоНомеру(string numberDoc, DateTime date, string idKind = null);
        double ПолучитьСтрокуПоНомеруh(double num);
        double ВыбратьДокументы(DateTime? begin, DateTime? end);
        double ВыбратьПодчиненныеДокументы(DateTime? begin, DateTime? end, Документ1С77 doc);
        double ВыбратьПоЗначению(DateTime? begin, DateTime? end, string selectorName, object value);
        double ВыбратьПоНомеру(string numberDoc, DateTime date, string idKind);
        double ОбратныйПорядок(double? backward = null);
        void УстановитьФильтр(double transacted, double noTransacted, double oper, double calculated, double account);
        double ПолучитьДокумент();
        double ВыбратьСтроки();
        double ПолучитьСтроку();
        string ПрефиксНомера(string prefix = null);
        void УстановитьНовыйНомер(string prefix);
        void НазначитьТип(string attributeName, string typeName, double? longType, double? precision);
        void Записать();
        void Удалить(double full = 1);
        double ПометкаУдаления();
        void СнятьПометкуУдаления();
        void НоваяСтрока();
        void УдалитьСтроку();
        void УдалитьСтроки();
        string ПолучитьПозицию();

    }
}
