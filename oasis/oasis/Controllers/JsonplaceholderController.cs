using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using oasis.DTOs;
using System.Collections.Generic;
using System.Text.Json;
using oasis.Extensions;
using oasis.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace oasis.Controllers
{
    public class JsonplaceholderController : BaseApiController
    {
        static readonly HttpClient client = new HttpClient();

        // get Jsonplaceholder with httpClient

        [HttpGet("GetJsonplaceholder")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult> GetJsonplaceholder(int pageNum)
        {
            string url = "https://jsonplaceholder.typicode.com/todos";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                List<JsonplaceholderDto> responseData = JsonSerializer.Deserialize<List<JsonplaceholderDto>>(responseContent);
                PageinationHeaderHolders pag = new();
                pag.Data = responseData.Skip(pageNum - 1).Take(10);
                pag.CurrentPage = pageNum;
                pag.TotalPages =(int)Math.Ceiling((decimal)responseData.Count / 10);
                pag.TotalItems = responseData.Count;
                return Ok(new ApiResponseMessageDto()
                {
                    Date = pag,
                    StatusCode = 200,
                    IsSuccess = true,
                });
            }
            else
            {
                return BadRequest("Error !");
            }

        }
    }
}
