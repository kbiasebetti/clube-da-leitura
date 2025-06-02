using ClubeDaLeitura.Compartilhado;
namespace ClubeDaLeitura.ModuloRevistas;

public class RepositorioRevista : RepositorioBase
{
	public bool ExisteRevistaComMesmoTituloEdicao(string titulo, int numeroEdicao, int idExcluidoDaVerificacao = -1)
	{
		EntidadeBase[] registros = SelecionarTodos();

		foreach (EntidadeBase entidade in registros)
		{
			if (entidade == null)
				continue;

			Revista revista = (Revista)entidade;

			if (revista.id != idExcluidoDaVerificacao &&
				revista.titulo.Equals(titulo, System.StringComparison.OrdinalIgnoreCase) && // Considerar case-insensitivity para título
				revista.numeroEdicao == numeroEdicao)
			{
				return true;
			}
		}
		return false;
	}
}