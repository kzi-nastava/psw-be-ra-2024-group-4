using Explorer.API.DTOs;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Route("api/chatbot")]
    public class ChatbotController : BaseApiController
    {
        private readonly IChatbotService _chatbotService;


        public ChatbotController(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpPost("message")]
        public async Task<IActionResult> GetResponse([FromBody] ChatMessageDto messageDto)
        {
            if (string.IsNullOrEmpty(messageDto.Message))
            {
                return BadRequest("Message cannot be empty");
            }

            var response = await _chatbotService.GetResponseAsync(messageDto.Message);
            return Ok(new { response });
        }
    }
}
