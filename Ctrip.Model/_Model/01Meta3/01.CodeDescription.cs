using System.Collections.ObjectModel;
using System.Linq;

namespace Ctrip.Model
{
   public class CodeDescription
   {
       public string Code { get; set; }
       public string Description { get; set; }
       public string Category{get;set;}
    
       public CodeDescription(string code, string description, string category)
       {
             this.Code = code;
             this.Description = description;
             this.Category = category;
         }
     }
     public static class CodeManager
     {
         private static CodeDescription[] codes = new CodeDescription[]
         {
             new CodeDescription("M","Male","Gender"),
             new CodeDescription("F","Female","Gender"),
             new CodeDescription("S","Single","MaritalStatus"),
             new CodeDescription("M","Married","MaritalStatus"),
             new CodeDescription("CN","China","Country"),
             new CodeDescription("US","Unite States","Country"),
             new CodeDescription("UK","Britain","Country"),
             new CodeDescription("SG","Singapore","Country")
         };
         public static Collection<CodeDescription> GetCodes(string category)
         {
             Collection<CodeDescription> codeCollection = new Collection<CodeDescription>();
             foreach(var code in codes.Where(code=>code.Category == category))
             {
                 codeCollection.Add(code);
             }
             return codeCollection;
         }
     }
}
