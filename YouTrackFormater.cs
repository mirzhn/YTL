using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouTrackSharp;

namespace YTL
{

    static class YouTrackFormater
    {
        public static List<string[]> getProjectList(ICollection<YouTrackSharp.Projects.Project> projects)
        {
            List<string[]> ProjectList = new List<string[]>();
            ProjectList.Add(new string[] { "Name", "ShortName" });

            foreach (YouTrackSharp.Projects.Project project in projects)
            {
                ProjectList.Add(new string[] { project.Name, project.ShortName });
            }

            return ProjectList;
        }

        public static List<string[]> getUserStoryTagList(ICollection<YouTrackSharp.Issues.Issue> Issues)
        {
            List<string[]> UserStoryTagList = new List<string[]>();
            UserStoryTagList.Add(new string[] { "UserStory", "Tag" });

            foreach (YouTrackSharp.Issues.Issue issue in Issues)
            {
                foreach (SubValue<string> tag in issue.Tags)
                {
                    UserStoryTagList.Add(new string[] {issue.Id, tag.Value });
                }
            }

            return UserStoryTagList;
        }

        public static List<string[]> getUserStorySprintList(ICollection<YouTrackSharp.Issues.Issue> Issues)
        {
            List<string[]> UserStorySprintList = new List<string[]>();
            UserStorySprintList.Add(new string[] { "UserStory", "Sprint" });

            foreach (YouTrackSharp.Issues.Issue issue in Issues)
            {
                var sprints = issue.Fields.Where(x => x.Name == "Sprints").Select(x => x.Value);
                foreach (List<string> sprint in sprints)
                {
                    foreach(string current in sprint)
                    {
                        UserStorySprintList.Add(new string[] { issue.Id, current});
                    }
                    
                }
            }
            return UserStorySprintList;
        }

        public static List<string[]> getUserStoryWorkItemList(List<WorkItemIssue> WorkItemIssues)
        {
            List<string[]> UserStoryWorkItemList = new List<string[]>();
            UserStoryWorkItemList.Add(new string[] { "UserStory", "WorkItemId", "Date", "Created", "Author", "DurationlMinutes" });

            foreach (WorkItemIssue WorkItemIssue in WorkItemIssues)
            {
                var UserStory = WorkItemIssue.IssueId;

                foreach (YouTrackSharp.TimeTracking.WorkItem WorkItem in WorkItemIssue.WorkItems)
                {
                    UserStoryWorkItemList.Add(new string[] { 
                        UserStory, 
                        WorkItem.Id, 
                        WorkItem.Date.Value.ToString(), 
                        WorkItem.Created.Value.ToString(),
                        WorkItem.Author.Login,
                        WorkItem.Duration.TotalMinutes.ToString()

                    });
                }
            }
            return UserStoryWorkItemList;
        }

        public static List<string[]> getUserStoryLinkList(ICollection<YouTrackSharp.Issues.Issue> Issues)
        {
            List<string[]> UserStoryLinkList = new List<string[]>();
            UserStoryLinkList.Add(new string[] { "UserStory", "UserStoryLink", "TypeLink", "role" });

            foreach (YouTrackSharp.Issues.Issue issue in Issues)
            {
                var links = issue.Fields.Where(x => x.Name == "links").Select(x => x.Value);
                foreach (JArray current in links)
                {
                    foreach (JObject content in current.Children<JObject>())
                    {
                        string type = content.Properties().Where(x => x.Name == "type").First().Value.ToString();
                        string value = content.Properties().Where(x => x.Name == "value").First().Value.ToString();
                        string role = content.Properties().Where(x => x.Name == "role").First().Value.ToString();
                        UserStoryLinkList.Add(new string[] { issue.Id, value, type, role });
                    }
 
                }

            }
            return UserStoryLinkList;
        }

        public static List<string[]> getUserStoryList(ICollection<YouTrackSharp.Issues.Issue> Issues)
        {
            List<string[]> UserStoryList = new List<string[]>();
            UserStoryList.Add(new string[] { "UserStory", "Summary", "projectShortName", "Assignee", "State", "SpentTime", "Estimation", "Type", "StoryPoint", "Version" });

            string SpentTime;
            string FullName;
            string projectShortName;
            string State;
            string Estimation;
            string Type;
            string StoryPoint;
            string Version;

            foreach (YouTrackSharp.Issues.Issue issue in Issues)
            {
                
                projectShortName = issue.Fields.Where(x => x.Name == "projectShortName").Select(x => x.Value).First().ToString();
                State = ((List<string>)issue.Fields.Where(x => x.Name == "State").Select(x => x.Value).First()).First();
                Type = ((List<string>)issue.Fields.Where(x => x.Name == "Type").Select(x => x.Value).First()).First();

                if (issue.Fields.Where(x => x.Name == "Story points").Select(x => x.Value).Any())
                    StoryPoint = ((List<string>)issue.Fields.Where(x => x.Name == "Story points").Select(x => x.Value).FirstOrDefault()).FirstOrDefault();
                else
                    StoryPoint = "0";

                if (issue.Fields.Where(x => x.Name == "Version").Select(x => x.Value).Any())
                    Version = ((List<string>)issue.Fields.Where(x => x.Name == "Version").Select(x => x.Value).FirstOrDefault()).FirstOrDefault();
                else
                    Version = "";

                if (issue.Fields.Where(x => x.Name == "Spent time").Select(x => x.Value).Any())
                    SpentTime = ((List<string>)issue.Fields.Where(x => x.Name == "Spent time").Select(x => x.Value).FirstOrDefault()).FirstOrDefault();
                else
                    SpentTime = "0";

                if (issue.Fields.Where(x => x.Name == "Estimation").Select(x => x.Value).Any())
                    Estimation = ((List<string>)issue.Fields.Where(x => x.Name == "Estimation").Select(x => x.Value).FirstOrDefault()).FirstOrDefault() ?? "";
                else
                    Estimation = "0";
                if (issue.Fields.Where(x => x.Name == "Assignee").Select(x => x.Value).Any())
                    FullName = ((List<YouTrackSharp.Issues.Assignee>)issue.Fields.Where(x => x.Name == "Assignee").Select(x => x.Value).First()).First().FullName;
                else
                    FullName = "";

                UserStoryList.Add(new string[] { issue.Id, issue.Summary, projectShortName, FullName, State, SpentTime, Estimation, Type, StoryPoint, Version });
            }
            return UserStoryList;
        }
    }

   
}
