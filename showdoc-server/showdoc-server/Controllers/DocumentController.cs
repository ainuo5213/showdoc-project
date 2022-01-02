using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using showdoc_server.Attributes;
using showdoc_server.Dtos.Request.Document;
using showdoc_server.Services.Document;

namespace showdoc_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ShowdocAuthorize]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService documentService;

        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteFolderOrDocument([FromBody] DeleteFolderOrDocumentDTO entity)
        {
            int userID = this.GetUserID();
            bool data = await this.documentService.DeleteFolderOrDocument(userID, entity);
            return await this.SuccessAsync(data);
        }

        [HttpPost("content")]
        public async Task<IActionResult> GetDocumentContent([FromQuery] int documentID)
        {
            int userID = this.GetUserID();
            DocumentContentDTO data = await this.documentService.GetDocumentContent(userID, documentID);
            return await this.SuccessAsync(data);
        }
    }
}
