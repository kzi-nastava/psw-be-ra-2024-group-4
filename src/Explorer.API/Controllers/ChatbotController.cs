using Explorer.API.DTOs;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/chatbot/")]
    public class ChatbotController : BaseApiController
    {
        private readonly IChatbotService _chatbotService;


        public ChatbotController(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpGet("message/{userid:int}")]
        public ActionResult<string> GetResponse([FromQuery] string message, long userid)
        {
            if (string.IsNullOrEmpty(message))
            {
                return BadRequest(new { message = "Message cannot be empty" });
            }

            var result =  _chatbotService.GetResponse(message, userid);
            return Ok(new { message = result });
        }

        [HttpGet("questions")]
        public ActionResult<List<string>> GetQuestions([FromQuery] string tag)
        {
            var result = _chatbotService.GetQuestionSet(tag);
            return Ok(new { questions = result });
        }
    }
}
