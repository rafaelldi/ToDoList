using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Web.Models;
using TodoList.Web.Services;

namespace TodoList.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoService _toDoService;

        public ToDoController(ToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public ActionResult<List<ToDoItem>> GetItems()
        {
            return _toDoService.Get();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<ToDoItem> GetItem(int id)
        {
            if (!_toDoService.Contains(id))
            {
                return NotFound();
            }

            return _toDoService.Get(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ToDoItem))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<ToDoItem> CreateItem(ToDoItem toDoItem)
        {
            _toDoService.AddItem(toDoItem.Id, toDoItem);

            return CreatedAtAction("GetItem", new {id = toDoItem.Id}, toDoItem);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<ToDoItem> UpdateItem(int id, ToDoItem toDoItem)
        {
            if (!_toDoService.Contains(id))
            {
                return NotFound();
            }

            if (id != toDoItem.Id)
            {
                return BadRequest();
            }

            _toDoService.UpdateItem(id, toDoItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<ToDoItem> DeleteItem(int id)
        {
            if (!_toDoService.Contains(id))
            {
                return NotFound();
            }

            _toDoService.DeleteItem(id);
            return NoContent();
        }
    }
}