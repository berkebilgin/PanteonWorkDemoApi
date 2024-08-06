using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using EntityLayer.PanteonEntity.MongoEntity.BaseEntity;
using FluentValidation;

namespace EntityLayer.PanteonEntity.MongoEntity
{
    public class PanteonBuilding : BaseEntityModel
    {

        public int BuildingCost { get; set; }

        public int ConstructionTime { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string BuildingTypeId { get; set; } 

    }

    public class BuildingValidator : AbstractValidator<PanteonBuilding>
    {
        public BuildingValidator()
        {
            RuleFor(building => building.BuildingTypeId)
                .NotEmpty().WithMessage("Bina Türü Seçilmelidir.");

            RuleFor(building => building.BuildingCost)
                .GreaterThan(0).WithMessage("Bina maaliyeti 0 veya daha düşük olamaz.");

            RuleFor(building => building.ConstructionTime)
                .InclusiveBetween(30, 1800).WithMessage("Yapım süresi minimum 30, maksimum 1800 saniye olmalıdır.");

            RuleFor(building => building.AddUserId)
                .GreaterThan(0).WithMessage("İşleme Devam Edilemiyor. Daha Sonra Tekrar Deneyin.");
        }
    }
}
