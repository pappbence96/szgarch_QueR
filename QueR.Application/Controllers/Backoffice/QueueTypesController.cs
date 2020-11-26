using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueR.Application.Middlewares.ExceptionHandling;
using QueR.BLL.Services.Company;
using QueR.BLL.Services.QueueType;
using QueR.BLL.Services.QueueType.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.Controllers.Backoffice
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "administrator")]
    [ApiExplorerSettings(GroupName = "backoffice")]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    public class QueueTypesController : ControllerBase
    {
        private readonly IQueueTypeService queueTypeService;

        public QueueTypesController(IQueueTypeService queueTypeService)
        {
            this.queueTypeService = queueTypeService;
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(QueueTypeDto))]
        public async Task<ActionResult<QueueTypeDto>> CreateQueueType([FromBody] QueueTypeModel model)
        {
            return Ok(await queueTypeService.CreateQueueType(model));
        }

        [HttpPut("{queueTypeId}")]
        public async Task<ActionResult> UpdateQueueType(int queueTypeId, QueueTypeModel model)
        {
            await queueTypeService.UpdateQueueType(queueTypeId, model);
            return Ok();
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(IEnumerable<QueueTypeDto>))]
        public async Task<ActionResult<IEnumerable<QueueTypeDto>>> GetQueueTypes()
        {
            return Ok(await queueTypeService.GetQueueTypes());
        }
    }
}
