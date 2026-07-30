using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Business.Services.AiServices;

internal static class AiImageCompressor
{
    private const int MaxDimension = 1568;
    private const int JpegQuality = 82;

    public static async Task<(byte[] Bytes, string MediaType)> CompressAsync(byte[] imageBytes,
        CancellationToken cancellationToken)
    {
        using var input = new MemoryStream(imageBytes);
        using var image = await Image.LoadAsync(input, cancellationToken);

        if (image.Width > MaxDimension || image.Height > MaxDimension)
            image.Mutate(ctx => ctx.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(MaxDimension, MaxDimension)
            }));

        using var output = new MemoryStream();
        await image.SaveAsJpegAsync(output, new JpegEncoder { Quality = JpegQuality }, cancellationToken);
        return (output.ToArray(), "image/jpeg");
    }
}
