using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Sdl.Community.GroupShareKit.Exceptions;
using Sdl.Community.GroupShareKit.Helpers;
using Sdl.Community.GroupShareKit.Http;
using Sdl.Community.GroupShareKit.Models.Response;

namespace Sdl.Community.GroupShareKit.Clients
{
    /// <summary>
    /// A client for GroupShare's ProjectServer API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">ProjectServer API documentation</a> for more details.
    /// </remarks>
    public class ProjectClient : ApiClient, IProjectClient
    {
        public ProjectClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        #region Project management methods
        /// <summary>
        /// Gets a <see cref="Project"/>s
        /// </summary>
        /// <remarks>
        /// <param name="request"><see cref="ProjectsRequest"/></param>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns><see cref="Project"/></returns>
        public Task<Project> GetProject(ProjectsRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return ApiConnection.Get<Project>(ApiUrls.GetAllProjects(), request.ToParametersDictionary());
        }

        /// <summary>
        /// Gets all <see cref="Project"/>s
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="Project"/>s.</returns>
        public Task<Project> GetAllProjects()
        {
            return ApiConnection.Get<Project>(ApiUrls.GetAllProjects(), null);
        }

        /// <summary>
        /// Gets all <see cref="Project"/>s for the organization.
        /// </summary>
        /// <remarks>
        /// <param name="organizationName">string</param>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="ProjectDetails"/>s.</returns>
        public List<ProjectDetails> GetProjectsForOrganization(string organizationName)
        {
            var allProjects = ApiConnection.Get<Project>(ApiUrls.GetAllProjects(), null);
            return allProjects.Result.Items.Where(o => o.OrganizationName == organizationName).ToList();
        }


        /// <summary>
        /// Gets all <see cref="File"/>s for  project.
        /// </summary>
        /// <param name="projectId">string</param>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="File"/>s.</returns>
        public Task<IReadOnlyList<File>> GetAllFilesForProject(string projectId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");

            return ApiConnection.GetAll<File>(ApiUrls.ProjectFiles(projectId));
        }

        /// <summary>
        /// Gets all <see cref="Phase"/>s for the project.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="Phase"/>s.</returns>
        public Task<IReadOnlyList<Phase>> GetAllPhasesForProject(string projectId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");
            return ApiConnection.GetAll<Phase>(ApiUrls.ProjectPhases(projectId));

        }

        /// <summary>
        /// Gets the list of files for the requested project with all the project phases and theirs assignees <see cref="PhasesWithAssignees"/>
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="PhasesWithAssignees"/>s.</returns>
        public Task<IReadOnlyList<PhasesWithAssignees>> GetPhasesWithAssignees(string projectId, int phaseId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");
            Ensure.ArgumentNotNullOrEmptyString(phaseId.ToString(), "phaseId");
            return ApiConnection.GetAll<PhasesWithAssignees>(ApiUrls.ProjectPhasesWithAssignees(projectId, phaseId));

        }


        /// <summary>
        /// Changes the phases for files from a server project
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="Phase"/>s.</returns>
        public Task ChangePhases(string projectId, ChangePhaseRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectid");
            Ensure.ArgumentNotNull(request, "request");

            return ApiConnection.Post<string>(ApiUrls.ChangePhases(projectId), request, "application/json");
        }

        /// <summary>
        /// Changes the assignment for a specific phase for files from a server project,
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="Phase"/>s.</returns>
        public Task ChangeAssignments(string projectId, ChangeAssignmentRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectid");
            Ensure.ArgumentNotNull(request, "request");

            return ApiConnection.Post<string>(ApiUrls.ChangeAssignments(projectId), request, "application/json");
        }

        /// <summary>
        /// Create project
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<string> CreateProject(CreateProjectRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            var projectUri = await ApiConnection.Post<string>(ApiUrls.GetAllProjects(), request, "application/json");
            var projectId = projectUri.Split('/').Last();

            await UploadFilesForProject(projectId, request.RawData, request.Name);
            return projectId;
        }

        /// <summary>
        /// Create project
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <param name="request">The basic project parameters</param>
        /// <param name="filesPath">The path pointing to the files for the project. The path can be a zip file, a single file, or a directory. 
        /// If it is a zip file path, the zip should have a folder SourceFiles, an optional folder ReferenceFiles, and an optional folder PerfectMatchFiles.
        /// If it is a file, the project will be created as a single file project. If it is a directory, all the files under the directory will be the project files. 
        /// </param>
        /// <param name="referenceFilesPath">If filesPath parameter is not a zip file, this optional parameter points to a reference file or a directory containing reference files.</param>
        /// <param name="perfectMatchFilesPaths">If filesPath parameter is not a zip file, this optional parameter points to a directories containing the perfect match files.
        /// </param>
        /// <returns>The project Id</returns>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<string> CreateProject(BasicCreateProjectRequest request, 
            string filesPath, string referenceFilesPath = null, string[] perfectMatchFilesPaths = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(filesPath, "filesPath");

