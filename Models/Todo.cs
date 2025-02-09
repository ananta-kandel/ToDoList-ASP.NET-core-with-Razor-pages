using System.ComponentModel.DataAnnotations;
using ToDoList.Models;

namespace ToDoList.Models
{
    public class Todoo{
        public int Id{get;set;}
        
        [Required(ErrorMessage="please enter a descriptions.")]
        public string Descriptions{get;set;}

        // [Required(ErrorMessage="please Enter a due date.")]

        public DateTime? DueDate{get;set;}
        // [Required(ErrorMessage="please select a category")]
        public string CategoryId{get;set;}

 
        public Category category{get;set;}
        // [Required(ErrorMessage="please select a Status")]
        public string StatusId{get;set;}
        
      
         public Status status{get;set;} 

         public bool Overdue => StatusId == "open" && DueDate < DateTime.Today;
    }
}