using System.Collections.Generic;
using System.Linq;
using TodoList.Web.Models;

namespace TodoList.Web.Services
{
    public class ToDoService
    {
        private readonly Dictionary<int, ToDoItem> _toDoItems = new Dictionary<int, ToDoItem>();

        public void AddItem(int id, ToDoItem toDoItem)
        {
            _toDoItems.Add(id, toDoItem);
        }

        public void UpdateItem(int id, ToDoItem toDoItem)
        {
            _toDoItems[id] = toDoItem;
        }

        public void DeleteItem(int id)
        {
            _toDoItems.Remove(id);
        }

        public List<ToDoItem> Get() => _toDoItems.Values.ToList();

        public ToDoItem Get(int id) => _toDoItems[id];

        public bool Contains(int id) => _toDoItems.ContainsKey(id);
    }
}