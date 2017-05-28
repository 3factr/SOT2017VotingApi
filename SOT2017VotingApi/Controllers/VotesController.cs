using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SOT2017VotingApi.Controllers
{
    [Route("api/[controller]")]
    public class VotesController : Controller
    {
        [HttpGet]
        public IReadOnlyDictionary<string, int> Get() => VoteStorage.Instance.GetVotes();
        
        [HttpPost]
        public IActionResult Post([FromBody] string value) => VoteStorage.Instance.Add(value) ? (IActionResult) Ok() : BadRequest();
    }
}