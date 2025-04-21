using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelSnab.Domain.Commons.Request;

public class SendLinkJitsiRequest
{
    public string Link {  get; set; }
    public string Email { get; set; }
}