            // register the project creation
            var projectCreateResponse = await ApiConnection.Post<string>(ApiUrls.GetAllProjects(), request, "application/json");
            var projectId = projectCreateResponse.Split('/').Last();

            var started = await UploadFilesForProject(projectId, filesPath);
            if (started)
            {
                return projectId;
            }

            if (!string.IsNullOrEmpty(referenceFilesPath))
            {
                await UploadReferenceFilesForProject(projectId, referenceFilesPath);
            }

            if (perfectMatchFilesPaths != null && perfectMatchFilesPaths.Length > 0)
            {
                for (var i = 0; i < perfectMatchFilesPaths.Length; i ++)
                {
                    var uri = ApiUrls.GetPerfectMatchFiles(projectId, i);
                    await UploadPerfectMatchFilesForProject(uri.ToString(), perfectMatchFilesPaths[i]);
                }
            }

            await ApiConnection.Post<string>(ApiUrls.StartProjectCreationUri(projectId));
            return projectId;
        }

        private async Task<bool> UploadFilesForProject(string projectId, string filesPath)
        {
            if (System.IO.File.Exists(filesPath))
            {
                if (filesPath.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
                {
                    var uri = ApiUrls.UploadFilesForProject(projectId.ToString(), false, true);
                    await UploadFilesForProject(uri.ToString(), new string[] { filesPath} );
                    return true;
                }
                else
                {
                    var uri = ApiUrls.UploadFilesForProject(projectId.ToString(), false, false);
                    System.Diagnostics.Debug.WriteLine($"Upload files: {filesPath}" );
                    await UploadFilesForProject(uri.ToString(), new string[] { filesPath });
                    return false;
                }
            }
            else
            {
                var uri = ApiUrls.UploadFilesForProject(projectId.ToString(), false, false);
                System.Diagnostics.Debug.WriteLine($"Upload folder: {filesPath}");
                await UploadDirectoryForProject(uri.ToString(), filesPath);
                return false;
            }            
        }

        private async Task UploadReferenceFilesForProject(string projectId, string referenceFilesPath)
        {
            var uri = ApiUrls.UploadFilesForProject(projectId.ToString(), true, false);
            if (System.IO.File.Exists(referenceFilesPath))
            {
                await UploadFilesForProject(uri.ToString(), new string[] { referenceFilesPath });
            }
            else
            {
                await UploadDirectoryForProject(uri.ToString(), referenceFilesPath);
            }
        }

        private async Task UploadPerfectMatchFilesForProject(string uri, string directory)
        {
            // first level are langugage folders
            foreach (var languageDir in new System.IO.DirectoryInfo(directory).GetDirectories())
            {
                //var languageCode = languageDir.Name;
                await UploadDirectoryForProject(uri, languageDir.FullName);
            }
        }

        private async Task UploadDirectoryForProject(string uri, string directory)
        {            
            var files = System.IO.Directory.GetFiles(directory);
            if (files.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine("Upload files: " + string.Join(System.Environment.NewLine, files));
                await UploadFilesForProject(uri, files);  
            }

            foreach (var subDir in new System.IO.DirectoryInfo(directory).GetDirectories())
            {
                var name = subDir.Name;
                var folderUri = uri + name + "\\";
                System.Diagnostics.Debug.WriteLine(folderUri);
                System.Diagnostics.Debug.WriteLine($"Upload folder: {subDir.FullName}");
                await UploadDirectoryForProject(folderUri, subDir.FullName);
            }
        }

        private async Task UploadFilesForProject(string uri, string[] filesPaths)
        {
            using (var content = new MultipartFormDataContent())
            {
                foreach (var file in filesPaths)
                {
                    var stream = new System.IO.FileStream(file, System.IO.FileMode.Open);
                    var streamContent = new StreamContent(stream);
                    streamContent.Headers.Add("Content-Type", "application/octet-stream");
                    content.Add(streamContent, "file", System.IO.Path.GetFileName(file));
                }
            await ApiConnection.Post<string>(uri, content, null);
            }
        }

        /// <summary>
        /// Delete project
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task DeleteProject(string projectId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");

            return ApiConnection.Delete(ApiUrls.Project(projectId));
        }

        /// <summary>
        /// Get project
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns> <see cref="ProjectDetails"/></returns>
        public Task<ProjectDetails> Get(string projectId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");

            return ApiConnection.Get<ProjectDetails>(ApiUrls.Project(projectId), null);
        }

        /// <summary>
        /// Get the publishing status of a server project.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns> <see cref="PublishingStatus"/></returns>
        public async Task<PublishingStatus> PublishingStatus(string projectId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");
            return await ApiConnection.Get<PublishingStatus>(ApiUrls.PublishingStatus(projectId), null);
        }



        /// <summary>
        ///Downloads the files with the specific language ids.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of byte[] wich represents downloaded files.</returns>
        public async Task<byte[]> DownloadFiles(string projectId, List<string> languageFileIds)
        {
            Ensure.ArgumentNotEmpty(languageFileIds, "languageFileIds");

            return await ApiConnection.Get<byte[]>(ApiUrls.DownloadFiles(projectId, LanguageIdQuery(languageFileIds)), null);
        }

        /// <summary>
        ///Downloads the native files of a project.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of byte[] wich represents downloaded files.</returns>></param>
        /// <returns></returns>
        public async Task<byte[]> DownloadNative(string projectId)
        {
            return await ApiConnection.Get<byte[]>(ApiUrls.DownloadNative(projectId), null);
        }

        /// <summary>
		/// Finalizez the files of a project.
		/// </summary>
		/// <remarks>
		/// This method requires authentication.
		/// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		/// </remarks>
		/// <exception cref="AuthorizationException">
		/// Thrown when the current user does not have permission to make the request.
		/// </exception>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A list of byte[] wich represents downloaded files.</returns>
        public async Task<byte[]> Finalize(string projectId, List<string> languageFileIds)
        {
            Ensure.ArgumentNotEmpty(languageFileIds, "languageFileIds");

            return await ApiConnection.Post<byte[]>(ApiUrls.Finalize(projectId, LanguageIdQuery(languageFileIds)));
        }

        /// <summary>
        /// Helper method to create  query.
        /// </summary>
        /// <param name="languageFileIds"></param>
        /// <returns></returns>
        public string LanguageIdQuery(List<string> languageFileIds)
        {
            var query = string.Empty;

            if (languageFileIds.Count() == 1)
            {
                return "languageFileIds=" + languageFileIds.FirstOrDefault();
            }

            foreach (var id in languageFileIds)
            {
                query = query + "languageFileIds=" + id + "&";
            }

            return query;
        }

        /// <summary>
        /// Helper method to create  query
        /// </summary>
        /// <param name="languageFileIds"></param>
        /// <returns></returns>
        public string FileIdQuery(List<string> languageFileIds)
        {
            var query = string.Empty;
            foreach (var id in languageFileIds)
            {
                query = query + "fileId=" + id + "&";
            }

            return query;
        }
        /// <summary>
        ///Downloads the files with the specific type and language code.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of byte[] wich represents downloaded files.</returns>
        public async Task<byte[]> DownloadFile(FileDownloadRequest downloadRequest)
        {
            if (downloadRequest.Type != null)
            {
                return
                 await
                     ApiConnection.Get<byte[]>(
                         ApiUrls.DownloadFile(downloadRequest.ProjectId, Enum.GetName(typeof(FileDownloadRequest.Types), downloadRequest.Type)),
                         null);
            }

            return await ApiConnection.Get<byte[]>(ApiUrls.DownloadFile(downloadRequest.ProjectId, "all"), null);

        }


        /// <summary>
        ///Gets a list of user assignements
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="UserAssignments"/>s.</returns>
        public async Task<IReadOnlyList<UserAssignments>> GetUserAssignments()
        {

            return await ApiConnection.GetAll<UserAssignments>(ApiUrls.GetProjectsAssignments(), null);
        }

        /// <summary>
        ///Gets a list of assignements for a project
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="ProjectAssignment"/>s.</returns>
        public async Task<IReadOnlyList<ProjectAssignment>> GetProjectAssignmentById(string projectId, List<string> fileIdsList)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");
            Ensure.ArgumentNotNull(fileIdsList, "fileIdsList");

            return
                await
                    ApiConnection.GetAll<ProjectAssignment>(
                        ApiUrls.GetProjectAssignmentById(projectId, FileIdQuery(fileIdsList)), null);
        }

