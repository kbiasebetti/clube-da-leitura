namespace ClubeDaLeitura.Compartilhado;

public class Notificador
{
    public void ApresentarMensagem(string mensagem, TipoMensagem tipo)
    {
        switch (tipo)
        {
            case TipoMensagem.Sucesso:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case TipoMensagem.Erro:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
        }

        Console.WriteLine($"\n{mensagem}\n");
        Console.ResetColor();
    }
}

public enum TipoMensagem
{
    Sucesso,
    Erro
}