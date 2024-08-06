using AutoMapper;
using EntityLayer.PanteonEntity.MsEntity;
using PanteonWorkDemo.Models;

namespace PanteonWorkDemo.Helpers
{
    public static class MapperHelper
    {
        static IMapper staticMapper;

        private static MapperConfiguration GetMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegisterModel, PanteonUser>();
            });

            return config;
        }

        public static object MapFrom(object entity, System.Type destionationType, System.Type sourceType = null)
        {
            var config = GetMapperConfiguration();

            staticMapper = config.CreateMapper();

            if (sourceType == null)
            {
                sourceType = entity.GetType();
            }

            return staticMapper.Map(entity, sourceType, destionationType);
        }

        public static object MapFrom(object source, object destination)
        {
            var config = GetMapperConfiguration();
            staticMapper = config.CreateMapper();

            return staticMapper.Map(source, destination);
        }
    }

    class MyMapper
    {
        IMapper mapper;

        public MyMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public T MapFrom<T>(object entity)
        {
            return mapper.Map<T>(entity);
        }
    }
}
