// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

DateTime BirthDate = new DateTime(1999, 10, 27);

Console.WriteLine(DateTime.Now - BirthDate);
