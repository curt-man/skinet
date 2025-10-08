using System.Runtime.Serialization;

namespace Skinet.Core.Entities.OrderAggregate;

public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    Pending,

    [EnumMember(Value = "Payment Received")]
    PaymentReceived,

    [EnumMember(Value = "Payment Failed")]
    PaymentFailed,

    [EnumMember(Value = "Payment Mismatch")]
    PaymentMismatch,
}
