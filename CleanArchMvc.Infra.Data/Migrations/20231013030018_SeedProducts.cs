using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " + 
            "VALUES('Cardeno espiral','Caderno espiral 100 folhoas',7.45, 50,'caderno1.jp',1)");

            mb.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " + 
            "VALUES('Estojo escolar','Estojo escolar cinza',5.67, 70,'estojo1.jp',1)");

            mb.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " + 
            "VALUES('Borracha escolar','Borracha escolar pequena',3.40, 80,'borracha1.jp',1)");

            mb.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " + 
            "VALUES('Calculadora','Calculadora científica simples',24.90, 20,'calculadora1.jp',1)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }
    }
}
