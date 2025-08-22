using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.SharedData
{
    public interface ITaskAccessLayer
    {
         Task<List<Tasks>> GetAllTasks();
        Task<bool> UpdateTask(Guid id, Tasks task);
        Task<bool> DeleteTask(Guid id);
        Task<Tasks> AddTask(Tasks task);
    }
    public class TasksAccessLayer : ITaskAccessLayer
    {
        private ApplicationDbContext _context;
        public TasksAccessLayer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            try
            {
                return await _context.Tasks.ToListAsync();
            }
            catch
            {
                return new List<Tasks>();
            }
        }
        public async Task<bool> UpdateTask(Guid id, Tasks task)
        {
            try
            {
                var exisitngTask = await _context.Tasks.FindAsync(id);
                if (exisitngTask == null)
                {
                    return false;
                }
                else
                {
                    exisitngTask.TaskName = task.TaskName;
                    exisitngTask.IsCompleted = task.IsCompleted;
                    await _context.SaveChangesAsync();
                    return true;
                }

              
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteTask(Guid id)
        {
            try
            {
                var existingTask = await _context.Tasks.FindAsync(id);
                if (existingTask == null)
                {
                    return false;
                }
                else
                {
                    _context.Tasks.Remove(existingTask);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<Tasks?> AddTask(Tasks task)
        {
            try
            {
                var name = (task.TaskName ?? "").Trim();
                if (string.IsNullOrEmpty(name))
                { 
                    return null; 
                }

               
                var exists = await _context.Tasks.AnyAsync(x => x.TaskName.ToLower() == name.ToLower());
                if (exists)
                {
                    return null;
                }

                task.Id = Guid.NewGuid();
                task.TaskName = name;
                task.CreatedAt = DateTime.UtcNow;

                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
                return task;
            }
            catch
            {
                return null;
            }
        }

    }
}
