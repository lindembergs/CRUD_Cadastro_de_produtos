using System;
using System.Collections.Generic;
using System.Linq;

namespace Crud;

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
      Console.WriteLine("[1] - ADICIONAR PRODUTO\n[2] - LISTAR PRODUTOS\n[3] - ATUALIZAR PRODUTO\n[5] - SAIR");
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
        case "3":
          Console.Clear();
          AtualizarProduto();
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
    // Console.Clear();
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
  public static void AtualizarProduto()
  {
    if (produtos.Count < 1)
    {
      Console.WriteLine("Sua lista está vazia!");
      return;
    }

    Console.WriteLine("-------- DIGITE O ID DO PRODUTO A SER ATUALIZADO --------");
    ListarProdutos();
    string? input = Console.ReadLine();
    int idParaAtualizar;
    int.TryParse(input, out idParaAtualizar);

    if (!produtos.ContainsKey(idParaAtualizar))
    {
      Console.WriteLine("O produto com o ID digitado não existe!");
      return;
    }

    var produto = produtos[idParaAtualizar];
    bool isUpdated = false;

    Console.WriteLine("Atualizar nome? S/N");
    string? editName = Console.ReadLine()?.ToLower();
    if (editName == "s")
    {
      Console.WriteLine("Digite o novo nome");
      string? newName = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(newName))
      {
        Console.WriteLine("Nome inválido.");
        return;
      }

      if (produtos.Values.Any(p => p.name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
      {
        Console.WriteLine("Nome já existente na lista.");
        return;
      }

      ExibirProduto(idParaAtualizar, "Produto antes da atualização:");
      produto.name = newName!;
      isUpdated = true;
    }

    Console.WriteLine("Atualizar quantidade? S/N");
    string? editQuantity = Console.ReadLine()?.ToLower();
    if (editQuantity == "s")
    {
      Console.WriteLine("Digite a nova quantidade");
      string? quantityInput = Console.ReadLine();
      int newQuantity;

      if (int.TryParse(quantityInput, out newQuantity) && newQuantity >= 0)
      {
        ExibirProduto(idParaAtualizar, "Produto antes da atualização:");
        produto.quantity = newQuantity;
        isUpdated = true;
      }
      else
      {
        Console.WriteLine("Quantidade inválida.");
        return;
      }
    }
    Console.WriteLine("Atualizar o preço? S/N");
    string? editPrice = Console.ReadLine()?.ToLower();
    if (editPrice == "s")
    {
      Console.WriteLine("DIGITE O NOVO PREÇO");
      string? priceInput = Console.ReadLine();
      decimal newPrice;
      decimal.TryParse(priceInput, out newPrice);
      if (newPrice < 0)
      {
        Console.WriteLine("Não é possível editar com valor negativo");
        return;
      }
      produto.price = newPrice;
      isUpdated = true;
    }
    if (isUpdated)
    {
      produtos[idParaAtualizar] = (produto.name, produto.quantity, produto.price);
      Console.WriteLine("Produto atualizado com sucesso!");
      ExibirProduto(idParaAtualizar, "Produto após a atualização:");
    }
    else
    {
      Console.WriteLine("Nenhuma atualização realizada.");
    }


  }

  public static void ExibirProduto(int id, string title)
  {
    if (!produtos.ContainsKey(id))
    {
      Console.WriteLine($"Produto com o id {id} não existe ");
    }
    var p = produtos[id];
    Console.WriteLine($"\n{title}");
    Console.WriteLine($"\n{"ID",-5} {"NOME",-20} {"QUANT.",-10} {"PREÇO",10}");
    Console.WriteLine(new string('-', 50));
    Console.WriteLine($"{id,-5} {p.name,-20} {p.quantity,-10} {"R$ " + p.price.ToString("F2"),12}");
  }
}

