using CityInfo.Application.Services.Contracts;

namespace CityInfo.Application.Features.BaseImplementations
{
    public class GeneralHandler
    {
        #region [ Fields ]
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMailService MailService;
        protected readonly IPropertyCheckerService PropertyCheckerService;
        protected readonly IPropertyMappingService PropertyMappingService;
        #endregion

        #region [ Constructor ]
        public GeneralHandler(
            IUnitOfWork unitOfWork,
            IMailService mailService,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService)
        {
            UnitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
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
