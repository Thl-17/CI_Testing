using Microsoft.AspNetCore.Components.Sections;
using SWP391.EventFlowerExchange.Domain.ObjectValues;
using SWP391.EventFlowerExchange.Infrastructure;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductRepository _repo = new ProductRepository();
            var check = _repo.RemoveProductAsync(new Product() { ProductId = 7 });
            Console.WriteLine(check);
        }
    }
}
