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

        Console.Write("Digite o nome do respons�vel: ");
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
            notificador.ApresentarMensagem("J� existe um amigo com o mesmo nome e telefone!", TipoMensagem.Erro);
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

            notificador.ApresentarMensagem("ID inv�lido, por favor digite um n�mero inteiro.", TipoMensagem.Erro);
        }

        Amigo amigo = (Amigo)repositorioAmigo.SelecionarPorId(idSelecionado);

        if (amigo == null)
        {
            notificador.ApresentarMensagem("Amigo n�o encontrado!", TipoMensagem.Erro);
            return;
        }

        // TODO: Verificar se o amigo possui empr�stimos vinculados antes de excluir.

        bool conseguiuExcluir = repositorioAmigo.Excluir(idSelecionado);

        if (conseguiuExcluir)
            notificador.ApresentarMensagem("Amigo exclu�do com sucesso!", TipoMensagem.Sucesso);
        else
            notificador.ApresentarMensagem("N�o foi poss�vel excluir o amigo.", TipoMensagem.Erro);
    }
}


// TODO: M�todo para visualizar empr�stimos do amigo - implementar quando o m�dulo de empr�stimos estiver pronto
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