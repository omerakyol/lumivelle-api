using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace DataAccess.Concrete.MongoDb;

public class MediaFileRepository(MongoDbContext context) : IMediaFileRepository
{
    public async Task<ObjectId> UploadAsync(byte[] bytes, string fileName, string contentType,
        ObjectId? ownerId = null, CancellationToken cancellationToken = default)
    {
        var metadata = new BsonDocument { { "contentType", contentType } };
        if (ownerId.HasValue)
            metadata.Add("ownerId", ownerId.Value);

        var options = new GridFSUploadOptions { Metadata = metadata };

        return await context.Media.UploadFromBytesAsync(fileName, bytes, options, cancellationToken);
    }

    public async Task<(Stream Stream, string ContentType, string FileName, long Length, ObjectId? OwnerId)>
        OpenDownloadStreamAsync(ObjectId id, CancellationToken cancellationToken = default)
    {
        var stream = await context.Media.OpenDownloadStreamAsync(id, cancellationToken: cancellationToken);
        var contentType = stream.FileInfo.Metadata?.GetValue("contentType", "application/octet-stream").AsString
                           ?? "application/octet-stream";
        var ownerId = stream.FileInfo.Metadata != null && stream.FileInfo.Metadata.TryGetValue("ownerId", out var ownerValue)
            ? ownerValue.AsObjectId
            : (ObjectId?)null;

        return (stream, contentType, stream.FileInfo.Filename, stream.FileInfo.Length, ownerId);
    }

    public async Task DeleteAsync(ObjectId id, CancellationToken cancellationToken = default)
    {
        await context.Media.DeleteAsync(id, cancellationToken);
    }
}
