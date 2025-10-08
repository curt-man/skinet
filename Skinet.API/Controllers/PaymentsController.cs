using System;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Skinet.API.Extensions;
using Skinet.API.SignalR;
using Skinet.Core.Entities;
using Skinet.Core.Entities.OrderAggregate;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using Skinet.Infrastructure.Configurations;
using Stripe;

namespace Skinet.API.Controllers;

public class PaymentsController(
    IPaymentService paymentService,
    IUnitOfWork uow,
    ILogger<PaymentsController> logger,
    IOptions<StripeSettings> stripeSettings,
    IHubContext<NotificationHub> hubContext) : BaseApiController
{
    private readonly string _whSecret = stripeSettings.Value.WhSecret!;

    [Authorize]
    [HttpPost("{cartId}")]
    public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId)
    {
        var cart = await paymentService.CreateOrUpdatePaymentIntent(cartId);

        if (cart == null) return BadRequest("Problem with your cart");

        return Ok(cart);
    }

    [HttpGet("delivery-methods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        var deliveryMethods = await uow.Repository<DeliveryMethod>().ListAllAsync();
        return Ok(deliveryMethods);
    }

    [HttpPost("Webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = ConstructStripeEvent(json);

            if(stripeEvent.Data.Object is not PaymentIntent intent)
            {
                return BadRequest("Invalid event data");
            }
            await HandlePaymentIntentSucceeded(intent);

            return Ok();
        }
        catch (StripeException ex)
        {
            logger.LogError(ex, "Stripe webhook error occured");
            return StatusCode(StatusCodes.Status500InternalServerError, "Stripe webhook error occured");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occured");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
        }
    }

    private async Task HandlePaymentIntentSucceeded(PaymentIntent intent)
    {
        if(intent.Status == "succeeded")
        {
            var specification = new OrderSpecification(intent.Id, true);
            var order = await uow.Repository<Order>().GetEntityWithSpec(specification)
                ?? throw new Exception("Order not found");

            if((long)order.GetTotal() * 100 != intent.Amount)
            {
                order.Status = OrderStatus.PaymentMismatch;
            }
            else 
            {
                order.Status = OrderStatus.PaymentReceived;
            }
            await uow.Complete();

            var connectionId = NotificationHub.GetConnectionIdByEmail(order.BuyerEmail);
            if(!string.IsNullOrEmpty(connectionId))
            {
                await hubContext.Clients.Client(connectionId).SendAsync("OrderCompleteNotification", order.ToDto());
            }
        }
    }

    private Event ConstructStripeEvent(string json)
    {
        try
        {
            return EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to construct stripe event");
            throw new StripeException("Invalid signature");
        }
    }
}