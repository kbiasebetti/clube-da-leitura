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

        Console.Write("Digite o nome do respons�vel: ");
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
            notificador.ApresentarMensagem("J� existe um amigo com esse nome e telefone.", TipoMensagem.Erro);
            return false;
        }

        return true;
    }

    protected override bool PodeExcluir(int id)
    {
        Amigo amigo = (Amigo)repositorio.SelecionarPorId(id);

        if (amigo == null)
        {
            notificador.ApresentarMensagem("Amigo n�o encontrado.", TipoMensagem.Erro);
            return false;
        }

        // TODO: Verificar se o amigo possui empr�stimos vinculados antes de excluir.
        return true;
    }
}

// TODO: M�todo para visualizar empr�stimos do amigo - implementar quando o m�dulo de empr�stimos estiver prontoAdd commentMore actions
/*
public void VisualizarEmprestimos()
{
VisualizarTodos(false)

int idSelecionado

while (true)
{
    Console.Write($"\nDigite o ID do amigo para visualizar os empr�stimos: ")
    string entrada = Console.ReadLine()

    if (int.TryParse(entrada, out idSelecionado))
        break

    notificador.ApresentarMensagem("ID inv�lido, por favor digite um n�mero inteiro.", TipoMensagem.Erro)
}

Amigo amigo = (Amigo)repositorioAmigo.SelecionarPorId(idSelecionado)

if (amigo == null)
{
    notificador.ApresentarMensagem("Amigo n�o encontrado!", TipoMensagem.Erro)
    return
}

// Aqui chamar m�todo do amigo para obter empr�stimos e mostrar
}
*/