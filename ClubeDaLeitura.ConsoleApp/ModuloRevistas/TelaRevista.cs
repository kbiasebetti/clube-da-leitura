using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloCaixas;
using System;
namespace ClubeDaLeitura.ModuloRevistas;

public class TelaRevista : TelaBase
{
    private RepositorioRevista repositorioRevista;
    private RepositorioCaixa repositorioCaixa;

    public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa, Notificador notificador) : base("Revista", repositorioRevista, notificador)
    {
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
    }

    public override void Inserir()
    {
        Console.Clear();
        MostrarCabecalho($"Inserindo {nomeEntidade}");

        Revista novaRevista = (Revista)ObterRegistro();

        if (novaRevista == null)
            return;

        if (!ValidarAntesDeInserir(novaRevista))
        {
            Console.WriteLine("Pressione ENTER para tentar novamente...");
            Console.ReadLine();
            return;
        }

        repositorioRevista.Inserir(novaRevista);

        Caixa caixaDaRevista = novaRevista.caixa;
        caixaDaRevista.AdicionarRevista(novaRevista);

        repositorioCaixa.Editar(caixaDaRevista.id, caixaDaRevista);

        notificador.ApresentarMensagem($"{nomeEntidade} inserida com sucesso!", TipoMensagem.Sucesso);
        Console.WriteLine("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    protected override EntidadeBase ObterRegistro()
    {
        EntidadeBase[] caixas = repositorioCaixa.SelecionarTodos();

        bool possuiCaixa = false;
        foreach (var c in caixas)
        {
            if (c != null)
            {
                possuiCaixa = true;
                break;
            }
        }

        if (!possuiCaixa)
        {
            notificador.ApresentarMensagem("Não há nenhuma caixa cadastrada. Cadastre uma caixa antes de adicionar uma revista.", TipoMensagem.Erro);
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
            return null;
        }

        Console.Write("Digite o título da revista: ");
        string titulo = Console.ReadLine();

        Console.Write("Digite o número da edição: ");
        int numeroEdicao = int.TryParse(Console.ReadLine(), out int numEd) ? numEd : 0;

        Console.Write("Digite o ano de publicação: ");
        int anoPublicacao = int.TryParse(Console.ReadLine(), out int ano) ? ano : 0;

        VisualizarCaixas();

        Console.Write("Digite o ID da caixa: ");
        int idCaixa = int.TryParse(Console.ReadLine(), out int id) ? id : -1;

        Caixa caixaSelecionada = (Caixa)repositorioCaixa.SelecionarPorId(idCaixa);

        if (caixaSelecionada == null)
        {
            notificador.ApresentarMensagem("Caixa não encontrada. Operação cancelada.", TipoMensagem.Erro);
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
            return null;
        }

        Revista revista = new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);

        return revista;
    }

    protected override bool ValidarAntesDeInserir(EntidadeBase entidade)
    {
        Revista revista = (Revista)entidade;

        string resultadoValidacao = revista.Validar();

        if (!string.IsNullOrEmpty(resultadoValidacao))
        {
            notificador.ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
            return false;
        }

        if (repositorioRevista.ExisteRevistaComMesmoTituloEdicao(revista.titulo, revista.numeroEdicao))
        {
            notificador.ApresentarMensagem("Já existe uma revista com o mesmo título e número de edição.", TipoMensagem.Erro);
            return false;
        }

        return true;
    }

    protected override bool ValidarAntesDeEditar(int id, EntidadeBase entidade)
    {
        Revista revista = (Revista)entidade;

        string resultadoValidacao = revista.Validar();

        if (!string.IsNullOrEmpty(resultadoValidacao))
        {
            notificador.ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
            return false;
        }

        if (repositorioRevista.ExisteRevistaComMesmoTituloEdicao(revista.titulo, revista.numeroEdicao, id))
        {
            notificador.ApresentarMensagem("Já existe uma revista com o mesmo título e número de edição.", TipoMensagem.Erro);
            return false;
        }

        return true;
    }

    protected override bool PodeExcluir(int id)
    {
        Revista revista = (Revista)repositorioRevista.SelecionarPorId(id);

        if (revista == null)
        {
            notificador.ApresentarMensagem("Revista não encontrada.", TipoMensagem.Erro);
            return false;
        }

        if (revista.status != StatusRevista.Disponivel)
        {
            notificador.ApresentarMensagem("Não é possível excluir uma revista que não está disponível.", TipoMensagem.Erro);
            return false;
        }

        return true;
    }

    public override void VisualizarTodos(bool exibirTitulo, bool pausarNoFinal)
    {
        if (!PodeVisualizar())
            return;

        if (exibirTitulo)
        {
            Console.Clear();
            MostrarCabecalho("Visualizando Revistas");
            Console.WriteLine($"{"ID",-5} | {"Título",-30} | {"Edição",-6} | {"Ano",-6} | {"Caixa",-15} | {"Status"}");
            Console.WriteLine(new string('-', 80));
        }

        EntidadeBase[] revistas = repositorioRevista.SelecionarTodos();

        bool encontrou = false;

        foreach (EntidadeBase entidade in revistas)
        {
            if (entidade == null)
                continue;

            Console.WriteLine(((Revista)entidade).DadosFormatados());
            encontrou = true;
        }

        if (!encontrou)
            notificador.ApresentarMensagem("Nenhuma revista cadastrada.", TipoMensagem.Erro);

        if (pausarNoFinal)
        {
            Console.WriteLine();
            Console.Write("Pressione ENTER para continuar...");
            Console.ReadLine();
        }
    }

    private void VisualizarCaixas()
    {
        Console.Clear();
        MostrarCabecalho("Caixas disponíveis");

        EntidadeBase[] caixas = repositorioCaixa.SelecionarTodos();

        Console.WriteLine($"{"ID",-5} | {"Etiqueta",-30} | {"Cor",-20} | {"Dias Empréstimo"}");
        Console.WriteLine(new string('-', 70));

        foreach (EntidadeBase entidade in caixas)
        {
            if (entidade == null)
                continue;

            Console.WriteLine(((Caixa)entidade).DadosFormatados());
        }

        Console.WriteLine();
    }
}