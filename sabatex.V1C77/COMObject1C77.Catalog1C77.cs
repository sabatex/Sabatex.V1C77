using sabatex.V1C77.Models;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public partial class COMObject1C77:ICatalog1C77
    {
        string ICatalog1C77.Code { get => GetProperty<string>("Code").Trim(); set => SetProperty("Code", value); }
        bool ICatalog1C77.FindByCode(string code, double flag) => Method<bool>("FindByCode", code, flag);
        bool ICatalog1C77.FindByCode(string code) => Method<bool>("FindByCode", code, null);
        ICatalog1C77 ICatalog1C77.CurrentItem => Method<ICatalog1C77>("ТекущийЭлемент");
        ICatalog1C77 ICatalog1C77.Owner { get => GetProperty<ICatalog1C77>("Владелец"); set => SetProperty("Владелец", value); }
        ICatalog1C77 ICatalog1C77.Parent { get => GetProperty<ICatalog1C77>("Родитель"); set => SetProperty("Родитель", value); }
        string ICatalog1C77.Description {get => GetProperty<string>("Description");set => SetProperty("Description", value); }
        bool ICatalog1C77.DeleteMark => Method<bool>("ПометкаУдаления");
        void ICatalog1C77.ClearDeleteMark() => Method<object>("ClearDeleteMark");
        void ICatalog1C77.Delete(bool deleteFromDatabase) => Method<object>("Delete");
        void ICatalog1C77.Delete() => Method<object>("Delete");
        bool ICatalog1C77.IsFolder => Method<bool>("ЭтоГруппа");
        bool ICatalog1C77.SelectItems(bool hierarchically) => Method<bool>("ВыбратьЭлементы", hierarchically);
        bool ICatalog1C77.SelectItems() => Method<bool>("ВыбратьЭлементы", true);

        bool ICatalog1C77.NextItem(bool hierarchically ) => Method<bool>("ПолучитьЭлемент", hierarchically);
        bool ICatalog1C77.NextItem() => Method<bool>("ПолучитьЭлемент", true);

        bool ICatalog1C77.SelectGroup(bool withGroup) => Method<bool>("SelectGroup", withGroup);
        bool ICatalog1C77.SelectGroup() => Method<bool>("SelectGroup", true);

    }
}
