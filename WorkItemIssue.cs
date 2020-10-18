using System;
using System.Collections.Generic;
using System.Text;
using YouTrackSharp.Json;
using YouTrackSharp.TimeTracking;

namespace YTL
{
    class WorkItemIssue
    {
        public WorkItemIssue(string IssueId, IEnumerable<WorkItem>  WorkItems)
        {
            this.IssueId = IssueId;
            this.WorkItems = WorkItems;
        }
        public string IssueId;
        public IEnumerable<WorkItem> WorkItems;
    }
}
