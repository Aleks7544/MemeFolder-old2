namespace MemeFolder.Services.Tags
{
    using System.Collections.Generic;
    using Data.Models;

    public interface ITagsService
    {
        Tag CreateTag(string name);

        T GetTagByName<T>(string name);

        T GetTagById<T>(string id);
    }
}
