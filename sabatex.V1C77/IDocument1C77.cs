// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public interface IDocument1C77:ICOMObject1C77
    {
        /// <summary>
        /// Позволяет получать и устанавливать номер документа(в виде строки).
        /// Подробнее см.в документации, глава 'Работа с Документами'
        /// </summary>
        string DocNum { get; set; }
        DateTime DocDate { get; set;}
        double LineNum { get; set; }
        string Kind { get; }
        string KindPresent { get; }
        void SetAttrib(string attributeName, object value);
        T GetAttrib<T>(string attributeName);
        bool Selected { get; }
        bool IsTransacted { get; }
        IDocument1C77 CurrentDocument { get; }
        double Total(string attributeName);
        double LinesCnt { get;}
        bool FindDocument(IDocument1C77 doc);
        bool FindByNum(string numberDoc, DateTime date);
        bool FindByNum(string numberDoc, DateTime date, string idKind);
        bool GetLineByNum(double num);
        bool SelectDocuments(DateTime? begin, DateTime? end);
        bool SelectChildDocs(DateTime? begin, DateTime? end, IDocument1C77 doc);
        bool SelectByValue(DateTime? begin, DateTime? end, string selectorName, object value);
        bool SelectByNum(string numberDoc, DateTime date, string idKind);
        bool BackwardOrder(bool backward);
        bool BackwardOrder();
        void SetFilter(bool transacted, bool noTransacted, bool oper, bool calculated, bool account);
        bool GetDocument();
        bool SelectLines();
        bool GetLine();
        string NumPrefix(string prefix);
        string NumPrefix();
        void SetNewNum(string prefix);
        void SetType(string attributeName, string typeName, double? longType, double? precision);
        void Write();
        void Delete(bool full);
        void Delete();
        bool DeleteMark { get; }
        void ClearDeleteMark();
        void NewLine();
        void DeleteLine();
        void DeleteLines();
        string GetPosition();
        bool Locking(bool? lockDoc);
        ICOMObject1C77 Operation { get; set; }
    }
}
