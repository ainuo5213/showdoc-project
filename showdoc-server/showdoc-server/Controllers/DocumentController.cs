using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using showdoc_server.Attributes;
using showdoc_server.Dtos.Json;
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

        [HttpGet("content")]
        public async Task<IActionResult> GetDocumentContent([FromQuery] int documentID)
        {
            int userID = this.GetUserID();
            DocumentContentDTO data = await this.documentService.GetDocumentContent(userID, documentID);
            return await this.SuccessAsync(data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDocumentOrFolder([FromBody] CreateDocumentOrFolderDTO entity)
        {
            int userID = this.GetUserID();
            DocumentContentDTO data = await this.documentService.CreateDocumentOrFolder(userID, entity);
            return await this.SuccessAsync(data);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetDocumentHistory([FromQuery] int documentID, [FromQuery] int page = 1)
        {
            int userID = this.GetUserID();
            ListItemDTO<DocumentHistoryDTO> data = await this.documentService.GetDocumentHistory(userID, documentID, page);
            return await this.SuccessAsync(data);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateDocument([FromBody] DocumentUpdateDTO entity)
        {
            int userID = this.GetUserID();
            bool data = await this.documentService.UpdateDocument(userID, entity);
            return await this.SuccessAsync(data);
        }

        [HttpGet("historyComparison")]
        public async Task<IActionResult> HistoryDocumentComparison([FromQuery] int historyID)
        {
            int userID = this.GetUserID();
            HistoryComparisonDTO data = await this.documentService.HistoryDocumentComparison(userID, historyID);
            return await this.SuccessAsync(data);
        }
    }
}
