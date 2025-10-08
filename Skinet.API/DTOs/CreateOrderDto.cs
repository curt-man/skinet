using System.ComponentModel.DataAnnotations;
using Skinet.Core.Entities.OrderAggregate;

namespace Skinet.API.DTOs;

public class CreateOrderDto
{
    [Required]
    public string CartId { get; set; } = string.Empty;

    [Required]
    public int DeliveryMethodId { get; set; }

    [Required]
    public ShippingAddress ShippingAddress { get; set; } = default!;

    [Required]
    public PaymentSummary PaymentSummary { get; set; } = default!;
}
