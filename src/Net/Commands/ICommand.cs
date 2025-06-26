namespace ArgentumOnline.Net.Commands;

public interface ICommand
{
    void Unpack(Reader reader);
}