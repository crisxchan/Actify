using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetActivities(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new List.Query(), cancellationToken);

            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new Details.Query{Id = id}, cancellationToken);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new Create.Command{Activity = activity}, cancellationToken);

            return HandleResult(result);
        } 

        [Authorize(Policy = "IsActivityHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity, CancellationToken cancellationToken)
        {
            activity.Id = id;
            var result = await Mediator.Send(new Edit.Command{Activity = activity}, cancellationToken);
            
            return HandleResult(result);
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new Delete.Command{Id = id}, cancellationToken); 
            return HandleResult(result);
        }

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> UpdateAttendance(Guid id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new UpdateAttendance.Command{Id = id}, cancellationToken);
            return HandleResult(result);
        }
    }
}