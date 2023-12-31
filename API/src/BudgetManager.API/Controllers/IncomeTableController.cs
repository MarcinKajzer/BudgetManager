﻿using BudgetManager.Application.IncomesTable.Queries;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeTableController : ApplicationControllerBase
    {
        public IncomeTableController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<IActionResult> Get(int Year, int Month, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetIncomeTableQuery(Year, Month), cancellationToken));
        }
    }
}
