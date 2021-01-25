Инсталяция:
   dotnet add package sabatex.V1C77

Добавить пространство имен:
   using sabatex.V1C77;

пример использованя:

         static void Main(string[] args)
        {
            // создаём строку соединения
            var connection = new sabatex.V1C77.Models.Connection
            {
                DataBasePath = @"C:\demo\1SBUKRD", // путь к базе
                PlatformType = sabatex.V1C77.Models.EPlatform1C.V77M, // платформа 1С77
                UserName = "Админов", // имя пользователя
                UserPass = "" // пароль или пустая строка
            };

            // соединяемся с 1С77
            // 
            using (var _1c77 = sabatex.V1C77.COMObject1C77.CreateConnection(connection))
            {
                // перебор всего справочника Контрагенты
                var contr = _1c77.GlobalContext.CreateObject("Справочник.Контрагенты");
                if (contr.Method<double>("ВыбратьЭлементы") == 1)
                {
                    while (contr.Method<double>("ПолучитьЭлемент")==1)
                    {
                        if (contr.Method<double>("ЭтоГруппа") == 1) continue;
                        var name = contr.GetProperty<string>("Наименование");
                        Console.WriteLine(name);
                    }
                }


            }
        }  
        

соответствие типов данных
  строка - string
  число  - double
  дата   - DataTime
  остальное - V1C77COMObject

  возможно использовать bool для замены double(0- false;1 - true)
  тогда выражение сократится к if (contr.Method<bool>("ВыбратьЭлементы"))

   