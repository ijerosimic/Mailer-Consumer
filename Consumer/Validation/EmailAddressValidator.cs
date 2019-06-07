using Consumer.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Consumer
{
    public class EmailAddressValidator : IEmailAddressValidator
    {
        public List<string> ValidateAndProcessEmailAddress(string str)
        {
            var validMails = new List<string>();

            if (string.IsNullOrWhiteSpace(str) == false)
            {
                var addresses = str.Split(',').ToList();

                foreach (var address in addresses)
                {
                    var trimmedAddress = address.Trim();

                    if (IsValidEmail(trimmedAddress))
                        validMails.Add(trimmedAddress);
                }

                return validMails;
            }

            return new List<string>();
        }

        public bool IsValidEmail(string address)
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
