using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BL.TwilioSMSService
{
    public class SMSService : ISMSService
    {
        private readonly TwilioModel SMS;
        public SMSService(IOptions<TwilioModel> _sms)
        {
            this.SMS = _sms.Value;
        }
        public async Task<MessageResource> SendSMSAsync(string ReceiverPhone, string Body)
        {
            TwilioClient.Init(SMS.AccountSSID, SMS.Token);
            MessageResource result = await MessageResource.CreateAsync(
                body: Body,
                from: new Twilio.Types.PhoneNumber(SMS.Phone),
                to:ReceiverPhone
                );

            return result;
        }
    }
}
