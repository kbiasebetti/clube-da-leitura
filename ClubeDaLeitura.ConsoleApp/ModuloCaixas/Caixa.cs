using ClubeDaLeitura.Compartilhado;
using System;

namespace ClubeDaLeitura.ModuloCaixas
{
    public class Caixa : EntidadeBase
    {
        public string etiqueta;
        public string cor;
        public int diasDeEmprestimo;
        // TODO: Quando o m�dulo Revista for implementado, esta se��o ser� usada.
        // Dever� ser uma cole��o baseada em array, conforme restri��es do projeto.
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
                erros += "A etiqueta n�o pode ser vazia!\n";
            else if (etiqueta.Length > 50)
                erros += "A etiqueta deve ter no m�ximo 50 caracteres!\n";

            if (string.IsNullOrWhiteSpace(cor))
                erros += "A cor n�o pode ser vazia!\n";

            if (diasDeEmprestimo <= 0)
                erros += "Os dias de empr�stimo devem ser um n�mero positivo!\n";

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

        // TODO: Implementar AdicionarRevista() quando o m�dulo Revista estiver pronto e usando arrays.
        /*
        public bool AdicionarRevista(Revista revista)
        {
            // L�gica para adicionar revista � caixa
        }
        */

        // TODO: Implementar RemoverRevista() quando o m�dulo Revista estiver pronto e usando arrays.
        /*
        public bool RemoverRevista(Revista revista)
        {
            // L�gica para remover revista da caixa.
        }
        */

        // TODO: Implementar m�todo para verificar se a caixa tem revistas vinculadas.
        /*
        public bool TemRevistasVinculadas()
        {
            // return contadorRevistas > 0; // Exemplo
        }
        */
    }
}