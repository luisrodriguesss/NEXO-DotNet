
using NEXO.DotNet.Services;

JogadorService jogadorService = new JogadorService();

bool executando = true;

while (executando)
{
    Console.WriteLine("\n===== NEXO - LABORATÓRIO DE DECISÕES =====");
    Console.WriteLine("1 - Cadastrar jogador");
    Console.WriteLine("2 - Listar jogadores");
    Console.WriteLine("3 - Buscar jogador");
    Console.WriteLine("4 - Sair");
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
            var jogadores = jogadorService.ListarTodos();

            if (jogadores.Count == 0)
            {
                Console.WriteLine("\nNenhum jogador cadastrado.");
            }
            else
            {
                Console.WriteLine("\n===== JOGADORES =====");

                foreach (var jogador in jogadores)
                {
                    Console.WriteLine(
                        $"ID: {jogador.Id} | " +
                        $"Usuário: {jogador.NomeUsuario} | " +
                        $"Apelido: {jogador.Apelido} | " +
                        $"Cadastro: {jogador.DataCadastro:dd/MM/yyyy}");
                }
            }
            break;

        case "3":
            try
            {
                Console.Write("\nDigite o nome ou apelido: ");
                string termo = Console.ReadLine() ?? "";

                var resultados = jogadorService.BuscarPorNome(termo);

                if (resultados.Count == 0)
                {
                    Console.WriteLine("\nNenhum jogador encontrado.");
                }
                else
                {
                    Console.WriteLine("\n===== RESULTADOS =====");

                    foreach (var jogador in resultados)
                    {
                        Console.WriteLine(
                            $"ID: {jogador.Id} | " +
                            $"Usuário: {jogador.NomeUsuario} | " +
                            $"Apelido: {jogador.Apelido}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            break;

        case "4":
            Console.WriteLine("\nEncerrando o NEXO...");
            executando = false;
            break;

        default:
            Console.WriteLine("\nOpção inválida. Tente novamente.");
            break;
    }
}
