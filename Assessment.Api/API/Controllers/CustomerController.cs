using Assessment.Api.WebApi;
using Microsoft.AspNetCore.Http;

namespace Assessment.Api.API.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : BaseController
{
    private readonly Customer[] _customers =
    [
        new(){
            Id = 1,
            Name = "Price Salinas",
            Phone = "(732) 751-5143",
            Email = "nonummy.ipsum@icloud.net",
            PostalZip = "19476",
            Region = "Nord-Pas-de-Calais",
            Country = "South Africa"
        },
        new(){
            Id = 2,
            Name = "Melissa Rosario",
            Phone = "1-812-458-7857",
            Email = "libero@protonmail.org",
            PostalZip = "31X 6K6",
            Region = "Chernihiv oblast",
            Country = "South Africa"
        },
        new(){
            Id = 3,
            Name = "Zachery Terry",
            Phone = "(436) 663-8703",
            Email = "erat.etiam.vestibulum@yahoo.edu",
            PostalZip = "14899",
            Region = "Provence-Alpes-Côte d'Azur",
            Country = "Poland"
        },
        new(){
            Id = 4,
            Name = "Clare Pierce",
            Phone = "1-283-462-5146",
            Email = "pede.ac@outlook.com",
            PostalZip = "495462",
            Region = "Andaman and Nicobar Islands",
            Country = "Poland"
        },
        new()
        {
            Id = 5,
            Name = "Daria Scott",
            Phone = "1-334-811-8841",
            Email = "pharetra.sed@hotmail.couk",
            PostalZip = "7233",
            Region = "Illinois",
            Country = "Poland"
        },
        new(){
            Id = 6,
            Name = "David Berger",
            Phone = "(975) 293-1694",
            Email = "lectus.quis.massa@outlook.org",
            PostalZip = "55013",
            Region = "São Paulo",
            Country = "Vietnam"
        },
        new(){
            Id = 7,
            Name = "Roth Roberts",
            Phone = "1-228-722-3870",
            Email = "sed.pede.nec@outlook.com",
            PostalZip = "17880",
            Region = "Saskatchewan",
            Country = "China"
        },
        new(){
            Id = 8,
            Name = "Abbot Sanchez",
            Phone = "1-433-311-7451",
            Email = "id.risus@icloud.couk",
            PostalZip = "871408",
            Region = "Zhōngnán",
            Country = "Italy"
        },
        new(){
            Id = 9,
            Name = "Ralph Osborne",
            Phone = "1-618-529-6873",
            Email = "ornare.lectus@aol.net",
            PostalZip = "143564",
            Region = "Kahramanmaraş",
            Country = "Australia"
        },
        new(){
            Id = 10,
            Name = "Driscoll Randolph",
            Phone = "1-367-463-5790",
            Email = "sit@icloud.com",
            PostalZip = "72-132",
            Region = "West Region",
            Country = "Ukraine"
        }
    ];
    
    [HttpGet]
    [Authorize(Roles = PoliciesAndRoles.Roles.User, Policy = PoliciesAndRoles.Policies.CanRead)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<Customer>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllCustomersAsync()
    {
        try
        {
            return await Task.FromResult(Ok(_customers));
        }
        catch (Exception e)
        {
            return BadRequestActionResult(e.Message);
        }
    }
}