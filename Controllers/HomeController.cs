using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace ToDoList.Controllers{

public class HomeController : Controller
{
    private ToDoContext context;

    public HomeController(ToDoContext toDoContext){
        context = toDoContext;
    }
    public IActionResult Index(string id)
    {
        var filters = new Filters(id);
        ViewBag.Filters = filters;
        ViewBag.Categories = context.Categories.ToList();
        ViewBag.Statues = context.Statues.ToList();
        ViewBag.DueFilters = Filters.DueFilterValues;
        
         IQueryable<Todoo> query = context.Todos
            .Include(t=> t.category)
            .Include(t=>t.status);
        
        if(filters.HasCategory){
            query = query.Where(t=> t.CategoryId == filters.CategoryId);
        }

        if(filters.HasStatus){
            query = query.Where(t=> t.StatusId== filters.StatusId);
        }
        if(filters.HasDue){
           var today = DateTime.Today;
           if(filters.IsPast){
            query = query.Where(t => t.DueDate < today);
           }
           else if(filters.IsFuture){
            query = query.Where(t => t.DueDate> today);

           } else if(filters.IsToday){
            query = query.Where(t => t.DueDate == today);
           }
        }
        var tasks = query.OrderBy(t => t.DueDate).ToList();
        return View(tasks);
    }
    [HttpGet]
    [HttpGet]
public IActionResult Add()
{
    ViewBag.Categories = context.Categories.ToList();
    ViewBag.Statues = context.Statues.ToList();
    var task = new Todoo { StatusId = "open" };
    return View(task);
}
    
    [HttpPost]
    public IActionResult Add(Todoo task){
        if(ModelState.IsValid){
            context.Todos.Add(task);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        else{
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statues = context.Statues.ToList();
            return View(task);
        }
    }
    [HttpPost]
    public IActionResult Filter(string[] filter){
        string id = string.Join('-',filter);
        return RedirectToAction("Index", new { ID = id});
    }

    [HttpPost]
    public IActionResult MarkComplete([FromRoute]string id, Todoo selected){
        selected = context.Todos.Find(selected.Id)!;
        if(selected != null){
            selected.StatusId = "closed";
            context.SaveChanges();
        }
       return RedirectToAction("Index", new {ID =id}); 
    }

    [HttpPost]
    public IActionResult DeleteComplete(string id){
  var toDelete = context.Todos.Where(t => t.StatusId == "closed").ToList();

        foreach(var task in toDelete){
            context.Todos.Remove(task);
        }
        context.SaveChanges();
        return RedirectToAction("Index", new {ID = id});
    }
}
}

