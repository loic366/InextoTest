namespace Inexto.Mappers
{
    using AutoMapper;

    using Inexto.Models;

    public class InextoMapper : Profile
    {
        /// <summary>
        /// Mapper configured with the Inexto profile, singleton since it's a small project
        /// </summary>
        private static IMapper? mapper = null;

        public InextoMapper()
        {
            this.CreateMap<FluentValidation.Results.ValidationFailure, Error>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.ErrorCode))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.ErrorMessage));
        }

        /// <summary>
        /// Create a configured AutoMapper instance with the Inexto mappings
        /// </summary>
        /// <remarks>This is usually not needed, but since this project is not using DI</remarks>
        /// <returns></returns>
        public static IMapper GetMapper()
        {
            if (mapper != null)
            {
                return mapper;
            }

            var config = new MapperConfiguration(cfg => cfg.AddProfile<InextoMapper>());
            mapper = config.CreateMapper();
            return mapper;
        }
    }
}
