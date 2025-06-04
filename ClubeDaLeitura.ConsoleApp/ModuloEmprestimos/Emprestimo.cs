using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigos;
using ClubeDaLeitura.ModuloRevistas;
using System;
namespace ClubeDaLeitura.ModuloEmprestimos;

public class Emprestimo : EntidadeBase
{
    public Amigo amigo;
    public Revista revista;
    public DateTime dataEmprestimo;
    public DateTime dataDevolucao;
    public StatusEmprestimo status;

    public Emprestimo(Amigo amigo, Revista revista)
    {
        this.amigo = amigo;
        this.revista = revista;
        this.dataEmprestimo = DateTime.Today;
        this.dataDevolucao = dataEmprestimo.AddDays(revista.caixa.diasDeEmprestimo);
        this.status = StatusEmprestimo.Aberto;
    }

    public override string Validar()
    {
        string erros = "";

        if (amigo == null)
            erros += "O amigo é obrigatório!\n";

        if (revista == null)
            erros += "A revista é obrigatória!\n";

        if (revista.status != StatusRevista.Disponivel)
            erros += "A revista selecionada não está disponível!\n";

        return erros;
    }

    public void RegistrarDevolucao()
    {
        if (status == StatusEmprestimo.Aberto || status == StatusEmprestimo.Atrasado)
        {
            status = StatusEmprestimo.Concluido;
            revista.Devolver();
        }
    }

    public void VerificarAtraso()
    {
        if (status == StatusEmprestimo.Aberto && DateTime.Today > dataDevolucao)
            status = StatusEmprestimo.Atrasado;
    }

    public override void AtualizarRegistro(EntidadeBase registroAtualizado)
    {
        Emprestimo emprestimoAtualizado = (Emprestimo)registroAtualizado;

        this.amigo = emprestimoAtualizado.amigo;
        this.revista = emprestimoAtualizado.revista;
        this.dataEmprestimo = emprestimoAtualizado.dataEmprestimo;
        this.dataDevolucao = emprestimoAtualizado.dataDevolucao;
        this.status = emprestimoAtualizado.status;
    }

    public override string DadosFormatados()
    {
        VerificarAtraso();

        string atraso = "";
        if (status == StatusEmprestimo.Atrasado)
            atraso = " <-- ATRASADO";

        return $"{id,-3} | Amigo: {amigo.nome,-20} | Revista: {revista.titulo,-25} | Empréstimo: {dataEmprestimo:dd/MM/yyyy} | Devolução: {dataDevolucao:dd/MM/yyyy} | Status: {status}{atraso}";
    }
}