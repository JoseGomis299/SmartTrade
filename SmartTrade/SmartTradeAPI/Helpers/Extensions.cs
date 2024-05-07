using SmartTrade.Entities;
using SmartTradeDTOs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace SmartTradeAPI.Helpers
{
    public static class Extensions
    {
        public static IQueryable<SimplePostDTO> SelectSimplePost(this IQueryable<Post> p)
        {
            return p.Select(p => new SimplePostDTO
            {
                Id = p.Id,
                Title = p.Title,
                Category = p.Offers.Select(o => o.Product.GetCategories().First()).FirstOrDefault(),
                MinimumAge = p.Offers.Select(o => o.Product.MinimumAge).FirstOrDefault(),
                EcologicPrint = p.Offers.Select(o => o.Product.EcologicPrint).FirstOrDefault(),
                Validated = p.Validated,
                SellerID = p.Seller.Email,
                Price = p.Offers.Select(o => o.Price).FirstOrDefault(),
                Image = p.Offers.Select(o => o.Product.Images.Select(i => i.Thumbnail).FirstOrDefault()).FirstOrDefault(),
                ProductName = p.Offers.Select(o => o.Product.Name).FirstOrDefault(),
                ShippingCost = p.Offers.Select(o => o.ShippingCost).First()

            });
        }

        public static byte[] ResizeImage(this byte[] imageData, int maxWidth, int maxHeight)
        {
            using (var image = Image.Load(imageData))
            {
                // Calculate aspect ratio
                double aspectRatio = (double)image.Width / image.Height;

                // Calculate new width and height based on aspect ratio
                int newWidth = maxWidth;
                int newHeight = (int)(newWidth / aspectRatio);

                // If calculated height exceeds maxHeight, recalculate based on height
                if (newHeight > maxHeight)
                {
                    newHeight = maxHeight;
                    newWidth = (int)(newHeight * aspectRatio);
                }

                // Resize image
                image.Mutate(x => x.Resize(newWidth, newHeight));

                // Save resized image to memory stream
                using (var outputStream = new MemoryStream())
                {
                    image.Save(outputStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                    return outputStream.ToArray();
                }
            }
        }

        public static byte[] EncodeAsPng(this byte[] imageData)
        {
            using (var image = Image.Load(imageData))
            {
                using (var outputStream = new MemoryStream())
                {
                    image.Save(outputStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                    return outputStream.ToArray();
                }
            }
        }
    }
}
