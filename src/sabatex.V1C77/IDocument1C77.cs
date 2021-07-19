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
        public string DocNum
        {
            get => GetProperty<string>("DocNum").Trim();
            set => SetProperty("DocNum", value);
        }
        public DateTime DocDate { get => GetProperty<DateTime>("DocDate"); set => SetProperty("DocDate", value); }
        public double LineNum { get => GetProperty<double>("LineNum"); set => SetProperty("LineNum", value); }
        public string Kind => Method<string>("Kind");
        public string KindPresent => Method<string>("KindPresent");
        public void SetAttrib(string attributeName, object value) => Method<object>("SetAttrib", attributeName, value);
        public T GetAttrib<T>(string attributeName) => Method<T>("GetAttrib", attributeName);
        public bool Selected => Method<bool>("Selected");
        public bool IsTransacted => Method<bool>("IsTransacted");
        public IDocument1C77 CurrentDocument => Method<IDocument1C77>("CurrentDocument");
        public double Total(string attributeName) => Method<double>("Total", attributeName);
        public double LinesCnt => Method<double>("LinesCnt");
        public bool FindDocument(IDocument1C77 doc) => Method<bool>("FindDocument", doc);
        public bool FindByNum(string numberDoc, DateTime date, string idKind = null) => Method<bool>("FindByNum", numberDoc, date, idKind);
        public bool GetLineByNum(double num) => Method<bool>("GetLineByNum", num);
        public bool SelectDocuments(DateTime? begin, DateTime? end) => Method<bool>("SelectDocuments", begin, end);
        public bool SelectChildDocs(DateTime? begin, DateTime? end, IDocument1C77 doc) => Method<bool>("SelectChildDocs", begin, end, doc);
        public bool SelectByValue(DateTime? begin, DateTime? end, string selectorName, object value) => Method<bool>("SelectByValue", begin, end, selectorName, value);
        public bool SelectByNum(string numberDoc, DateTime date, string idKind) => Method<bool>("SelectByNum", numberDoc, date, idKind);
        public bool BackwardOrder(bool? backward=null) => Method<bool>("BackwardOrder", backward);
        public void SetFilter(bool transacted, bool noTransacted, bool oper, bool calculated, bool account) => Method<object>("SetFilter",transacted, noTransacted, oper, calculated, account);
        public bool GetDocument() => Method<bool>("GetDocument");
        public bool SelectLines() => Method<bool>("SelectLines");
        public bool GetLine() => Method<bool>("GetLine");
        public string NumPrefix(string prefix = null) => Method<string>("NumPrefix", prefix);
        public void SetNewNum(string prefix) => Method<object>("SetNewNum", prefix);
        public void SetType(string attributeName, string typeName, double? longType, double? precision) => Method<object>("SetType", attributeName, typeName, longType, precision);
        public void Write() => Method<object>("Write");
        public void Delete(bool full = true) => Method<object>("Delete", full);
        public bool DeleteMark => Method<bool>("DeleteMark");
        public void ClearDeleteMark() => Method<object>("ClearDeleteMark");
        public void NewLine() => Method<object>("NewLine");
        public void DeleteLine() => Method<object>("DeleteLine");
        public void DeleteLines() => Method<object>("DeleteLines");
        public string GetPosition() => Method<string>("GetPosition");
        public bool Locking(bool? lockDoc) => Method<bool>("Locking", lockDoc);
        public ICOMObject1C77 Operation { get => GetProperty<ICOMObject1C77>("Operation"); set => SetProperty("Operation", value); }
    }
}
