
using NEXO.DotNet.Services;

JogadorService jogadorService = new JogadorService();

bool executando = true;

while (executando)
{
    Console.WriteLine("\n===== NEXO - LABORATÓRIO DE DECISÕES =====");
    Console.WriteLine("1 - Cadastrar jogador");
    Console.WriteLine("2 - Sair");
    Console.Write("Escolha uma opção: ");

    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            try
            {
                Console.Write("\nNome de usuário: ");
                string nomeUsuario = Console.ReadLine() ?? "";

                Console.Write("Apelido: ");
                string apelido = Console.ReadLine() ?? "";

                jogadorService.Cadastrar(nomeUsuario, apelido);

                Console.WriteLine(
                    "\nJogador cadastrado com sucesso!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            break;

        case "2":
            Console.WriteLine("\nEncerrando o NEXO...");
            executando = false;
            break;

        default:
            Console.WriteLine("\nOpção inválida. Tente novamente.");
            break;
    }
}
