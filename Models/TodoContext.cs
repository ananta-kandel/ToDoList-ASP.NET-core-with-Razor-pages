using  ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
namespace Todo.Models
{
    
    public class ToDoContext : DbContext{
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options){
        }
          public DbSet<Todoo> Todos {get;set;} = null!;
          public DbSet<Category> Categories{get;set;}=null!;

          public DbSet<Status> Statues{get;set;} = null!;

          protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Category>().HasData(
                new Category {CategoryId ="Work" , Name  = "Work"},
                new Category {CategoryId ="Home" , Name  = "Home"},
                new Category {CategoryId ="ex" , Name  = "ex"},
                new Category {CategoryId ="shop" , Name  = "Shop"},
                new Category {CategoryId ="call" , Name  = "call"}
            );
             modelBuilder.Entity<Status>().HasData(
                new Status {StatusId ="Open" , Name  = "Open"},
                new Status {StatusId ="Closed" , Name  = "Close"}
            );
          }
        }
    }
