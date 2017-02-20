﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Sdl.Community.GroupShareKit.Clients;
using Xunit;

namespace Sdl.Community.GroupShareKit.Tests.Integration.Clients
{
    public class ProjectClientTests
    {

        [Fact]
        public async Task GetProjectByName()
        {
            var groupShareClient = await Helper.GetAuthenticatedClient();

            var projectRequest = new ProjectsRequest("/", true, 7) {Filter = {ProjectName = "Andrea"}};
            var result =
                await
                    groupShareClient.Project.GetProjects(projectRequest);

            Assert.True(result.Items[0].Name=="Andrea");
        }

        [Fact]
        public async Task SortProjectsByName()
        {
            var groupShareClient = await Helper.GetAuthenticatedClient();
            var sortParameters = new SortParameters()
            {
                Property = SortParameters.PropertyOption.ProjectName,
                Direction = SortParameters.DirectionOption.DESC
            };
            var test = sortParameters.Stringify();
            var projectRequest = new ProjectsRequest(sortParameters);
            
            var sortedProjects = await groupShareClient.Project.GetProjects(projectRequest);
            Assert.True(sortedProjects.Items[0].Name == "Test");
        }

        [Fact]
        public async Task GetAllProjects()
        {
            var groupShareClient = await Helper.GetAuthenticatedClient();

            var projects = await groupShareClient.Project.GetAllProjects();

            Assert.True(projects.Count>0);

        }



        //[Theory]
        //[InlineData("c1f47d9c-a9dd-4069-b636-3405d4fb98a8")]
        //public async Task GetProjectFiles(string projectId)
        //{
        //    var groupShareClient = await Helper.GetAuthenticatedClient();

            //var projects =
            //    await
            //        groupShareClient.Project.GetAllProjectsForOrganization(new ProjectsRequest(Helper.TestOrganization,
            //            true));

            //Assert.True(projects != null);
            //Assert.True(projects.Count > 0, "There are no projects available");

           // var project = projects[0];

        //    var projectFiles = await groupShareClient.Project.GetAllFilesForProject(project.ProjectId.ToString());

        //    Assert.True(projectFiles != null);

        //}

        //[Fact]
        //public async Task GetProjectPhases()
        //{
        //    var groupShareClient = await Helper.GetAuthenticatedClient();

        //    var projects =
        //        await
        //            groupShareClient.Project.GetAllProjectsForOrganization(new ProjectsRequest(Helper.TestOrganization,
        //                true));

        //    Assert.True(projects != null);
        //    Assert.True(projects.Count > 0, "There are no projects available");

        //    var project = projects[0];

        //    var projectPhases = await groupShareClient.Project.GetAllPhasesForProject(project.ProjectId.ToString());

        //    Assert.True(projectPhases != null);
        //}


        //[Fact]
        //public async Task ChangeProjectPhases()
        //{
        //    var groupShareClient = await Helper.GetAuthenticatedClient();

        //    var projects =
        //        await
        //            groupShareClient.Project.GetAllProjectsForOrganization(new ProjectsRequest(Helper.TestOrganization,
        //                true));

        //    var request = new[]
        //    {
        //        new ChangePhaseRequest.File()
        //        {
        //            LanguageFileId = "6fc116cd-c63b-4022-871b-15c0b9cb7aef",
        //            PhaseId = 1850
        //        },
        //    };

        //    var projectPhases =
        //        await
        //            groupShareClient.Project.ChangePhases("3d7211e8-8b76-4f88-a76c-2ff4509f22c8",
        //                new ChangePhaseRequest("test", request)); 

        //    Assert.True(projectPhases != null);
        //}


        //[Fact]
        //public async Task ChangeProjectAssignments()
        //{
        //    var groupShareClient = await Helper.GetAuthenticatedClient();

        //    var projects =
        //        await
        //            groupShareClient.Project.GetAllProjectsForOrganization(new ProjectsRequest(Helper.TestOrganization,
        //                true));

        //    var request = new[]
        //    {
        //        new ChangeAssignmentRequest.File()
        //        {
        //            LanguageFileId = "6fc116cd-c63b-4022-871b-15c0b9cb7aef",
        //            DueDate =  DateTime.Now,
        //            PhaseId = 1850,
        //            AssignedUsers = new[] {"test_api"}
        //        }
        //    };

        //    var projectAssignements =
        //        await
        //            groupShareClient.Project.ChangeAssignments("3d7211e8-8b76-4f88-a76c-2ff4509f22c8",
        //                new ChangeAssignmentRequest("test", request)); 
        //}


        //[Fact]
        //public async Task CreateProject()
        //{
        //    var groupShareClient = await Helper.GetAuthenticatedClient();
        //    var rawData =
        //        File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\ProjectPackage.sdlppx"));

        //    var projectId =
        //        await groupShareClient.Project.CreateProject(new CreateProjectRequest("ProjectPackage.sdlppx",
        //            "c03a0a9e-a841-47ba-9f31-f5963e71bbb7", rawData));

        //    Assert.True(!string.IsNullOrEmpty(projectId));

        //    await groupShareClient.Project.DeleteProject(projectId);
        //}

        [Theory]
        [InlineData("c1f47d9c-a9dd-4069-b636-3405d4fb98a8")]
        public async Task GetProjectById(string projectId)
        {
            var groupShareClient = await Helper.GetAuthenticatedClient();

            var actualProject = await groupShareClient.Project.Get(projectId);

           Assert.Equal(actualProject.ProjectId,projectId);
        }

        //[Theory]
        //[InlineData("3d7211e8-8b76-4f88-a76c-2ff4509f22c8")]
        //public async Task ProjectFileStatus(string projectId)
        //{
        //    var groupShareClient = await Helper.GetAuthenticatedClient();

        //    var fileStatus = await groupShareClient.Project.FileStatus(projectId);

        //    Assert.True(fileStatus != null);
        //}

        //[Theory]
        //[InlineData("3d7211e8-8b76-4f88-a76c-2ff4509f22c8")]
        //public async Task ProjectLanguageFiles(string projectId)
        //{
        //    var groupShareClient = await Helper.GetAuthenticatedClient();

        //    var languageFiles = await groupShareClient.Project.LanguageFiles(projectId);

        //    Assert.True(languageFiles != null);
        //}


        //[Fact]
        //public async Task DownloadFile()
        //{
        //    var groupShareClient = await Helper.GetAuthenticatedClient();

        //    //project id, type
        //    var file = await groupShareClient.Project.DownloadFile(new FileDownloadRequest("3d7211e8-8b76-4f88-a76c-2ff4509f22c8", null, FileDownloadRequest.Types.All));   
        //    Assert.True(file != null);

        //    //project id, language code
        //    var request = await groupShareClient.Project.DownloadFile(new FileDownloadRequest("3d7211e8-8b76-4f88-a76c-2ff4509f22c8", "de-DE",null));

        //    Assert.True(request != null);

        //}

        //[Theory]
        //[InlineData("3d7211e8-8b76-4f88-a76c-2ff4509f22c8")]
        //public async Task DownloadFiles(string projectId)
        //{
        //    var groupShareClient = await Helper.GetAuthenticatedClient();
        //     var languageFilesId = new List<string> { "6fc116cd-c63b-4022-871b-15c0b9cb7aef", "827bbb26-8d72-4104-90b3-4e2eb14c5194" };

        //    var files = await groupShareClient.Project.DownloadFiles(projectId, languageFilesId);

        //    Assert.True(files != null);
        //} 

    }
}
