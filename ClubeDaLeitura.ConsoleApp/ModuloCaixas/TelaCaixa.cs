using ClubeDaLeitura.Compartilhado;
namespace ClubeDaLeitura.ModuloCaixas;

public class TelaCaixa : TelaBase
{
    private RepositorioCaixa repositorioCaixa;

    public TelaCaixa(RepositorioCaixa repositorioCaixa, Notificador notificador) : base("Caixa", repositorioCaixa, notificador)
    {
        this.repositorioCaixa = repositorioCaixa;
    }

    protected override EntidadeBase ObterRegistro()
    {
        Console.Write("Digite a etiqueta da caixa: ");
        string etiqueta = Console.ReadLine();

        Console.Write("Digite a cor da caixa: ");
        string cor = Console.ReadLine();

        Console.Write("Digite os dias de empréstimo: ");
        string entrada = Console.ReadLine();

        int diasEmprestimo = int.TryParse(entrada, out int valor) ? valor : 0;

        return new Caixa(etiqueta, cor, diasEmprestimo);
    }

    protected override bool ValidarAntesDeInserir(EntidadeBase entidade)
    {
        Caixa caixa = (Caixa)entidade;

        if (repositorioCaixa.ExisteEtiquetaDuplicada(caixa.etiqueta))
        {
            notificador.ApresentarMensagem("Já existe uma caixa com esta etiqueta. A inserção foi cancelada.", TipoMensagem.Erro);
            return false;
        }

        return true;
    }
}
