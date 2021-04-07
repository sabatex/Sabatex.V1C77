using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sabatex.V1C77;
using sabatex.V1C77.Models;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi1C.Shared;

namespace WebApi1C77.Services
{
    public class Service1C77
    {
        private readonly IConfiguration configuration;
        private IGlobalContext _connection;
        private readonly object connectionLock = new object();
        private DateTime runTime;
        private bool _isDoStarted = false;

        private const string errorGetConfigParamString = "Ошибка чтения параметра {0} из appsettings.json";
        private const string tokenMetadata = "metadata";
        private const string tokenConstant = "константа";
        private const string tokenEnum = "перечисление";
        private const string tokenCatalog = "catalog";
        private const string nullValue = "null";
        private static readonly string[] tokenDocument = { "document", "документ" };

        private string _dataBasePath;
        private string _userName;
        private string _userPass;
        //private string _casheDir;

        private string _lastError = "";

        public TimeSpan RunTime => IsStarted ? DateTime.Now - runTime : TimeSpan.FromSeconds(0);
        public bool IsStarted => _connection != null;

        public Service1C77(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }


        private RootMetadata1C77 rootMetadata;
        public RootMetadata1C77 Metadata { get => rootMetadata; }


        private void ReadConfig()
        {
            var v1c77Connection = configuration.GetSection("1C7.7");
            if (v1c77Connection == null)
                throw new Exception("Не существеут раздел настроек 1C7.7 в файле appsettings.json");
            _dataBasePath = v1c77Connection["dbPath"];
            if (_dataBasePath == null)
                throw new Exception(string.Format(errorGetConfigParamString, "dbPath"));
            _userName = v1c77Connection["user"];
            if (_userName == null)
                throw new Exception(string.Format(errorGetConfigParamString, "user"));
            _userPass = v1c77Connection["pass"];
            if (_userPass == null)
                throw new Exception(string.Format(errorGetConfigParamString, "pass"));
        }

        public void Start()
        {
            _lastError = string.Empty;
            try
            {
                ReadConfig();
            }
            catch (Exception e)
            {
                _lastError = e.Message;
                return;
            }


            var con = new sabatex.V1C77.Models.Connection
            {
                DataBasePath = _dataBasePath,
                PlatformType = sabatex.V1C77.Models.EPlatform1C.V77M,
                UserName = _userName,
                UserPass = _userPass
            };
            lock (connectionLock)
            {
                if (_connection == null)
                {
                    try
                    {
                        _isDoStarted = true;
                        var connection = sabatex.V1C77.COMObject1C77.CreateConnection(con);
                        runTime = DateTime.Now;
                        try
                        {
                            rootMetadata = MetadataBuilder.GetMetadata(connection);
                            _connection = connection;
                        }
                        catch (Exception e)
                        {
                            connection.Dispose();
                            throw new Exception(e.Message);
                        }

                    }
                    catch (Exception e)
                    {
                        _lastError = e.Message;
                    }
                    finally
                    {
                        _isDoStarted = false;
                    }
                }

            }

        }
        public void Stop()
        {
            lock (connectionLock)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
                rootMetadata = null;
            }
        }

        public Service1C77State GetState()
        {
            var result = new Service1C77State
            {
                IsWorked = IsStarted,
                IsDoStarted = _isDoStarted,
                TimeWorked = RunTime.TotalMilliseconds,
                LastError = _lastError
            };
            _lastError = string.Empty;
            return result;
        }

