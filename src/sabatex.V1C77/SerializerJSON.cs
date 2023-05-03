using sabatex.V1C77.Models;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public static class SerializerJSON
    {
        static bool IsNullOrWhiteSpace(string s)
        {
            if (s == null) return true;
            return s.Trim().Length == 0;
        }
        private const string nullValue = "null";
        static object GetValue(RootMetadata1C77 rootMetadata, AttributeMetadata1C77 attribute, ICOMObject1C77 com, string name, int level)
        {
            switch (attribute.Тип)
            {
                case "Дата":
                    if (attribute.Идентификатор != name)
                        return com.GetProperty<DateTime>(name);
                    else
                        return com.Method<DateTime>("GetAttrib", name);
                case "Число":
                    if (attribute.Идентификатор != name)
                        return com.GetProperty<double>(name);
                    else
                        return com.Method<double>("GetAttrib", name);
                case "Строка":
                    if (attribute.Идентификатор != name)
                        return com.GetProperty<string>(name).Trim();
                    else
                        return com.Method<string>("GetAttrib", name).Trim();
                case "Справочник":
                    ICatalog1C77 cat;
                    if (attribute.Идентификатор != name)
                        cat = com.GetProperty<ICatalog1C77>(name);
                    else
                        cat = com.Method<ICatalog1C77>("GetAttrib", name);
                    var kindName = "";
                    if (IsNullOrWhiteSpace(attribute.Вид))
                    {
                        kindName = cat.Method<string>("kind");
                    }
                    else
                    {
                        kindName = attribute.Вид;
                    }
                    return getCatalogProperties(rootMetadata, cat, kindName, level + 1);
                case "Перечисление":
                    ICOMObject1C77 enumVal;
                    if (attribute.Идентификатор != name)
                        enumVal = com.GetProperty<ICOMObject1C77>(name);
                    else
                        enumVal = com.Method<ICOMObject1C77>("GetAttrib", name);
                    if (com.GlobalContext.EmptyValue(enumVal))
                        return nullValue;
                    else
                        return enumVal.Method<string>("Идентификатор");
                case "Документ":
                    IDocument1C77 doc;
                    if (attribute.Идентификатор != name)
                        doc = com.GetProperty<IDocument1C77>(name);
                    else
                        doc = com.Method<IDocument1C77>("GetAttrib", name);
                    if (com.GlobalContext.EmptyValue(doc))
                        return nullValue;
                    else
                        return new { НомерДок = doc.GetProperty<string>("НомерДок"), ДатаДок = doc.GetProperty<DateTime>("ДатаДок") };
                case "Счет":
                    ICOMObject1C77 count;
                    if (attribute.Идентификатор != name)
                        count = com.GetProperty<ICOMObject1C77>(name);
                    else
                        count = com.Method<ICOMObject1C77>("GetAttrib", name);
                    if (com.GlobalContext.EmptyValue(count))
                        return nullValue;
                    else
                        return count.GetProperty<string>("Код");
                default: return nullValue;
            }
            throw new Exception($"Error load {attribute}");
        }
        static object Get1CAttribute(RootMetadata1C77 rootMetadata, AttributeMetadata1C77 attribute, ICOMObject1C77 com, int level = 0)
        {

            if ((attribute as IAttributePeriodicMetadata1C77)?.Периодический ?? false)
            {
                var per = com.GlobalContext.CreateObject("Периодический");
                if ((attribute as CatalogAttributeMetadata1C77) != null)
                    // set owner for 
                    per.Method<object>("ИспользоватьОбъект", attribute.Идентификатор, com);
                else
                    per.Method<object>("ИспользоватьОбъект", attribute.Идентификатор);

                var l = new List<PeriodicValue<object>>();
                if (per.Method<bool>("ВыбратьЗначения"))
                {
                    while (per.Method<bool>("ПолучитьЗначение"))
                    {
                        DateTime date = per.GetProperty<DateTime>("ДатаЗнач");
                        object value = GetValue(rootMetadata, attribute, per, "Значение", level);
                        l.Add(new PeriodicValue<object> { Date = date, Value = value });
                    }
                }
                return l;
            }
            else
            {
                return GetValue(rootMetadata, attribute, com, attribute.Идентификатор, level);
            }
        }
        static Dictionary<string, object> getCatalogProperties(RootMetadata1C77 rootMetadata, ICatalog1C77 catalog, string catalogName, int level = 0)
        {
            var result = new Dictionary<string, object>();
            result.Add("Код", catalog.Code);
            result.Add("Наименование", catalog.Description);
            result.Add("ЭтоГруппа", catalog.IsFolder);
            result.Add("ПометкаУдаления", catalog.DeleteMark);
            if (level >= 1) return result;

            var metadata = rootMetadata.Справочники[catalogName];


            if (metadata.КоличествоУровней > 0)
            {
                var parent = getCatalogProperties(rootMetadata, catalog.Parent, catalogName, level + 1);
                if (IsNullOrWhiteSpace(parent["Код"] as string) && IsNullOrWhiteSpace(parent["Наименование"] as string))
                {
                    result.Add("Родитель", "");

                }
                else
                {
                    result.Add("Родитель", parent);
                }
            }

            if (!IsNullOrWhiteSpace(metadata.Владелец))
                result.Add("Владелец", getCatalogProperties(rootMetadata, catalog.Owner, metadata.Владелец, level + 1));
            if (level > 0) return result;
            foreach (var m in metadata.Attributes)
            {
                try
                {
                    result.Add(m.Идентификатор, Get1CAttribute(rootMetadata, m, catalog, level));
                }
                catch (Exception e)
                {
                    throw new Exception($"Ошибка чтения реквизита {m.Идентификатор} {e.Message}");
                }
            }
            return result;
        }
        static Dictionary<string, object> getDocumentProperties(RootMetadata1C77 rootMetadata, IDocument1C77 document, string documentName, int level = 0)
        {
            var result = new Dictionary<string, object>();
            result.Add("ДатаДок", document.DocDate);
            result.Add("НомерДок", document.DocNum);
            result.Add("ПометкаУдаления", document.DeleteMark);
            result.Add("Проведен", document.IsTransacted);
            if (level > 1) return result;
            var metadata = rootMetadata.Документы[documentName];
            foreach (var attribute in metadata.РеквизитШапки.Values)
            {
                result.Add(attribute.Идентификатор, Get1CAttribute(rootMetadata, attribute, document, level + 1));
            }
            // common atributes
            foreach (var attribute in rootMetadata.ОбщиеРеквизитыДокумента.Values)
            {
                result.Add(attribute.Идентификатор, Get1CAttribute(rootMetadata, attribute, document, level + 1));
            }

            
            var table = new List<Dictionary<string, object>>();
            if (document.SelectLines())
            {
                while (document.GetLine())
                {
                    var tableData = new Dictionary<string, object>();
                    foreach (var attribute in metadata.РеквизитТабличнойЧасти.Values)
                    {
                        tableData.Add(attribute.Идентификатор, Get1CAttribute(rootMetadata, attribute, document, level + 1));
                    }
                    table.Add(tableData);
                }
            }
            result.Add("TableSection", table);


            return result;
        }
        /// <summary>
        /// Serialize 1C7.7 object to JSON
        /// </summary>
        /// <typeparam name="T">ICatalog1C77 or IDocument1C77</typeparam>
        /// <param name="rootMetadata"> root metadata</param>
        /// <param name="obj">Serialized object</param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static string SerializeJSON<T>(this RootMetadata1C77 rootMetadata, T obj,string objectName,Dictionary<string,object> externalData = null )
        {
            Dictionary<string, object> result;
            if (obj== null)
                throw new ArgumentNullException(nameof(obj));
            if (typeof(T) == typeof(ICatalog1C77))
                result = getCatalogProperties(rootMetadata, (ICatalog1C77)obj, objectName, 0);
            else if (typeof(T) == typeof(IDocument1C77))
                result = getDocumentProperties(rootMetadata, (IDocument1C77)obj, objectName, 0);
            else
                throw new Exception("Unsupported type");
            if (externalData != null) result.Add("externalData",externalData);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
    }
}
