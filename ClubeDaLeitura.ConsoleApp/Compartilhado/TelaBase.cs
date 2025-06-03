namespace ClubeDaLeitura.Compartilhado;

public abstract class TelaBase
{
    protected string nomeEntidade;
    protected RepositorioBase repositorio;
    protected Notificador notificador;

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

        EntidadeBase novaEntidade;
        string errosValidacao;

        while (true)
        {
            novaEntidade = ObterRegistro();

            if (novaEntidade == null)
                return;

            errosValidacao = novaEntidade.Validar();

            if (string.IsNullOrEmpty(errosValidacao))
            {
                if (ValidarAntesDeInserir(novaEntidade))
                    break;
                else
                {
                    Console.WriteLine("Pressione Enter para tentar novamente...");
                    Console.ReadLine();
                    Console.Clear();
                    MostrarCabecalho($"Inserindo {nomeEntidade}");
                }
            }
            else
            {
                notificador.ApresentarMensagem(errosValidacao, TipoMensagem.Erro);
                Console.WriteLine("Pressione Enter para tentar novamente...");
                Console.ReadLine();
                Console.Clear();
                MostrarCabecalho($"Inserindo {nomeEntidade}");
            }
        }

        repositorio.Inserir(novaEntidade);

        notificador.ApresentarMensagem($"{nomeEntidade} inserido com sucesso!", TipoMensagem.Sucesso);
        Console.WriteLine("Pressione Enter para continuar...");
        Console.ReadLine();
    }

    public virtual void Editar()
    {
        Console.Clear();
        MostrarCabecalho($"Editando {nomeEntidade}");

        VisualizarTodos(exibirTitulo: false, pausarNoFinal: false);

        int idSelecionado;
        while (true)
        {
            Console.Write($"\nDigite o ID do {nomeEntidade} que deseja editar: ");
            string entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out idSelecionado))
            {
                notificador.ApresentarMensagem("ID inválido, por favor digite um número inteiro.", TipoMensagem.Erro);
                continue;
            }

            var entidadeExistente = repositorio.SelecionarPorId(idSelecionado);
            if (entidadeExistente == null)
            {
                notificador.ApresentarMensagem($"Nenhum {nomeEntidade} encontrado com o ID {idSelecionado}.", TipoMensagem.Erro);
                continue;
            }
            break;
        }

        EntidadeBase entidadeAtualizada;
        string errosValidacao;

        Console.WriteLine($"\nEditando dados do {nomeEntidade} ID: {idSelecionado}");
        while (true)
        {
            entidadeAtualizada = ObterRegistro();
            errosValidacao = entidadeAtualizada.Validar();

            if (string.IsNullOrEmpty(errosValidacao))
            {
                if (ValidarAntesDeEditar(idSelecionado, entidadeAtualizada))
                    break;
                else
                {
                    Console.WriteLine("Pressione Enter para tentar novamente...");
                    Console.ReadLine();
                    Console.Clear();
                    MostrarCabecalho($"Editando {nomeEntidade}");
                    Console.WriteLine($"\nEditando dados do {nomeEntidade} ID: {idSelecionado}");
                }
            }
            else
            {
                notificador.ApresentarMensagem(errosValidacao, TipoMensagem.Erro);
                Console.WriteLine("Pressione Enter para tentar novamente...");
                Console.ReadLine();
                Console.Clear();
                MostrarCabecalho($"Editando {nomeEntidade}");
                Console.WriteLine($"\nEditando dados do {nomeEntidade} ID: {idSelecionado}");
            }
        }

        repositorio.Editar(idSelecionado, entidadeAtualizada);

        notificador.ApresentarMensagem($"{nomeEntidade} editado com sucesso!", TipoMensagem.Sucesso);
        Console.WriteLine("Pressione Enter para continuar...");
        Console.ReadLine();
    }

    public virtual void Excluir()
    {
        Console.Clear();
        MostrarCabecalho($"Excluindo {nomeEntidade}");

        VisualizarTodos(exibirTitulo: false, pausarNoFinal: false); 

        int idSelecionado;
        while (true)
        {
            Console.Write($"\nDigite o ID do {nomeEntidade} que deseja excluir: ");
            string entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out idSelecionado))
            {
                notificador.ApresentarMensagem("ID inválido, por favor digite um número inteiro.", TipoMensagem.Erro);
                continue;
            }

            var entidadeExistente = repositorio.SelecionarPorId(idSelecionado);
            if (entidadeExistente == null)
            {
                notificador.ApresentarMensagem($"Nenhum {nomeEntidade} encontrado com o ID {idSelecionado}.", TipoMensagem.Erro);
                continue;
            }
            break;
        }

        if (!PodeExcluir(idSelecionado))
        {
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
            return;
        }


        bool conseguiuExcluir = repositorio.Excluir(idSelecionado);

        if (conseguiuExcluir)
            notificador.ApresentarMensagem($"{nomeEntidade} excluído com sucesso!", TipoMensagem.Sucesso);
        else
            notificador.ApresentarMensagem($"Não foi possível excluir. {nomeEntidade} não encontrado.", TipoMensagem.Erro);

        Console.WriteLine("Pressione Enter para continuar...");
        Console.ReadLine();
    }

    public virtual void VisualizarTodos(bool exibirTitulo = true, bool pausarNoFinal = true) 
    {
        if (!PodeVisualizar())
            return;

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

        if (pausarNoFinal)
        {
            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
        }
    }

    public void MostrarCabecalho(string titulo)
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine($"{titulo}");
        Console.WriteLine("-------------------------");
        Console.WriteLine();
    }

    protected virtual bool ValidarAntesDeInserir(EntidadeBase entidade) => true;
    protected virtual bool ValidarAntesDeEditar(int id, EntidadeBase entidade) => true;
    protected virtual bool PodeExcluir(int id) => true;
    protected virtual bool PodeVisualizar() => true;

    protected abstract EntidadeBase ObterRegistro();
}