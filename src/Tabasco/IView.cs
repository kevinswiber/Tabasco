using NRack;

namespace Tabasco
{
    public interface IView : IIterable
    {
        IView Template(string viewPath);
    }
}