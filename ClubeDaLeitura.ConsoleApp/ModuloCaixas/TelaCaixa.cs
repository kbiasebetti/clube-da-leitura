using ClubeDaLeitura.Compartilhado;
namespace ClubeDaLeitura.ModuloCaixas;

public class TelaCaixa : TelaBase
{
    private RepositorioCaixa repositorioCaixa;
    private Notificador notificador;

    public TelaCaixa(RepositorioCaixa repositorioCaixa, Notificador notificador) : base("Caixa", repositorioCaixa, notificador)
    {
        this.repositorioCaixa = repositorioCaixa;
        this.notificador = notificador;
    }

    protected override EntidadeBase ObterRegistro()
    {
        Console.Write("Digite a etiqueta da caixa: ");
        string etiqueta = Console.ReadLine();

        Console.Write("Digite a cor da caixa: ");
        string cor = Console.ReadLine();

        Console.Write("Digite os dias de empréstimo (padrão: 7): ");
        string entrada = Console.ReadLine();

        int diasEmprestimo;
        if (string.IsNullOrWhiteSpace(entrada))
        {
            diasEmprestimo = 7;
        }
        else
        {
            diasEmprestimo = int.TryParse(entrada, out int valor) ? valor : 0;
        }

        return new Caixa(etiqueta, cor, diasEmprestimo);
    }

    protected override bool ValidarAntesDeInserir(EntidadeBase entidade)
    {
        Caixa caixa = (Caixa)entidade;
        var repositorioCaixa = (RepositorioCaixa)repositorio;

        if (repositorioCaixa.ExisteEtiquetaDuplicada(caixa.etiqueta))
        {
            notificador.ApresentarMensagem("Já existe uma caixa com esta etiqueta.", TipoMensagem.Erro);
            return false;
        }
        return true;
    }

    protected override bool ValidarAntesDeEditar(int id, EntidadeBase entidade)
    {
        Caixa caixa = (Caixa)entidade;
        var repositorioCaixa = (RepositorioCaixa)repositorio;

        if (repositorioCaixa.ExisteEtiquetaDuplicada(caixa.etiqueta, id))
        {
            notificador.ApresentarMensagem("Já existe outra caixa com esta etiqueta.", TipoMensagem.Erro);
            return false;
        }
        return true;
    }

    protected override bool PodeExcluir(int id)
    {
        Caixa caixa = (Caixa)repositorio.SelecionarPorId(id);

        if (caixa.revistas.Length > 0)
        {
            notificador.ApresentarMensagem("Não é possível excluir esta caixa, pois ela possui revistas vinculadas.", TipoMensagem.Erro);
            return false;
        }
        return true;
    }
}