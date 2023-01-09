using Microsoft.Data.SqlClient;
using System.Text;

SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

builder.DataSource = @".\SQLEXPRESS";
//builder.UserID = "<your_username>";
//builder.Password = "<your_password>";
// 範例資料庫: AdventureWorksLT2019
// https://learn.microsoft.com/zh-tw/sql/samples/adventureworks-install-configure
builder.InitialCatalog = "AdventureWorksLT2019";
builder.IntegratedSecurity = true;
builder.MultipleActiveResultSets = true;
builder.TrustServerCertificate = true;

using SqlConnection conn = new SqlConnection(builder.ConnectionString);

var sql = "SELECT TOP (3) ProductID, Name, ProductNumber FROM [SalesLT].[Product] FOR JSON PATH";
using var cmd = new SqlCommand(sql, conn);

var jsonResult = new StringBuilder();

conn.Open();

var reader = cmd.ExecuteReader();
if (!reader.HasRows)
{
    jsonResult.Append("[]");
}
else
{
    while (reader.Read())
    {
        jsonResult.Append(reader.GetValue(0).ToString());
    }
}

Console.WriteLine(jsonResult.ToString());
