using CrossCutting.CoreModels;
using DataLayer.Repository.MongoRepository.PanteonBuildingRepository;
using DataLayer.Repository.MongoRepository.PanteonBuildingTypeRepository;
using EntityLayer.PanteonEntity.MongoEntity;


namespace BusinessLayer.Services
{
    public class PanteonBuildingService
    {
        private readonly PanteonBuildingRepository _buildingRepository;
        private readonly PanteonBuildingTypeRepository _buildingTypeRepository;

        public PanteonBuildingService()
        {
            _buildingRepository = new();
            _buildingTypeRepository = new();
        }

        public async Task<OperationResult> AddBuildingAsync(PanteonBuilding newBuilding)
        {
            var buildingType = await _buildingTypeRepository.GetByIdAsync(newBuilding.BuildingTypeId);
            if (buildingType is null)
            {
                return new OperationResult
                {
                    IsValid = false,
                    Messages = new List<string>() { "Bina Tipini Kontrol Ediniz." }
                };
            }

            bool alreadyExists =
                await _buildingRepository.AnyAsync(x => x.BuildingTypeId == newBuilding.BuildingTypeId && x.AddUserId == newBuilding.AddUserId);

            if (alreadyExists)
            {
                return new OperationResult
                {
                    IsValid = false,
                    Messages = new List<string>() { "Seçilen Bina Tipi İle Mevcut Kayıt Bulunmaktadır." }
                };
            }
            return await _buildingRepository.AddAsync(newBuilding);
           
        }

        public async Task<object> GetBuildingTypeListByUserId(string id)
        {
            List<PanteonBuildingType> buildingTypeList = await _buildingTypeRepository.GetActiveAsync();
            List<PanteonBuilding> buildingList = await _buildingRepository.GetActiveAsync(x => x.AddUserId == int.Parse(id));
            List<string> usedBuildingTypeIds = buildingList
                .Select(b => b.BuildingTypeId)
                .Distinct()
                .ToList();

            return buildingTypeList.Select(i => new
            {
                Id = i.Id,
                IdString = i.IdString,
                Name = i.Name,
                Unused = !usedBuildingTypeIds.Contains(i.Id.ToString())
            }).ToList();
        }


        public async Task<List<PanteonBuilding>> GetBuildingListByUserId(string id)
        {
            List<PanteonBuilding> buildingList = await _buildingRepository.GetActiveAsync(x => x.AddUserId == int.Parse(id));
                return buildingList;
        }
    }
}