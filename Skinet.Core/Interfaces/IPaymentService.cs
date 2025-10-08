using System;
using Skinet.Core.Entities;

namespace Skinet.Core.Interfaces;

public interface IPaymentService
{
    Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId);
}
