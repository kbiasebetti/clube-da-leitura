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

    private Caixa SelecionarCaixaParaRevista()
    {
        Console.WriteLine("\n-- Seleção de Caixa para a Revista --");
        EntidadeBase[] caixasDisponiveis = repositorioCaixa.SelecionarTodos();
        bool encontrouAlgumaCaixa = false;

        Console.WriteLine("Caixas disponíveis:");
        Console.WriteLine($"{"ID",-5} | {"Etiqueta",-20} | {"Cor",-15} | {"Dias Empréstimo",-10}");
        Console.WriteLine(new string('-', 65));

        foreach (EntidadeBase entidadeCaixa in caixasDisponiveis)
        {
            if (entidadeCaixa is Caixa c)
            {
                Console.WriteLine($"{c.id,-5} | {c.etiqueta,-20} | {c.cor,-15} | {c.diasDeEmprestimo,-10}");
                encontrouAlgumaCaixa = true;
            }
        }
        Console.WriteLine(new string('-', 65));


        if (!encontrouAlgumaCaixa)
        {
            notificador.ApresentarMensagem("Nenhuma caixa cadastrada. É necessário cadastrar uma caixa antes de adicionar revistas.", TipoMensagem.Erro);
            return null;
        }

        int idCaixaSelecionada;
        Caixa caixaSelecionada = null;
        while (caixaSelecionada == null)
        {
            Console.Write("Digite o ID da Caixa para esta revista: ");
            string entrada = Console.ReadLine();
            if (int.TryParse(entrada, out idCaixaSelecionada))
            {
                caixaSelecionada = (Caixa)repositorioCaixa.SelecionarPorId(idCaixaSelecionada);
                if (caixaSelecionada == null)
                {
                    notificador.ApresentarMensagem("ID de Caixa inválido. Tente novamente.", TipoMensagem.Erro);
                }
            }
            else
            {
                notificador.ApresentarMensagem("Entrada inválida. Digite um número para o ID da Caixa.", TipoMensagem.Erro);
            }
        }
        return caixaSelecionada;
    }

    protected override EntidadeBase ObterRegistro()
    {
        Console.Write("Digite o título da revista (2-100 caracteres): ");
        string titulo = Console.ReadLine();

        int numeroEdicao = 0;
        while (numeroEdicao <= 0)
        {
            Console.Write("Digite o número da edição (positivo): ");
            string numEdicaoStr = Console.ReadLine();
            if (!int.TryParse(numEdicaoStr, out numeroEdicao) || numeroEdicao <= 0)
            {
                notificador.ApresentarMensagem("Número da edição inválido. Deve ser um inteiro positivo.", TipoMensagem.Erro);
                numeroEdicao = 0;
            }
        }

        int anoPublicacao = 0;
        while (anoPublicacao <= 0 || anoPublicacao > DateTime.Now.Year + 1)
        {
            Console.Write($"Digite o ano de publicação (ex: entre 1800 e {DateTime.Now.Year + 1}): ");
            string anoPubStr = Console.ReadLine();
            if (!int.TryParse(anoPubStr, out anoPublicacao) || anoPublicacao <= 0 || anoPublicacao > DateTime.Now.Year + 1)
            {
                notificador.ApresentarMensagem($"Ano de publicação inválido. Deve ser um ano válido.", TipoMensagem.Erro);
                anoPublicacao = 0;
            }
        }

        Caixa caixaSelecionada = SelecionarCaixaParaRevista();
        if (caixaSelecionada == null)
            return null;

        Revista revista = new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);

        string errosValidacao = revista.Validar();
        if (!string.IsNullOrWhiteSpace(errosValidacao))
        {
            notificador.ApresentarMensagem(errosValidacao, TipoMensagem.Erro);
            return ObterRegistro();
        }

        return revista;
    }

    public override void Inserir()
    {
        Console.Clear();
        MostrarCabecalho($"Inserindo {nomeEntidade}");

        Revista novaRevista = (Revista)ObterRegistro();

        if (novaRevista == null)
        {
            notificador.ApresentarMensagem($"Não foi possível obter os dados da revista. Verifique se existem caixas cadastradas.", TipoMensagem.Erro);
            return;
        }

        if (repositorioRevista.ExisteRevistaComMesmoTituloEdicao(novaRevista.titulo, novaRevista.numeroEdicao))
        {
            notificador.ApresentarMensagem("Já existe uma revista com este mesmo título e número de edição.", TipoMensagem.Erro);
            return;
        }

        repositorio.Inserir(novaRevista);
        notificador.ApresentarMensagem($"{nomeEntidade} inserida com sucesso!", TipoMensagem.Sucesso);
    }

    public override void Editar()
    {
        Console.Clear();
        MostrarCabecalho($"Editando {nomeEntidade}");
        VisualizarTodos(false);

        if (!ExistemRegistrosParaOperacao()) return;

        int idSelecionado = ObterIdParaOperacao($"Digite o ID da {nomeEntidade} que deseja editar: ");
        if (idSelecionado == -1) return;

        Console.WriteLine($"\nEditando a {nomeEntidade} com ID {idSelecionado}:");
        Revista revistaAtualizada = (Revista)ObterRegistro();

        if (revistaAtualizada == null)
        {
            notificador.ApresentarMensagem($"Não foi possível obter os dados para edição da revista.", TipoMensagem.Erro);
            return;
        }

        if (repositorioRevista.ExisteRevistaComMesmoTituloEdicao(revistaAtualizada.titulo, revistaAtualizada.numeroEdicao, idSelecionado))
        {
            notificador.ApresentarMensagem("Já existe outra revista com este mesmo título e número de edição.", TipoMensagem.Erro);
            return;
        }

        repositorio.Editar(idSelecionado, revistaAtualizada);
        notificador.ApresentarMensagem($"{nomeEntidade} editada com sucesso!", TipoMensagem.Sucesso);
    }

    private int ObterIdParaOperacao(string mensagemPrompt)
    {
        int idSelecionado;
        while (true)
        {
            Console.Write(mensagemPrompt);
            string entrada = Console.ReadLine();
            if (int.TryParse(entrada, out idSelecionado))
            {
                if (repositorio.SelecionarPorId(idSelecionado) != null)
                    return idSelecionado;
                else
                    notificador.ApresentarMensagem("ID inválido. Registro não encontrado.", TipoMensagem.Erro);
            }
            else
            {
                notificador.ApresentarMensagem("ID inválido, por favor digite um número inteiro.", TipoMensagem.Erro);
            }
        }
    }

    private bool ExistemRegistrosParaOperacao()
    {
        EntidadeBase[] todosOsRegistros = repositorio.SelecionarTodos();
        foreach (EntidadeBase reg in todosOsRegistros)
        {
            if (reg != null) return true;
        }

        return false;
    }
}