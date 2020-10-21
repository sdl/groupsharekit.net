using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sdl.Community.GroupShareKit.Clients;
using Sdl.Community.GroupShareKit.Models.Response;
using Xunit;
using File = System.IO.File;

namespace Sdl.Community.GroupShareKit.Tests.Integration.Clients
{
    public class ProjectClientTests
    {
        private readonly string ProjectId;
        private readonly string LanguageFileId;
        private readonly string ProjectTemplateId;

        public ProjectClientTests()
        {
            var groupShareClient = Helper.GsClient;

            var projectRequest = new ProjectsRequest("/", true, 7) { Page = "0", Limit = "1" };
            var project = groupShareClient.Project.GetProject(projectRequest).Result.Items.FirstOrDefault();

            ProjectId = project != null ? project.ProjectId : string.Empty;

            var languageFile = groupShareClient.Project.GetAllFilesForProject(ProjectId).Result.FirstOrDefault(f => f.FileRole == "Translatable");
            LanguageFileId = languageFile != null ? languageFile.UniqueId.ToString() : string.Empty;

            var projectTemplate = groupShareClient.Project.GetAllTemplates().Result.ToList().FirstOrDefault();
            ProjectTemplateId = projectTemplate != null ? projectTemplate.Id : string.Empty;
        }

        #region Project tests
        [Fact]
        public async Task Projects_GetProjectByName_Succeeds()
        {
            var groupShareClient = Helper.GsClient;
            var projects = await groupShareClient.Project.GetAllProjects();
            var project = projects.Items.FirstOrDefault();

            if (project == null)
                return;

            var projectRequest = new ProjectsRequest("/", true, 7) { Filter = { ProjectName = project.Name } };
            var result = await groupShareClient.Project.GetProject(projectRequest);

            Assert.True(result.Items[0].Name == project.Name);
        }

        [Fact]
        public async Task Projects_SortProjectsByName_Succeeds()
        {
            var groupShareClient = Helper.GsClient;
            var sortParameters = new SortParameters
            {
                Property = SortParameters.PropertyOption.ProjectName,
                Direction = SortParameters.DirectionOption.ASC
            };
            var projectRequest = new ProjectsRequest(sortParameters);

            var sortedProjects = await groupShareClient.Project.GetProject(projectRequest);
            var projects = await groupShareClient.Project.GetAllProjects();

            Assert.True(sortedProjects.Items.Count == projects.Items.Count);

            var projectsNames = projects.Items.Select(p => p.Name).ToList();
            projectsNames.Sort();

            int i = 0;
            foreach (var proj in sortedProjects.Items)
            {
                Assert.True(string.Compare(proj.Name, projectsNames[i++], StringComparison.CurrentCultureIgnoreCase) == 0);
            }
        }

        [Fact]
        public async Task Projects_GetProjectFiles_Succeeds()
        {
            var groupShareClient = Helper.GsClient;
            var projects = await groupShareClient.Project.GetAllProjects();
            var project = projects.Items.FirstOrDefault();

            var projectFiles = await groupShareClient.Project.GetAllFilesForProject(project.ProjectId);

            Assert.True(projectFiles.Count > 0);
        }

        [Fact]
        public async Task Projects_GetAllProjects_Succeeds()
        {
            var groupShareClient = Helper.GsClient;
            var projects = await groupShareClient.Project.GetAllProjects();
            Assert.True(projects.Count > 0);
        }

        [Fact]
        public async Task Projects_AnalysisReports_Succeeds()
        {
            var groupShareClient = Helper.GsClient;
            var report = await groupShareClient.Project.GetAnalysisReports(ProjectId, null);
            Assert.True(report.Count > 0);
        }

        [Fact]
        public async Task Projects_AnalysisReportsAsHtml_Succeeds()
        {
            var groupShareClient = Helper.GsClient;
            var report = await groupShareClient.Project.GetAnalysisReportsAsHtml(ProjectId, null);
            Assert.True(report != null);
        }

	    [Fact]
	    public async Task Projects_GetProjectSettings_Succeeds()
	    {
		    var groupShareClient = Helper.GsClient;
		    var projectSettings = await groupShareClient.Project.GetProjectSettings(ProjectId, LanguageFileId);

			Assert.True(projectSettings!=null);
	    }

