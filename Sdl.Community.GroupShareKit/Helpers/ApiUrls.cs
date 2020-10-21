using System;
using Sdl.Community.GroupShareKit.Clients;

namespace Sdl.Community.GroupShareKit.Helpers
{
    public static class ApiUrls
    {
        public static readonly Uri CurrentProjectServerUrl = new Uri("api/projectserver/v2", UriKind.Relative);
        public static readonly Uri CurrentProjectServerV3Url = new Uri("api/projectserver/v3", UriKind.Relative);
        public static readonly Uri CurrentProjectServerV4Url = new Uri("api/projectserver/v4", UriKind.Relative);
        public static readonly Uri CurrentManagementUrl = new Uri("api/management/v2", UriKind.Relative);
        public static readonly Uri CurrentAuthenticationUrl = new Uri("authentication/api/1.0", UriKind.Relative);
        public static readonly Uri CurrentTranslationMemoriesUrl = new Uri("api/tmservice", UriKind.Relative);
        public static readonly Uri CurrentFieldServiceUrl = new Uri("api/fieldservice", UriKind.Relative);
        public static readonly Uri CurrentMultitermUrl = new Uri("multiterm/api/1.0", UriKind.Relative);
        public static readonly Uri CurrentLanguageResourceServiceUrl = new Uri("api/language-resource-service", UriKind.Relative);
        public static readonly Uri TranslateAndAnalysisServiceUrl = new Uri("ta/api", UriKind.Relative);
        public static readonly Uri ReportingServiceUrl = new Uri("api/reports", UriKind.Relative);

