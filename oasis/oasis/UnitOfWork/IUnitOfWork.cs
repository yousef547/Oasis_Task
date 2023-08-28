using oasis.Interface;

namespace oasis.UnitOfWork
{
    public interface IUnitOfWork
    {
        IToDoRepository ToDo { get; }
    }
}
