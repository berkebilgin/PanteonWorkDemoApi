using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PanteonWorkDemo.Models.Base
{
    [DataContract]
    public class ApiResult
    {
        public ApiResult()
        {
            ErrorMessages = new List<string>();
            SuccessMessages = new List<string>();
            WarningMessages = new List<string>();
        }

        public bool IsValid { get; set; }

        public List<string> ErrorMessages { get; set; }

        public List<string> WarningMessages { get; set; }

        public List<string> SuccessMessages { get; set; }

        public object Model { get; set; }

    }
}