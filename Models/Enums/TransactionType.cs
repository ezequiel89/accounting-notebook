using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.Enums
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionType
    {
        [EnumMember(Value = "credit")]
        Credit = 1,
        [EnumMember(Value = "debit")]
        Debit = 2
    }
}
