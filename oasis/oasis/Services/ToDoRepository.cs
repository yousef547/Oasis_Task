using oasis.Data;
using oasis.Entities;
using oasis.Interface;

namespace oasis.Services
{
    public class ToDoRepository : Repository<ToDoUsers>, IToDoRepository
    {
        public ToDoRepository(DataContext context) : base(context)
        {
            
        }
    }
}
