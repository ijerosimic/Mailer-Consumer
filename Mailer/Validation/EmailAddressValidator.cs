using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AprossMailer.Validation
{
    public class EmailAddressValidator : IEmailAddressValidator
    {
        public List<string> _validMails { get; }
        public EmailAddressValidator()
        {
            _validMails = new List<string>();
        }

        public async Task<string> ValidateInput(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            var addresses = str.Split(',').ToList();

            foreach (var address in addresses)
            {
                var trimmedAddress = address.Trim();

                if (await IsStructureValid(trimmedAddress))
                    _validMails.Add(trimmedAddress);
            }

            if (_validMails.Count > 0)
                return string.Join(',', _validMails);

            return string.Empty;
        }

        public async Task<bool> IsStructureValid(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) return false;

            try
            {
                address = Regex.Replace(address, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(address,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
