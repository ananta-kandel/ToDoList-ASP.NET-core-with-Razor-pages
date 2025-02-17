namespace ToDoList.Models
{
    public class Filters
    {
       public Filters(string filterstring){
        Filterstring = filterstring ?? "all-all-all";
        string [] filters = Filterstring.Split('-');
        CategoryId = filters[0];
        Due = filters[1];
        StatusId = filters[2];
       }
       public string Filterstring{get;}
       public string CategoryId{get;}
       public string Due{get;}
       public string StatusId{get;}

       public bool HasCategory => CategoryId.ToLower() != "all";
       public bool HasDue => Due.ToLower() != "all";

       public bool HasStatus => StatusId.ToLower() != "all";

       public static Dictionary<string, string> DueFilterValues => 
       new Dictionary<string, string>{
       {"future","future"},
       {"past","past"},
       {"today","today"}
       };
       public bool IsPast => Due.ToLower() == "past";
       public bool IsFuture => Due.ToLower() == "future";

       public bool IsToday => Due.ToLower()== "today";
    }
}