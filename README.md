# SQLGit (Tentative)

SQLGit is an SQL table replicator which it is main function is to
compare and evaluate tables from different databases, also allowing to
transfer tables to another database.

# License

This project is licensed under the MIT license and any further
modifications or usage of this project needs be according the mentioned
[license](https://github.com/WaterBlueNewWorld/SQLGit/blob/master/LICENSE).

# Project information

This project integrates two design patterns for data consistency.
- [Generic Repository pattern](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application).
- [Unit Of Work pattern](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application).

More examples of these patterns can be be even found in tutorials made
by Microsoft (Most of them focused in ASP.NET Core MVC).

- [Microsoft tutorial about MVC and design patterns](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application).
- ["DotNet Tutorials" tutorial about Unit Of Work](https://dotnettutorials.net/lesson/unit-of-work-csharp-mvc/).

# Current features

- The project is able to connect to a database and fetch the specified
  fields according to the binded model.
- Most of the views here are placeholders created by Microsoft; views
  were made in the `master-piza` branch which can be seen
  [here](https://github.com/WaterBlueNewWorld/SQLGit/tree/master-piza).
- The project does not require to implement any Context for each new
  model, as the project contains a Generic DBContext for that purpose
  - To create a model for a new database context you only need to create
    a new .cs file in the `Models` folder.
  - To access an specific connection to the database you must call an
    object of `DBContext` (not to be confused with DbContext), and then
    pass the connection string of the database you wish to connect for
    example:

```c#
namespace Dummy.Controllers
{
    public class DummyConnection : Controller
    {
        static string _connection = "Data Source=192.168.100.1;Initial Catalog=database_main;"
                                     +"Persist Security Info=True;User ID=user;Password=pa$$";
        private static DBContext db = new DBContext(_connection);
    }
}
```

- The project contains a generic repository for CRUD actions, is
  recommended to use the repository with the UnitOfWork class.
  - To create a repository first you need to create a DBContext,
    following the last example the code should be like this:

```c#
namespace Dummy.Controllers
{
    public class DummyConnection : Controller
    {
        static string _connection = "Data Source=192.168.100.1;Initial Catalog=database_main;"
                                    +"Persist Security Info=True;User ID=user;Password=pa$$";
        private static DBContext db = new DBContext(_connection);  
        public GenericRepository _repo = new GenericRepository(db);
    }
}
```

- Unit Of Work design pattern is declared similar on how a Generic
  Repository is made with a little few differences; the UnitOfWork
  class/pattern is a great and powerful tool in to managing the
  database, do not refrain in trying to modify it is main structure.
  - To create an object of UnitOfWork you can do the following:

```c#
namespace Dummy.Controllers
{
    public class DummyConnection : Controller
    {
        static string _connection = "Data Source=192.168.100.1;Initial Catalog=database_main;"
                                    +"Persist Security Info=True;User ID=user;Password=pa$$";
        public static GenericRepository<DummyModel> _repo;
        public static UnitOfWork _unit = new UnitOfWork<DBContext>();
    
        public DummyConnection(){
            _repo = new GenericRepository<DummyModel>(_unit);
        }
    }
}
```

###
###
# Spanish

# SQLGit (Tentativo)

SQLGit es un replicador de tablas SQL cuya función principal es
comparar y evaluar tablas de diferentes bases de datos, también permitiendo
transferencia de tablas.

# Licencia

Este proyecto está licenciado bajo la licencia MIT y cualquier otra modificacion o el uso de este proyecto necesita estar según la [licencia](https://github.com/WaterBlueNewWorld/SQLGit/blob/master/LICENSE).

# Información del proyecto

Este proyecto integra dos patrones de diseño para la coherencia de los datos.
- [Patrón de repositorio genérico](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application).
- [Patrón de unidad de trabajo](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application).

Más ejemplos de estos patrones se pueden encontrar incluso en tutoriales hechos por Microsoft (la mayoría de ellos se centraron en ASP.NET Core MVC).

- [Tutorial de Microsoft sobre MVC y patrones de diseño](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application).
- ["Tutoriales DotNet" sobre la unidad de trabajo](https://dotnettutorials.net/lesson/unit-of-work-csharp-mvc/).

# Características actuales

- El proyecto es capaz de conectarse a una base de datos y obtener los campos especificados según el modelo enlazado.
- La mayoría de las vistas aquí son templates hechos por Microsoft; las vistas se hicieron en la rama `master-piza` que se puede ver [aquí](https://github.com/WaterBlueNewWorld/SQLGit/tree/master-piza).
- El proyecto no requiere implementar ningún contexto para cada nuevo modelo, ya que el proyecto contiene un DBContext genérico para ese propósito
- Para crear un modelo para un nuevo contexto de base de datos, solo necesita crear un nuevo archivo .cs en la carpeta `Models`.
- Para acceder a una conexión específica a la base de datos debe llamar a un objeto de `DBContext` (no debe confundirse con DbContext), y luego pase la cadena de conexión de la base de datos a la que desea conectarse
ejemplo:

```c#
namespace Dummy.Controllers
{
    public class DummyConnection : Controller
    {
        static string _connection = "Data Source=192.168.100.1;Initial Catalog=database_main;"
                                     +"Persist Security Info=True;User ID=user;Password=pa$$";
        private static DBContext db = new DBContext(_connection);
    }
}
```

- El proyecto contiene un repositorio genérico para acciones CRUD, y para este es recomendable utilizar el repositorio con la clase UnitOfWork.
- Para crear un repositorio primero necesitas crear un DBContext, siguiendo el último ejemplo el código debería ser así:

```c#
namespace Dummy.Controllers
{
    public class DummyConnection : Controller
    {
        static string _connection = "Data Source=192.168.100.1;Initial Catalog=database_main;"
                                    +"Persist Security Info=True;User ID=user;Password=pa$$";
        private static DBContext db = new DBContext(_connection);  
        public GenericRepository _repo = new GenericRepository(db);
    }
}
```

- La clase Unidad de trabajo (UnitOfWork) se declara similar en cómo un repositorio generico (GenericRepository), pero con algunas diferencias; la clase / patrón UnitOfWork es una gran y poderosa herramienta en la gestión de la base de datos, no se abstengan en tratar de modificar su estructura principal.
- Para crear un objeto de UnitOfWork puede hacer lo siguiente:

```c#
namespace Dummy.Controllers
{
    public class DummyConnection : Controller
    {
        static string _connection = "Data Source=192.168.100.1;Initial Catalog=database_main;"
                                    +"Persist Security Info=True;User ID=user;Password=pa$$";
        public static GenericRepository<DummyModel> _repo;
        public static UnitOfWork _unit = new UnitOfWork<DBContext>();
    
        public DummyConnection(){
            _repo = new GenericRepository<DummyModel>(_unit);
        }
    }
}
```