        [Fact]
		public async Task Projects_IsUserAuthorizedToOpenTheFile_Succeeds()
	    {
		    var groupShareClient = Helper.GsClient;
		    var response = await groupShareClient.Project.IsUserAuthorizedToOpenFile(ProjectId, LanguageFileId);

		    Assert.True(string.IsNullOrEmpty(response));
	    }

        [Fact]
	    public async Task Projects_EditorProfile_Succeeds()
	    {
		    var groupShareClient = Helper.GsClient;
		    var editorProfile = await groupShareClient.Project.EditorProfile(ProjectId, LanguageFileId);

		    Assert.True(editorProfile!=null);
	    }

        [Fact]
	    public async Task Projects_IsCheckOutToSomeoneElseBasicOnlineEditorMode_Succeeds()
	    {
		    var groupShareClient = Helper.GsClient;
            var editorProfileMode = OnlineCheckout.EditorProfileMode.Basic.ToString();
            await groupShareClient.Project.IsCheckoutToSomeoneElse(LanguageFileId, editorProfileMode);  
	    }

        [Fact]
        public async Task Projects_IsCheckOutToSomeoneElseAdvancedOnlineEditorMode_Succeeds()
        {
            var groupShareClient = Helper.GsClient;
            var editorProfileMode = OnlineCheckout.EditorProfileMode.Advanced.ToString();
            await groupShareClient.Project.IsCheckoutToSomeoneElse(LanguageFileId, editorProfileMode);
        }

        //[Fact(Skip = "")]
        //public async Task Projects_OnlineCheckIn_Succeeds()
        //{
        //	var groupShareClient = Helper.GsClient;	 

        //	var response =await groupShareClient.Project.OnlineCheckin(ProjectId, LanguageFileId).ConfigureAwait(true);
        //	Assert.True(response!=null);
        //}

        //[Fact]
        //public async Task OnlineCheckout()
        //{
        //    var groupShareClient = Helper.GsClient;

        //    var checkoutResponse = await groupShareClient.Project.OnlineCheckout(ProjectId, LanguageFileId);
        //    Assert.True(checkoutResponse != null);

        //    await groupShareClient.Project.UndoCheckout(ProjectId, LanguageFileId);
        //}

        //[Fact(Skip = "")]
        //public async Task ExternalCheckIn()
        //{
        //    var groupShareClient = Helper.GsClient;

        //    var response = await groupShareClient.Project.ExternalCheckout(ProjectId, LanguageFileId);
        //    Assert.True(response != null);
        //    await groupShareClient.Project.ExternalCheckin(ProjectId, LanguageFileId, "comment");
        //}

        [Fact]
	    public async Task Dashboard()
	    {
			var groupShareClient = Helper.GsClient;
		    var dashboard = await groupShareClient.Project.Dashboard();
			Assert.True(dashboard!=null);
	    }

        [Fact]
	    public async Task AuditTrail()
	    {
			var groupShareClient = Helper.GsClient;
		    var auditTrail = await groupShareClient.Project.AuditTrail(ProjectId);

			Assert.True(auditTrial?.Count>0);  
	    }

		[Fact]
	    public async Task OnlineCheckoutHealthCheckBasicOnlineEditorMode()
	    {
			var groupShareClient = Helper.GsClient;
            var editorProfileMode = OnlineCheckout.EditorProfileMode.Basic.ToString();
		    var response = await groupShareClient.Project.OnlineCheckoutHealthCheck(editorProfileMode);
		    Assert.True(response != null);
	    }

        [Fact]
        public async Task OnlineCheckoutHealthCheckAdvancedOnlineEditorMode()
        {
            var groupShareClient = Helper.GsClient;
            var editorProfileMode = OnlineCheckout.EditorProfileMode.Advanced.ToString();
            var response = await groupShareClient.Project.OnlineCheckoutHealthCheck(editorProfileMode);
            Assert.True(response != null);
        }

        [Fact]
        public async Task GetProjectById()
        {
            var groupShareClient = Helper.GsClient;

            var projects = await groupShareClient.Project.GetAllProjects();
            var project = projects.Items.FirstOrDefault();		  

	        if (project != null)
	        {
		        var actualProject = await groupShareClient.Project.Get(project.ProjectId);

		        Assert.Equal(actualProject.ProjectId, project.ProjectId);
	        }
        }

		
        // test fails because of a bug that has to be fixed in the REST API
        //[Fact]
        //public async Task GetProjectLanguageStatistics()
        //{
        //    var groupShareClient = Helper.GsClient;

        //    var projects = await groupShareClient.Project.GetAllProjects();
        //    var project = projects.Items.FirstOrDefault();

