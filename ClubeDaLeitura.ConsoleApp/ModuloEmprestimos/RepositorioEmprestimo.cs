using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigos;
namespace ClubeDaLeitura.ModuloEmprestimos;

public class RepositorioEmprestimo : RepositorioBase
{
    public Emprestimo[] SelecionarTodosEmprestimos()
    {
        EntidadeBase[] registros = SelecionarTodos();

        int count = 0;
        for (int i = 0; i < registros.Length; i++)
            if (registros[i] != null)
                count++;

        Emprestimo[] emprestimos = new Emprestimo[count];
        int index = 0;

        for (int i = 0; i < registros.Length; i++)
        {
            if (registros[i] != null)
            {
                emprestimos[index] = (Emprestimo)registros[i];
                index++;
            }
        }

        return emprestimos;
    }

    public Emprestimo SelecionarEmprestimoPorId(int id)
    {
        return (Emprestimo)SelecionarPorId(id);
    }

    public bool PossuiEmprestimoAtivo(Amigo amigo)
    {
        Emprestimo[] emprestimos = SelecionarTodosEmprestimos();

        for (int i = 0; i < emprestimos.Length; i++)
        {
            if (emprestimos[i] != null &&
                emprestimos[i].amigo == amigo &&
                emprestimos[i].status == StatusEmprestimo.Aberto)
                return true;
        }

        return false;
    }
}