using ClubeDaLeitura.Compartilhado;
namespace ClubeDaLeitura.ModuloAmigos;

public class TelaAmigo : TelaBase
{
    private RepositorioAmigo repositorioAmigo;

    public TelaAmigo(RepositorioAmigo repositorio, Notificador notificador) : base("Amigo", repositorio, notificador)
    {
        this.repositorioAmigo = repositorio;
        this.notificador = notificador;
    }

    protected override EntidadeBase ObterRegistro()
    {
        Console.Write("Digite o nome do amigo: ");
        string nome = Console.ReadLine();

        Console.Write("Digite o nome do responsável: ");
        string nomeResponsavel = Console.ReadLine();

        Console.Write("Digite o telefone ((XX) XXXX-XXXX ou (XX) XXXXX-XXXX): ");
        string telefone = Console.ReadLine();

        Amigo amigo = new Amigo(nome, nomeResponsavel, telefone);

        string erros = amigo.Validar();

        if (!string.IsNullOrWhiteSpace(erros))
        {
            notificador.ApresentarMensagem(erros, TipoMensagem.Erro);
            return ObterRegistro();
        }

        if (repositorioAmigo.ExisteDuplicado(nome, telefone))
        {
            notificador.ApresentarMensagem("Já existe um amigo com o mesmo nome e telefone!", TipoMensagem.Erro);
            return ObterRegistro();
        }

        return amigo;
    }

    public override void Excluir()
    {
        VisualizarTodos(false);

        int idSelecionado;

        while (true)
        {
            Console.Write($"\nDigite o ID do amigo que deseja excluir: ");
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out idSelecionado))
                break;

            notificador.ApresentarMensagem("ID inválido, por favor digite um número inteiro.", TipoMensagem.Erro);
        }

        Amigo amigo = (Amigo)repositorioAmigo.SelecionarPorId(idSelecionado);

        if (amigo == null)
        {
            notificador.ApresentarMensagem("Amigo não encontrado!", TipoMensagem.Erro);
            return;
        }

        // TODO: Verificar se o amigo possui empréstimos vinculados antes de excluir.

        bool conseguiuExcluir = repositorioAmigo.Excluir(idSelecionado);

        if (conseguiuExcluir)
            notificador.ApresentarMensagem("Amigo excluído com sucesso!", TipoMensagem.Sucesso);
        else
            notificador.ApresentarMensagem("Não foi possível excluir o amigo.", TipoMensagem.Erro);
    }
}


// TODO: Método para visualizar empréstimos do amigo - implementar quando o módulo de empréstimos estiver pronto
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