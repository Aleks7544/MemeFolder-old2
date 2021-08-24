namespace MemeFolder.Data
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class MemeFolderDbContext : IdentityDbContext<User>
    {
        public MemeFolderDbContext(DbContextOptions<MemeFolderDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Collection> Collections { get; init; }

        public DbSet<CollectionLike> CollectionLikes { get; init; }

        public DbSet<Comment> Comments { get; init; }

        public DbSet<CommentLike> CommentLikes { get; init; }

        public DbSet<MediaFile> MediaFiles { get; init; }

        public DbSet<Post> Posts { get; init; }

        public DbSet<PostLike> PostLikes { get; init; }

        public DbSet<Relationship> Relationships { get; init; }

        public DbSet<Tag> Tags { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Collection>()
                .HasOne(c => c.Creator)
                .WithMany(c => c.Collections)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Collection>()
                .HasMany(c => c.MediaFiles)
                .WithMany(m => m.Collections)
                .UsingEntity<Dictionary<string, object>>(
                    "CollectionMediaFiles",
                    j => j
                        .HasOne<MediaFile>()
                        .WithMany()
                        .HasForeignKey("MediaFileId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Collection>()
                        .WithMany()
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Restrict));

            builder
                .Entity<Collection>()
                .HasMany(c => c.Posts)
                .WithMany(p => p.Collections)
                .UsingEntity<Dictionary<string, object>>(
                    "CollectionPosts",
                    j => j
                        .HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Collection>()
                        .WithMany()
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Restrict));

            builder
                .Entity<Collection>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Collections)
                .UsingEntity<Dictionary<string, object>>(
                    "CollectionTags",
                    j => j
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Collection>()
                        .WithMany()
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Restrict));

            builder
                .Entity<Collection>()
                .HasMany(c => c.CollectionLikes)
                .WithOne();

            builder
                .Entity<CollectionLike>()
                .HasOne(cl => cl.Collection)
                .WithMany(c => c.CollectionLikes)
                .HasForeignKey(cl => cl.CollectionId);

            builder
                .Entity<CollectionLike>()
                .HasOne(cl => cl.User)
                .WithMany(u => u.CollectionLikes)
                .HasForeignKey(cl => cl.UserId);

            builder
                .Entity<CollectionLike>()
                .HasKey(cl => new { cl.CollectionId, cl.UserId });

            builder
                .Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Comment>()
                .HasOne(c => c.Commentator)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.CommentatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Comment>()
                .HasMany(c => c.MediaFiles)
                .WithMany(m => m.Comments)
                .UsingEntity<Dictionary<string, object>>(
                    "CommentMediaFiles",
                    j => j
                        .HasOne<MediaFile>()
                        .WithMany()
                        .HasForeignKey("MediaFileId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Comment>()
                        .WithMany()
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Restrict));

            builder
                .Entity<Comment>()
                .HasMany(c => c.CommentLikes)
                .WithOne(cl => cl.Comment);

            builder
                .Entity<CommentLike>()
                .HasOne(cl => cl.Comment)
                .WithMany(c => c.CommentLikes)
                .HasForeignKey(cl => cl.CommentId);

            builder
                .Entity<CommentLike>()
                .HasOne(cl => cl.User)
                .WithMany(u => u.CommentLikes)
                .HasForeignKey(cl => cl.UserId);

            builder
                .Entity<CommentLike>()
                .HasKey(cl => new { cl.CommentId, cl.UserId });

            builder
                .Entity<MediaFile>()
                .HasOne(m => m.Uploader)
                .WithMany(u => u.MediaFiles)
                .HasForeignKey(m => m.UploaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<MediaFile>()
                .HasMany(m => m.Posts)
                .WithMany(p => p.MediaFiles)
                .UsingEntity<Dictionary<string, object>>(
                    "MediaFilePosts",
                    j => j
                        .HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<MediaFile>()
                        .WithMany()
                        .HasForeignKey("MediaFileId")
                        .OnDelete(DeleteBehavior.Restrict));

            builder
                .Entity<MediaFile>()
                .HasMany(m => m.Comments)
                .WithMany(c => c.MediaFiles);

            builder
                .Entity<MediaFile>()
                .HasMany(m => m.Collections)
                .WithMany(c => c.MediaFiles);

            builder
                .Entity<MediaFile>()
                .HasMany(m => m.Tags)
                .WithMany(t => t.MediaFiles)
                .UsingEntity<Dictionary<string, object>>(
                    "MediaFileTags",
                    j => j
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<MediaFile>()
                        .WithMany()
                        .HasForeignKey("MediaFileId")
                        .OnDelete(DeleteBehavior.Restrict));

            builder
                .Entity<Post>()
                .HasOne(p => p.Poster)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.PosterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Post>()
                .HasMany(p => p.MediaFiles)
                .WithMany(m => m.Posts);

            builder
                .Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTags",
                    j => j
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict));

            builder
                .Entity<Post>()
                .HasMany(p => p.Collections)
                .WithMany(c => c.Posts);

            builder
                .Entity<Post>()
                .HasMany(p => p.PostLikes)
                .WithOne(pl => pl.Post);

            builder
                .Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post);

            builder
                .Entity<PostLike>()
                .HasOne(pl => pl.Post)
                .WithMany(p => p.PostLikes)
                .HasForeignKey(pl => pl.PostId);

            builder
                .Entity<PostLike>()
                .HasOne(pl => pl.User)
                .WithMany(u => u.PostLikes)
                .HasForeignKey(pl => pl.UserId);

            builder
                .Entity<PostLike>()
                .HasKey(pl => new { pl.PostId, pl.UserId });

            builder
                .Entity<Relationship>()
                .HasOne(r => r.FirstUser)
                .WithMany(u => u.Relationships)
                .HasForeignKey(r => r.FirstUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Relationship>()
                .HasOne(r => r.SecondUser)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Relationship>()
                .HasKey(r => new { r.FirstUserId, r.SecondUserId });

            builder
                .Entity<Tag>()
                .HasMany(t => t.MediaFiles)
                .WithMany(m => m.Tags);

            builder
                .Entity<Tag>()
                .HasMany(t => t.Collections)
                .WithMany(c => c.Tags);

            builder
                .Entity<Tag>()
                .HasMany(t => t.Posts)
                .WithMany(p => p.Tags);

            builder
                .Entity<User>()
                .HasMany(u => u.Collections)
                .WithOne(c => c.Creator);

            builder
                .Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.Commentator);

            builder
                .Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Poster);

            builder
                .Entity<User>()
                .HasMany(u => u.CollectionLikes)
                .WithOne(cl => cl.User);

            builder
                .Entity<User>()
                .HasMany(u => u.CommentLikes)
                .WithOne(cl => cl.User);

            builder
                .Entity<User>()
                .HasMany(u => u.PostLikes)
                .WithOne(pl => pl.User);

            builder
                .Entity<User>()
                .HasMany(u => u.MediaFiles)
                .WithOne(m => m.Uploader);

            builder
                .Entity<User>()
                .Property(u => u.UserName)
                .IsRequired();

            base.OnModelCreating(builder);
        }
    }
}
