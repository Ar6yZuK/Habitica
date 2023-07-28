using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitica;
public class Credentials
{
    public string UserId { get; }

    public string ApiKey { get; }
    public Credentials(string userId, string apiKey)
    {
        ApiKey = apiKey;
        UserId = userId;
    }
}