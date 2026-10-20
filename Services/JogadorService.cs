
using NEXO.DotNet.Models;

namespace NEXO.DotNet.Services;

public class JogadorService
{
    private readonly List<Jogador> jogadores = new();

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
            DataCadastro = DateTime.Now
        };

        jogadores.Add(novoJogador);
    }
}
