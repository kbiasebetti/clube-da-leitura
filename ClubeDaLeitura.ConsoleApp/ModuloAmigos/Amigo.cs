using ClubeDaLeitura.Compartilhado;
namespace ClubeDaLeitura.ModuloAmigos;

public class Amigo : EntidadeBase
{
    public string nome;
    public string nomeResponsavel;
    public string telefone;

    public Amigo(string nome, string nomeResponsavel, string telefone)
    {
        this.nome = nome;
        this.nomeResponsavel = nomeResponsavel;
        this.telefone = telefone;
    }


    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(nome) || nome.Length < 3 || nome.Length > 100)
            erros += "O nome deve ter entre 3 e 100 caracteres!\n";

        if (string.IsNullOrWhiteSpace(nomeResponsavel) || nomeResponsavel.Length < 3 || nomeResponsavel.Length > 100)
            erros += "O nome do responsável deve ter entre 3 e 100 caracteres!\n";

        if (string.IsNullOrWhiteSpace(telefone) || telefone.Length < 14 || telefone.Length > 15)
            erros += "O telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX!\n";
        else if (telefone[0] != '(' || telefone[3] != ')' || telefone[4] != ' ' || telefone[telefone.Length - 5] != '-')
            erros += "Telefone com formato inválido!\n";

        return erros;
    }

    public override void AtualizarRegistro(EntidadeBase registroAtualizado)
    {
        Amigo amigoAtualizado = (Amigo)registroAtualizado;

        this.nome = amigoAtualizado.nome;
        this.nomeResponsavel = amigoAtualizado.nomeResponsavel;
        this.telefone = amigoAtualizado.telefone;
    }

    // TODO: Implementar ObterEmprestimos() na hora de integrar com emprestimos

    public override string DadosFormatados()
    {
        return $"{id,-10} | {nome,-20} | {nomeResponsavel,-30} | {telefone,-15}";
    }
}