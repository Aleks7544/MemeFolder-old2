﻿namespace MemeFolder.Services.MediaFiles
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.AspNetCore.Http;

    using Data.Models;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Hosting;

    public class MediaFilesService : IMediaFilesService
    {
        private readonly MemeFolderDbContext db;
        private readonly IConfigurationProvider mapper;
        private readonly IHostingEnvironment environment;

        public MediaFilesService(IHostingEnvironment environment, MemeFolderDbContext db, IConfigurationProvider mapper)
        {
            this.environment = environment;
            this.db = db;
            this.mapper = mapper;
        }

        public MediaFile CreateMediaFile(IFormFile file, string userId)
        {
            string wwwPath = this.environment.WebRootPath;
            string contentPath = this.environment.ContentRootPath;

            string path = Path.Combine(wwwPath, "MediaFiles");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string ext = System.IO.Path.GetExtension(file.FileName);
            MediaFileType mediaFileType = MediaFileType.Image;

            if (ext == ".jpg" || ext == ".png")
            {
                mediaFileType = MediaFileType.Image;
            }
            else if (ext == ".gif")
            {
                mediaFileType = MediaFileType.Gif;
            }
            else if (ext == ".webm" || ext == ".mp4")
            {
                mediaFileType = MediaFileType.Video;
            }
            else if (ext == ".wav" || ext == ".aac" || ext == ".mp3")
            {
                mediaFileType = MediaFileType.Audio;
            }

            MediaFile mediaFile = new MediaFile
            {
                MediaFileType = mediaFileType,
                UploaderId = userId,
                FileExtension = ext,
            };

            string fileName = Path.GetFileName(mediaFile.Id);

            using FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);

            file.CopyTo(stream);

            mediaFile.FilePath = Path.Combine(path, fileName);

            return mediaFile;
        }

        public ICollection<MediaFile> ConvertToMediaFiles(IEnumerable<IFormFile> forms, string userId)
        {
            List<MediaFile> mediaFiles = new List<MediaFile>();

            foreach (var formFile in forms)
            {
                mediaFiles.Add(CreateMediaFile(formFile, userId));
            }

            return mediaFiles;
        }

        public T GetMediaFileById<T>(string mediaFileId)
            => this.db.MediaFiles
                .Where(m => m.Id == mediaFileId)
                .ProjectTo<T>(this.mapper)
                .FirstOrDefault();
    }
}
