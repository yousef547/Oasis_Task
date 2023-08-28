using AutoMapper;
using oasis.DTOs;
using oasis.Entities;
using oasis.Extensions;
using oasis.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oasis.Controllers
{
    [Authorize]
    public class ToDoController : BaseApiController
    {
        private readonly IUnitOfWork _UnitOfWork;
        public readonly IMapper _mapper;
        public ToDoController(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _UnitOfWork = UnitOfWork;
            _mapper = mapper;
        }
        // CreateTodo
        [HttpPost("CreateTodo")]
        public async Task<ActionResult<ToDoDto>> CreateTodo(ToDoDto model)
        {

            if (!ModelState.IsValid)
                return Ok(new ApiResponseMessageDto()
                     {
                         Date = { },
                         StatusCode = 400,
                         IsSuccess = false,
                         Messages = BadRequest(ModelState)
                     });

            var data = _mapper.Map<ToDoUsers>(model);
            data.UserId = User.GetUserId();
            var isSaved = await _UnitOfWork.ToDo.AddAsync(data);
            if (isSaved) return Ok(new ApiResponseMessageDto()
            {
                Date = data,
                StatusCode = 200,
                IsSuccess = true,
            });

            return Ok(new ApiResponseMessageDto(){
                         Date = { },
                         StatusCode = 400,
                         IsSuccess = false,
                         Messages = BadRequest("Failed to send ToDo")
                     });
        }
        // Update Todo
        [HttpPut("UpdateTodo")]
        public async Task<ActionResult<ToDoDto>> UpdateTodo(UpdateToDoDto model)
        {

            if (!ModelState.IsValid)
                return Ok(new ApiResponseMessageDto()
                {
                    Date = { },
                    StatusCode = 400,
                    IsSuccess = false,
                    Messages = BadRequest(ModelState)
                });
    
            var data = _mapper.Map<ToDoUsers>(model);
            data.UserId = User.GetUserId();
            var isSaved = await _UnitOfWork.ToDo.UpdateAsync(data);
            if (isSaved) return Ok(new ApiResponseMessageDto()
            {
                Date = data,
                StatusCode = 200,
                IsSuccess = true,
            });

            return Ok(new ApiResponseMessageDto()
            {
                Date = { },
                StatusCode = 400,
                IsSuccess = false,
                Messages = BadRequest("Failed to send ToDo")
            });
        }
        // Get All Todo User
        [HttpGet("GetAllTodoUser")]
        public async Task<ActionResult<ToDoDto>> GetAllTodoUser()
        {
            var data = await _UnitOfWork.ToDo.GetAllAsync(x => x.UserId == User.GetUserId());

            return Ok(new ApiResponseMessageDto() {
                    Date = _mapper.Map<List<ToDoDto>>(data),
                    StatusCode = 200,
                    IsSuccess = true,
                });
        }
        // Get Todo User By Id
        [HttpGet("GetTodoById")]
        public async Task<ActionResult<ToDoDto>> GetTodoById(int id)
        {
            var data = await _UnitOfWork.ToDo.GetAllAsync(x => x.UserId == User.GetUserId() && x.Id == id);
            if (data.Count == 0)
            {
                return Ok(new ApiResponseMessageDto()
                     {
                         Date = { },
                         StatusCode = 400,
                         IsSuccess = false,
                         Messages = BadRequest("this ToDo not found")
                     });
            }
            var mapDoto = _mapper.Map<ToDoDto>(data.FirstOrDefault());
            return Ok(new ApiResponseMessageDto()                {
                    Date = mapDoto,
                    StatusCode = 200,
                    IsSuccess = true,
                } );
        }
        // Delete Todo User By Id
        [HttpDelete("DeleteTodoById")]
        public async Task<ActionResult<ToDoDto>> DeleteTodoById(int id)
        {
            var todo = await _UnitOfWork.ToDo.GetAllAsync(x => x.UserId == User.GetUserId() && x.Id == id);
            if (todo.Count == 0)
            {
                return Ok(new ApiResponseMessageDto()
                     {
                         Date = { },
                         StatusCode = 400,
                         IsSuccess = false,
                         Messages = BadRequest("this ToDo not found")
                     } );
            }
            var data = await _UnitOfWork.ToDo.DeleteAsync(todo.FirstOrDefault());
            if (!data)
            {
                return Ok(new ApiResponseMessageDto()
                     {
                         Date = { },
                         StatusCode = 400,
                         IsSuccess = false,
                         Messages = BadRequest("error !")
                     });
            }
            return Ok(new ApiResponseMessageDto()
                {
                    Date = todo,
                    StatusCode = 200,
                    IsSuccess = true,
                });
        }
    }
}
