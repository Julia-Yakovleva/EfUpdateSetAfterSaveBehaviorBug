using TestEFBreakingChanges;

var connectionString = "Server=localhost;Port=3306;User ID=bob;Password=pass;Database=test;";

var service = new StoreService(connectionString);

Create();

//Update();

Console.WriteLine("Hello, World!");

void Create()
{
    service.DropTables();

    service.CreateSchema();

    service.CreateStore();

    var s1v1 = service.GetStore(1);
}

void Update()
{
    service.UpdateStore(1);

    var s1v2 = service.GetStore(1);
}
