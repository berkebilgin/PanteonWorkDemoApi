using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.CoreModels
{
    /*Alternatif Olarak DataLayer'da CRUD Result Wrapper Model Olarak Tanımlanarak Tüm Katmanlarda Kullanılabilir*/
    public class OperationResult
    {
        public bool IsValid { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public object? Data { get; set; }
    }
}