        public static Uri Modules()
        {
            return "{0}/modules".FormatUri(CurrentManagementUrl);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single user for the user name
        /// </summary>
        public static Uri Login()
        {
            return "{0}/login".FormatUri(CurrentAuthenticationUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single user for the user name
        /// </summary>
        public static Uri User()
        {
            return "{0}/users".FormatUri(CurrentManagementUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single user for the user id
        /// </summary>
        public static Uri User(string userId)
        {
            return "{0}/users/{1}".
                FormatUri(CurrentManagementUrl,userId);
        }


        /// <summary>
        ///  Returns the <see cref="Uri"/> that returns all roles
        /// </summary>
        public static Uri Roles()
        {
            return "{0}/roles".
                FormatUri(CurrentManagementUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single role
        /// </summary>
        public static Uri Role(string roleId)
        {

            return "{0}/roles/{1}".
                FormatUri(CurrentManagementUrl, roleId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a membership
        /// </summary>
        public static Uri RoleMembership()
        {
            return "{0}/roles/membership".FormatUri(CurrentManagementUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a single organization
        /// </summary>
        public static Uri Organization(string organizationId)
        {
            return "{0}/organizations/{1}".
                FormatUri(CurrentManagementUrl, organizationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that a list of organizations
        /// </summary>
        public static Uri Organizations()
        {
            return "{0}/organizations".
                FormatUri(CurrentManagementUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> based on the organization tag
        /// </summary>
        public static Uri GetOrganizationsByTag(string tag)
        {
            return "{0}/organizations?tag={1}".
                FormatUri(CurrentManagementUrl, tag);
        }



        /// <summary>
        ///  Returns the <see cref="Uri"/> that gets all organization resources
        /// </summary>
        /// <param name="organizationId">Organization Id</param>
        public static Uri OrganizationResources(string organizationId)
        {
            return "{0}/organizationresources/{1}".
                FormatUri(CurrentManagementUrl,organizationId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that moves a resource to a organization
        /// </summary>
        public static Uri MoveOrganizationsResources()
        {
            return "{0}/organizationresources".
                FormatUri(CurrentManagementUrl);
        }

        /// <summary>
        /// Returs the <see cref="Uri" /> that gets all projects
        /// </summary>
        public static Uri GetAllProjects()
        {
            return "{0}/projects".
                FormatUri(CurrentProjectServerUrl);
        }

        public static Uri GetPerfectMatchFiles(string projectId, int matchOrderIndex)
        {
            return "{0}/projects/{1}/files/upload/perfectmatch/{2}?relativePath="
                .FormatUri(CurrentProjectServerV4Url, projectId, matchOrderIndex);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all permissions
        /// </summary>
        public static Uri Permissions()
        {
            return "{0}/permissions".
                FormatUri(CurrentManagementUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all permissions for users
        /// </summary>
        public static Uri PermissionUsers()
        {
            return "{0}/permissions/user".
                FormatUri(CurrentManagementUrl);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that represents the project
        /// </summary>
        public static Uri Project(string projectId)
        {
            return "{0}/projects/{1}".
                FormatUri(CurrentProjectServerUrl, projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that represents publishing status of a server project.
        /// </summary>
        public static Uri PublishingStatus(string projectId)
        {
            return "{0}/projects/{1}/publishingstatus".
                FormatUri(CurrentProjectServerUrl, projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that represents downloaded files with the specific languageCodeid and type
        /// </summary>
        public static Uri DownloadFile(string projectId, string type)
        {
            return "{0}/projects/{1}/download/{2}".
                FormatUri(CurrentProjectServerUrl, projectId, type);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that represents downloaded files with the specific languageCode ids
        /// </summary>
        public static Uri DownloadFiles(string projectId,string languageFileIdsQuery)
        {
            return "{0}/projects/{1}/download/?{2}archive=true".
                FormatUri(CurrentProjectServerUrl, projectId,languageFileIdsQuery);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that represents downloaded native files
        /// </summary>
        public static Uri DownloadNative(string projectId)
        {
            return "{0}/projects/{1}/download/targetnativefiles".
                FormatUri(CurrentProjectServerUrl, projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that represents the finalization of a project
        /// </summary>
        public static Uri Finalize(string projectId, string languageFileIdsQuery)
        {
            return "{0}/projects/{1}/finalize/?{2}".
                FormatUri(CurrentProjectServerUrl, projectId, languageFileIdsQuery);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that  publish project package associated with the specified organziation
        /// </summary>
        public static Uri PublishProjectPackage(string projectId)
        {
            return "{0}/projects/{1}/publishpackage".
                FormatUri(CurrentProjectServerUrl,projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all files associated with the specified project
        /// </summary>
        /// <param name="projectId">The project id</param>
        public static Uri ProjectFiles(string projectId)
        {
            return "{0}/projects/{1}/files".
                FormatUri(CurrentProjectServerUrl, projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all project templates
        /// </summary>
        public static Uri ProjectTemplates()
        {
            return "{0}/projects/templates".
                FormatUri(CurrentProjectServerUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a template
        /// </summary>
        /// <param name="templateId">The template id</param>
        public static Uri ProjectTemplates(string templateId)
        {
            return "{0}/projects/templates/{1}".
                FormatUri(CurrentProjectServerUrl,templateId);
        }


        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all project templates
        /// </summary>
        public static Uri ProjectTemplatesV3()
        {
            return "{0}/projects/templates".
                FormatUri(CurrentProjectServerV3Url);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all phases associated with the specified project
        /// </summary>
        /// <param name="projectId">The project id</param>
        public static Uri ProjectPhases(string projectId)
        {
            return "{0}/phases/{1}".
                FormatUri(CurrentProjectServerUrl, projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri" />that gets a list of files for the requested project with all the project phases and theirs assignees
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="phaseId">Phase id</param>
        public static Uri ProjectPhasesWithAssignees(string projectId,int phaseId)
        {
            return "{0}/projects/{1}/phaseswithassignees/{2}".
                FormatUri(CurrentProjectServerUrl, projectId,phaseId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that change the project phases
        /// </summary>
        /// <param name="projectId">The project id</param>
          public static Uri ChangePhases(string projectId)
        {
            return "{0}/projects/{1}/changephase".
               FormatUri(CurrentProjectServerUrl, projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that change the project phases
        /// </summary>
        /// <param name="projectId">The project id</param>
        public static Uri ChangeAssignments(string projectId)
        {
            return "{0}/projects/{1}/changeassignment".
               FormatUri(CurrentProjectServerUrl, projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets  file versions informations
        /// </summary>
        /// <param name="languageFileId">Language file id</param>
        public static Uri GetFileVersion(string languageFileId)
        {
            return "{0}/projects/fileversions/{1}".
                FormatUri(CurrentProjectServerUrl, languageFileId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> to download the file for a specified version 
        /// </summary>
        /// <param name="projectId">The project id</param>
        /// <param name="languageFileId"> Language file id</param>
        /// <param name="version"> File version</param>
        public static Uri DownloadFileForVersion(string projectId, string languageFileId, int version)
        {
            return "{0}/projects/{1}/fileversions/download/{2}/{3}".
                FormatUri(CurrentProjectServerUrl, projectId, languageFileId, version);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for license informations
        /// </summary>
        public static Uri GetLicenseInformations()
        {
            return "{0}/license".FormatUri(CurrentManagementUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets user assignments
        /// </summary>
        public static Uri GetProjectsAssignments()
        {
            return "{0}/projects/userassignments".FormatUri(CurrentProjectServerUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> thet gets a list of assignements for a project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="fileIdQuery"></param>
        public static Uri GetProjectAssignmentById(string projectId, string fileIdQuery)
        {
            return "{0}/projects/{1}/assignment?{2}"
                .FormatUri(CurrentProjectServerUrl, projectId, fileIdQuery);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that adds the template
        /// </summary>
        /// <param name="templateId">Template id</param>
        public static Uri UploadProjectTemplate(string templateId)
        {
            return "{0}/projects/templates/{1}/upload".FormatUri(CurrentProjectServerUrl, templateId);
        }

        /// <summary>
        ///  Returns the <see cref="Uri"/> that adds files for project
        /// </summary>
        /// <param name="projectId">Project id</param>
        public static Uri UploadFilesForProject(string projectId)
        {
            return "{0}/projects/{1}/files/upload".FormatUri(CurrentProjectServerUrl, projectId);
        }

        /// <summary>
        ///  Returns the uri string that adds files for project
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="isReferenceFile">The flag to indicate if the files to upload are rerference files</param>
        /// <param name="createProjectAfterUpload">The flag to indicate if the project should be started creating after the upload immediately</param>
        public static string UploadFilesForProject(string projectId, bool isReferenceFile, bool createProjectAfterUpload)
        {
            // use string instead of Uri is to avoid the format errors. In some case we might append the path for this uri.
            return string.Format("{0}/projects/{1}/files/upload?&reference={2}&create={3}&relativePath=", 
                CurrentProjectServerUrl, projectId, isReferenceFile, createProjectAfterUpload); 
        }        

        /// <summary>
        ///  Returns the <see cref="Uri"/> that starts the project creation
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Uri StartProjectCreationUri(string projectId)
        {
            return "{0}/projects/{1}/create".FormatUri(CurrentProjectServerUrl, projectId);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that changes the project status
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="status">Status</param>
        public static Uri ChangeProjectStatus(string projectId, string status)
        {
            return "{0}/projects/{1}/changestatus/{2}".FormatUri(CurrentProjectServerUrl,projectId, status);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that changes the project status
        /// </summary>
        /// <param name="statusRequest">Status request</param>
        public static Uri ChangeProjectStatus(ChangeStatusRequest statusRequest)
        {
            var status = Enum.GetName(typeof (ChangeStatusRequest.ProjectStatus), statusRequest.Status);
            return "{0}/projects/{1}/detach?status={2}".FormatUri(CurrentProjectServerUrl, statusRequest.ProjectId,status);
        }

        /// <summary>
        ///  Returns the <see cref="Uri"/> that deletes detach to a project status
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="deleteTms">True/False if the tms should be deleted</param>
        public static Uri ProjectStatusDeleteDetach(string projectId, bool deleteTms)
        {
            return "{0}/projects/{1}/detach/{2}".FormatUri(CurrentProjectServerUrl, projectId, deleteTms);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that links resource to a organization
        /// </summary>
        public static Uri LinkResourceToOrganization()
        {
            return "{0}/resourcelink ".FormatUri(CurrentManagementUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets a list of users in a certain role 
        /// </summary>
        /// <param name="roleId">The role id</param>
        public static Uri GetUsersForRole(string roleId)
        {
            return "{0}/roles/{1}/membership".FormatUri(CurrentManagementUrl, roleId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that removes  users from a certain role 
        /// </summary>
        /// <param name="roleId">The role id</param>
        public static Uri DeleteUserFromRole(string roleId)
        {
            return "{0}/roles/{1}/users".FormatUri(CurrentManagementUrl, roleId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gives a list of tms
        /// </summary>
        public static Uri GetTms()
        {
            return "{0}/tms".FormatUri(CurrentTranslationMemoriesUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gives specified tm.
        /// </summary>
        public static Uri GetTmById(string tmId)
        {
            return "{0}/tms/{1}".FormatUri(CurrentTranslationMemoriesUrl,tmId);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that gives the language direction information for a specified tm
        /// </summary>
        /// <param name="tmId"></param>
        /// <param name="languageDirectionId"></param>
        public static Uri GetLanguageDirectionForTm(string tmId, string languageDirectionId)
        {
            return "{0}/tms/{1}/language-directions/{2}".FormatUri(CurrentTranslationMemoriesUrl, tmId,languageDirectionId);
        }

        /// <summary>
        ///  Returns the <see cref="Uri"/> that gives the tms for resource language template id
        /// </summary>
        /// <param name="resourceTemplateId">resource te,plate id</param>
        public static Uri GetTmsNumberByLanguageResourceTemplateId(string resourceTemplateId)
        {
            return "{0}/tms/by-language-resource-template/{1}/count".FormatUri(CurrentTranslationMemoriesUrl, resourceTemplateId);
        }
        /// <summary>
        ///  Returns the <see cref="Uri"/> that gives the tms for field template id
        /// </summary>
        /// <param name="fieldTemplateId"> field template id</param>
        public static Uri GetTmsNumberByFieldTemplateId(string fieldTemplateId)
        {
            return "{0}/tms/by-field-template/{1}/count".FormatUri(CurrentTranslationMemoriesUrl, fieldTemplateId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gives a list of termbases
        /// </summary>
        public static Uri GetTermbases()
        {
            return "{0}/termbases".FormatUri(CurrentMultitermUrl);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that gives specified tm
        /// </summary>
        public static Uri GetTermbaseById(string termbaseId)
        {
            return "{0}/termbases/{1}".FormatUri(CurrentMultitermUrl,termbaseId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns field templates
        /// </summary>
        public static Uri FieldTemplate()
        {
            return "{0}/templates".FormatUri(CurrentFieldServiceUrl);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that returns specific  field template
        /// </summary>
        public static Uri GetFieldTemplateById(string id)
        {
            return "{0}/templates/{1}".FormatUri(CurrentFieldServiceUrl,id);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the health status of tm service
        /// </summary>
        public static Uri Health()
        {
            return "{0}/health".FormatUri(CurrentTranslationMemoriesUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the info of tm service
        /// </summary>
        public static Uri GetTmServiceInfo()
        {
            return "{0}/info".FormatUri(CurrentTranslationMemoriesUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets filters from the specified termbase
        /// </summary>
        public static Uri GetFilers(string termbaseId)
        {
            return "{0}/termbases/{1}/filters".FormatUri(CurrentMultitermUrl, termbaseId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that search for terms in termbase
        /// </summary>
        public static Uri Search()
        {
            return "{0}/termbases/search".FormatUri(CurrentMultitermUrl);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that gets a concept
        /// </summary>
        public static Uri GetConcepts(ConceptResponse response)
        {
            return "{0}/termbases/{1}/concepts?conceptId={2}".FormatUri(CurrentMultitermUrl,response.TermbaseId,response.ConceptId);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that gets a concept
        /// </summary>
        public static Uri GetConcepts(string termbaseId )
        {
            return "{0}/termbases/{1}/concepts".FormatUri(CurrentMultitermUrl, termbaseId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets a concept
        /// </summary>
        public static Uri GetConcepts(string termbaseId, string conceptId)
        {
            return "{0}/termbases/{1}/concepts/{2}".FormatUri(CurrentMultitermUrl, termbaseId, conceptId);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that gets all language resource templates
        /// </summary>
        public static Uri LanguageResourceServiceTemplates()
        {
            return "{0}/templates".FormatUri(CurrentLanguageResourceServiceUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for specified  language resource template
        /// </summary>
        public static Uri GetLanguageResourceTemplateById(string templateId)
        {
            return "{0}/templates/{1}".FormatUri(CurrentLanguageResourceServiceUrl, templateId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gives a list of language resource for specified templateId
        /// </summary>
        public static Uri LanguageResource(string templateId)
        {
            return "{0}/templates/{1}/resources".FormatUri(CurrentLanguageResourceServiceUrl, templateId);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that gives default type for a language
        /// </summary>
        public static Uri GetDefaults(string type, string language)
        {
            return "{0}/defaults/{1}/{2}".FormatUri(CurrentLanguageResourceServiceUrl, type, language);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that language resources for specified template and resource.
        /// </summary>
        public static Uri LanguageResourcesForTemplate(string templateId, string languageResourceId)
        {
            return "{0}/templates/{1}/resources/{2}".FormatUri(CurrentLanguageResourceServiceUrl, templateId,
                languageResourceId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for the following language resource actions:reset, import or export .
        /// </summary>
        public static Uri LanguageResourceActions(string templateId, string languageResourceId, string action)
        {
            return "{0}/templates/{1}/resources/{2}/{3}".FormatUri(CurrentLanguageResourceServiceUrl, templateId,
                languageResourceId, action);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that gets a list of Fields for a specific Template ID.
        /// </summary>
        public static Uri GetFields(string templateId)
        {
            return "{0}/templates/{1}/fields".FormatUri(CurrentFieldServiceUrl, templateId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that gets a specified Field for a specific Field Template ID.
        /// </summary>
        public static Uri GetField(string fieldTemplateId, string fieldId)
        {
            return "{0}/templates/{1}/fields/{2}".FormatUri(CurrentFieldServiceUrl, fieldTemplateId, fieldId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that adds a new translation unit to the translation memory
        /// </summary>
        public static Uri TranslationUnits( string tmId,string action)
        {
            return "{0}/tms/{1}/tus/{2}".FormatUri(CurrentTranslationMemoriesUrl, tmId, action);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that retrieves the translation units from the Translation memory
        /// </summary>
        public static Uri Tus(string tmId)
        {
            return "{0}/tms/{1}/tus".FormatUri(CurrentTranslationMemoriesUrl,tmId);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that retrieves the number of translation units from the Translation memory
        /// </summary>
        public static Uri TusCount(string tmId)
        {
            return "{0}/tms/{1}/tus/count".FormatUri(CurrentTranslationMemoriesUrl, tmId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that retrieves the number of translation units from the Translation memory
        /// </summary>
        public static Uri TusByType(string tmId,string type)
        {
            return "{0}/tms/{1}/tus/{2}/count".FormatUri(CurrentTranslationMemoriesUrl, tmId,type);
        }
        /// <summary>
        /// Returns the <see cref="Uri"/> that retrieves the Duplicate Translation Units in a specific TM
        /// </summary>
        public static Uri TranslationUnitsDuplicates(string tmId, string source, string target)
        {
            return "{0}/tms/{1}/tus/duplicate/source={2}&target={3}".FormatUri(CurrentTranslationMemoriesUrl, tmId,
                source, target);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that retrieves  a list of all available database servers
        /// </summary>
        public static Uri DbServers()
        {
            return "{0}/dbservers".FormatUri(CurrentTranslationMemoriesUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that retrieves  specified database server
        /// </summary>
        public static Uri DbServers(string serverId)
        {
            return "{0}/dbservers/{1}".FormatUri(CurrentTranslationMemoriesUrl,serverId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that schedules a recompute statistics operation
        /// </summary>
        public static Uri Fuzzy(string tmId, string action)
        {
            return "{0}/tms/{1}/fuzzyindex/{2}".FormatUri(CurrentTranslationMemoriesUrl, tmId, action);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that exports TUs from a Translation Memory
        /// </summary>
        public static Uri Export(string tmId, string source, string target)
        {
            return "{0}/tms/{1}/export?source={2}&target={3}".FormatUri(CurrentTranslationMemoriesUrl, tmId,source,target);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that imports TUs into a Translation Memory
        /// </summary>
        public static Uri Import(string tmId, string source, string target)
        {
            return "{0}/tms/{1}/import?source={2}&target={3}".FormatUri(CurrentTranslationMemoriesUrl, tmId, source, target);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a list of all available containers
        /// </summary>
        public static Uri Containers()
        {
            return "{0}/containers".FormatUri(CurrentTranslationMemoriesUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that a specified container
        /// </summary>
        public static Uri Containers(string containerid)
        {
            return "{0}/containers/{1}".FormatUri(CurrentTranslationMemoriesUrl,containerid);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns the analysis report for a project
        /// </summary>
        public static Uri AnalysisReports(string projectId,string languageCode)
        {
            return "{0}/projects/{1}/analysisreports/{2}".FormatUri(CurrentProjectServerUrl, projectId, languageCode);
        }

		/// <summary>
		/// Returns the <see cref="Uri"/> that returns the progest settings for a language file
		/// </summary>
		public static Uri GetProjectSettings(string projectId, string languageFileId)
	    {
		    return "{0}/projects/{1}/files/{2}/settings".FormatUri(CurrentProjectServerUrl, projectId,languageFileId);
	    }

	    /// <summary>
	    /// Returns the <see cref="Uri"/> that checks in a file edited in the Online Editor
	    /// </summary>
	    public static Uri OnlineCheckIn(string projectId, string languageFileId)
	    {
		    return "{0}/projects/{1}/files/{2}/onlinecheckin".FormatUri(CurrentProjectServerUrl, projectId, languageFileId);
	    }

		/// <summary>
		/// Returns the <see cref="Uri"/> that Undoes an online checkout
		/// </summary>
		public static Uri UndoCheckout(string projectId, string languageFileId)
	    {
		    return "{0}/projects/{1}/files/{2}/undoCheckout".FormatUri(CurrentProjectServerUrl, projectId, languageFileId);
		}

		/// <summary>
		/// Returns the <see cref="Uri"/> for health check call used to keep the OE license seat taken
		/// </summary>
		public static Uri OnlineCheckoutHealthCheck(string editorProfileMode)
	    {
			return "{0}/onlinecheckout/{1}".FormatUri(CurrentProjectServerUrl, editorProfileMode);	
		}

		/// <summary>
		/// Returns the <see cref="Uri"/> rthat checks if the given language file is check-out to someone other than the user making this call
		/// </summary>
		public static Uri IsCheckoutToSomeoneElse(string languageFileId, string editorProfileMode)
	    {
			return "{0}/onlinecheckout/isCheckOutToSomeoneElse/{1}/{2}".FormatUri(CurrentProjectServerUrl, languageFileId, editorProfileMode);
		}

	    /// <summary>
	    /// Returns the <see cref="Uri"/> that checks out a file for editing in the Universal Editor
	    /// </summary>
		public static Uri OnlineCheckout(string projectId, string languageFileId)
	    {
		    return "{0}/projects/{1}/files/{2}/onlinecheckout".FormatUri(CurrentProjectServerUrl, projectId, languageFileId);	   
		}
		
	    /// <summary>
	    /// Returns the <see cref="Uri"/> that checks out a file for editing
	    /// </summary>
	    public static Uri ExternalCheckout(string projectId, string languageFileId)
	    {
		    return "{0}/projects/{1}/files/{2}/externalcheckout".FormatUri(CurrentProjectServerUrl, projectId, languageFileId);
	    }

	    /// <summary>
	    /// Returns the <see cref="Uri"/> that checks in a file for editing
	    /// </summary>
	    public static Uri ExternalCheckin(string projectId, string languageFileId)
	    {
		    return "{0}/projects/{1}/files/{2}/externalcheckin".FormatUri(CurrentProjectServerUrl, projectId, languageFileId);
	    }

	    /// <summary>
	    /// Returns the <see cref="Uri"/> that returns dashboard data
	    /// </summary>
	    public static Uri Dashboard()
	    {
		    return "{0}/dashboard".FormatUri(CurrentProjectServerUrl);
	    }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dashboard projects per month
        /// </summary>
        /// <returns></returns>
        public static Uri DashboardProjectsPerMonth()
        {
            return "{0}/dashboard/projectsPerMonth".FormatUri(CurrentProjectServerUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dashboard top language pairs
        /// </summary>
        /// <param name="noOfTopLanguagePairs"></param>
        /// <returns></returns>
        public static Uri DashboardTopLanguagePairs(int? noOfTopLanguagePairs = null)
        {
            return "{0}/dashboard/topLanguagePairs/{1}".FormatUri(CurrentProjectServerUrl, noOfTopLanguagePairs);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dashboard words per month
        /// </summary>
        /// <returns></returns>
        public static Uri DashboardWordsPerMonth()
        {
            return "{0}/dashboard/wordsPerMonth".FormatUri(CurrentProjectServerUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dashboard words per month
        /// </summary>
        /// <returns></returns>
        public static Uri DashboardWordsPerOrganization()
        {
            return "{0}/dashboard/wordsPerOrganization".FormatUri(CurrentProjectServerUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dashboard statistics
        /// </summary>
        /// <returns></returns>
        public static Uri DashboardStatistics()
        {
            return "{0}/dashboard/statistics".FormatUri(CurrentProjectServerUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that retries the audit trail for all the language files in the given project
        /// </summary>
        public static Uri AuditTrail(string projectId)
	    {
		    return "{0}/auditTrail/languageFiles/{1}".FormatUri(CurrentProjectServerUrl, projectId);
	    }
		/// <summary>
		/// Returns the <see cref="Uri"/> that checks if the user can open the file in universal editor 
		/// </summary>
		public static Uri IsAuthorizedToOpenInEditor(string projectId, string languageFileId)
	    {
			return "{0}/projects/{1}/files/{2}/isauthorized".FormatUri(CurrentProjectServerUrl, projectId, languageFileId);		   
		}

		/// <summary>
		/// Returns the <see cref="Uri"/> that returns the permissions in editor for the user	 
		/// </summary>
		public static Uri EditorProfile(string projectId, string languageFileId)
	    {
		    return "{0}/projects/{1}/files/{2}/editorprofile".FormatUri(CurrentProjectServerUrl, projectId, languageFileId);
	    }

		/// <summary>
		/// Returns the <see cref="Uri"/> that returns the output of a background task
		/// </summary>
		public static Uri TaskOutput(string taskId)
        {
            return "{0}/tasks/{1}/output".FormatUri(CurrentTranslationMemoriesUrl, taskId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns a background task by id
        /// </summary>
        public static Uri GetTaskById(string taskId)
        {
            return "{0}/tasks/{1}".FormatUri(CurrentTranslationMemoriesUrl, taskId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all files statistics associated with the specified project
        /// </summary>
        /// <param name="projectId">The project id</param>
        public static Uri ProjectFileStatistics(string projectId)
        {
            return "{0}/projects/{1}/files/detailed-information".FormatUri(CurrentProjectServerUrl, projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that returns all language statistics associated with the specified project
        /// </summary>
        /// <param name="projectId">The project id</param>
        public static Uri ProjectLanguageStatistics(string projectId)
        {
            return "{0}/projects/{1}/languages".FormatUri(CurrentProjectServerUrl, projectId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that performs a concordance search
        /// </summary>
        /// <param name="tmId">The project id</param>
        public static Uri ConcordanceSearch(string tmId)
        {
            return "{0}/tms/{1}/search/concordance".FormatUri(CurrentTranslationMemoriesUrl, tmId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> that initiate a new translate and analysis job
        /// </summary>
        public static Uri InitiateTranslateAndAnalysisJob()
        {
            return "{0}/job".FormatUri(TranslateAndAnalysisServiceUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> translatable document identifier
        /// </summary>
        public static Uri TranslationJob(string jobId)
        {
            return "{0}/translation/{1}".FormatUri(TranslateAndAnalysisServiceUrl, jobId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> translatable document status
        /// </summary>
        public static Uri TranslationJobStatus(string translationJob)
        {
            return "{0}/translation/status/{1}".FormatUri(TranslateAndAnalysisServiceUrl, translationJob);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> translatable document
        /// </summary>
        public static Uri DownloadTranslationDocument(string translationJob)
        {
            return "{0}/translation/download/{1}".FormatUri(TranslateAndAnalysisServiceUrl, translationJob);
        }
        
        /// <summary>
        ///  Returns the <see cref="Uri"/> analysis document identifier
        /// </summary>
        public static Uri AnalysisJob(string jobId)
        {
            return "{0}/analysis/{1}".FormatUri(TranslateAndAnalysisServiceUrl, jobId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> translatable document analysis status
        /// </summary>
        public static Uri AnalysisJobStatus(string analysisJob)
        {
            return "{0}/analysis/status/{1}".FormatUri(TranslateAndAnalysisServiceUrl, analysisJob);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> translatable document analysis statistics
        /// </summary>
        public static Uri AnalysisStatistics(string analysisJob)
        {
            return "{0}/analysis/{1}".FormatUri(TranslateAndAnalysisServiceUrl, analysisJob);
        }

        /// <summary>
        /// Deletes the job for translate and analysis
        /// </summary>
        public static Uri DeleteJob(string jobId)
        {
            return "{0}/job/{1}".FormatUri(TranslateAndAnalysisServiceUrl, jobId);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for projects report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetProjectPredefinedReportData(string options)
        {
            return "{0}/projectPredefinedReports?filter={1}".FormatUri(CurrentProjectServerUrl, options);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for tasks Report report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetTasksReportData(string options)
        {
            return "{0}/tasksReport?filter={1}".FormatUri(CurrentProjectServerUrl, options);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for TM Leverage report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetTmLeverageData(string options)
        {
            return "{0}/tmLeverageReport?filter={1}".FormatUri(CurrentProjectServerUrl, options);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for predefined projects report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetPredefinedProjectsData()
        {

            return "{0}/predefined/projects".FormatUri(ReportingServiceUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for predefined tasks report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetPredefinedTasksData()
        {
            return "{0}/predefined/tasks".FormatUri(ReportingServiceUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for predefined tmleverage report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetPredefinedTmLeverageData()
        {
            return "{0}/predefined/tmleverage".FormatUri(ReportingServiceUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dashboard Deliveries Due Soon report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetDeliveriesDueSoonData()
        {
            return "{0}/dashboard/DeliveriesDueSoon".FormatUri(ReportingServiceUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dashboard Your Tasks report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetYourTasksData()
        {
            return "{0}/dashboard/YourTasks".FormatUri(ReportingServiceUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dashboard Projects Per Month report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetProjectsPerMonthData()
        {
            return "{0}/dashboard/ProjectsPerMonth".FormatUri(ReportingServiceUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dashboard Words Per Month report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetWordsPerMonthData()
        {
            return "{0}/dashboard/WordsPerMonth".FormatUri(ReportingServiceUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dashboard Words Per Organization report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetWordsPerOrganizationData()
        {
            return "{0}/dashboard/WordsPerOrganization".FormatUri(ReportingServiceUrl);
        }

        /// <summary>
        /// Returns the <see cref="Uri"/> for dashboard Top Language Pairs report data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri GetTopLanguagePairsData()
        {
            return "{0}/dashboard/TopLanguagePairs".FormatUri(ReportingServiceUrl);
        }
    }
}
