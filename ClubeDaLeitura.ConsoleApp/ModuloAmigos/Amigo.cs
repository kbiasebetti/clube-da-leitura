using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimos;
namespace ClubeDaLeitura.ModuloAmigos;

public class Amigo : EntidadeBase
{
    public string nome;
    public string nomeResponsavel;
    public string telefone;

    private Emprestimo[] emprestimos = new Emprestimo[100];
    private int quantidadeEmprestimos;

    public Amigo(string nome, string nomeResponsavel, string telefone)
    {
        this.nome = nome;
        this.nomeResponsavel = nomeResponsavel;
        this.telefone = telefone;
    }

    public void RegistrarEmprestimo(Emprestimo emprestimo)
    {
        if (quantidadeEmprestimos < emprestimos.Length)
        {
            emprestimos[quantidadeEmprestimos] = emprestimo;
            quantidadeEmprestimos++;
        }
    }

    public Emprestimo[] ObterEmprestimos(Emprestimo[] todosEmprestimos)
    {
        Emprestimo[] emprestimosDoAmigo = new Emprestimo[todosEmprestimos.Length];
        int index = 0;

        for (int i = 0; i < todosEmprestimos.Length; i++)
        {
            if (todosEmprestimos[i] != null && todosEmprestimos[i].amigo.id == this.id)
            {
                emprestimosDoAmigo[index] = todosEmprestimos[i];
                index++;
            }
        }

        return emprestimosDoAmigo;
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

    public override string DadosFormatados()
    {
        return $"{id,-10} | {nome,-20} | {nomeResponsavel,-30} | {telefone,-15}";
    }
}