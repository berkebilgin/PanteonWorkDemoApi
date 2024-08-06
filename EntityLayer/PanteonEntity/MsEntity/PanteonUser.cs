using EntityLayer.PanteonEntity.MsEntity.BaseEntity;

namespace EntityLayer.PanteonEntity.MsEntity

{
    public class PanteonUser : BaseEntityModel
    {
        public string UserName { get; set;}

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
