using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimos;

namespace ClubeDaLeitura.ModuloAmigos
{
    public class TelaAmigo : TelaBase
    {
        private RepositorioAmigo repositorioAmigo;
        private RepositorioEmprestimo repositorioEmprestimo;

        public TelaAmigo(RepositorioAmigo repositorioAmigo,
                         RepositorioEmprestimo repositorioEmprestimo,
                         Notificador notificador)
            : base("Amigo", repositorioAmigo, notificador)
        {
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioEmprestimo = repositorioEmprestimo;
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o telefone do amigo ((XX) XXXX-XXXX ou (XX) XXXXX-XXXX): ");
            string telefone = Console.ReadLine();

            return new Amigo(nome, nomeResponsavel, telefone);
        }

        protected override bool ValidarAntesDeInserir(EntidadeBase entidade)
        {
            Amigo amigo = (Amigo)entidade;

            if (repositorioAmigo.ExisteDuplicado(amigo.nome, amigo.telefone))
            {
                notificador.ApresentarMensagem("Já existe um amigo com esse nome e telefone.", TipoMensagem.Erro);
                return false;
            }

            return true;
        }

        protected override bool PodeExcluir(int id)
        {
            Amigo amigo = (Amigo)repositorio.SelecionarPorId(id);

            if (amigo == null)
            {
                notificador.ApresentarMensagem("Amigo não encontrado.", TipoMensagem.Erro);
                return false;
            }

            Emprestimo[] emprestimos = ObterTodosEmprestimos();

            Emprestimo[] emprestimosDoAmigo = amigo.ObterEmprestimos(emprestimos);

            if (emprestimosDoAmigo.Length > 0)
            {
                notificador.ApresentarMensagem("Este amigo possui empréstimos vinculados e não pode ser excluído.", TipoMensagem.Erro);
                return false;
            }

            return true;
        }

        public void VisualizarEmprestimos()
        {
            VisualizarTodos(false, false);

            Console.Write("\nDigite o ID do amigo para visualizar os empréstimos: ");
            string entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out int idSelecionado))
            {
                notificador.ApresentarMensagem("ID inválido, por favor digite um número inteiro.", TipoMensagem.Erro);
                return;
            }

            Amigo amigo = (Amigo)repositorioAmigo.SelecionarPorId(idSelecionado);

            if (amigo == null)
            {
                notificador.ApresentarMensagem("Amigo não encontrado!", TipoMensagem.Erro);
                return;
            }

            Emprestimo[] todosEmprestimos = ObterTodosEmprestimos();

            Emprestimo[] emprestimosDoAmigo = amigo.ObterEmprestimos(todosEmprestimos);

            if (emprestimosDoAmigo.Length == 0)
            {
                notificador.ApresentarMensagem("Este amigo não possui empréstimos!", TipoMensagem.Sucesso);
                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"\nEmpréstimos do amigo: {amigo.nome}\n");

            foreach (Emprestimo emprestimo in emprestimosDoAmigo)
                Console.WriteLine(emprestimo);

            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }

        private Emprestimo[] ObterTodosEmprestimos()
        {
            EntidadeBase[] registros = repositorioEmprestimo.SelecionarTodos();
            int quantidade = registros.Length;

            Emprestimo[] emprestimos = new Emprestimo[quantidade];
            int contador = 0;

            for (int i = 0; i < quantidade; i++)
            {
                if (registros[i] is Emprestimo emprestimo)
                {
                    emprestimos[contador] = emprestimo;
                    contador++;
                }
            }

            Emprestimo[] emprestimosCompactados = new Emprestimo[contador];
            for (int i = 0; i < contador; i++)
                emprestimosCompactados[i] = emprestimos[i];

            return emprestimosCompactados;
        }
    }
}
