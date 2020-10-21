using System.Collections.Generic;

namespace Sdl.Community.GroupShareKit.Models.Response
{
    public class AuditTrailDetails
    {
	    public LanguageFileDetails LanguageFile { get; set; }
	    public List<Trail> Trails { get; set; }
    }
}