        /// <summary>
        ///Uploads file for a specific project
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<string> UploadFilesForProject(string projectId, byte[] rawData, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");
            var byteContent = new ByteArrayContent(rawData);
            byteContent.Headers.Add("Content-Type", "application/zip");
            var multipartContent = new MultipartFormDataContent
            {
                {byteContent,"file", name + ".zip"}
            };
            return await ApiConnection.Post<string>(ApiUrls.UploadFilesForProject(projectId), multipartContent, "application/zip");
        }

        

        /// <summary>
        ///Change project status
        /// <param name="statusRequest"><see cref="ChangeStatusRequest"/></param>
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<string> ChangeProjectStatus(ChangeStatusRequest statusRequest)
        {

            return await ApiConnection.Put<string>(ApiUrls.ChangeProjectStatus(statusRequest.ProjectId, Enum.GetName(typeof(ChangeStatusRequest.ProjectStatus), statusRequest.Status)), statusRequest);

        }

        /// <summary>
        ///Change project status detach
        /// <param name="statusRequest"><see cref="ChangeStatusRequest"/></param>
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<string> ChangeProjectStatusDetach(ChangeStatusRequest statusRequest)
        {
            return await ApiConnection.Put<string>(ApiUrls.ChangeProjectStatus(statusRequest), statusRequest);
        }


