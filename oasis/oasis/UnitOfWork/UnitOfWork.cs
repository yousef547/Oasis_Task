using oasis.Data;
using oasis.Interface;
using oasis.Services;
using Microsoft.EntityFrameworkCore;

namespace oasis.UnitOfWork
{
    public class UnitOfWorks : IUnitOfWork
    {
        private readonly DataContext _context;
        public IToDoRepository ToDo { get; private set; }
        public UnitOfWorks(DataContext context)
        {
            _context = context;
            ToDo = new ToDoRepository( context);
        }

    }
}
