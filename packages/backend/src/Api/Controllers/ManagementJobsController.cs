﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.ManagementJobs.Commands.CreateManagementJob;
using Application.ManagementJobs.Commands.StartManagementJob;
using Application.ManagementJobs.Commands.StopManagementJob;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class ManagementJobsController : ControllerBase
    {
        private readonly IMediator _Mediator;
        private readonly ICurrentUserService _CurrentUserService;

        public ManagementJobsController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _Mediator = mediator;
            _CurrentUserService = currentUserService;
        }

        [HttpPost]
        [Route("api/management-jobs")]
        [Authorize]
        public async Task<IActionResult> CreateManagementJob([FromBody] CreateManagementJobCommand command, CancellationToken cancellationToken)
        {
            await _Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut]
        [Route("api/management-jobs/{jobId}/start")]
        [Authorize]
        public async Task<IActionResult> StartManagementJob([FromRoute] Guid jobId, CancellationToken cancellationToken)
        {
            await _Mediator.Send(new StartManagementJobCommand { ManagementJobId = jobId }, cancellationToken);
            return Accepted();
        }

        [HttpPut]
        [Route("api/management-jobs/{jobId}/stop")]
        [Authorize]
        public async Task<IActionResult> StopManagementJob([FromRoute] Guid jobId, CancellationToken cancellationToken)
        {
            await _Mediator.Send(new StopManagementJobCommand { ManagementJobId = jobId}, cancellationToken);
            return Accepted();
        }
    }
}
