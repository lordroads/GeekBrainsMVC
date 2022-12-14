using CombiningProducts.Enums;
using CombiningProducts.Factories;
using CombiningProducts.Models;

string stringJsonPerekrestok = @"{ ""ID"": 1, ""Name"": ""Name Product 1"" ,""Descroption"": ""Some text... 1"", ""Cost"": 150.00 }";
string pathToFileJsonPeterochka = @"D:\Projects\GeekBrainsMVC\Lesson-4\CombiningProducts\Data\product.txt";
string pathToApiJsonAshan = "https://fakerapi.it/api/v1/custom?_quantity=1&id=counter&title=word&description=text&price=number";

Product productPerekrestok = ProductFactory.GetProduct(stringJsonPerekrestok, SourceType.PEREKRESTOK)!;
Product productPeterochka = ProductFactory.GetProduct(pathToFileJsonPeterochka, SourceType.PETEROCHKA)!;
Product productAshan = ProductFactory.GetProduct(pathToApiJsonAshan, SourceType.ASHAN)!;

Console.WriteLine(productPerekrestok);
Console.WriteLine(productPeterochka);
Console.WriteLine(productAshan);
Console.ReadKey(true);
