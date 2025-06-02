using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloCaixas;
using System;
namespace ClubeDaLeitura.ModuloRevistas;

public class Revista : EntidadeBase
{
    public string titulo;
    public int numeroEdicao;
    public int anoPublicacao;
    public Caixa caixa;
    public StatusRevista status;

    public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixaSelecionada)
    {
        this.titulo = titulo;
        this.numeroEdicao = numeroEdicao;
        this.anoPublicacao = anoPublicacao;
        this.caixa = caixaSelecionada;
        this.status = StatusRevista.Disponivel;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(titulo) || titulo.Length < 2 || titulo.Length > 100) // Título (2-100 caracteres)
            erros += "O título da revista deve ter entre 2 e 100 caracteres!\n";

        if (numeroEdicao <= 0)
            erros += "O número da edição deve ser um valor positivo!\n";

        if (anoPublicacao <= 0 || anoPublicacao > DateTime.Now.Year + 1)
            erros += $"O ano de publicação deve ser válido (ex: entre 1800 e {DateTime.Now.Year + 1})!\n";

        if (caixa == null)
            erros += "A revista deve ser associada a uma caixa!\n";

        return erros;
    }

    public override void AtualizarRegistro(EntidadeBase registroAtualizado)
    {
        Revista revistaAtualizada = (Revista)registroAtualizado;

        this.titulo = revistaAtualizada.titulo;
        this.numeroEdicao = revistaAtualizada.numeroEdicao;
        this.anoPublicacao = revistaAtualizada.anoPublicacao;
        this.caixa = revistaAtualizada.caixa;
        this.status = revistaAtualizada.status;
    }

    public override string DadosFormatados()
    {
        return $"{id,-5} | {titulo,-30} | Ed. {numeroEdicao,-5} | Ano: {anoPublicacao,-5} | Caixa: {caixa.etiqueta,-15} | Status: {status}";
    }

    public void Emprestar()
    {
        this.status = StatusRevista.Emprestada;
    }

    public void Devolver()
    {
        this.status = StatusRevista.Disponivel;
    }

    public void Reservar()
    {
        this.status = StatusRevista.Reservada;
    }
}