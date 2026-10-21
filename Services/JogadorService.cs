
using System.Text.Json;
using NEXO.DotNet.Models;

namespace NEXO.DotNet.Services;

public class JogadorService
{
    private readonly List<Jogador> jogadores;
    private readonly string caminhoArquivo;

    public JogadorService()
    {
        caminhoArquivo = Path.Combine(
            AppContext.BaseDirectory,
            "usuarios.json"
        );

        jogadores = CarregarJogadores();
    }

    private List<Jogador> CarregarJogadores()
    {
        if (!File.Exists(caminhoArquivo))
        {
            return new List<Jogador>();
        }

        try
        {
            string json = File.ReadAllText(caminhoArquivo);

            using var documento = JsonDocument.Parse(json);

            var jogadoresCarregados =
                JsonSerializer.Deserialize<List<Jogador>>(json)
                ?? new List<Jogador>();

            if (documento.RootElement.ValueKind == JsonValueKind.Array)
            {
                int indice = 0;

                foreach (var elemento in
                    documento.RootElement.EnumerateArray())
                {
                    if (!elemento.TryGetProperty("Ativo", out _))
                    {
                        jogadoresCarregados[indice].Ativo = true;
                    }

                    indice++;
                }
            }

            return jogadoresCarregados;
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                "O arquivo de jogadores contém dados inválidos.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException(
                "Erro ao ler o arquivo de jogadores.", ex);
        }
    }

    private void SalvarJogadores()
    {
        var opcoes = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(jogadores, opcoes);

        File.WriteAllText(caminhoArquivo, json);
    }

    public void Cadastrar(string nomeUsuario, string apelido)
    {
        if (string.IsNullOrWhiteSpace(nomeUsuario))
        {
            throw new ArgumentException(
                "O nome de usuário é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(apelido))
        {
            throw new ArgumentException(
                "O apelido é obrigatório.");
        }

        nomeUsuario = nomeUsuario.Trim();
        apelido = apelido.Trim();

        bool usuarioExiste = jogadores.Any(j =>
            string.Equals(
                j.NomeUsuario,
                nomeUsuario,
                StringComparison.OrdinalIgnoreCase));

        if (usuarioExiste)
        {
            throw new InvalidOperationException(
                "Este nome de usuário já está cadastrado.");
        }

        Jogador novoJogador = new Jogador
        {
            Id = jogadores.Count == 0
                ? 1
                : jogadores.Max(j => j.Id) + 1,
            NomeUsuario = nomeUsuario,
            Apelido = apelido,
            DataCadastro = DateTime.Now,
            Ativo = true
        };

        jogadores.Add(novoJogador);

        try
        {
            SalvarJogadores();
        }
        catch
        {
            jogadores.Remove(novoJogador);
            throw;
        }
    }

    public List<Jogador> ListarTodos()
    {
        return jogadores.ToList();
    }

    public List<Jogador> BuscarPorNome(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            throw new ArgumentException(
                "Informe um nome para realizar a busca.");
        }

        termo = termo.Trim();

        return jogadores
            .Where(j =>
                j.NomeUsuario.Contains(
                    termo, StringComparison.OrdinalIgnoreCase)
                ||
                j.Apelido.Contains(
                    termo, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
    
public void AlterarStatus(int id, bool ativo)
{
    Jogador? jogador = jogadores
        .FirstOrDefault(j => j.Id == id);

    if (jogador == null)
    {
        throw new InvalidOperationException(
            "Jogador não encontrado.");
    }

    if (jogador.Ativo == ativo)
    {
        throw new InvalidOperationException(
            ativo
                ? "O jogador já está ativo."
                : "O jogador já está inativo.");
    }

    bool statusAnterior = jogador.Ativo;

    jogador.Ativo = ativo;

    try
    {
        SalvarJogadores();
    }
    catch
    {
        jogador.Ativo = statusAnterior;
        throw;
    }
}

public void EditarApelido(int id, string novoApelido)
{
    if (string.IsNullOrWhiteSpace(novoApelido))
    {
        throw new ArgumentException(
            "O novo apelido é obrigatório.");
    }

    Jogador? jogador = jogadores
        .FirstOrDefault(j => j.Id == id);

    if (jogador == null)
    {
        throw new InvalidOperationException(
            "Jogador não encontrado.");
    }

    novoApelido = novoApelido.Trim();

    if (jogador.Apelido == novoApelido)
    {
        throw new InvalidOperationException(
            "O novo apelido deve ser diferente do atual.");
    }

    string apelidoAnterior = jogador.Apelido;

    jogador.Apelido = novoApelido;

    try
    {
        SalvarJogadores();
    }
    catch
    {
        jogador.Apelido = apelidoAnterior;
        throw;
    }
}


}
