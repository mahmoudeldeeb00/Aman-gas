using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace BL.TwilioSMSService
{
    public interface ISMSService
    {
       Task<MessageResource> SendSMSAsync(string ReceiverPhone, string Body);
    }
}
