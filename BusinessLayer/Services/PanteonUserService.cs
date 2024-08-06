using CrossCutting.CoreModels;
using CrossCutting.Helpers;
using DataLayer.Repository.MsRepository.PanteonUserRepository;
using EntityLayer.PanteonEntity.MsEntity;
using Microsoft.IdentityModel.Tokens;


namespace BusinessLayer.BLL
{
    public class PanteonUserService
    {
        private readonly PanteonUserRepository _userRepository;


        public PanteonUserService()
        {
            _userRepository = new();
        }

        public async Task<OperationResult> RegisterAsync(PanteonUser newUser)
        {
            PanteonUser? existingUser = await _userRepository
                .GetActiveFirstOrDefaultAsync(u => u.Email == newUser.Email || u.UserName == newUser.UserName);

            if (existingUser is not null)
            {
                string errText = existingUser.UserName == newUser.UserName ? "Aynı Kullanıcı Adı İle Kayıtlı Hesap Bulunmaktadır" : "Aynı E-Posta Adresi İle Kayıtlı Hesap Bulunmaktadır";
                return new OperationResult
                {
                    IsValid = false,
                    Messages = new List<string>() { errText }
                };
            }
            /*Insert Öncesinde Password'de MD5 vs. hash işlemi uygulanabilir. Demo olduğundan dolayı direkt kayıt atıyoruz*/
            return await _userRepository.AddAsync(newUser);
        }

        public async Task<OperationResult> LoginAync(string userName, string password)
        {
            PanteonUser? currentUser = await _userRepository
                .GetActiveFirstOrDefaultAsync(
                    u => u.UserName == userName && u.Password == password);

            if (currentUser is null)
            {
                return new OperationResult
                {
                    IsValid = false,
                    Messages = new List<string>() { "Kullanıcı Adı Veya Şifre Hatalı" }
                };
            }

            if (currentUser.Reserved1.IsNullOrEmpty() || !TokenHelper.ValidateTokenTime(currentUser.Reserved1))
            {
                currentUser.Reserved1 = TokenHelper.GenerateToken();
                OperationResult tokenRes = await _userRepository.UpdateAsync(currentUser);
                tokenRes.Data = currentUser;
                return tokenRes;
            }

            return new OperationResult
            {
                IsValid = true,
                Data = currentUser
            };
        }
    }
}