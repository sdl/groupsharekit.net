using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sdl.Community.GroupShareKit.Exceptions;
using Sdl.Community.GroupShareKit.Models.Response;

namespace Sdl.Community.GroupShareKit.Clients
{
	/// <summary>
	/// A client for GroupShare's Project API.
	/// </summary>
	/// <remarks>
	/// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">Project API documentation</a> for more details.
	/// </remarks>
	public interface IProjectClient
	{
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

		Task<Project> GetProject(ProjectsRequest request);

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
		Task<Project> GetAllProjects();

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
		List<ProjectDetails> GetProjectsForOrganization(string organizationName);

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
		Task<IReadOnlyList<File>> GetAllFilesForProject(string projectId);

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
		Task<IReadOnlyList<Phase>> GetAllPhasesForProject(string projectId);

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
		Task<IReadOnlyList<PhasesWithAssignees>> GetPhasesWithAssignees(string projectId, int phaseId);

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
		Task ChangePhases(string projectId, ChangePhaseRequest request);


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
		Task ChangeAssignments(string projectId, ChangeAssignmentRequest request);

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
		Task<string> CreateProject(CreateProjectRequest request);

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
        Task<string> CreateProject(BasicCreateProjectRequest request,
            string filesPath, string referenceFilesPath = null, string[] perfectMatchFilesPaths = null);

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
        Task DeleteProject(string projectId);

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
		Task<ProjectDetails> Get(string projectId);

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
		Task<PublishingStatus> PublishingStatus(string projectId);

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
		Task<byte[]> DownloadFiles(string projectId, List<string> languageFileIds);

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
		Task<byte[]> DownloadFile(FileDownloadRequest downloadRequest);

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
        Task<byte[]> DownloadNative(string projectId);


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
        Task<byte[]> Finalize(string projectId, List<string> languageFileIds);

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
        Task<IReadOnlyList<UserAssignments>> GetUserAssignments();

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
		Task<IReadOnlyList<ProjectAssignment>> GetProjectAssignmentById(string projectId, List<string> fileIdsList);

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
		Task<string> UploadFilesForProject(string projectId, byte[] rawData, string projectName);

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
		Task<string> ChangeProjectStatus(ChangeStatusRequest statusRequest);

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
		Task<string> ChangeProjectStatusDetach(ChangeStatusRequest statusRequest);

		/// <summary>
		///Deletes detach to a project status
		/// <param name="projectId deleteTms">></param>
		/// </summary>
		/// <remarks>
		/// This method requires authentication.
		/// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		/// </remarks>
		/// <exception cref="AuthorizationException">
		/// Thrown when the current user does not have permission to make the request.
		/// </exception>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		Task ChangeProjectStatusDeleteDetach(string projectId, bool deleteTms);

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
		Task<string> PublishPackage(CreateProjectRequest projectRequest);

		/// <summary>
		///Gets a list of file statistics for a project
		/// </summary>
		/// <remarks>
		/// This method requires authentication.
		/// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		/// </remarks>
		/// <exception cref="AuthorizationException">
		/// Thrown when the current user does not have permission to make the request.
		/// </exception>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A list of <see cref="GetAllProjectFileStatistics"/>s.</returns>
		Task<IReadOnlyList<ProjectFileStatistics>> GetAllProjectFileStatistics(string projectId);

		/// <summary>
		/// Get the project language statistics.
		/// </summary>
		/// <remarks>
		/// This method requires authentication.
		/// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		/// </remarks>
		/// <exception cref="AuthorizationException">
		/// Thrown when the current user does not have permission to make the request.
		/// </exception>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A dictionary with the key represneting the language code and the value as project statistics </returns>
		Task<Dictionary<string, ProjectStatistics>> GetProjectLanguageStatistics(string projectId);

		#endregion

		#region Project template client

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
		Task<IReadOnlyList<ProjectTemplates>> GetAllTemplates();

		/// <summary>
		///Creates a template
		/// </summary>
		/// <param name="projectRequest"><see cref="ProjectTemplates"/></param>
		/// <remarks>
		/// This method requires authentication.
		/// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		/// </remarks>
		/// <exception cref="AuthorizationException">
		/// Thrown when the current user does not have permission to make the request.
		/// </exception>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>Id of created template/></returns>
		Task<string> CreateTemplate(ProjectTemplates projectRequest, byte[] rawData);

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
		Task<string> GetTemplateById(string templateId);

