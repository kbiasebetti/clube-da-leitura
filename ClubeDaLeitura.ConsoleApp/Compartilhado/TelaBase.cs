namespace ClubeDaLeitura.Compartilhado;

public abstract class TelaBase
{
    private string nomeEntidade;
    private RepositorioBase repositorio;
    private Notificador notificador;

    public TelaBase(string nomeEntidade, RepositorioBase repositorio, Notificador notificador)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
        this.notificador = notificador;
    }

    public virtual void Inserir()
    {
        Console.Clear();
        MostrarCabecalho($"Inserindo {nomeEntidade}");

        EntidadeBase novaEntidade = ObterRegistro();

        repositorio.Inserir(novaEntidade);

        notificador.ApresentarMensagem($"{nomeEntidade} inserido com sucesso!", TipoMensagem.Sucesso);
    }

    public virtual void Editar()
    {
        int idSelecionado;

        Console.Clear();
        MostrarCabecalho($"Editando {nomeEntidade}");

        VisualizarTodos(false);

        while (true)
        {
            Console.Write($"\nDigite o ID do {nomeEntidade} que deseja editar: ");
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out idSelecionado))
                break;

            notificador.ApresentarMensagem("ID inválido, por favor digite um número inteiro.", TipoMensagem.Erro);
        }

        EntidadeBase entidadeAtualizada = ObterRegistro();
        repositorio.Editar(idSelecionado, entidadeAtualizada);

        notificador.ApresentarMensagem($"{nomeEntidade} editado com sucesso!", TipoMensagem.Sucesso);
    }

    public virtual void Excluir()
    {
        int idSelecionado;

        Console.Clear();
        MostrarCabecalho($"Excluindo {nomeEntidade}");

        VisualizarTodos(false);

        while (true)
        {
            Console.Write($"\nDigite o ID do {nomeEntidade} que deseja excluir: ");
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out idSelecionado))
                break;

            notificador.ApresentarMensagem("ID inválido, por favor digite um número inteiro.", TipoMensagem.Erro);
        }

        bool conseguiuExcluir = repositorio.Excluir(idSelecionado);

        if (conseguiuExcluir)
            notificador.ApresentarMensagem($"{nomeEntidade} excluído com sucesso!", TipoMensagem.Sucesso);
        else
            notificador.ApresentarMensagem($"Não foi possível excluir. {nomeEntidade} não encontrado.", TipoMensagem.Erro);
    }

    public virtual void VisualizarTodos(bool exibirTitulo = true)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            MostrarCabecalho($"Visualizando {nomeEntidade}");
        }

        EntidadeBase[] registros = repositorio.SelecionarTodos();

        bool encontrouAlgum = false;

        foreach (EntidadeBase entidade in registros)
        {
            if (entidade == null)
                continue;

            Console.WriteLine(entidade);
            encontrouAlgum = true;
        }

        if (!encontrouAlgum)
            notificador.ApresentarMensagem($"Nenhum {nomeEntidade} encontrado.", TipoMensagem.Erro);
    }

    private void MostrarCabecalho(string titulo)
    {
        Console.WriteLine(titulo);
        Console.WriteLine(new string('-', titulo.Length));
        Console.WriteLine();
    }

    protected abstract EntidadeBase ObterRegistro();
}