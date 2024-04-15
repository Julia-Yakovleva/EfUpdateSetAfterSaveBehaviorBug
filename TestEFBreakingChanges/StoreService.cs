using Dapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MySqlConnector;
using TestEFBreakingChanges.DataAccess;
using TestEFBreakingChanges.Models;

namespace TestEFBreakingChanges;

public class StoreService
{
    private readonly string _connectionString;

    public StoreService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool UpdateStore(int storeId)
    {
        using var context = new StoreContext(_connectionString);
        var store = context.Stores.First(s => s.StoreId == storeId);

        store.Update("new books");

        var newItems = new List<Item>
        {
            new("abc", "new title" )
        };
        store.Items = newItems;

        context.SaveChanges();

        return true;
    }

    public Store GetStore(int storeId)
    {
        using var context = new StoreContext(_connectionString);
        var store = context.Stores.First(s => s.StoreId == storeId);

        return store;
    }

    public bool CreateStore()
    {
        using var context = new StoreContext(_connectionString);

        var store = new Store(1, "books");
        store.Items = [new Item("abc", "title")];

        context.Stores.Add(store);

        context.SaveChanges();

        return true;
    }

    public void CreateSchema()
    {
        using var connection = new MySqlConnection(_connectionString);

        connection.Execute(ddl);
    }

    public void DropTables()
    {
        using var connection = new MySqlConnection(_connectionString);

        connection.Execute(dropTables);
    }

    private string dropTables =
        """
        DROP TABLE `test`.`item`;
        DROP TABLE `test`.`store`;
        """;

    private string ddl =
        """
        -- CREATE SCHEMA test;

        CREATE TABLE `test`.`store` (
          `storeId` int NOT NULL,
          `name` varchar(20),
          `createdAt` varchar(60),
          `updatedAt` varchar(60),
          PRIMARY KEY (`storeId`)
        ) ENGINE=InnoDB AUTO_INCREMENT=777132 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

        CREATE TABLE `test`.`item` (
          `storeId` int NOT NULL,
          `itemCode` varchar(3) NOT NULL,
          `name` varchar(20),
          `createdAt` varchar(60),
          `updatedAt` varchar(60),
          PRIMARY KEY (`storeId`, `itemCode`),
          CONSTRAINT `item_store_fk` FOREIGN KEY (`storeId`) REFERENCES `test`.`store` (`storeId`)
        ) ENGINE=InnoDB AUTO_INCREMENT=777132 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
        """;
}
