using System;
using System.Collections.Generic;
using System.Linq;

namespace Crud
{
  class Program
  {
    static Dictionary<int, (string name, int quantity, decimal price)> produtos = new();
    static int id = 1;

    public static void Main(string[] args)
    {
      Console.Clear();
      MostrarMenu();
    }

    public static void MostrarMenu()
    {
      while (true)
      {
        Console.WriteLine("------- CONTROLE DE ESTOQUE -------");
        Console.WriteLine("Escolha uma opção: ");
        Console.WriteLine("[1] - ADICIONAR PRODUTO\n[2] - LISTAR PRODUTOS\n[5] - SAIR");
        string? opcao = Console.ReadLine();

        switch (opcao)
        {
          case "1":
            Console.Clear();
            CadastrarProduto();
            Console.WriteLine("\nAperte qualquer tecla para continuar");
            Console.ReadKey();
            break;
          case "2":
            Console.Clear();
            ListarProdutos();
            Console.WriteLine("\nAperte qualquer tecla para continuar");
            Console.ReadKey();
            break;

          case "5":
            return;

          default:
            Console.Clear();
            Console.WriteLine("Opção inválida");
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
            break;
        }
      }
    }

    public static void CadastrarProduto()
    {
      string? name;
      do
      {
        Console.Write("Digite o nome: ");
        name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
          Console.WriteLine("Nome inválido.");
          continue;
        }

        if (produtos.Values.Any(p => p.name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
          Console.WriteLine("Produto já existente, digite outro nome.");
          name = null;
        }

      } while (string.IsNullOrWhiteSpace(name));

      int quantity;
      bool validQuantity;
      do
      {
        Console.Write("Digite a quantidade: ");
        string? input = Console.ReadLine();
        validQuantity = int.TryParse(input, out quantity) && quantity >= 0;
        if (!validQuantity)
        {
          Console.WriteLine("Quantidade inválida. Tente novamente.");
        }
      } while (!validQuantity);

      decimal price;
      bool validPrice;
      do
      {
        Console.Write("Digite o preço: ");
        string? input = Console.ReadLine();
        validPrice = decimal.TryParse(input, out price) && price >= 0;
        if (!validPrice)
        {
          Console.WriteLine("Preço inválido. Tente novamente.");
        }
      } while (!validPrice);

      produtos.Add(id, (name!, quantity, price));
      Console.WriteLine("Produto cadastrado com sucesso!");
      Console.WriteLine($"\n{"ID",-5} {"NOME",-20} {"QUANT.",-10} {"PREÇO",10}");
      Console.WriteLine(new string('-', 50));
      Console.WriteLine($"{id,-5} {name,-20} {quantity,-10} {"R$ " + price.ToString("F2"),12}");


      id++;
    }
    public static void ListarProdutos()
    {
      Console.Clear();
      if (produtos.Count < 1)
      {
        Console.WriteLine("Seu estoque está vazio");
      }
      else
      {
        Console.WriteLine($"\n{"ID",-5} {"NOME",-20} {"QUANT.",-10} {"PREÇO",10}");
        Console.WriteLine(new string('-', 50));
        foreach (var p in produtos)
        {
          Console.WriteLine($"{p.Key,-5} {p.Value.name,-20} {p.Value.quantity,-10} {"R$ " + p.Value.price.ToString("F2"),12}");
        }
      }

      ;
    }
  }
}
