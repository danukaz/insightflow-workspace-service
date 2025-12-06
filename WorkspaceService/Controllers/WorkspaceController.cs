using Microsoft.AspNetCore.Mvc;
using WorkspaceService.DTOs;
using WorkspaceService.Services;

namespace WorkspaceService.Controllers
{
    [ApiController]
    [Route("api/workspaces")]
    public class WorkspaceController : ControllerBase
    {
        private readonly WorkspaceServiceImpl _service;

        public WorkspaceController(WorkspaceServiceImpl service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateWorkspaceDto dto)
        {
            var ws = _service.Create(dto);
            return Ok(ws);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(Guid userId)
        {
            return Ok(_service.GetByUser(userId));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var ws = _service.GetById(id);
            return ws == null ? NotFound() : Ok(ws);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Guid requesterId, [FromBody] UpdateWorkspaceDto dto)
        {
            return Ok(_service.Update(id, dto, requesterId));
        }

        [HttpDelete("{id}")]
        public IActionResult SoftDelete(Guid id, Guid requesterId)
        {
            _service.SoftDelete(id, requesterId);
            return NoContent();
        }
    }
}
