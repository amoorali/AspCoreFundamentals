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
        #endregion

        #region [ Constructor ]
        public GeneralHandler(IUnitOfWork unitOfWork, IMapper mapper, IMailService mailService,
            IPropertyCheckerService propertyCheckerService)
        {
            UnitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            Mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            MailService = mailService ??
                throw new ArgumentNullException(nameof(mailService));
            PropertyCheckerService = propertyCheckerService;
        }
        #endregion
    }
}
