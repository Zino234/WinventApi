using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Winvent.Domain.Models.Enums;

namespace Winvent.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ServiceType
    {
        [EnumMember(Value = "MidweekService")]
        MidWeekService,


        [EnumMember(Value = "SundayService")]
        SundayService
    }
}

