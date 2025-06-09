using ClubeDaLeitura.ModuloCaixas;
using ClubeDaLeitura.ModuloRevistas;
using ClubeDaLeitura.ModuloAmigos;
using ClubeDaLeitura.ModuloEmprestimos;

namespace ClubeDaLeitura.Compartilhado
{
    public class TelaPrincipal
    {
        private Notificador notificador;

        private TelaCaixa telaCaixa;
        private TelaRevista telaRevista;
        private TelaAmigo telaAmigo;
        private TelaEmprestimo telaEmprestimo;

        public TelaPrincipal()
        {
            notificador = new Notificador();

            RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
            RepositorioRevista repositorioRevista = new RepositorioRevista();
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
            RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();

            telaAmigo = new TelaAmigo(repositorioAmigo, repositorioEmprestimo, notificador);
            telaCaixa = new TelaCaixa(repositorioCaixa, notificador);
            telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa, notificador);
            telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, notificador, repositorioAmigo, repositorioRevista);
        }

        public void ApresentarMenuPrincipal()
        {
            while (true)
            {
                MostrarMenuPrincipal();

                string opcao = Console.ReadLine().ToUpper();

                if (opcao == "S")
                    break;

                if (opcao == "1") 
                {
                    ApresentarMenuAmigo(telaAmigo);
                }
                else 
                {
                    TelaBase telaSelecionada = ObterTela(opcao);

                    if (telaSelecionada != null)
                        ApresentarMenuDeCrud(telaSelecionada);
                    else
                        notificador.ApresentarMensagem("Opção inválida, tente novamente.", TipoMensagem.Erro);
                }
            }
        }

        private void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("-------------------------");
            Console.WriteLine("    Clube da Leitura");
            Console.WriteLine("-------------------------");
            Console.WriteLine("\nMenu Principal:\n");
            Console.WriteLine("1 - Gerenciar Amigos");
            Console.WriteLine("2 - Gerenciar Caixas");
            Console.WriteLine("3 - Gerenciar Revistas");
            Console.WriteLine("4 - Gerenciar Empréstimos");
            Console.WriteLine("\nS - Sair");
            Console.Write("\nEscolha uma opção: ");
        }

        private TelaBase ObterTela(string opcao)
        {
            switch (opcao)
            {
                case "2": return telaCaixa;
                case "3": return telaRevista;
                case "4": return telaEmprestimo;
                default: return null;
            }
        }

        private void ApresentarMenuAmigo(TelaAmigo tela)
        {
            while (true)
            {
                MostrarMenuDeCrud(tela.nomeEntidade);
                Console.WriteLine("5 - Visualizar Empréstimos do Amigo"); 
                Console.WriteLine("\nS - Voltar ao Menu Principal");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine().ToUpper();

                if (opcao == "S")
                    break;

                switch (opcao)
                {
                    case "1": tela.Inserir(); break;
                    case "2": tela.Editar(); break;
                    case "3": tela.Excluir(); break;
                    case "4": tela.VisualizarTodos(exibirTitulo: true, pausarNoFinal: true); break;
                    case "5": tela.VisualizarEmprestimos(); break;
                    default:
                        notificador.ApresentarMensagem("Opção inválida, tente novamente.", TipoMensagem.Erro);
                        break;
                }
            }
        }

        private void ApresentarMenuDeCrud(TelaBase tela)
        {
            while (true)
            {
                MostrarMenuDeCrud(tela.nomeEntidade);
                Console.WriteLine("\nS - Voltar ao Menu Principal");
                Console.Write("\nEscolha uma opção: ");

                string opcao = Console.ReadLine().ToUpper();

                if (opcao == "S")
                    break;

                switch (opcao)
                {
                    case "1": tela.Inserir(); break;
                    case "2": tela.Editar(); break;
                    case "3": tela.Excluir(); break;
                    case "4": tela.VisualizarTodos(exibirTitulo: true, pausarNoFinal: true); break;
                    default:
                        notificador.ApresentarMensagem("Opção inválida, tente novamente.", TipoMensagem.Erro);
                        break;
                }
            }
        }

        private void MostrarMenuDeCrud(string nomeEntidade)
        {
            Console.Clear();
            Console.WriteLine("-------------------------");
            Console.WriteLine($"   Gestão de {nomeEntidade}s");
            Console.WriteLine("-------------------------");
            Console.WriteLine("\nMenu de Ações:\n");
            Console.WriteLine($"1 - Inserir Novo(a) {nomeEntidade}");
            Console.WriteLine($"2 - Editar {nomeEntidade}");
            Console.WriteLine($"3 - Excluir {nomeEntidade}");
            Console.WriteLine($"4 - Visualizar {nomeEntidade}s");
        }
    }
}