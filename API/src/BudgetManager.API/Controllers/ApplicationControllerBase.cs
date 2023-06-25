using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    public class ApplicationControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;
        public ApplicationControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
