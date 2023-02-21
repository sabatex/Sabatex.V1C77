using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public partial class COMObject1C77 : IDocument1C77
    {
        string IDocument1C77.DocNum
        {
            get => GetProperty<string>("DocNum").Trim();
            set => SetProperty("DocNum", value);
        }
        DateTime IDocument1C77.DocDate { get => GetProperty<DateTime>("DocDate"); set => SetProperty("DocDate", value); }
        double IDocument1C77.LineNum { get => GetProperty<double>("LineNum"); set => SetProperty("LineNum", value); }
        string IDocument1C77.Kind => Method<string>("Kind");
        string IDocument1C77.KindPresent => Method<string>("KindPresent");
        void IDocument1C77.SetAttrib(string attributeName, object value) => Method<object>("SetAttrib", attributeName, value);
        T IDocument1C77.GetAttrib<T>(string attributeName) => Method<T>("GetAttrib", attributeName);
        bool IDocument1C77.Selected => Method<bool>("Selected");
        bool IDocument1C77.IsTransacted => Method<bool>("IsTransacted");
        IDocument1C77 IDocument1C77.CurrentDocument => Method<IDocument1C77>("CurrentDocument");
        double IDocument1C77.Total(string attributeName) => Method<double>("Total", attributeName);
        double IDocument1C77.LinesCnt => Method<double>("LinesCnt");
        bool IDocument1C77.FindDocument(IDocument1C77 doc)
        { 
            return Method<bool>("FindDocument", doc);
        }
        bool IDocument1C77.FindByNum(string numberDoc, DateTime date, string idKind) => Method<bool>("FindByNum", numberDoc, date, idKind);
        bool IDocument1C77.FindByNum(string numberDoc, DateTime date) => Method<bool>("FindByNum", numberDoc, date, null);
        bool IDocument1C77.GetLineByNum(double num) => Method<bool>("GetLineByNum", num);
        bool IDocument1C77.SelectDocuments(DateTime? begin, DateTime? end) => Method<bool>("SelectDocuments", begin, end);
        bool IDocument1C77.SelectChildDocs(DateTime? begin, DateTime? end, IDocument1C77 doc) => Method<bool>("SelectChildDocs", begin, end, doc);
        bool IDocument1C77.SelectByValue(DateTime? begin, DateTime? end, string selectorName, object value) => Method<bool>("SelectByValue", begin, end, selectorName, value);
        bool IDocument1C77.SelectByNum(string numberDoc, DateTime date, string idKind) => Method<bool>("SelectByNum", numberDoc, date, idKind);
        bool IDocument1C77.BackwardOrder(bool backward) => Method<bool>("BackwardOrder", backward);
        bool IDocument1C77.BackwardOrder() => Method<bool>("BackwardOrder", null);
        void IDocument1C77.SetFilter(bool transacted, bool noTransacted, bool oper, bool calculated, bool account) => Method<object>("SetFilter", transacted, noTransacted, oper, calculated, account);
        bool IDocument1C77.GetDocument() => Method<bool>("GetDocument");
        bool IDocument1C77.SelectLines() => Method<bool>("SelectLines");
        bool IDocument1C77.GetLine() => Method<bool>("GetLine");
        string IDocument1C77.NumPrefix(string prefix) => Method<string>("NumPrefix", prefix);
        string IDocument1C77.NumPrefix() => Method<string>("NumPrefix", null);
        void IDocument1C77.SetNewNum(string prefix) => Method<object>("SetNewNum", prefix);
        void IDocument1C77.SetType(string attributeName, string typeName, double? longType, double? precision) => Method<object>("SetType", attributeName, typeName, longType, precision);
        void IDocument1C77.Write() => Method<object>("Write");
        void IDocument1C77.Delete(bool full) => Method<object>("Delete", full);
        void IDocument1C77.Delete() => Method<object>("Delete", true);
        bool IDocument1C77.DeleteMark => Method<bool>("DeleteMark");
        void IDocument1C77.ClearDeleteMark() => Method<object>("ClearDeleteMark");
        void IDocument1C77.NewLine() => Method<object>("NewLine");
        void IDocument1C77.DeleteLine() => Method<object>("DeleteLine");
        void IDocument1C77.DeleteLines() => Method<object>("DeleteLines");
        string IDocument1C77.GetPosition() => Method<string>("GetPosition");
        bool IDocument1C77.Locking(bool? lockDoc) => Method<bool>("Locking", lockDoc);
        ICOMObject1C77 IDocument1C77.Operation { get => GetProperty<ICOMObject1C77>("Operation"); set => SetProperty("Operation", value); }

 
    }
}
