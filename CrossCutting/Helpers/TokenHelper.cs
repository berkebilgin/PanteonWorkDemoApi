using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Helpers
{
    //Demo Çalışma Olmasından Dolayı Sadece Temel Şablonlar Oluşturuldu. JWT TOKEN ÜZERİNDEN İÇERİKLER DOLDURULABİLİR.
    public static class TokenHelper
    {
        public static bool ValidateToken(string token)
        {
            return true;
        }

        public static bool ValidateTokenTime(string token)
        {
            return false;
        }

        public static int GetUserIdFromToken(string token)
        {
            return int.Parse(token);
        }

        public static string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
