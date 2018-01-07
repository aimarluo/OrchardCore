using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using OrchardCore.Entities;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Models;

namespace OrchardCore.Workflows.Services
{
    public interface IActivity : IEntity
    {
        string Name { get; }
        LocalizedString Category { get; }
        LocalizedString Description { get; }
        new JObject Properties { get; set; }

        /// <summary>
        /// List of possible outcomes when the activity is executed.
        /// </summary>
        IEnumerable<Outcome> GetPossibleOutcomes(WorkflowContext workflowContext, ActivityContext activityContext);

        /// <summary>
        /// Whether the activity can transition to the next outcome. Can prevent the activity from being transitioned
        /// because a condition is not valid.
        /// </summary>
        Task<bool> CanExecuteAsync(WorkflowContext workflowContext, ActivityContext activityContext);

        /// <summary>
        /// Executes the current activity.
        /// </summary>
        /// <returns>The names of the resulting outcomes.</returns>
        Task<IEnumerable<string>> ExecuteAsync(WorkflowContext workflowContext, ActivityContext activityContext);

        /// <summary>
        /// Called on each activity when a workflow is about to start.
        /// </summary>
        Task OnWorkflowStartingAsync(WorkflowContext context, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Called on each activity when a workflow has started.
        /// </summary>
        Task OnWorkflowStartedAsync(WorkflowContext context);

        /// <summary>
        /// Called on each activity when a workflow is about to be resumed.
        /// </summary>
        Task OnWorkflowResumingAsync(WorkflowContext context, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Called on each activity when a workflow is resumed.
        /// </summary>
        Task OnWorkflowResumedAsync(WorkflowContext context);

        /// <summary>
        /// Called on each activity when an activity is about to be executed.
        /// </summary>
        Task OnActivityExecutingAsync(WorkflowContext workflowContext, ActivityContext activityContext, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Called on each activity when an activity has been executed.
        /// </summary>
        Task OnActivityExecutedAsync(WorkflowContext workflowContext, ActivityContext activityContext);
    }
}