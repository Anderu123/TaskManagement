using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.SharedData;


namespace TaskManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskAccessLayer _taskAccessLayer;

        public TasksController(ITaskAccessLayer taskAccessLayer)
        {
            _taskAccessLayer = taskAccessLayer;
        }


        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            return await _taskAccessLayer.GetAllTasks();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTasks(Guid id)
        {
            var allTasks = await _taskAccessLayer.GetAllTasks();
            

            if (allTasks== null)
            {
                return NotFound();
            }
            
            var specificTask = allTasks.FirstOrDefault(task => task.Id == id);

            if (specificTask == null)
            {
                return NotFound();
            }

            return Ok(specificTask);
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasks(Guid id, Tasks tasks)
        {
            if (id != tasks.Id)
            {
                return BadRequest();
            }

            var result = await _taskAccessLayer.UpdateTask(id, tasks);
            if (!result)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }

        }
        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasks(Guid id)
        {
            
            var result = await _taskAccessLayer.DeleteTask(id);
            if (!result)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }
        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTask([FromBody] Tasks task)
        {
            if (string.IsNullOrWhiteSpace(task.TaskName))
            {
                return BadRequest("title required");
            }

            var saved = await _taskAccessLayer.AddTask(task);
            if (saved is null)
            { 
                return Conflict("duplicate task name"); 
            }

            return CreatedAtAction(nameof(GetTasks), new { id = saved.Id }, saved);
        }




    }
}
