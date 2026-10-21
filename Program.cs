
using NEXO.DotNet.Services;

JogadorService jogadorService;

try
{
    jogadorService = new JogadorService();
}
catch (Exception ex) when (
    ex is InvalidOperationException ||
    ex is UnauthorizedAccessException)
{
    Console.WriteLine(
        $"\nErro ao carregar os jogadores: {ex.Message}");

    Console.WriteLine(
        "Verifique o arquivo usuarios.json e tente novamente.");

    return;
}

bool executando = true;

while (executando)
{
    Console.WriteLine("\n===== NEXO - LABORATÓRIO DE DECISÕES =====");
    Console.WriteLine("1 - Cadastrar jogador");
    Console.WriteLine("2 - Listar jogadores");
    Console.WriteLine("3 - Buscar jogador");
    Console.WriteLine("4 - Alterar status do jogador");
    Console.WriteLine("5 - Sair");
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
            catch (IOException)
            {
                Console.WriteLine(
                    "\nErro ao salvar o arquivo de jogadores.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine(
                    "\nSem permissão para salvar os jogadores.");
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
                        $"Cadastro: {jogador.DataCadastro:dd/MM/yyyy} | " +
                        $"Status: {(jogador.Ativo ? "Ativo" : "Inativo")}");
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
                            $"Apelido: {jogador.Apelido} | " +
                            $"Status: {(jogador.Ativo ? "Ativo" : "Inativo")}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            break;

        case "4":
            try
            {
                Console.Write("\nDigite o ID do jogador: ");
                string entradaId = Console.ReadLine() ?? "";

                if (!int.TryParse(entradaId, out int id) || id <= 0)
                {
                    Console.WriteLine("\nID inválido.");
                    break;
                }

                Console.WriteLine("\n1 - Ativar jogador");
                Console.WriteLine("2 - Desativar jogador");
                Console.Write("Escolha uma opção: ");

                string? escolha = Console.ReadLine();

                if (escolha != "1" && escolha != "2")
                {
                    Console.WriteLine("\nOpção inválida.");
                    break;
                }

                bool ativo = escolha == "1";

                jogadorService.AlterarStatus(id, ativo);

                Console.WriteLine(
                    ativo
                        ? "\nJogador ativado com sucesso!"
                        : "\nJogador desativado com sucesso!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
            catch (IOException)
            {
                Console.WriteLine("\nErro ao salvar os dados.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("\nSem permissão para salvar os dados.");
            }
            break;

        case "5":
            Console.WriteLine("\nEncerrando o NEXO...");
            executando = false;
            break;

        default:
            Console.WriteLine("\nOpção inválida. Tente novamente.");
            break;
    }
}
