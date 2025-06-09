using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigos;
using ClubeDaLeitura.ModuloCaixas;
using ClubeDaLeitura.ModuloEmprestimos;
using ClubeDaLeitura.ModuloRevistas;

class Program
{
    static void Main(string[] args)
    {
        Notificador notificador = new();

        RepositorioCaixa repositorioCaixa = new();
        RepositorioRevista repositorioRevista = new();
        RepositorioAmigo repositorioAmigo = new();
        RepositorioEmprestimo repositorioEmprestimo = new();

        TelaCaixa telaCaixa = new(repositorioCaixa, notificador);
        TelaRevista telaRevista = new(repositorioRevista, repositorioCaixa, notificador);
        TelaAmigo telaAmigo = new(repositorioAmigo, repositorioEmprestimo, notificador);
        TelaEmprestimo telaEmprestimo = new(repositorioEmprestimo, notificador, repositorioAmigo, repositorioRevista);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Clube da Leitura");
            Console.WriteLine("------------------------");
            Console.WriteLine("1 - Gerenciar Caixas");
            Console.WriteLine("2 - Gerenciar Revistas");
            Console.WriteLine("3 - Gerenciar Amigos");
            Console.WriteLine("4 - Gerenciar Empréstimos");
            Console.WriteLine("S - Sair");
            Console.Write("Opção: ");

            string opcaoPrincipal = Console.ReadLine();

            if (opcaoPrincipal.ToUpper() == "S")
                break;

            TelaBase telaSelecionada = null;

            switch (opcaoPrincipal)
            {
                case "1": telaSelecionada = telaCaixa; break;
                case "2": telaSelecionada = telaRevista; break;
                case "3": telaSelecionada = telaAmigo; break;
                case "4": telaSelecionada = telaEmprestimo; break;
                default:
                    notificador.ApresentarMensagem("Opção inválida!", TipoMensagem.Erro);
                    continue;
            }

            MostrarMenuCrud(telaSelecionada, telaAmigo);
        }
    }

    static void MostrarMenuCrud(TelaBase tela, TelaAmigo telaAmigo)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Menu {tela.nomeEntidade}");
            Console.WriteLine("------------------------");
            Console.WriteLine("1 - Inserir");
            Console.WriteLine("2 - Editar");
            Console.WriteLine("3 - Excluir");
            Console.WriteLine("4 - Visualizar");
            if (tela is TelaAmigo)
                Console.WriteLine("5 - Visualizar Empréstimos do Amigo");
            Console.WriteLine("V - Voltar");
            Console.Write("Opção: ");

            string opcao = Console.ReadLine();

            if (opcao.ToUpper() == "V")
                break;

            switch (opcao)
            {
                case "1": tela.Inserir(); break;
                case "2": tela.Editar(); break;
                case "3": tela.Excluir(); break;
                case "4": tela.VisualizarTodos(); break;
                case "5":
                    if (tela is TelaAmigo)
                        telaAmigo.VisualizarEmprestimos();
                    else
                        Console.WriteLine("Opção inválida.");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
