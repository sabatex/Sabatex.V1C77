using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public static class MetadataBuilder
    {
        const string metadataToken = "Metadata";
        const string constantToken = "Константа";
        const string catalogToken = "Справочник";
        const string enumToken = "Перечисление";
        const string documentToken = "Документ";
        const string commonDocumentAttributesToken = "ОбщийРеквизитДокумента";
        static void SetAttributeMetadata<T>(ICOMObject1C77 com, T metadata) where T : AttributeMetadata1C77
        {
            metadata.Идентификатор = com.GetProperty<string>("Идентификатор");
            metadata.Комментарий = com.GetProperty<string>("Комментарий");
            metadata.Синоним = com.GetProperty<string>("Синоним");

            metadata.Тип = com.GetProperty<string>("Тип");
            metadata.Вид = com.GetProperty<string>("Вид");
            metadata.Длина = com.GetProperty<double>("Длина");
            metadata.НеОтрицательный = com.GetProperty<double>("НеОтрицательный") == 1;
            metadata.РазделятьТриады = com.GetProperty<double>("РазделятьТриады") == 1;
            metadata.Точность = com.GetProperty<double>("Точность");
        }
        static void SetCatalogAttributeMetadata(ICOMObject1C77 com, CatalogAttributeMetadata1C77 metadata)
        {
            SetAttributeMetadata(com, metadata);
            metadata.Сортировка = com.GetProperty<double>("Сортировка") == 1;
            metadata.Периодический = com.GetProperty<double>("Периодический") == 1;
        }

        static ConstantMetadata1C77 GetConstantMetadata(ICOMObject1C77 com)
        {
            var result = new ConstantMetadata1C77();
            SetAttributeMetadata(com, result);
            result.Периодический = com.GetProperty<double>("Периодический") == 1;
            return result;
        }
        static CatalogMetadata1C77 GetCatalogMetadata(ICOMObject1C77 com)
        {
            var metadata = new CatalogMetadata1C77();
            
            metadata.Идентификатор = com.GetProperty<string>("Идентификатор");
            metadata.Комментарий = com.GetProperty<string>("Комментарий");
            metadata.Синоним = com.GetProperty<string>("Синоним");

            metadata.КоличествоУровней = (int)com.GetProperty<double>("КоличествоУровней");
            metadata.ДлинаКода = (int)com.GetProperty<double>("ДлинаКода");
            metadata.ДлинаНаименования = (int)com.GetProperty<double>("ДлинаНаименования");
            metadata.СерииКодов = com.GetProperty<string>("СерииКодов");
            metadata.ТипКода = com.GetProperty<string>("ТипКода");
            metadata.ОсновноеПредставление = com.GetProperty<string>("ОсновноеПредставление");
            metadata.КонтрольУникальности = com.GetProperty<double>("КонтрольУникальности") == 1;
            metadata.АвтоНумерация = com.GetProperty<double>("АвтоНумерация") == 2;
            var owner = com.GetProperty<ICOMObject1C77>("Владелец");
            metadata.Владелец = owner.Method<string>("Представление");
            int count = (int)com.Method<double>("Реквизит");

            var attributes = new List<CatalogAttributeMetadata1C77>();
            for (int i = 0; i < count; i++)
            {
                var metadataAttr = new CatalogAttributeMetadata1C77();
                SetCatalogAttributeMetadata(com.Method<ICOMObject1C77>("Реквизит", i + 1), metadataAttr);
                attributes.Add(metadataAttr);
            }
            metadata.Attributes = attributes;

            return metadata;
        }
        static CommonDocummentAttributeMetadata1C77 GetCommonDocummentAttributeMetadata(ICOMObject1C77 com)
        {
            var metadata = new CommonDocummentAttributeMetadata1C77();
            metadata.Идентификатор = com.GetProperty<string>("Идентификатор");
            metadata.Комментарий = com.GetProperty<string>("Комментарий");
            metadata.Синоним = com.GetProperty<string>("Синоним");
            
            SetAttributeMetadata(com, metadata);
            metadata.Сортировка = com.GetProperty<double>("Сортировка") == 1;
            return metadata;
        }
        static DocumentTableAttributeMetadata1C77 GetDocumentTableAttributeMetadata(ICOMObject1C77 com)
        {
            var metadata = new DocumentTableAttributeMetadata1C77();
            metadata.Идентификатор = com.GetProperty<string>("Идентификатор");
            metadata.Комментарий = com.GetProperty<string>("Комментарий");
            metadata.Синоним = com.GetProperty<string>("Синоним");
            SetAttributeMetadata(com, metadata);
            metadata.ИтогПоКолонке = com.GetProperty<double>("ИтогПоКолонке") == 1;
            return metadata;
        }
        static DocummentMetadata1C77 GetDocumentMetadata(ICOMObject1C77 com)
        {
            var result = new DocummentMetadata1C77();
            result.Идентификатор = com.GetProperty<string>("Идентификатор");
            result.Комментарий = com.GetProperty<string>("Комментарий");
            result.Синоним = com.GetProperty<string>("Синоним");

            result.АвтоНумерация = com.GetProperty<double>("АвтоНумерация") == 2;
            result.АвтоНумерацияСтрок = com.GetProperty<double>("АвтоНумерацияСтрок") == 1;
            result.АвтоРегистрация = com.GetProperty<double>("АвтоРегистрация") == 1;
            result.АвтоудалениеДвижений = com.GetProperty<double>("АвтоудалениеДвижений") == 1;
            result.БухгалтерскийУчет = com.GetProperty<double>("БухгалтерскийУчет") == 1;
            //var ownerDocumemt = com.GetProperty<ICOMObject1C77>("ВводимыеНаОснованииДокументы");
            //var sb = new List<string>();
            //int count = (int)ownerDocumemt.Method<double>("Реквизит");
            //for (int i = 0;i < count; i++)
            //{
            //   sb.Add(com.Method<ICOMObject1C77>("Реквизит", i + 1).Method<string>("Представление"));
            //}
            //result.ВводимыеНаОснованииДокументы = sb.ToArray();



            result.ДлинаНомера = com.GetProperty<double>("ДлинаНомера");
            result.ДополнительныеКодыИБ = com.GetProperty<string>("ДополнительныеКодыИБ");
            //result.Журнал = com.GetProperty<string>("Журнал");
            result.КонтрольУникальности = com.GetProperty<double>("КонтрольУникальности") == 1;
            //result.Нумератор = com.GetProperty<string>("Нумератор");
            result.ОбластьРаспространения = com.GetProperty<string>("ОбластьРаспространения");
            result.ОперативныйУчет = com.GetProperty<double>("ОперативныйУчет") == 1;
            result.ОснованиеДляЛюбогоДокумента = com.GetProperty<double>("ОснованиеДляЛюбогоДокумента") == 1;
            result.ПериодичностьНомера = com.GetProperty<string>("ПериодичностьНомера");
            result.РазрешитьПроведение = com.GetProperty<double>("РазрешитьПроведение") == 1;
            result.Расчет = com.GetProperty<double>("Расчет") == 1;
            result.РедактированиеОпераций = com.GetProperty<double>("РедактированиеОпераций") == 1;
            result.СоздаватьОперацию = com.GetProperty<string>("СоздаватьОперацию");
            result.ТипНомера = com.GetProperty<string>("ТипНомера");
            result.РеквизитШапки = new Dictionary<string, AttributeMetadata1C77>();
            int count = (int)com.Method<double>("РеквизитШапки");
            for (int i = 0; i < count; i++)
            {
                var rCOM = com.Method<ICOMObject1C77>("РеквизитШапки", i + 1);
                var metadata = new AttributeMetadata1C77();
                metadata.Идентификатор = com.GetProperty<string>("Идентификатор");
                metadata.Комментарий = com.GetProperty<string>("Комментарий");
                metadata.Синоним = com.GetProperty<string>("Синоним");

                SetAttributeMetadata(rCOM, metadata);
                result.РеквизитШапки.Add(metadata.Идентификатор.ToLower(), metadata);
            }

            result.РеквизитТабличнойЧасти = new Dictionary<string, DocumentTableAttributeMetadata1C77>();
            count = (int)com.Method<double>("РеквизитТабличнойЧасти");
            for (int i = 0; i < count; i++)
            {
                var metadata = GetDocumentTableAttributeMetadata(com.Method<ICOMObject1C77>("РеквизитТабличнойЧасти", i + 1));
                result.РеквизитТабличнойЧасти.Add(metadata.Идентификатор.ToLower(), metadata);
            }


            return result;
        }
        static EnumMetadata1C77 GetEnumMetadata(ICOMObject1C77 com)
        {

            var result = new EnumMetadata1C77();
            result.Идентификатор = com.GetProperty<string>("Идентификатор");
            result.Комментарий = com.GetProperty<string>("Комментарий");
            result.Синоним = com.GetProperty<string>("Синоним");

            int count = (int)com.Method<double>("Значение");
            var values = new List<EnumValueMetadata1C77>();
            for (int i = 0; i < count; i++)
            {
                var enumCOM = com.Method<ICOMObject1C77>("Значение", i + 1);
                var enumValue = new EnumValueMetadata1C77 { ValueId = i + 1 };
                enumValue.Идентификатор = enumCOM.GetProperty<string>("Идентификатор");
                enumValue.Комментарий = enumCOM.GetProperty<string>("Комментарий");
                enumValue.Синоним = enumCOM.Method<string>("Представление"); // FUCK 1C
                values.Add(enumValue);
            }
            result.Values = values;
            return result;
        }
        public static string GetMetadataDescriptor(IGlobalContext global)
        {
            var com = global.GetProperty<ICOMObject1C77>(metadataToken);
            return $"{com.GetProperty<string>("Идентификатор")}-{com.GetProperty<string>("Комментарий")}-{com.GetProperty<string>("Синоним")}";
        }
   
        public static Dictionary<string,ConstantMetadata1C77> GetMetadataConstants(IGlobalContext global)
        {
            var result = new Dictionary<string, ConstantMetadata1C77>();
            var com = global.GetProperty<ICOMObject1C77>(metadataToken);
            var count = (int)com.Method<double>(constantToken);
            for (int i = 0; i < count; i++)
            {
                var metadata = GetConstantMetadata(com.Method<ICOMObject1C77>(constantToken, i + 1));
                result.Add(metadata.Идентификатор.ToLower(), metadata);
            }
            return result;

        }
   
        public static Dictionary<string,CatalogMetadata1C77> GetMetadataCatalogs(IGlobalContext global)
        {
            var result = new Dictionary<string, CatalogMetadata1C77>();
            var com = global.GetProperty<ICOMObject1C77>(metadataToken);
            var count = (int)com.Method<double>(catalogToken);
            for (int i = 0; i < count; i++)
            {
                try
                {
                    var metadata = GetCatalogMetadata(com.Method<ICOMObject1C77>(catalogToken, i + 1));
                    result.Add(metadata.Идентификатор.ToLower(), metadata);
                }
                catch (Exception e)
                {
                    throw new Exception($"Метаданные Справочник-{i}:{e.Message}");
                }

            }
            return result;
        }

        public static Dictionary<string, EnumMetadata1C77> GetMetadataEnums(IGlobalContext global)
        {
            var result = new Dictionary<string, EnumMetadata1C77>();
            var com = global.GetProperty<ICOMObject1C77>(metadataToken);
            var count = (int)com.Method<double>(enumToken);
            for (int i = 0; i < count; i++)
            {
                try
                {
                    var metadata = GetEnumMetadata(com.Method<ICOMObject1C77>(enumToken, i + 1));
                    result.Add(metadata.Идентификатор.ToLower(), metadata);
                }
                catch (Exception e)
                {
                    throw new Exception($"Метаданные Перечисление -{i}:{e.Message}");
                }

            }
            return result;
        }

        public static Dictionary<string,DocummentMetadata1C77> GetMetadataDocuments(IGlobalContext global)
        {
            var result = new Dictionary<string, DocummentMetadata1C77>();
            var com = global.GetProperty<ICOMObject1C77>(metadataToken);
            var count = (int)com.Method<double>(documentToken);
            for (int i = 0; i < count; i++)
            {
                try
                {
                    var metadata = GetDocumentMetadata(com.Method<ICOMObject1C77>(documentToken, i + 1));
                    result.Add(metadata.Идентификатор.ToLower(), metadata);
                }
                catch (Exception e)
                {
                    throw new Exception($"Метаданные Документи -{i}:{e.Message}");
                }

            }
            return result;
        }
        public static Dictionary<string, CommonDocummentAttributeMetadata1C77> GetMetadataCommonDocumentAttributes(IGlobalContext global)
        {
            var result = new Dictionary<string, CommonDocummentAttributeMetadata1C77>();
            var com = global.GetProperty<ICOMObject1C77>(metadataToken);
            var count = (int)com.Method<double>(commonDocumentAttributesToken);
            for (int i = 0; i < count; i++)
            {
                try
                {
                    var metadata = GetCommonDocummentAttributeMetadata(com.Method<ICOMObject1C77>(commonDocumentAttributesToken, i + 1));
                    result.Add(metadata.Идентификатор.ToLower(), metadata);
                }
                catch (Exception e)
                {
                    throw new Exception($"Метаданные Документи -{i}:{e.Message}");
                }

            }
            return result;
        }
        public static RootMetadata1C77 GetMetadata(IGlobalContext global)
        {
            var metadata = new RootMetadata1C77();
            var com = global.GetProperty<ICOMObject1C77>(metadataToken);
            metadata.Id = GetMetadataDescriptor(global);
            metadata.Идентификатор = com.GetProperty<string>("Идентификатор");
            metadata.Комментарий = com.GetProperty<string>("Комментарий");
            metadata.Синоним = com.GetProperty<string>("Синоним");
            metadata.Константы = GetMetadataConstants(global);
            metadata.Документы = GetMetadataDocuments(global);
            metadata.ОбщиеРеквизитыДокумента = GetMetadataCommonDocumentAttributes(global);
            metadata.Перечисления = GetMetadataEnums(global);
            metadata.Справочники = GetMetadataCatalogs(global);
            return metadata;
        }


    }
}