        //    var response = await groupShareClient.Project.GetProjectLanguageStatistics(project.ProjectId);

        //    Assert.Equal(response.Keys.First(), "en-US");
        //}

        [Fact]
        public async Task GetProjectFileStatistics()
        {
            var groupShareClient = Helper.GsClient;

            var projects = await groupShareClient.Project.GetAllProjects();
            var project = projects.Items.FirstOrDefault();

            var response = await groupShareClient.Project.GetAllProjectFileStatistics(project.ProjectId);

            Assert.True(response.Count > 0);
        }

        [Theory]
        [InlineData("SDL Community Developers")]
        public void GetProjectsForOrganization(string organizationName)
        {
            var groupShareClient = Helper.GsClient;
            var projects = groupShareClient.Project.GetProjectsForOrganization(organizationName);

            foreach (var project in projects)
            {
                Assert.Equal(project.OrganizationName, organizationName);
            }
        }

        [Fact]
        public async Task<string> CreateProject()
        {
            var groupShareClient = Helper.GsClient;

            var projectRequest = new ProjectsRequest("/", true, 7) { Filter = { ProjectName = "today" } };
            var result = await groupShareClient.Project.GetProject(projectRequest);

            if (result.Items.Count == 0)
            {
                var rawData = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\Grammar.zip"));
                var projectName = $"Project - {Guid.NewGuid()}";

                var projectId = await groupShareClient.Project.CreateProject(new CreateProjectRequest(
                    projectName,
                    Helper.OrganizationId,
                    null,
                    DateTime.Now.AddDays(2),
                    ProjectTemplateId,
                    rawData));

                Assert.True(!string.IsNullOrEmpty(projectId));

                return projectId;
            }
            return null;
        }

        [Fact(Skip = "Used to work until GroupShare 2017 CU7")]
        public async Task PublishPackage()
        {
            var groupShareClient = Helper.GsClient;
            var rawData = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\ProjectPackage.sdlppx"));
            var createProjectRequest = new CreateProjectRequest(
                "ProjectForPublish",
                Helper.OrganizationId,
                null,
                DateTime.Today,
                "7bf6410d-58a7-4817-a559-7aa8a3a99aa9",
                rawData);

            await groupShareClient.Project.PublishPackage(createProjectRequest);
        }

        [Fact]
        public async Task GetProjectsAssignments()
        {
            var groupShareClient = Helper.GsClient;
            var projects = await groupShareClient.Project.GetUserAssignments();

            Assert.True(projects.Count > 0);
        }

        [Fact]
        public async Task GetProjectAssignmentsById()
        {
            var groupShareClient = Helper.GsClient;
            var fileIds = new List<string> { LanguageFileId };

            var assignments = await groupShareClient.Project.GetProjectAssignmentById(ProjectId, fileIds);

            Assert.True(assignments.Count > 0);
        }

        [Fact]
        public async Task ChangeProjectStatus()
        {
            var groupShareClient = Helper.GsClient;

            var projectStatusRequest = new ChangeStatusRequest(ProjectId, ChangeStatusRequest.ProjectStatus.Completed);
            await groupShareClient.Project.ChangeProjectStatus(projectStatusRequest);

            var project = await groupShareClient.Project.Get(ProjectId);
            Assert.Equal(4, project.Status);

            projectStatusRequest = new ChangeStatusRequest(ProjectId, ChangeStatusRequest.ProjectStatus.Started);
            await groupShareClient.Project.ChangeProjectStatus(projectStatusRequest);

            project = await groupShareClient.Project.Get(ProjectId);
            Assert.Equal(2, project.Status);
        }

        [Fact]
        public async Task ChangeProjectStatusDetach()
        {
            var groupShareClient = Helper.GsClient;

            var projectStatusRequest = new ChangeStatusRequest(ProjectId, ChangeStatusRequest.ProjectStatus.Completed);
            await groupShareClient.Project.ChangeProjectStatusDetach(projectStatusRequest);

            var project = await groupShareClient.Project.Get(ProjectId);
            Assert.Equal(4, project.Status);

            projectStatusRequest = new ChangeStatusRequest(ProjectId, ChangeStatusRequest.ProjectStatus.Started);
            await groupShareClient.Project.ChangeProjectStatus(projectStatusRequest);
        }

