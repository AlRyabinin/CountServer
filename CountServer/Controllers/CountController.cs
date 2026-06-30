using Microsoft.AspNetCore.Mvc;

namespace CountServer.Controllers
{
    /// <summary>
    /// API-контроллер для работы со счётчиком <c>count</c> из <see cref="CountServer"/>.
    /// Предоставляет эндпоинты для чтения текущего значения и добавления к нему.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CountController : ControllerBase
    {
        /// <summary>
        /// Возвращает текущее значение счётчика <c>count</c>.
        /// </summary>
        /// <returns>
        /// Ответ <c>200 OK</c> с объектом <c>{ count: int }</c>, содержащим текущее значение счётчика.
        /// </returns>
        /// <response code="200">Значение успешно получено.</response>
        [HttpGet]
        [ProducesResponseType(typeof(object), 200)]
        public IActionResult GetCount()
        {
            int currentCount = CountServer.GetCount();
            return Ok(new { count = currentCount });
        }

        /// <summary>
        /// Прибавляет значение из тела запроса к счётчику <c>count</c> и возвращает обновлённый результат.
        /// </summary>
        /// <param name="request">Тело запроса, содержащее прибавляемое значение.</param>
        /// <returns>
        /// Ответ <c>200 OK</c> с сообщением об успехе и новым значением счётчика,
        /// либо <c>400 Bad Request</c>, если тело запроса некорректно.
        /// </returns>
        /// <response code="200">Значение успешно добавлено.</response>
        /// <response code="400">Некорректное тело запроса.</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        public IActionResult AddToCount([FromBody] AddCountRequest request)
        {
            if(request == null)
            {
                return BadRequest("Invalid request body");
            }

            CountServer.AddToCount(request.Value);
            int newCount = CountServer.GetCount();
            return Ok(new { message = "Value added successfully", newCount = newCount });
        }
    }

    /// <summary>
    /// DTO (Data Transfer Object) для запроса на добавление значения к счётчику.
    /// </summary>
    public class AddCountRequest
    {
        /// <summary>
        /// Значение, которое необходимо прибавить к счётчику <c>count</c>.
        /// </summary>
        /// <example>5</example>
        public int Value { get; set; }
    }
}