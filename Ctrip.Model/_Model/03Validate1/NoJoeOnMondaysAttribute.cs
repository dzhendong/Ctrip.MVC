using System.ComponentModel.DataAnnotations;

namespace Ctrip.Model
{
    /// <summary>
    /// 自定义 Model 级验证特性
    /// </summary>
    public class NoJoeOnMondaysAttribute : ValidationAttribute
    {
        public NoJoeOnMondaysAttribute()
        {
            ErrorMessage = "Joe cannot book appointments on Mondays";
        }

        public override bool IsValid(object value)
        {
            //Appointment app = value as Appointment;
            //if (app == null || string.IsNullOrEmpty(app.ClientName) || app.Date == null)
            //{
            //    return true;
            //}
            //else
            //{
            //    return !(app.ClientName == "Joe" && app.Date.DayOfWeek == DayOfWeek.Monday);
            //}

            return true;
        }
    }   
}