        [Fact]
        public async Task PerfectMatchWithZip()
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\PerfectMatch");
            var projectTemplateId = await CreateProjectTemplateForPerfectMatch(Path.Combine(baseDir, "project-template.sdltpl"));

            var basicProjectCreateRequest = CreateBasicCreateProjectRequest(projectTemplateId);
            var projectId = await Helper.GsClient.Project.CreateProject(basicProjectCreateRequest, Path.Combine(baseDir, "PerfectMatch.zip"));
            var created = await WaitForProjectCreated(projectId);

            Assert.True(created);
            var analysisReports = await Helper.GsClient.Project.GetAnalysisReports(projectId.ToString(), "fr-fr");
            Assert.True(analysisReports[0].Report.Task.File.Count == 4);
            Assert.True(analysisReports[0].Report.Task.File[0].Analyse.Total.Segments == "3");
            Assert.True(analysisReports[0].Report.Task.File[0].Analyse.Perfect.Segments == "0");
            Assert.True(analysisReports[0].Report.Task.File[1].Analyse.Perfect.Segments == "1");
            Assert.True(analysisReports[0].Report.Task.File[2].Analyse.Perfect.Segments == "2");
            Assert.True(analysisReports[0].Report.Task.File[3].Analyse.Perfect.Segments == "3");

            await Helper.GsClient.Project.DeleteProject(projectId.ToString());
            await Helper.GsClient.Project.Delete(projectTemplateId.ToString());
        }



        [Fact]
        public async Task PerfectMatchWithFiles()
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\PerfectMatch");
            var projectTemplateId = await CreateProjectTemplateForPerfectMatch(Path.Combine(baseDir, "project-template.sdltpl"));

            var basicProjectCreateRequest = CreateBasicCreateProjectRequest(projectTemplateId);
            var projectId = await Helper.GsClient.Project.CreateProject(
                basicProjectCreateRequest, Path.Combine(baseDir, "project-files"), null,
                new string[] { Path.Combine(baseDir, "previous-translations")});
            var created = await WaitForProjectCreated(projectId);
            Assert.True(created);

            var analysisReports = await Helper.GsClient.Project.GetAnalysisReports(projectId.ToString(), "fr-fr");
            Assert.True(analysisReports[0].Report.Task.File.Count == 4);
            Assert.True(analysisReports[0].Report.Task.File[0].Analyse.Total.Segments == "3");
            Assert.True(analysisReports[0].Report.Task.File[0].Analyse.Perfect.Segments == "0");
            Assert.True(analysisReports[0].Report.Task.File[1].Analyse.Perfect.Segments == "1");
            Assert.True(analysisReports[0].Report.Task.File[2].Analyse.Perfect.Segments == "2");
            Assert.True(analysisReports[0].Report.Task.File[3].Analyse.Perfect.Segments == "3");