        /// <summary>
        ///Deletes detach to a project status
        /// <param name="projectId deleteTms">></param>
        /// <param name="deleteTms"></param>
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// 
        public async Task ChangeProjectStatusDeleteDetach(string projectId, bool deleteTms)
        {
            await ApiConnection.Delete(ApiUrls.ProjectStatusDeleteDetach(projectId, deleteTms));
        }

        /// <summary>
        ///Publish  project
        /// <param name="projectRequest"><see cref="CreateProjectRequest"/></param>
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<string> PublishPackage(CreateProjectRequest projectRequest)
        {
            Ensure.ArgumentNotNull(projectRequest, "request");
            var projectId = await CreateProjectForPublishingPackage(projectRequest);

            var byteContent = new ByteArrayContent(projectRequest.RawData);
            byteContent.Headers.Add("Content-Type", "application/json");
            var multipartContent = new MultipartFormDataContent
            {
                {byteContent,projectRequest.Name, projectRequest.Name}
            };
            await ApiConnection.Post<string>(ApiUrls.PublishProjectPackage(projectId), multipartContent, "application/json");

            return projectId;
        }
        private async Task<string> CreateProjectForPublishingPackage(CreateProjectRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            var projectUri = await ApiConnection.Post<string>(ApiUrls.GetAllProjects(), request, "application/json");
            return projectUri.Split('/').Last();
        }
        public Task<IReadOnlyList<ProjectFileStatistics>> GetAllProjectFileStatistics(string projectId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");

            return ApiConnection.GetAll<ProjectFileStatistics>(ApiUrls.ProjectFileStatistics(projectId), null);
        }
        public Task<Dictionary<string, ProjectStatistics>> GetProjectLanguageStatistics(string projectId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");

            return ApiConnection.Get<Dictionary<string, ProjectStatistics>>(ApiUrls.ProjectLanguageStatistics(projectId), null);
        }
        #endregion


        #region Project template methods
        /// <summary>
        ///Gets all templates
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A List of <see cref="ProjectTemplates"/></returns>
        public async Task<IReadOnlyList<ProjectTemplates>> GetAllTemplates()
        {
            return await ApiConnection.GetAll<ProjectTemplates>(ApiUrls.ProjectTemplates(), null);
        }

        /// <summary>
        ///Creates a template
        /// </summary>
        /// <param name="templateRequest"><see cref="ProjectTemplates"/></param>
        /// <param name="rawData">The project temaplate file as a byte array.</param>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Id of created template/></returns>
        public async Task<string> CreateTemplate(ProjectTemplates templateRequest, byte[] rawData)
        {
            var templateId = await ApiConnection.Post<string>(ApiUrls.ProjectTemplates(), templateRequest, "application/json");
            await UploadProjectTemplate(templateId, rawData, templateRequest.Name);
            return templateId;
        }

        /// <summary>
        ///Creates a template
        /// </summary>
        /// <param name="templateRequest"><see cref="ProjectTemplateV3"/></param>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Id of created template/></returns>
        public async Task<string> CreateTemplateV3(ProjectTemplateV3 templateRequest)
        {
            var templateId = await ApiConnection.Post<string>(ApiUrls.ProjectTemplatesV3(), templateRequest, "application/json");
            //await UploadProjectTemplate(templateId, templateRequest.Name);
            return templateId;
        }

        ///// <summary>
        /////Creates a template
        ///// </summary>
        ///// <param name="templateRequest"><see cref="ProjectTemplateV3"/></param>
        ///// <remarks>
        ///// This method requires authentication.
        ///// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        ///// </remarks>
        ///// <exception cref="AuthorizationException">
        ///// Thrown when the current user does not have permission to make the request.
        ///// </exception>
        ///// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        ///// <returns>Id of created template/></returns>
        //public async Task<string> UpdateProjectTemplateV3(ProjectTemplateV3 templateRequest)
        //{
        //    //var templateId = await ApiConnection.Put<>(ApiUrls.ProjectTemplatesV3(), templateRequest, "application/json");
        //    ////await UploadProjectTemplate(templateId, templateRequest.Name);
        //    //return templateId;
        //}

