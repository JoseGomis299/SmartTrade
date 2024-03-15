using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Microsoft.IdentityModel.Tokens;

namespace SmartTrade    
{
    public static class Extensions
    {
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static Bitmap? ToBitmap(this Byte[] bytes)
        {
            Bitmap? image = null;

            if (bytes.Length > 0)
            {
                var ms = new MemoryStream(bytes);
                image = new Bitmap(ms);
            }

            return image;
        }

        public static async Task<List<Byte[]>> OpenFileDialogMultiple(this UserControl caller, string title, string filters = "")
        {
            List<FilePickerFileType> filePickerFileTypes = new();
            List<string> filtersList = filters.Split(" ").ToList();
            List<Byte[]> list = new();

            foreach (var filter in filtersList)
                filePickerFileTypes.Add(new FilePickerFileType(filter));

            var files = await TopLevel.GetTopLevel(caller).StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = title,
                AllowMultiple = true, 
                FileTypeFilter = filePickerFileTypes
            });

            foreach (var file in files)
            {
                if(!filters.IsNullOrEmpty() && !filtersList.Contains(file.Name.Split('.')[^1])) continue;

                await using var stream = await file.OpenReadAsync();

                var imageData = new byte[stream.Length];
                await stream.ReadAsync(imageData, 0, imageData.Length);
                list.Add(imageData);
            }

            return list;
        }

        public static async Task<Byte[]> OpenFileDialogSingle(this UserControl caller, string title, string filters = "")
        {
            List<FilePickerFileType> filePickerFileTypes = new();
            List<string> filtersList = filters.Split(" ").ToList();

            foreach (var filter in filtersList)
                filePickerFileTypes.Add(new FilePickerFileType(filter));

            var files = await TopLevel.GetTopLevel(caller).StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = title,
                AllowMultiple = false,
                FileTypeFilter = filePickerFileTypes
            });

            foreach (var file in files)
            {
                if (!filters.IsNullOrEmpty() && !filtersList.Contains(file.Name.Split('.')[^1])) return Array.Empty<byte>();

                await using var stream = await file.OpenReadAsync();

                var imageData = new byte[stream.Length];
                await stream.ReadAsync(imageData, 0, imageData.Length);
                return imageData;
            }

            return Array.Empty<byte>();
        }

    }
}
