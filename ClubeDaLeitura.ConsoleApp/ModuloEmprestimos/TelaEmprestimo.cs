using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigos;
using ClubeDaLeitura.ModuloRevistas;
using System; 
namespace ClubeDaLeitura.ModuloEmprestimos;

public class TelaEmprestimo : TelaBase
{
    private RepositorioAmigo repositorioAmigo;
    private RepositorioRevista repositorioRevista;

    public TelaEmprestimo(RepositorioEmprestimo repositorio, Notificador notificador, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista) : base("Empr�stimo", repositorio, notificador)
    {
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
    }

    protected override EntidadeBase ObterRegistro()
    {
        EntidadeBase[] registrosAmigo = repositorioAmigo.SelecionarTodos();
        bool possuiAmigo = false;
        foreach (var a in registrosAmigo)
        {
            if (a != null)
            {
                possuiAmigo = true;
                break;
            }
        }

        if (!possuiAmigo)
        {
            notificador.ApresentarMensagem("N�o h� nenhum amigo cadastrado. Cadastre um amigo antes de registrar um empr�stimo.", TipoMensagem.Erro);
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
            return null;
        }

        EntidadeBase[] registrosRevista = repositorioRevista.SelecionarTodos();
        bool possuiRevistaCadastrada = false;
        foreach (var r in registrosRevista)
        {
            if (r != null)
            {
                possuiRevistaCadastrada = true;
                break;
            }
        }

        if (!possuiRevistaCadastrada)
        {
            notificador.ApresentarMensagem("N�o h� nenhuma revista cadastrada. Cadastre uma revista antes de registrar um empr�stimo.", TipoMensagem.Erro);
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
            return null;
        }

        Console.WriteLine("Amigos dispon�veis:\n");
        foreach (EntidadeBase amigoEntidade in registrosAmigo)
        {
            if (amigoEntidade != null) 
                Console.WriteLine(amigoEntidade);
        }

        Console.Write("\nDigite o ID do amigo: ");
        int idAmigo = int.TryParse(Console.ReadLine(), out int idAmg) ? idAmg : -1; //
        Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarPorId(idAmigo);

        if (amigoSelecionado == null)
        {
            notificador.ApresentarMensagem("Amigo n�o encontrado!", TipoMensagem.Erro);
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
            return null;
        }

        Console.WriteLine("\nRevistas dispon�veis para empr�stimo:\n");
        bool algumaRevistaDisponivel = false;
        foreach (EntidadeBase entidadeRevista in registrosRevista)
        {
            if (entidadeRevista is Revista revista && revista.status == StatusRevista.Disponivel)
            {
                Console.WriteLine(revista.DadosFormatados()); 
                algumaRevistaDisponivel = true;
            }
        }

        if (!algumaRevistaDisponivel)
        {
            notificador.ApresentarMensagem("N�o h� nenhuma revista dispon�vel para empr�stimo no momento.", TipoMensagem.Erro);
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
            return null;
        }

        Console.Write("\nDigite o ID da revista: ");
        int idRevista = int.TryParse(Console.ReadLine(), out int idRev) ? idRev : -1; //
        Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarPorId(idRevista);

        if (revistaSelecionada == null)
        {
            notificador.ApresentarMensagem("Revista n�o encontrada!", TipoMensagem.Erro);
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
            return null;
        }

        Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada); 
        string errosValidacao = novoEmprestimo.Validar(); 

        if (!string.IsNullOrEmpty(errosValidacao))
        {
            notificador.ApresentarMensagem(errosValidacao, TipoMensagem.Erro);
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
            return null;
        }

        return novoEmprestimo;
    }

    protected override bool ValidarAntesDeInserir(EntidadeBase entidade)
    {
        Emprestimo emprestimo = (Emprestimo)entidade;

        bool amigoJaTemEmprestimoAtivo = ((RepositorioEmprestimo)repositorio).PossuiEmprestimoAtivo(emprestimo.amigo); 

        if (amigoJaTemEmprestimoAtivo)
        {
            notificador.ApresentarMensagem("Este amigo j� possui um empr�stimo ativo!", TipoMensagem.Erro);
            Console.WriteLine("Pressione ENTER para continuar..."); 
            Console.ReadLine(); 
            return false;
        }

        emprestimo.revista.Emprestar(); 
        return true;
    }
}