        /// <summary>
        ///Get a template by id
        /// </summary>
        /// <param name="templateId">string</param>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Te contend of template in a string/></returns>
        public async Task<string> GetTemplateById(string templateId)
        {
            Ensure.ArgumentNotNullOrEmptyString(templateId, "templateId");
            return await ApiConnection.Get<string>(ApiUrls.ProjectTemplates(templateId), null);
        }

        /// <summary>
        ///Deletes a template 
        /// </summary>
        /// <param name="templateId">string</param>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        public async Task Delete(string templateId)
        {
            Ensure.ArgumentNotNullOrEmptyString(templateId, "templateId");
            await ApiConnection.Delete(ApiUrls.ProjectTemplates(templateId));
        }

        /// <summary>
        ///Uploades a template to a newly created project 
        /// This method should be called after you create a project in order to add the template
        /// </summary>
        /// <param name="templateId">string</param>
        /// <param name="projectTemplate">byte[]</param>
        /// <param name="templateName">string</param>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<string> UploadProjectTemplate(string templateId, byte[] projectTemplate, string templateName)
        {
            Ensure.ArgumentNotNullOrEmptyString(templateId, "templateId");
            var templateByteArray = new ByteArrayContent(projectTemplate);
            var multipartContent = new MultipartFormDataContent
            {
                {templateByteArray,"file", templateName}
            };

            return await ApiConnection.Post<string>(ApiUrls.UploadProjectTemplate(templateId), multipartContent, "application/json");

        }
        #endregion

        #region File version methods

        /// <summary>
        /// Gets file versions informations<see cref="FileVersion"/>.
        /// </summary>
        ///  <param name="languageFileId">Language file id></param>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns> List <see cref="FileVersion"/>s.</returns>
        public async Task<IReadOnlyList<FileVersion>> GetFileVersions(string languageFileId)
        {
            Ensure.ArgumentNotNullOrEmptyString(languageFileId, "languageFileId");
            return await ApiConnection.GetAll<FileVersion>(ApiUrls.GetFileVersion(languageFileId), null);
        }

        /// <summary>
        /// Downloads the file/>.
        /// </summary>
        /// <param name="projectId">The project id</param>
        /// <param name="languageFileId"> Language file id</param>
        /// <param name="version"> File version</param>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns> Donwloaded file in bytes[].</returns>
        public async Task<byte[]> DownloadFileVersion(string projectId, string languageFileId, int version)
        {
            Ensure.ArgumentNotNullOrEmptyString(languageFileId, "languageFileId");
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");
            Ensure.ArgumentNotNull(version, "version");

            var fileContent = await
                    ApiConnection.Get<byte[]>(ApiUrls.DownloadFileForVersion(projectId, languageFileId, version), null);

            return fileContent;

        }

        /// <summary>
        /// Get the project analysis report for a given project.
        /// The project must be created in GroupShare, not in Studio and published in GS
        /// </summary>
        /// <param name="projectId">The project id</param>
        /// <param name="languageCode"> language code. Eg: en-US</param>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns> List <see cref="AnalysisReports"/>s.</returns>
        public async Task<IReadOnlyList<AnalysisReports>> GetAnalysisReports(string projectId, string languageCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");

            var reportResult = await ApiConnection.GetAll<AnalysisReports>(ApiUrls.AnalysisReports(projectId, languageCode),null);
            return reportResult;
        }

        /// <summary>
        /// Get the project analysis report for a given project, in html format.
        /// The project must be created in GroupShare, not in Studio and published in GS
        /// </summary>
        /// <param name="projectId">The project id</param>
        /// <param name="languageCode"> language code. Eg: en-US</param>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns> List <see cref="AnalyseResponseHtml"/>s.</returns>
        public async Task<IReadOnlyList<AnalyseResponseHtml>> GetAnalysisReportsAsHtml(string projectId, string languageCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");

            var reportResult = await ApiConnection.GetWithContent<IReadOnlyList<AnalyseResponseHtml>>(ApiUrls.AnalysisReports(projectId, languageCode), "text/html");

            return reportResult;
        }

	    /// <summary>
	    ///  Get project settings for a language file
	    /// </summary>
	    /// <param name="projectId">The id of the project</param>
	    /// <param name="languageFileId">The if of the language file</param>
	    /// <remarks>
	    /// This method requires authentication.
	    /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
	    /// </remarks>
	    /// <exception cref="AuthorizationException">
	    /// Thrown when the current user does not have permission to make the request.
	    /// </exception>
	    /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
	    /// <returns><see cref="ProjectSettings"/></returns>
		public async Task<ProjectSettings> GetProjectSettings(string projectId, string languageFileId)
	    {
			Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");
		    Ensure.ArgumentNotNullOrEmptyString(projectId, "languageFileId");

		    var projectSettings =
			    await ApiConnection.Get<ProjectSettings>(ApiUrls.GetProjectSettings(projectId, languageFileId),null);
		    return projectSettings;
	    }

