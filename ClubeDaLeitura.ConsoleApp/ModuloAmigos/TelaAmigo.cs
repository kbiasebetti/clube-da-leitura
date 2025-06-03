using ClubeDaLeitura.Compartilhado;
namespace ClubeDaLeitura.ModuloAmigos;

public class TelaAmigo : TelaBase
{
    private RepositorioAmigo repositorioAmigo;

    public TelaAmigo(RepositorioAmigo repositorioAmigo, Notificador notificador) : base("Amigo", repositorioAmigo, notificador)
    {
        this.repositorioAmigo = repositorioAmigo;
    }

    protected override EntidadeBase ObterRegistro()
    {
        Console.Write("Digite o nome do amigo: ");
        string nome = Console.ReadLine();

        Console.Write("Digite o nome do responsável: ");
        string nomeResponsavel = Console.ReadLine();

        Console.Write("Digite o telefone do amigo: ");
        string telefone = Console.ReadLine();

        return new Amigo(nome, nomeResponsavel, telefone);
    }

    protected override bool ValidarAntesDeInserir(EntidadeBase entidade)
    {
        Amigo amigo = (Amigo)entidade;

        if (repositorioAmigo.ExisteDuplicado(amigo.nome, amigo.telefone))
        {
            notificador.ApresentarMensagem("Já existe um amigo com esse nome e telefone.", TipoMensagem.Erro);
            return false;
        }

        return true;
    }

    protected override bool PodeExcluir(int id)
    {
        Amigo amigo = (Amigo)repositorio.SelecionarPorId(id);

        if (amigo == null)
        {
            notificador.ApresentarMensagem("Amigo não encontrado.", TipoMensagem.Erro);
            return false;
        }

        // TODO: Verificar se o amigo possui empréstimos vinculados antes de excluir.
        return true;
    }
}

// TODO: Método para visualizar empréstimos do amigo - implementar quando o módulo de empréstimos estiver prontoAdd commentMore actions
/*
public void VisualizarEmprestimos()
{
VisualizarTodos(false)

int idSelecionado

while (true)
{
    Console.Write($"\nDigite o ID do amigo para visualizar os empréstimos: ")
    string entrada = Console.ReadLine()

    if (int.TryParse(entrada, out idSelecionado))
        break

    notificador.ApresentarMensagem("ID inválido, por favor digite um número inteiro.", TipoMensagem.Erro)
}

Amigo amigo = (Amigo)repositorioAmigo.SelecionarPorId(idSelecionado)

if (amigo == null)
{
    notificador.ApresentarMensagem("Amigo não encontrado!", TipoMensagem.Erro)
    return
}

// Aqui chamar método do amigo para obter empréstimos e mostrar
}
*/