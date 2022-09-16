using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Tests
{
    public class ApiController : Controller
    {
        public const string CountryCodeHeaderName = "x-test-country-code";

        private readonly IRepository _repository;

        public ApiController(
            IRepository repository
        )
        {
            _repository = repository;
        }

        // Return UnauthorizedResult() or OkObjectResult(ICollection<Store>)
        public IActionResult GetStores()
        {
            if( !IsHeaderValid() )
                return Unauthorized();

            var userCountryCode = Request.Headers[CountryCodeHeaderName].FirstOrDefault();
            var storesTheUserHasAccessTo = _repository.GetStores(store => store.CountryCode == userCountryCode);
            return Ok(storesTheUserHasAccessTo);
        }

        // Return UnauthorizedResult(), NotFoundResult(), ForbidResult() or OkObjectResult(Store)
        public IActionResult GetStore(int storeId, bool includeCustomers = false)
        {
            if( !IsHeaderValid() )
                return Unauthorized();

            var userCountryCode = Request.Headers[CountryCodeHeaderName].FirstOrDefault();
            var storesTheUserHasAccessTo = _repository
                .GetStores(store => store.StoreId == storeId, includeCustomers)
                .ToList();

            if(!storesTheUserHasAccessTo.Any(store => store.StoreId == storeId))
                return NotFound();

            if(!storesTheUserHasAccessTo.Any(store => store.CountryCode == userCountryCode))
                return Forbid();

            return Ok(storesTheUserHasAccessTo.SingleOrDefault());
        }

        // Return UnauthorizedResult(), BadRequestResult() or OkObjectResult(Customer)
        public IActionResult CreateCustomer(Customer customer)
        {
            if( !IsHeaderValid() )
                return Unauthorized();

            if(!ModelState.IsValid)
                return BadRequest();

            var createdCustomer = _repository.AddCustomer(customer);
            return Ok(createdCustomer);
        }

        private bool IsHeaderValid()
        {
            return Request.Headers.ContainsKey(CountryCodeHeaderName)
                && Request.Headers[CountryCodeHeaderName].Count() <= 1
                && !String.IsNullOrWhiteSpace(Request.Headers[CountryCodeHeaderName]);
        }
    }

    public interface IRepository
    {
        ICollection<Store> GetStores(Func<Store, bool> filter, bool includeCustomers = false);
        ICollection<Customer> GetCustomers(int storeId);
        Customer AddCustomer(Customer customer);
    }

    public class Store
    {
        public int StoreId { get; set; }
        public string CountryCode { get; set; } = "";
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";
    }
}
