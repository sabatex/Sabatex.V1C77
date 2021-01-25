using System;
using System.Collections.Generic;

namespace V1C77.Models
{
    public class Document
    {
        public string DocNum { get; set; }
        public DateTime DocDate { get; set; }
        public bool IsMarkDelete { get; set; }
        public bool IsTransacted { get; set; }

        public Dictionary<string,object> Attributes { get; set; }
        public List<DocumentRow> Rows { get; set; }

    }
    public class DocumentRow
    {
        public int LineNumber { get; set; }
        public Dictionary<string, object> Attributes { get; set; }
    }

    public struct DocumentSelector
    {
        public DateTime DocDate;
        public string DocNum;
        public DocumentSelector(DateTime date,string docNum)
        {
            DocDate = date;
            DocNum = docNum; 
        }
    }

}
