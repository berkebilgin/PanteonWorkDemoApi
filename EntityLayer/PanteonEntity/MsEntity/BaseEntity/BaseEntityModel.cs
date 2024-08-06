using System.ComponentModel.DataAnnotations;


namespace EntityLayer.PanteonEntity.MsEntity.BaseEntity
{
    public abstract class BaseEntityModel
    {
        [Key]
        public int Id { get; set; }

        public int ObjectStatus { get; set; }

        public string? Reserved1 { get; set; }

    }
}
