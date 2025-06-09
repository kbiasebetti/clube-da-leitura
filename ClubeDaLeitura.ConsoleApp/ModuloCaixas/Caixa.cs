using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloRevistas;
using System;
namespace ClubeDaLeitura.ModuloCaixas;

public class Caixa : EntidadeBase
{
    public string etiqueta;
    public string cor;
    public int diasDeEmprestimo;

    public Revista[] revistas = new Revista[0];

    public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
    {
        this.etiqueta = etiqueta;
        this.cor = cor;
        this.diasDeEmprestimo = diasDeEmprestimo;
    }

    public void AdicionarRevista(Revista revista)
    {
        Revista[] novoArray = new Revista[revistas.Length + 1];

        for (int i = 0; i < revistas.Length; i++)
        {
            novoArray[i] = revistas[i];
        }

        novoArray[novoArray.Length - 1] = revista;

        revistas = novoArray;
    }

    public void RemoverRevista(Revista revista)
    {
        int posicaoEncontrada = -1;

        for (int i = 0; i < revistas.Length; i++)
        {
            if (revistas[i] != null && revistas[i].id == revista.id)
            {
                posicaoEncontrada = i;
                break;
            }
        }

        if (posicaoEncontrada == -1)
            return;

        Revista[] novoArray = new Revista[revistas.Length - 1];

        int contadorNovoArray = 0;
        for (int i = 0; i < revistas.Length; i++)
        {
            if (i != posicaoEncontrada)
            {
                novoArray[contadorNovoArray] = revistas[i];
                contadorNovoArray++;
            }
        }

        revistas = novoArray;
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
        return $"{id,-5} | {etiqueta,-30} | {cor,-20} | {diasDeEmprestimo,-5} dias";
    }
}