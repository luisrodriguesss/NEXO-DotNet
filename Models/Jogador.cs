
namespace NEXO.DotNet.Models;

public class Jogador
{
    public int Id { get; set; }

    public string NomeUsuario { get; set; } = string.Empty;

    public string Apelido { get; set; } = string.Empty;

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public bool Ativo { get; set; } = true;
}
