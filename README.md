# SQLGit (Tentative)

SQLGit is an SQL table replicator which it is main function is to
compare and evaluate tables from different databases, also allowing to
transfer tables to another database

# License

This project is licensed under the MIT license and any further
modifications or usage of this project needs be according the mentioned
[license](https://github.com/WaterBlueNewWorld/SQLGit/blob/master/LICENSE).

# Project information

This project integrates two design patterns for data consistency
- [Generic Repository pattern](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)
- [Unit Of Work pattern](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

More examples of these patterns can be be even found in tutorials made
by Microsoft (Most of them focused in ASP.NET Core MVC)

- [Microsoft tutorial about MVC and design patterns](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application)
- ["DotNet Tutorials" tutorial about Unit Of Work](https://dotnettutorials.net/lesson/unit-of-work-csharp-mvc/)

# Current features

- The project is able to connect to a database and fetch the specified
  fields according to the binded model
- Most of the views here are placeholders created by Microsoft; views
  were made in the `master-piza` branch which can be seen
  [here](https://github.com/WaterBlueNewWorld/SQLGit/tree/master-piza)
- The project does not require to implement any Context for each new
  model, as the project contains a Generic DBContext for that purpose
  - To create a model for a new database context you only need to create
    a new .cs file in the `Models` folder
  - To access an specific connection to the database you nust call an
    object of `DBContext` (not to be confused with DbContext), and then
    pass the connection string of the database you wish to connect for
    example:

```
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

```
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
  database, do not refrain in trying to modify it is main structure
  - To create an object of UnitOfWork you can do the following:

```
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

