namespace ClubeDaLeitura.Compartilhado;

public abstract class EntidadeBase
{
    public int id;

    public abstract string Validar();
    public abstract void AtualizarRegistro(EntidadeBase registroAtualizado);
    public abstract string DadosFormatados();

    public override string ToString()
    {
        return DadosFormatados();
        Console.WriteLine("\n");
    }
}