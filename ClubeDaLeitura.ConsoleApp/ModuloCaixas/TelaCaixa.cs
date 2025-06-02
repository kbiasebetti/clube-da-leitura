using ClubeDaLeitura.Compartilhado;
using System;

namespace ClubeDaLeitura.ModuloCaixas
{
    public class TelaCaixa : TelaBase
    {
        private readonly RepositorioCaixa repositorioCaixa;

        public TelaCaixa(RepositorioCaixa repositorioCaixa, Notificador notificador) : base("Caixa", repositorioCaixa, notificador)
        {
            this.repositorioCaixa = repositorioCaixa;
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.Write("Digite a etiqueta da caixa (máx 50 caracteres): ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            Console.Write("Digite os dias de empréstimo para revistas desta caixa (padrão 7): ");
            string entradaDiasEmprestimo = Console.ReadLine();
            int diasDeEmprestimoFinal = 7;

            if (!string.IsNullOrWhiteSpace(entradaDiasEmprestimo))
            {
                if (!int.TryParse(entradaDiasEmprestimo, out int diasEmprestimoNumerico))
                {
                    diasDeEmprestimoFinal = diasEmprestimoNumerico; 
                }
                else
                {
                    diasDeEmprestimoFinal = diasEmprestimoNumerico;
                }
            }

            Caixa caixa = new Caixa(etiqueta, cor, diasDeEmprestimoFinal);

            string errosValidacaoCampos = caixa.Validar();
            if (!string.IsNullOrWhiteSpace(errosValidacaoCampos))
            {
                notificador.ApresentarMensagem(errosValidacaoCampos, TipoMensagem.Erro);
                return ObterRegistro();
            }

            return caixa; 
        }

        public override void Inserir()
        {
            Console.Clear();
            MostrarCabecalho($"Inserindo {nomeEntidade}");

            Caixa novaCaixa = (Caixa)ObterRegistro();
            if (novaCaixa == null) return; 

            if (repositorioCaixa.ExisteEtiquetaDuplicada(novaCaixa.etiqueta))
            {
                notificador.ApresentarMensagem("Já existe uma caixa com esta etiqueta. A inserção foi cancelada.", TipoMensagem.Erro);
                return;
            }

            repositorio.Inserir(novaCaixa);
            notificador.ApresentarMensagem($"{nomeEntidade} inserida com sucesso!", TipoMensagem.Sucesso);
        }

        public override void Editar()
        {
            int idSelecionado;

            Console.Clear();
            MostrarCabecalho($"Editando {nomeEntidade}");

            VisualizarTodos(false);

            if (!ExistemRegistrosParaOperacao())
            {
                return;
            }

            while (true)
            {
                Console.Write($"\nDigite o ID da {nomeEntidade} que deseja editar: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out idSelecionado))
                {
                    if (repositorio.SelecionarPorId(idSelecionado) != null)
                        break;
                    else
                        notificador.ApresentarMensagem("ID inválido. Caixa não encontrada.", TipoMensagem.Erro);
                }
                else
                {
                    notificador.ApresentarMensagem("ID inválido, por favor digite um número inteiro.", TipoMensagem.Erro);
                }
            }

            Console.WriteLine($"\nEditando a {nomeEntidade} com ID {idSelecionado}:");
            Caixa caixaAtualizada = (Caixa)ObterRegistro();
            if (caixaAtualizada == null) return;

            if (repositorioCaixa.ExisteEtiquetaDuplicada(caixaAtualizada.etiqueta, idSelecionado))
            {
                notificador.ApresentarMensagem("Já existe outra caixa com esta etiqueta. A edição foi cancelada.", TipoMensagem.Erro);
                return;
            }

            repositorio.Editar(idSelecionado, caixaAtualizada);
            notificador.ApresentarMensagem($"{nomeEntidade} editada com sucesso!", TipoMensagem.Sucesso);
        }

        public override void Excluir()
        {
            Console.Clear();
            MostrarCabecalho($"Excluindo {nomeEntidade}");

            VisualizarTodos(false);

            if (!ExistemRegistrosParaOperacao())
            {
                return;
            }

            int idSelecionado;
            while (true)
            {
                Console.Write($"\nDigite o ID da {nomeEntidade} que deseja excluir: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out idSelecionado))
                {
                    if (repositorio.SelecionarPorId(idSelecionado) != null)
                        break;
                    else
                        notificador.ApresentarMensagem("ID inválido. Caixa não encontrada.", TipoMensagem.Erro);
                }
                else
                {
                    notificador.ApresentarMensagem("ID inválido, por favor digite um número inteiro.", TipoMensagem.Erro);
                }
            }

            // TODO: Regra de Negócio: "Não permitir excluir uma caixa caso tenha revistas vinculadas"
            // Caixa caixaParaExcluir = (Caixa)repositorio.SelecionarPorId(idSelecionado);
            // if (caixaParaExcluir != null && caixaParaExcluir.TemRevistasVinculadas())
            // {
            //     notificador.ApresentarMensagem($"Não é possível excluir a {nomeEntidade} pois ela possui revistas vinculadas.", TipoMensagem.Erro);
            //     return;
            // }

            bool conseguiuExcluir = repositorio.Excluir(idSelecionado);

            if (conseguiuExcluir)
                notificador.ApresentarMensagem($"{nomeEntidade} excluída com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem($"Não foi possível excluir a {nomeEntidade}.", TipoMensagem.Erro);
        }

        private bool ExistemRegistrosParaOperacao()
        {
            EntidadeBase[] todosOsRegistros = repositorio.SelecionarTodos();
            foreach (EntidadeBase reg in todosOsRegistros)
            {
                if (reg != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}