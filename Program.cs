using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Linq;
using YouTrackSharp;
using System.Collections.Generic;

namespace YTL
{
    
    class Program
    {

        static void Main(string[] args)
        {
            Param param = ParamReader.getParam(args);

            
            Setting setting = SettingImporter.getSettingFromXML(AppDomain.CurrentDomain.BaseDirectory + @"\Setting.xml");

            YouTrackLoader loader = new YouTrackLoader(setting.url, setting.token);

            string Now = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");

            switch (param.Type)
            {
                case "issue":
                    var issues = loader.getIssues(param.Filter).Result;

                    List<string[]> USTagList = YouTrackFormater.getUserStoryTagList(issues);
                    List<string[]> USSprintList = YouTrackFormater.getUserStorySprintList(issues);
                    List<string[]> USLinkList = YouTrackFormater.getUserStoryLinkList(issues);
                    List<string[]> USList = YouTrackFormater.getUserStoryList(issues);

                    CsvExporter.saveToCsv(setting.path + @"\" + @"USTag_" + Now + ".csv", USTagList);
                    CsvExporter.saveToCsv(setting.path + @"\" + @"USSprint_" + Now + ".csv", USSprintList);
                    CsvExporter.saveToCsv(setting.path + @"\" + @"USLink_" + Now + ".csv", USLinkList);
                    CsvExporter.saveToCsv(setting.path + @"\" + @"Issue_" + Now + ".csv", USList);
                    break;

                case "project":
                    var projects = loader.getProject().Result;

                    List<string[]> ProjectList = YouTrackFormater.getProjectList(projects);

                    CsvExporter.saveToCsv(setting.path + @"\" + @"Project" + Now + ".csv", ProjectList);
                    break;
            }
        }
 
    }
}
