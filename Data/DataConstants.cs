﻿namespace MemeFolder.Data
{
    public class DataConstants
    {
        public class Collection
        {
            public const int MinNameLength = 1;
            public const int MaxNameLength = 50;

            public const int MinDescriptionLength = 1;
            public const int MaxDescriptionLength = 1000;
        }

        public class Comment
        {
            public const int MaxTextLength = 1000;
            public const int MaxMediaFiles = 4;
        }

        public class Post
        {
            public const int MaxTextLength = 1000;
            public const int MaxMediaFiles = 4;
        }

        public class Tag
        {
            public const int MaxNameLength = 100;
        }

        public class User
        {
            public const int MinDisplayNameLength = 3;
            public const int MaxDisplayNameLength = 100;

            public const int MinUsernameLength = 3;
            public const int MaxUsernameLength = 50;

            public const int MaxBioLength = 1000;
        }

        public class HomeController
        {
            public const int PageSize = 20;
            public const int MaxAgeForHotPostsInDays = 5;

            public const string PostsFeedSectionName = "hot";
        }

        public class CommentsController
        {
            public const int PageSize = 50;
        }
    }
}