	    /// <summary>
	    /// Validates that the user can open the file in universal editor
	    /// </summary>
	    /// <param name="projectId">The id of the project</param>
	    /// <param name="languageFileId">The if of the language file</param>
	    /// <remarks>
	    /// This method requires authentication.
	    /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
	    /// </remarks>
	    /// <exception cref="AuthorizationException">
	    /// Thrown when the current user does not have permission to make the request.
	    /// </exception>
	    /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		public async Task<string> IsUserAuthorizedToOpenFile(string projectId, string languageFileId)
	    {
			Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");
		    Ensure.ArgumentNotNullOrEmptyString(projectId, "languageFileId");
		    var response = await ApiConnection.Get<string>(ApiUrls.IsAuthorizedToOpenInEditor(projectId, languageFileId),null);

			return response;
	    }

	    /// <summary>
	    ///Rreturns user permissions in editor
	    /// </summary>
	    /// <param name="projectId">The id of the project</param>
	    /// <param name="languageFileId">The if of the language file</param>
	    /// <remarks>
	    /// This method requires authentication.
	    /// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
	    /// </remarks>
	    /// <exception cref="AuthorizationException">
	    /// Thrown when the current user does not have permission to make the request.
	    /// </exception>
	    /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
	    /// <returns><see cref="EditorProfile"/></returns>
		public async Task<EditorProfile> EditorProfile(string projectId, string languageFileId)
	    {
		    Ensure.ArgumentNotNullOrEmptyString(projectId, "projectId");
		    Ensure.ArgumentNotNullOrEmptyString(projectId, "languageFileId");
		    var editorProfile = await ApiConnection.Get<EditorProfile>(ApiUrls.EditorProfile(projectId, languageFileId),null);

		    return editorProfile;
	    }

	    ///  <summary>
	    /// Checks in a file edited in the Universal Editor
	    ///  </summary>
	    /// <param name="projectId">The id of the project</param>
	    /// <param name="languageFileId">The if of the language file</param>
	    /// <remarks>
	    ///  This method requires authentication.
	    ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
	    ///  </remarks>
	    ///  <exception cref="AuthorizationException">
	    ///  Thrown when the current user does not have permission to make the request.
	    ///  </exception>
	    ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
	    public async Task<OnlineCheckInRequest> OnlineCheckin(string projectId, string languageFileId)
	    {
		    Ensure.ArgumentNotNullOrEmptyString(projectId, "projectid");
		    Ensure.ArgumentNotNullOrEmptyString(languageFileId, "LanguageFileId");

			//checkout file first
		    var checkoutResponse = await OnlineCheckout(projectId, languageFileId).ConfigureAwait(true);

			return await ApiConnection.Post<OnlineCheckInRequest>(
				ApiUrls.OnlineCheckIn(projectId, languageFileId), checkoutResponse,
				"application/json");  
	    }

	    ///  <summary>
	    /// Checks out a file for editing in the Universal Editor
	    ///  </summary>
	    /// <param name="projectId">The id of the project</param>
	    /// <param name="languageFileId">The if of the language file</param>
	    /// <remarks>
	    ///  This method requires authentication.
	    ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
	    ///  </remarks>
	    ///  <exception cref="AuthorizationException">
	    ///  Thrown when the current user does not have permission to make the request.
	    ///  </exception>
	    ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		public async Task<OnlineCheckInRequest> OnlineCheckout(string projectId, string languageFileId)
	    {
			Ensure.ArgumentNotNullOrEmptyString(projectId, "projectid");
		    Ensure.ArgumentNotNullOrEmptyString(languageFileId, "LanguageFileId");
		    return
			    await ApiConnection.Post<OnlineCheckInRequest>(ApiUrls.OnlineCheckout(projectId, languageFileId),
				    "application/json");
	    }

	    ///  <summary>
	    /// Undoes an online checkout, note that you will loose all the changes done inside the OnlineEditor. To make a proper checkin use the OnlineCheckOutController.
	    ///  </summary>
	    /// <param name="projectId">The id of the project</param>
	    /// <param name="languageFileId">The if of the language file</param>
	    /// <remarks>
	    ///  This method requires authentication.
	    ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
	    ///  </remarks>
	    ///  <exception cref="AuthorizationException">
	    ///  Thrown when the current user does not have permission to make the request.
	    ///  </exception>
	    ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		public async Task UndoCheckout(string projectId, string languageFileId)
	    {
			Ensure.ArgumentNotNullOrEmptyString(projectId, "projectid");
		    Ensure.ArgumentNotNullOrEmptyString(languageFileId, "LanguageFileId");

		    await ApiConnection.Delete(ApiUrls.UndoCheckout(projectId, languageFileId));
	    }

