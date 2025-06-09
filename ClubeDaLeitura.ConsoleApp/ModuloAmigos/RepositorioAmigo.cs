using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimos;
namespace ClubeDaLeitura.ModuloAmigos;

public class RepositorioAmigo : RepositorioBase
{
    public bool ExisteDuplicado(string nome, string telefone)
    {
        EntidadeBase[] registros = SelecionarTodos();

        if (registros == null)
            return false;

        foreach (var entidade in registros)
        {
            if (entidade is Amigo amigo)
            {
                if (amigo.nome == nome && amigo.telefone == telefone)
                    return true;
            }
        }

        return false;
    }

    public bool TemEmprestimos(int id, Emprestimo[] todosEmprestimos)
    {
        Amigo amigo = (Amigo)SelecionarPorId(id);
        if (amigo == null)
            return false;

        Emprestimo[] emprestimosDoAmigo = amigo.ObterEmprestimos(todosEmprestimos);

        for (int i = 0; i < emprestimosDoAmigo.Length; i++)
        {
            if (emprestimosDoAmigo[i] != null)
                return true;
        }

        return false;
    }

}