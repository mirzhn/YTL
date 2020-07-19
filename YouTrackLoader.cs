using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YouTrackSharp;

namespace YTL
{
    class YouTrackLoader
    {
        private BearerTokenConnection connection { get; set; }

        public YouTrackLoader(string url, string token)
        {
            this.connection = new BearerTokenConnection(url, token);
        }

        public async Task<ICollection<YouTrackSharp.Projects.Project>> getProject()
        {
            var projectsService = connection.CreateProjectsService();
            return await projectsService.GetAccessibleProjects();
        }

        public async Task<ICollection<YouTrackSharp.Issues.Issue>> getIssues(string filter)
        {
            var issueService = this.connection.CreateIssuesService();
            long issueCount = await issueService.GetIssueCount(filter);
            var issues = await issueService.GetIssues(filter, 0, Convert.ToInt32(issueCount));
            return issues;
        }

    }
}