	    ///  <summary>
	    /// Health check call used to keep the OE license seat taken
	    ///  </summary>
	    ///  This method requires authentication.
	    ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
	    ///  <exception cref="AuthorizationException">
	    ///  Thrown when the current user does not have permission to make the request.
	    ///  </exception>
	    ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		public async Task<string> OnlineCheckoutHealthCheck(string editorProfileMode)
	    {
		    var response = await ApiConnection.Get<string>(ApiUrls.OnlineCheckoutHealthCheck(editorProfileMode), null);
		    return response;
	    }

	    ///  <summary>
	    /// Checks if the given language file is check-out to someone other than the user making this call
	    ///  </summary>
	    /// <param name="languageFileId">The if of the language file</param>
	    /// <remarks>
	    ///  This method requires authentication.
	    ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
	    ///  </remarks>
	    ///  <exception cref="AuthorizationException">
	    ///  Thrown when the current user does not have permission to make the request.
	    ///  </exception>
	    ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		public async Task<bool> IsCheckoutToSomeoneElse(string languageFileId, string editorProfileMode)
	    {
			Ensure.ArgumentNotNullOrEmptyString(languageFileId, "LanguageFileId");
		    return await ApiConnection.Get<bool>(ApiUrls.IsCheckoutToSomeoneElse(languageFileId, editorProfileMode), null);
	    }

		///  <summary>
		/// Checks in a file for editing
		///  </summary>
		/// <param name="projectId">The id of the project</param>
		/// <param name="languageFileId">The if of the language file</param>
		/// <param name="comment">Comment</param>	 
		/// <remarks>
		///  This method requires authentication.
		///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		///  </remarks>
		///  <exception cref="AuthorizationException">
		///  Thrown when the current user does not have permission to make the request.
		///  </exception>
		///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		public async Task<string> ExternalCheckin(string projectId, string languageFileId, string comment)
	    {
		    Ensure.ArgumentNotNullOrEmptyString(projectId, "projectid");
		    Ensure.ArgumentNotNullOrEmptyString(languageFileId, "LanguageFileId");
		    await ExternalCheckout(projectId, languageFileId).ConfigureAwait(true);

			return await ApiConnection.Post<string>(ApiUrls.ExternalCheckin(projectId, languageFileId),comment, "application/json");
	    }

	    ///  <summary>
	    /// Checks out a file for editing
	    ///  </summary>
	    /// <param name="projectId">The id of the project</param>
	    /// <param name="languageFileId">The if of the language file</param>
	    /// <remarks>
	    ///  This method requires authentication.
	    ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
	    ///  </remarks>
	    ///  <exception cref="AuthorizationException">
	    ///  Thrown when the current user does not have permission to make the request.
	    ///  </exception>
	    ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		public async Task<string> ExternalCheckout(string projectId, string languageFileId)
	    {
		    Ensure.ArgumentNotNullOrEmptyString(projectId, "projectid");
		    Ensure.ArgumentNotNullOrEmptyString(languageFileId, "LanguageFileId");

		    return await ApiConnection.Post<string>(ApiUrls.ExternalCheckout(projectId, languageFileId), "application/json");
	    }

        ///  <summary>
        /// Gets the dashboard data
        ///  </summary>
        ///  This method requires authentication.
        ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        ///  <exception cref="AuthorizationException">
        ///  Thrown when the current user does not have permission to make the request.
        ///  </exception>
        ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [Obsolete("The dashboard function is obsolete in Groupshare 2020. " + 
            "Use the new functions DataboardProjectsPerMonth, DashboardTopLanguagePairs, " +
            "DashboardWordsPerMonth, DashboardWordsPerOrganization, DashboardStatistics instead for Groupshare 2020.")]
        public async Task<Dashboard> Dashboard()
	    {
		    return await ApiConnection.Get<Dashboard>(ApiUrls.Dashboard(), null);
	    }

        /// <summary>
        /// Gets the dashboard projects per month data
        /// </summary>
        /// <remarks>
		///  This method requires authentication.
		///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		///  </remarks>
		///  <exception cref="AuthorizationException">
		///  Thrown when the current user does not have permission to make the request.
		///  </exception>
		///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<IReadOnlyList<DashboardCount>> DataboardProjectsPerMonth()
        {
            return await ApiConnection.GetAll<DashboardCount>(ApiUrls.DashboardProjectsPerMonth());
        }