		/// <summary>
		///Deletes a template 
		/// </summary>
		/// <param name="id">string</param>
		/// <remarks>
		/// This method requires authentication.
		/// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		/// </remarks>
		/// <exception cref="AuthorizationException">
		/// Thrown when the current user does not have permission to make the request.
		/// </exception>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		Task Delete(string id);

		/// <summary>
		///Uploades a template to a newly created project 
		/// This method shoulg be called after you create a project in order to add the template
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
		Task<string> UploadProjectTemplate(string templateId, byte[] projectTemplate, string templateName);

		#endregion

		#region file version methods

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
		Task<IReadOnlyList<FileVersion>> GetFileVersions(string languageFileId);

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
		Task<byte[]> DownloadFileVersion(string projectId, string languageFileId, int version);

		#endregion

		/// <summary>
		/// Get the project analysis report for a given project.
		/// The project must be created in GroupShare, not in Studio and published in GS
		/// </summary>
		/// <param name="projectId">The project id</param>
		/// <param name="languageCode"> language code. Eg: en-US/param>
		/// <remarks>
		/// This method requires authentication.
		/// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		/// </remarks>
		/// <exception cref="AuthorizationException">
		/// Thrown when the current user does not have permission to make the request.
		/// </exception>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns> List <see cref="AnalysisReports"/>s.</returns>
		Task<IReadOnlyList<AnalysisReports>> GetAnalysisReports(string projectId, string languageCode);

		/// <summary>
		/// Get the project analysis report for a given project, in html format.
		/// The project must be created in GroupShare, not in Studio and published in GS
		/// </summary>
		/// <param name="projectId">The project id</param>
		/// <param name="languageCode"> language code. Eg: en-US/param>
		/// <remarks>
		/// This method requires authentication.
		/// See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		/// </remarks>
		/// <exception cref="AuthorizationException">
		/// Thrown when the current user does not have permission to make the request.
		/// </exception>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns> List <see cref="AnalyseResponseHtml"/>s.</returns>
		Task<IReadOnlyList<AnalyseResponseHtml>> GetAnalysisReportsAsHtml(string projectId, string languageCode);

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
		Task<ProjectSettings> GetProjectSettings(string projectId, string languageFileId);

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
		Task<string> IsUserAuthorizedToOpenFile(string projectId, string languageFileId);

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
		Task<EditorProfile> EditorProfile(string projectId, string languageFileId);

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
		Task<OnlineCheckInRequest> OnlineCheckin(string projectId, string languageFileId);

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
		Task<OnlineCheckInRequest> OnlineCheckout(string projectId, string languageFileId);

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
		Task UndoCheckout(string projectId, string languageFileId);

		///  <summary>
		/// Health check call used to keep the OE license advanced/basic seat taken
        /// By default, if no parameter advanced/basic is passed in api route, advanced mode is checkout.
		///  </summary>
		///  This method requires authentication.
		///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		///  <exception cref="AuthorizationException">
		///  Thrown when the current user does not have permission to make the request.
		///  </exception>
		///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		Task<string> OnlineCheckoutHealthCheck(string editorProfileMode);

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
		Task<bool> IsCheckoutToSomeoneElse(string languageFileId, string editorProfileMode);

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
		Task<string> ExternalCheckin(string projectId, string languageFileId,string comment);

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
		Task<string> ExternalCheckout(string projectId, string languageFileId);

		///  <summary>
		/// Gets the dashboard data
		///  </summary>
		///  This method requires authentication.
		///  See the <a href="http://gs2017dev.sdl.com:41234/documentation/api/index#/">API documentation</a> for more information.
		///  <exception cref="AuthorizationException">
		///  Thrown when the current user does not have permission to make the request.
		///  </exception>
		///  <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		Task<Dashboard> Dashboard();

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
        Task<IReadOnlyList<DashboardCount>> DataboardProjectsPerMonth();

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
        Task<IReadOnlyList<LanguagePairsInProject>> DashboardTopLanguagePairs(int? noOfTopLanguagePairs);

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
        Task<IReadOnlyList<DashboardCount>> DashboardWordsPerMonth();

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
        Task<IReadOnlyList<WordsInOrganization>> DashboardWordsPerOrganization();

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
        Task<DashboardStatistics> DashboardStatistics();

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
        Task<IReadOnlyList<AuditTrail>> AuditTrail(string projectId);

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
        Task<IReadOnlyList<ProjectReport>> ReportingProjectPredefinedReportData(ReportingOptions options);

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
        Task<IReadOnlyList<TaskReport>> ReportingTasksReportData(ReportingOptions options);

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
        Task<TmLeverageReport> ReportingTmLeverageData(ReportingOptions options);
    }
}