using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using sabatex.V1C77.Models;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi1C.Client.Shared
{
    public partial class Attribute1C77
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] ILogger<Attribute1C77> logger { get; set; }
        [Parameter] public AttributeMetadata1C77 metadata { get; set; }
        [Parameter] public JsonElement Value {get;set;}
        string stringValue="";
        bool isPeriodic = false;
        List<PeriodicValue<string>> periodicValues = new List<PeriodicValue<string>>();

        protected override void OnParametersSet()
        { 
            string getPresentation(JsonElement value)
            {
                switch (metadata.Тип)
                {
                    case "Дата":
                        if (value.TryGetDateTime(out DateTime date))
                            return date.ToShortDateString();
                        else
                            return "null";
                    case "Число":
                        if (value.TryGetDouble(out Double vd))
                            return vd.ToString();
                        else
                            return "null";
                    case "Строка":
                        return value.GetString();
                    case "Перечисление":
                        return value.GetString();
                    case "Счет":
                        return value.GetString();
                    case "Справочник":
                        if (value.TryGetProperty("Наименование", out var name))
                        {
                            return name.GetString();
                        }
                        else
                        {
                            return "null";
                        }
                }
                return "uknown";
            }


            isPeriodic = (metadata as IAttributePeriodicMetadata1C77)?.Периодический ?? false;
            if (isPeriodic)
            {
                var pv = Value.EnumerateArray();
                foreach (var ev in pv)
                {
                    if (ev.GetProperty("date").TryGetDateTime(out DateTime dt))
                    {
                        var p = ev.GetProperty("value");

                        try
                        {
                            periodicValues.Add(new PeriodicValue<string> { Date = dt, Value = getPresentation(p) });
                        }
                        catch (Exception e)
                        {
                            var error = e.Message;
                        }
                    }
                }
                if (periodicValues.Count == 0)
                {
                    stringValue = "NULL";
                }
                else
                {
                    periodicValues = periodicValues.OrderByDescending(o => o.Date).ToList();
                    stringValue = periodicValues[0].Value;
                }
            }
            else
                stringValue = getPresentation(Value);

        }
    }
}
