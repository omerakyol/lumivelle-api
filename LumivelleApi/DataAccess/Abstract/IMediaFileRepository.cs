using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataAccess.Abstract;

public interface IMediaFileRepository
{
    Task<ObjectId> UploadAsync(byte[] bytes, string fileName, string contentType, ObjectId? ownerId = null,
        CancellationToken cancellationToken = default);

    Task<(Stream Stream, string ContentType, string FileName, long Length, ObjectId? OwnerId)>
        OpenDownloadStreamAsync(ObjectId id, CancellationToken cancellationToken = default);

    Task DeleteAsync(ObjectId id, CancellationToken cancellationToken = default);
}
