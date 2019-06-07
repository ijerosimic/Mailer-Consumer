using AprossMailer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprossMailer.Validation
{
    public class ValidationManager : IValidationManager
    {
        public IApiRepository _repo { get; }
        public IContentValidator _contentValidator { get; }
        public IEmailAddressValidator _emailAddressValidator { get; }
        public ValidationManager(IApiRepository repo, IContentValidator contentValidator, IEmailAddressValidator emailAddressValidator)
        {
            _repo = repo;
            _contentValidator = contentValidator;
            _emailAddressValidator = emailAddressValidator;
        }

        public async Task<bool> IsValidGUID(Guid guid)
        {
            return await _repo.IsValidGUID(guid);
        }

        public async Task<bool> IsValidContent(Message message)
        {
            return await _contentValidator.IsValidContent(message);
        }

        public async Task<string> ValidateRecipientAddresses(string address)
        {
            return await _emailAddressValidator.ValidateInput(address);
        }
    }
}
