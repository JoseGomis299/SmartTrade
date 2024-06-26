﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.Entities;

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
           if (bytes == null || bytes.Length == 0)
                return null;

            using var ms = new MemoryStream(bytes);
            return new Bitmap(ms);
        }

        public static byte[] ToByteArray(this Bitmap bitmap)
        {
            using var ms = new MemoryStream();
            bitmap.Save(ms);
            return ms.ToArray();
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

        public static string[] GetAttributes(this Category category)
        {
            return category switch
            {
                Category.Book => new[] { "Author/s", "Publisher/s", "Pages/i", "Language/s", "ISBN/i"},
                Category.Clothing => new[] { "Brand/s","Size/s", "Color/s", "Material/s" },
                Category.Nutrition => new[] { "Weight (g)/f", "Calories (Kcal)/f", "Proteins (g)/f", "Carbohydrates (g)/f", "Fats (g)/f", "Allergens/l" },
                Category.Toy => new[] {"Brand/l", "Material/l"},
            };
        }

        public static int[] GetNonRepeatableAttributes(this Category category)
        {
            return category switch
            {
                Category.Book => new[] { 0, 1, 2, 4 },
                Category.Clothing => new[] { 0, 3},
                Category.Nutrition => new[] { 5, 1, 2, 3, 4 },
                Category.Toy => new[] { 0 },
            };
        }

    }
}
