using Microsoft.AspNetCore.Http;

namespace UserMgtService.Application.DTOs
{
    public class GenerateReportResponse
    {
        public byte[] File { get; set; }
    }
}
