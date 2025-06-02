using ClubeDaLeitura.Compartilhado;
namespace ClubeDaLeitura.ModuloAmigos;

public class RepositorioAmigo : RepositorioBase
{
	public bool ExisteDuplicado(string Nome, string Telefone)
	{
		EntidadeBase[] registros = SelecionarTodos();

		if (registros == null)
			return false;

		foreach (var entidade in registros)
		{
			if (entidade is Amigo amigo)
			{
				if (amigo.nome == Nome && amigo.telefone == Telefone)
					return true;
			}
		}

		return false;
	}

	// TODO: Verificar se o amigo possui empréstimos vinculados
}