        Dictionary<string, object> GetCatalogItem(ICatalog1C77 catalog, CatalogMetadata1C77 metadata, int level = 0)
        {
            var result = new Dictionary<string, object>();
            result.Add("Код", catalog.Code);
            result.Add("Наименование", catalog.Description);
            result.Add("ЭтоГруппа", catalog.IsFolder);
            result.Add("ПометкаУдаления", catalog.DeleteMark);
            if (level > 1) return result;
            if (metadata.КоличествоУровней > 0)
            {
                var parent = GetCatalogItem(catalog.Parent, metadata, level + 1);
                result.Add("Родитель", string.IsNullOrWhiteSpace(parent["Код"] as string) && string.IsNullOrWhiteSpace(parent["Наименование"] as string) ? "" : parent);
            }
            if (!string.IsNullOrWhiteSpace(metadata.Владелец))
                result.Add("Владелец", GetCatalogItem(catalog.Owner, rootMetadata.Справочники[metadata.Владелец], level + 1));
            if (level > 0) return result;
            foreach (var m in metadata.Attributes)
            {
                try
                {
                    result.Add(m.Идентификатор, Get1CAttribute(m, catalog, level));
                }
                catch (Exception e)
                {
                    throw new Exception($"Ошибка чтения реквизита {m.Идентификатор} {e.Message}");
                }
            }
            return result;
        }
        public Dictionary<string, object> GetCatalogItem(string catalogId, CatalogMetadata1C77 metadata)
        {
            lock (connectionLock)
            {
                var catalog = _connection.CreateObjectCatalog(metadata.Идентификатор);
                if (catalog.FindByCode(catalogId))
                {
                    return GetCatalogItem(catalog, metadata);
                }
                else
                {
                    return new Dictionary<string, object> { { "error", $"Catalog item with code {catalogId} not exist" } };
                }
            }
        }
        public Dictionary<string, object> GetDocumentItem(IDocument1C77 document, DocummentMetadata1C77 metadata, int level = 0)
        {
            var result = new Dictionary<string, object>();
            result.Add("ДатаДок", document.DocDate);
            result.Add("НомерДок", document.DocNum);
            result.Add("ПометкаУдаления", document.DeleteMark);
            result.Add("Проведен", document.IsTransacted);
            if (level > 1) return result;
            foreach(var attribute in metadata.РеквизитШапки.Values)
            {
                result.Add(attribute.Идентификатор, Get1CAttribute(attribute, document, level + 1));
            }
            return result;
        }


