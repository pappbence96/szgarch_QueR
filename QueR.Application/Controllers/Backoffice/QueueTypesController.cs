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
    [Route("api/backoffice/[controller]")]
    [ApiController]
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
        [Authorize(Roles = "administrator")]
        [ProducesDefaultResponseType(typeof(QueueTypeDto))]
        public async Task<ActionResult<QueueTypeDto>> CreateQueueType([FromBody] QueueTypeModel model)
        {
            return Ok(await queueTypeService.CreateQueueType(model));
        }

        [HttpPut("{queueTypeId}")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult> UpdateQueueType(int queueTypeId, QueueTypeModel model)
        {
            await queueTypeService.UpdateQueueType(queueTypeId, model);
            return Ok();
        }
    }
}
