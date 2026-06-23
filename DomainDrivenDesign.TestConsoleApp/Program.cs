using DomainDrivenDesign.Domain.Categories;
using DomainDrivenDesign.Domain.Shared;

Console.WriteLine("Hello, World!");

Name name = new("Bilgisayar");
Category category1 = Category.Create(name);
Category category2 = Category.Create(name);
category2.Id = category1.Id;

var res1 = category1 == category2;
var res2 = category2.Equals(category1);

var res3 = category1.GetType();

Console.WriteLine(res1);
Console.WriteLine(res2);
