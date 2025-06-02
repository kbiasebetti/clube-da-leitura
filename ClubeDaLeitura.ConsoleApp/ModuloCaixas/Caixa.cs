using ClubeDaLeitura.Compartilhado;
using System;

namespace ClubeDaLeitura.ModuloCaixas
{
    public class Caixa : EntidadeBase
    {
        public string etiqueta;
        public string cor;
        public int diasDeEmprestimo;
        // TODO: Quando o módulo Revista for implementado, esta seção será usada.
        // Deverá ser uma coleção baseada em array, conforme restrições do projeto.
        // Exemplo: private Revista[] revistasNaCaixa;
        // private int contadorRevistas;

        public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
        {
            this.etiqueta = etiqueta;
            this.cor = cor;
            this.diasDeEmprestimo = diasDeEmprestimo;
            // this.revistasNaCaixa = new Revista[CAPACIDADE_MAX_REVISTAS_CAIXA]; // Exemplo
            // this.contadorRevistas = 0;
        }

        public override string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(etiqueta))
                erros += "A etiqueta não pode ser vazia!\n";
            else if (etiqueta.Length > 50)
                erros += "A etiqueta deve ter no máximo 50 caracteres!\n";

            if (string.IsNullOrWhiteSpace(cor))
                erros += "A cor não pode ser vazia!\n";

            if (diasDeEmprestimo <= 0)
                erros += "Os dias de empréstimo devem ser um número positivo!\n";

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Caixa caixaAtualizada = (Caixa)registroAtualizado;

            this.etiqueta = caixaAtualizada.etiqueta;
            this.cor = caixaAtualizada.cor;
            this.diasDeEmprestimo = caixaAtualizada.diasDeEmprestimo;
        }

        public override string DadosFormatados()
        {
            return $"{id,-10} | {etiqueta,-30} | {cor,-20} | {diasDeEmprestimo,-5} dias";
        }

        // TODO: Implementar AdicionarRevista() quando o módulo Revista estiver pronto e usando arrays.
        /*
        public bool AdicionarRevista(Revista revista)
        {
            // Lógica para adicionar revista à caixa
        }
        */

        // TODO: Implementar RemoverRevista() quando o módulo Revista estiver pronto e usando arrays.
        /*
        public bool RemoverRevista(Revista revista)
        {
            // Lógica para remover revista da caixa.
        }
        */

        // TODO: Implementar método para verificar se a caixa tem revistas vinculadas.
        /*
        public bool TemRevistasVinculadas()
        {
            // return contadorRevistas > 0; // Exemplo
        }
        */
    }
}