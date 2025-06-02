namespace ClubeDaLeitura.Compartilhado;

public abstract class RepositorioBase
{
    private EntidadeBase[] registros = new EntidadeBase[100];
    private int proximoId;

    public virtual void Inserir(EntidadeBase entidade)
    {
        int indiceParaInserir = -1;

        entidade.id = proximoId;

        for (int i = 0; i < registros.Length; i++)
        {
            if (registros[i] == null)
            {
                indiceParaInserir = i;
                break;
            }
        }

        if (indiceParaInserir != -1)
        {
            registros[indiceParaInserir] = entidade;
            proximoId++;
        }
        else
            Console.WriteLine("Repositório cheio, não foi possível inserir!");
    }

    public virtual void Editar(int idSelecionado, EntidadeBase entidadeAtualizada)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            if (registros[i] != null && registros[i].id == idSelecionado)
            {
                entidadeAtualizada.id = idSelecionado;
                registros[i] = entidadeAtualizada;
                return;
            }
        }
    }

    public virtual bool Excluir(int idSelecionado)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            if (registros[i] != null && registros[i].id == idSelecionado)
            {
                registros[i] = null;
                return true;
            }
        }

        return false;
    }

    public virtual EntidadeBase SelecionarPorId(int idSelecionado)
    {
        foreach (EntidadeBase entidade in registros)
        {
            if (entidade != null && entidade.id == idSelecionado)
                return entidade;
        }

        return null;
    }

    public virtual EntidadeBase[] SelecionarTodos()
    {
        return registros;
    }
}