            await Helper.GsClient.Project.DeleteProject(projectId);
            await Helper.GsClient.Project.Delete(projectTemplateId);
        }

        private async Task<string> CreateProjectTemplateForPerfectMatch(string projectTemplateFilePath)
        {
            var projectTemplateData = File.ReadAllBytes(projectTemplateFilePath);
            var projectTemplateRequest = new ProjectTemplates
            {
                Id = Guid.NewGuid().ToString(),
                Name = "PerfectMatchProjectTemplate_" + Guid.NewGuid().ToString(),
                Description = "",
                OrganizationId = Helper.OrganizationId
            };
            var projectTemplateId = await Helper.GsClient.Project.CreateTemplate(projectTemplateRequest, projectTemplateData);
            return projectTemplateId;
        }

        private static BasicCreateProjectRequest CreateBasicCreateProjectRequest(string projectTemplateId)
        {
            return new BasicCreateProjectRequest
            {
                Name = "PerfectMatch_" + Guid.NewGuid().ToString(),
                Description = "Perfect match from zip file",
                OrganizationId = Helper.OrganizationId,
                ProjectTemplateId = projectTemplateId,
                DueDate = null,
                ReferenceProjects = null,
                SuppressEmail = false,
                IsSecure = false
            };
        }

        private async Task<bool> WaitForProjectCreated(string projectId, int retryInterval = 30, int maxTryCount = 20)
        {
            for (var i = 0; i < maxTryCount; i++)
            {
                var statusInfo = await Helper.GsClient.Project.PublishingStatus(projectId);
                switch (statusInfo.Status)
                {
                    case PublishProjectStatus.Uploading:
                    case PublishProjectStatus.Scheduled:
                    case PublishProjectStatus.Publishing:
                        break;
                    case PublishProjectStatus.Completed:
                        return true;
                    case PublishProjectStatus.Error:
                        throw new Exception(statusInfo.Description);
                }
                await Task.Delay(retryInterval * 1000);
                
            }
            return false;
        }


        #endregion

        #region Project template tests

        [Fact]
        public async Task GetAllProjectsTemplates()
        {
            var groupShareClient = Helper.GsClient;
            var templates = await groupShareClient.Project.GetAllTemplates();

            Assert.True(templates.Count > 0);
        }

        [Fact]
        public async Task<string> GetTemplateById()
        {
            var groupShareClient = Helper.GsClient;

            var template = await groupShareClient.Project.GetTemplateById(ProjectTemplateId);

            Assert.True(template != string.Empty);
            return template;
        }

        [Fact]
        public async Task CreateTemplate()
        {
            var groupShareClient = Helper.GsClient;
            var rawData = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\SampleTemplate.sdltpl"));

            var id = Guid.NewGuid().ToString();
			var templateName = Guid.NewGuid().ToString();
            var templateRequest = new ProjectTemplates(id, templateName, "", Helper.OrganizationId);
            var templateId = await groupShareClient.Project.CreateTemplate(templateRequest, rawData);

            Assert.True(templateId != string.Empty);

            await groupShareClient.Project.Delete(templateId);
        }
        #endregion

        #region File version tests
        [Fact]
        public async Task GetFileVersions()
        {
            var groupShareClient = Helper.GsClient;
            var fileVersion = await groupShareClient.Project.GetFileVersions(LanguageFileId);

            Assert.True(fileVersion.Count > 0);
        }

        [Fact]
        public async Task DownloadFileVersion()
        {
            var groupShareClient = Helper.GsClient;
            var downloadedFile = await groupShareClient.Project.DownloadFileVersion(ProjectId, LanguageFileId, 2);

            Assert.True(downloadedFile.Length != 0);
        }
        #endregion

        #region Dashboard

        //[Fact]
        //[Trait("GSVersion", "2017")]
        //public async Task Dashboard()
        //{
        //    var groupShareClient = await Helper.GetGroupShareClient();
        //    var dashboard = await groupShareClient.Project.Dashboard();
        //    Assert.True(dashboard != null);
        //}

        public async Task DashboardProjectsPerMonth()
        {
            var groupShareClient = Helper.GsClient;
            var projectCounts = await groupShareClient.Project.DataboardProjectsPerMonth();
            Assert.True(projectCounts != null);
        }

        public async Task DashboardTopLanguagePairs()
        {
            var groupShareClient = Helper.GsClient;
            var languagePairs = await groupShareClient.Project.DashboardTopLanguagePairs(5);
            Assert.True(languagePairs != null);
        }

        public async Task DashboardWordsPerMonth()
        {
            var groupShareClient = Helper.GsClient;
            var wordCounts = await groupShareClient.Project.DashboardWordsPerMonth();
            Assert.True(wordCounts != null);
        }

        public async Task DashboardWordsPerOrganization()
        {
            var groupShareClient = Helper.GsClient;
            var wordCounts = await groupShareClient.Project.DashboardWordsPerOrganization();
            Assert.True(wordCounts != null);
        }

        public async Task DashboardStatistics()
        {
            var groupShareClient = Helper.GsClient;
            var statistics = await groupShareClient.Project.DashboardStatistics();
            Assert.True(statistics != null);
        }
        #endregion

        #region Reporting
        public async Task ReportingProjectPredefinedReportData()
        {
            var groupShareClient = Helper.GsClient;
            var options = new ReportingOptions
            {
                Status = 7
            };
            var reportingData = await groupShareClient.Project.ReportingProjectPredefinedReportData(options);
            Assert.True(reportingData != null);
        }

        public async Task ReportingTasksReportData()
        {
            var groupShareClient = Helper.GsClient;
            var options = new ReportingOptions
            {
                Status = 7
            };
            var reportingData = await groupShareClient.Project.ReportingTasksReportData(options);
            Assert.True(reportingData != null);
        }

        public async Task ReportingTmLeverageData()
        {
            var groupShareClient = Helper.GsClient;
            var options = new ReportingOptions
            {
                Status = 7
            };
            var reportingData = await groupShareClient.Project.ReportingTmLeverageData(options);
            Assert.True(reportingData != null);
        }
        #endregion Reporting
    }
}