        public object Get1CAttribute(AttributeMetadata1C77 attribute, ICOMObject1C77 com, int level = 0)
        {
            #region functions
            object GetValue(AttributeMetadata1C77 attribute, ICOMObject1C77 com, string name)
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
                            return com.GetProperty<string>(name);
                        else
                            return com.Method<string>("GetAttrib",name);
                    case "Справочник":
                        ICatalog1C77 cat;
                        if (attribute.Идентификатор != name)
                            cat = com.GetProperty<ICatalog1C77>(name);
                        else
                            cat = com.Method<ICatalog1C77>("GetAttrib", name);
                        var kindName = "";
                        if (string.IsNullOrWhiteSpace(attribute.Вид))
                        {
                            kindName = cat.Method<string>("kind");
                        }
                        else
                        {
                            kindName = attribute.Вид;
                        }
                        var meta = rootMetadata.Справочники[kindName];
                        return GetCatalogItem(cat, meta, level + 1);
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
                }
                throw new Exception($"Error load {attribute}");
            }


            #endregion

            if ((attribute as IAttributePeriodicMetadata1C77)?.Периодический ?? false)
            {
                var per = _connection.CreateObject("Периодический");
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
                        object value =  GetValue(attribute,per,"Значение");
                        l.Add(new PeriodicValue<object> { Date = date, Value = value });
                    }
                }
                return l;
            }
            else
            {
                return GetValue(attribute, com, attribute.Идентификатор);
            }
        }


        public async Task<object> GetDataFrom1C([NotNull] string selector, string key, bool fresh = false)
        {


            Dictionary<string, object> FindCatalogItem(CatalogMetadata1C77 metadata, string key, bool fresh = false)
            {
                var sl = key.Split('=', 2);
                if (sl.Length != 2) throw new Exception($"The parameter key ({key}) is not valid");

                switch (sl[0].ToLower())
                {
                    case "код":

                        var catalog = _connection.CreateObjectCatalog(metadata.Идентификатор);
                        if (catalog.FindByCode("sl[1]"))
                            return GetCatalogItem(catalog, metadata, 0);
                        else
                            return new Dictionary<string, object>();
                }
                throw new Exception($"Не найден элемент по коду {key}");

            }
            async Task<List<Dictionary<string, object>>> GetCatalogItems(CatalogMetadata1C77 metadata, bool fresh = false)
            {
                List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
                var catalog = _connection.CreateObjectCatalog(metadata.Идентификатор);
                if (catalog.SelectItems())
                {
                    while (catalog.NextItem())
                        result.Add(GetCatalogItem(catalog, metadata, 0));
                }
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                var st = System.Text.Json.JsonSerializer.Serialize(result, options);
                return result;
            }


            string[] sl = selector.Trim().Split('.');

            // documents
            if (tokenDocument.Contains(sl[0]))
            {
                if (sl.Length == 1) return rootMetadata.Документы.Keys.ToArray();

            }




            switch (sl[0])
            {
                case tokenMetadata: return rootMetadata;
                case tokenConstant:
                    if (sl.Length > 1)
                    {
                        var cm = rootMetadata.Константы[sl[1]];
                        return Get1CAttribute(cm, _connection.GetProperty<ICOMObject1C77>(tokenConstant));
                    }
                    else
                        return rootMetadata.Константы.Keys.ToArray();
                case tokenEnum:
                    if (sl.Length == 2)
                    {
                        return rootMetadata.Перечисления[sl[1]];
                    }
                    return rootMetadata.Перечисления.Keys.ToArray();
                case tokenCatalog:
                    if (sl.Length == 1) return rootMetadata.Справочники.Keys.ToArray();
                    if (sl.Length == 2)
                    {
                        if (string.IsNullOrWhiteSpace(key))
                            return GetCatalogItems(rootMetadata.Справочники[sl[1]], fresh);
                        else
                            return FindCatalogItem(rootMetadata.Справочники[sl[1]], key);
                    }
                    throw new Exception($"Ошибка селектора {selector}");


            }
            throw new Exception($"Не иожна визначити дані {selector} - {key}");
        }

        /// <summary>
        /// get first 50 items
        /// </summary>
        /// <param name="catalogMetadata1C77"></param>
        public object[] GetCatalogItems(CatalogMetadata1C77 catalogMetadata1C77)
        {
            var result = new List<object>();

            lock (connectionLock)
            {
                var cat = _connection.CreateObjectCatalog(catalogMetadata1C77.Идентификатор);
                if (cat.SelectItems())
                {
                    int count = 50;
                    while (cat.NextItem() && count > 0)
                    {
                        if (cat.IsFolder) continue;
                        result.Add(GetCatalogItem(cat, catalogMetadata1C77));
                        count--;
                    }
                }
                return result.ToArray();
            }
        }


        /// <summary>
        /// get first 50 items
        /// </summary>
        /// <param name="catalogMetadata1C77"></param>
        public object[] GetDocumentItems(DocummentMetadata1C77 documentMetadata1C77,DocumentFilter documentSelector)
        {
            var result = new List<object>();

            lock (connectionLock)
            {
                var doc = _connection.CreateObjectDocument(documentMetadata1C77.Идентификатор);

                if (doc.SelectDocuments(documentSelector.BeginPeriod,documentSelector.EndPeriod))
                {
                    int count = documentSelector.Top;
                    while (doc.GetDocument() && count > 0)
                    {
                        result.Add(GetDocumentItem(doc, documentMetadata1C77));
                        count--;
                    }
                }
                return result.ToArray();
            }
        }

        public object GetConstant(string name)
        {
            if (rootMetadata.Константы.TryGetValue(name, out var constant))
            {
                lock (connectionLock)
                {
                    return Get1CAttribute(constant, _connection.GetProperty<ICOMObject1C77>(tokenConstant));
                }
            }
            throw new Exception($"Не найдена константа {name}");
        }

        public object MessageNotStarted => new {Error="Служба не запущена"};

    }


}
