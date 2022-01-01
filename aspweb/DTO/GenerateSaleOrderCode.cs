using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspweb.DTO
{
    public class GenerateSaleOrderCode
    {
        public static string Code()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");
            return GuidString;
        }
    }
}