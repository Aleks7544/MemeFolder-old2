namespace MemeFolder.Services.MediaFiles
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    using Data.Models;

    public interface IMediaFilesService
    {
        MediaFile CreateMediaFile(IFormFile file, string userId);

        ICollection<MediaFile> ConvertToMediaFiles(IEnumerable<IFormFile> forms, string userId);

        T GetMediaFileById<T>(string mediaFileId);
    }
}
