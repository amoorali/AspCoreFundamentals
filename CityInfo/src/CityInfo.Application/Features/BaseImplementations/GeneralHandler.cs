using CityInfo.Application.Services.Contracts;
using MapsterMapper;

namespace CityInfo.Application.Features.BaseImplementations
{
    public class GeneralHandler
    {
        #region [ Fields ]
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IMailService MailService;
        protected readonly IPropertyCheckerService PropertyCheckerService;
        protected readonly IPropertyMappingService PropertyMappingService;
        #endregion

        #region [ Constructor ]
        public GeneralHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMailService mailService,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService)
        {
            UnitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            Mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            MailService = mailService ??
                throw new ArgumentNullException(nameof(mailService));
            PropertyCheckerService = propertyCheckerService ??
                throw new ArgumentNullException(nameof(propertyCheckerService));
            PropertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));
        }
        #endregion
    }
}
