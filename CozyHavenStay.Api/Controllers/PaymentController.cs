using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CozyHavenStay.Api.Controllers
{
    //[Route("api/[controller]")]
    //public class PaymentController : ControllerBase
    //{
    //    private readonly IPaymentService _paymentsService;


    //    public PaymentController(
    //        IPaymentService paymentService
    //      )
    //    {
    //        _paymentsService = paymentService;
    //    }

    //    // Admin Endpoints
    //    [HttpPost("admin")]
    //    public async Task<ActionResult<Payment>> AddPayment(PaymentDTO payment)
    //    {

    //            var result = await _paymentsService.AddPayment(payment);
    //            return Ok(result);


    //    }

    //    [HttpDelete("admin/{paymentId}")]
    //    public async Task<ActionResult<Payment>> DeletePayment(int paymentId)
    //    {

    //            var result = await _paymentsService.DeletePayment(paymentId);
    //            return Ok(result);


    //    }

    //    [HttpPut("admin")]
    //    public async Task<ActionResult<Payment>> UpdatePayment(Payment payment)
    //    {

    //            var result = await _paymentsService.UpdatePayment(payment);
    //            return Ok(result);


    //    }

    //    [HttpGet("admin/{paymentId}")]
    //    public async Task<ActionResult<Payment>> GetPayment(int paymentId)
    //    {

    //            var result = await _paymentsService.GetPayment(paymentId);
    //            return Ok(result);

    //    }

    //    [HttpGet("admin")]
    //    public async Task<ActionResult<List<Payment>>> GetAllPayments()
    //    {

    //            var result = await _paymentsService.GetAllPayments();
    //            return Ok(result);

    //    }

    //    // Customer Endpoints
    //    [HttpGet("customer/{paymentId}")]
    //    public async Task<ActionResult<Payment>> GetPaymentForCustomer(int paymentId)
    //    {
    //          var result = await _paymentsService.GetPayment(paymentId);
    //            return Ok(result);

    //    }

    //    // Owner Endpoints
    //    //[HttpGet("GetAllPaymentsByOwner")]
    //    //public async Task<ActionResult<List<Payment>>> GetAllPaymentsByOwner(int ownerId)
    //    //{
    //    //    try
    //    //    {
    //    //        var result = await _paymentsService.GetAllPaymentsByOwner(ownerId);
    //    //        return Ok(result);
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        _logger.LogError(ex, $"Error occurred while retrieving payments for owner with ID: {ownerId}.");
    //    //        return StatusCode(500, "An error occurred while retrieving payments for the owner.");
    //    //    }
    //    //}
    //    //[HttpGet("GetAllPaymentsByUser")]
    //    //public async Task<ActionResult<List<Payment>>> GetAllPaymentByUser(int userId)
    //    //{
    //    //    try
    //    //    {
    //    //        // Call the service method to get payments by user
    //    //        var payments = await _paymentsService.GetAllPaymentByUser(userId);
    //    //        return Ok(payments);
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        // Log and handle exceptions
    //    //        return StatusCode(500, $"An error occurred while retrieving payments for user with ID {userId}: {ex.Message}");
    //    //    }
    //    //}
    //}
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentsService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentsService = paymentService;
        }

        // Admin Endpoints
        [HttpPost("create")]

        public async Task<ActionResult<Payment>> CreatePayment(PaymentDTO payment)
        {
            var result = await _paymentsService.AddPayment(payment);
            return Ok(result);
        }

        [HttpDelete("delete/{paymentId}")]

        public async Task<ActionResult<Payment>> DeletePayment(int paymentId)
        {
            var result = await _paymentsService.DeletePayment(paymentId);
            return Ok(result);
        }

        [HttpPut("update")]

        public async Task<ActionResult<Payment>> UpdatePayment(Payment payment)
        {
            var result = await _paymentsService.UpdatePayment(payment);
            return Ok(result);
        }

        [HttpGet("get/{paymentId}")]

        public async Task<ActionResult<Payment>> GetPayment(int paymentId)
        {
            var result = await _paymentsService.GetPayment(paymentId);
            return Ok(result);
        }

        [HttpGet("get-all")]

        public async Task<ActionResult<List<Payment>>> GetAllPayments()
        {
            var result = await _paymentsService.GetAllPayments();
            return Ok(result);
        }

        // Customer Endpoints
        [HttpGet("customer/get/{paymentId}")]

        public async Task<ActionResult<Payment>> GetPaymentForCustomer(int paymentId)
        {
            var result = await _paymentsService.GetPayment(paymentId);
            return Ok(result);
        }

        //    // Owner Endpoints
        //    [HttpGet("owner/get-all/{ownerId}")]
        //    [Authorize(Roles = "owner")]
        //    public async Task<ActionResult<List<Payment>>> GetAllPaymentsByOwner(int ownerId)
        //    {
        //        try
        //        {
        //            var result = await _paymentsService.GetAllPaymentsByOwner(ownerId);
        //            return Ok(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, $"Error occurred while retrieving payments for owner with ID: {ownerId}.");
        //            return StatusCode(500, "An error occurred while retrieving payments for the owner.");
        //        }
        //    }

        //    [HttpGet("user/get-all/{userId}")]
        //    [Authorize(Roles = "user")]
        //    public async Task<ActionResult<List<Payment>>> GetAllPaymentsByUser(int userId)
        //    {
        //        try
        //        {
        //            var payments = await _paymentsService.GetAllPaymentByUser(userId);
        //            return Ok(payments);
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(500, $"An error occurred while retrieving payments for user with ID {userId}: {ex.Message}");
        //        }
        //    }
        //}
    }
}