        /// <summary>
        /// Gets the dashboard top language pairs
        /// </summary>
        /// <param name="noOfTopLanguagePairs"></param>
        /// <returns></returns>
        /// <remarks>
        ///  This method requires authentication.
        ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        ///  </remarks>
        ///  <exception cref="AuthorizationException">
        ///  Thrown when the current user does not have permission to make the request.
        ///  </exception>
        ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<IReadOnlyList<LanguagePairsInProject>> DashboardTopLanguagePairs(int? noOfTopLanguagePairs)
        {
            return await ApiConnection.GetAll<LanguagePairsInProject>(ApiUrls.DashboardTopLanguagePairs(noOfTopLanguagePairs));
        }

        /// <summary>
        /// Gets the dashboard words per month
        /// </summary>
        /// <remarks>
        ///  This method requires authentication.
        ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        ///  </remarks>
        ///  <exception cref="AuthorizationException">
        ///  Thrown when the current user does not have permission to make the request.
        ///  </exception>
        ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<IReadOnlyList<DashboardCount>> DashboardWordsPerMonth()
        {
            return await ApiConnection.GetAll<DashboardCount>(ApiUrls.DashboardWordsPerMonth());
        }

        /// <summary>
        /// Gets the dashboard words per organization
        /// </summary>
        /// <remarks>
        ///  This method requires authentication.
        ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        ///  </remarks>
        ///  <exception cref="AuthorizationException">
        ///  Thrown when the current user does not have permission to make the request.
        ///  </exception>
        ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<IReadOnlyList<WordsInOrganization>> DashboardWordsPerOrganization()
        {
            return await ApiConnection.GetAll<WordsInOrganization>(ApiUrls.DashboardWordsPerOrganization());
        }

        /// <summary>
        /// Gets the dashboard statistics data
        /// </summary>
        /// <remarks>
        ///  This method requires authentication.
        ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        ///  </remarks>
        ///  <exception cref="AuthorizationException">
        ///  Thrown when the current user does not have permission to make the request.
        ///  </exception>
        ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<DashboardStatistics> DashboardStatistics()
        {
            return await ApiConnection.Get<DashboardStatistics>(ApiUrls.DashboardStatistics(), null);
        }

        ///  <summary>
        /// Retries the audit trail for all the language files in the given project
        ///  </summary>
        ///  This method requires authentication.
        /// <param name="projectId">The id of the project</param>
        ///  This method requires authentication.
        ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        ///  <exception cref="AuthorizationException">
        ///  Thrown when the current user does not have permission to make the request.
        ///  </exception>
        ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<IReadOnlyList<AuditTrail>> AuditTrail(string projectId)
	    {
			Ensure.ArgumentNotNullOrEmptyString(projectId, "projectid");
		    return ApiConnection.GetAll<AuditTrail>(ApiUrls.AuditTrail(projectId), null);
		}

        #endregion

        #region Reporting
        /// <summary>
        /// Returns the projects report data
        /// </summary>
        /// <param name="options"></param>
        /// <remarks>
        ///  This method requires authentication.
        ///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
        ///  </remarks>
        ///  <exception cref="AuthorizationException">
        ///  Thrown when the current user does not have permission to make the request.
        ///  </exception>
        ///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<IReadOnlyList<ProjectReport>> ReportingProjectPredefinedReportData(ReportingOptions options)
        {
            var jsonOptions = options.Stringify();
            return ApiConnection.GetAll<ProjectReport>(ApiUrls.GetProjectPredefinedReportData(jsonOptions));
        }

        /// <summary>
        /// Returns the tasks Report report data
        /// </summary>
        /// <param name="options"></param>
        /// <remarks>
		///  This method requires authentication.
		///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		///  </remarks>
		///  <exception cref="AuthorizationException">
		///  Thrown when the current user does not have permission to make the request.
		///  </exception>
		///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<IReadOnlyList<TaskReport>> ReportingTasksReportData(ReportingOptions options)
        {
            var jsonOptions = options.Stringify();
            return ApiConnection.GetAll<TaskReport>(ApiUrls.GetTasksReportData(jsonOptions));
        }

        /// <summary>
        /// Returns the TM Leverage report data
        /// </summary>
        /// <param name="options"></param>
        /// <remarks>
		///  This method requires authentication.
		///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		///  </remarks>
		///  <exception cref="AuthorizationException">
		///  Thrown when the current user does not have permission to make the request.
		///  </exception>
		///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<TmLeverageReport> ReportingTmLeverageData(ReportingOptions options)
        {
            var jsonOptions = options.Stringify();
            return ApiConnection.Get<TmLeverageReport>(ApiUrls.GetTmLeverageData(jsonOptions), null);
        }
        #endregion
    }
}