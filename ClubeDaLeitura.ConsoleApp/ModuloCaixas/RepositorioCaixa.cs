using ClubeDaLeitura.Compartilhado;
namespace ClubeDaLeitura.ModuloCaixas;

public class RepositorioCaixa : RepositorioBase
{
    public bool ExisteEtiquetaDuplicada(string etiqueta, int idParaExcluirDaVerificacao = -1)
    {
        EntidadeBase[] registros = SelecionarTodos();

        foreach (EntidadeBase entidade in registros)
        {
            if (entidade == null)
                continue;

            Caixa caixa = (Caixa)entidade;

            if (caixa.id != idParaExcluirDaVerificacao && caixa.etiqueta.Equals(etiqueta))
                return true;
        }
        return false;
    }
}