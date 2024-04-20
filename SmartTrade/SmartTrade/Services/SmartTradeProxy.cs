using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.Services
{
    public class SmartTradeProxy
    {
        public event Action? OnPostsChanged;
        private List<PostDTO> _completePosts = new List<PostDTO>();
        private List<SimplePostDTO>? _simplePosts;
        public List<SimplePostDTO>? Posts => _simplePosts;
        public List<NotificationDTO>? Notifications { get; set; }

        private bool _connectedToApi;
        private bool _initialized;

        private SmartTradeService _service;

        public void SetPosts(List<SimplePostDTO>? posts)
        {
            _simplePosts = posts;
            OnPostsChanged?.Invoke();
        }

        private async Task InitializeProxyAsync()
        {
            ServiceFactory factory = new();
            _service = factory.GetService();
            Uri filePath = new Uri("avares://SmartTrade/Assets/SimplePostData.json");
            _initialized = true;

            try
            {
                SetPosts(await _service.GetPostsAsync());
                if(Posts == null) throw new Exception("Could not get posts from API.");

                //Save data to local file

                //string jsonPosts = JsonConvert.SerializeObject(Posts);
                //var assembly = AssetLoader.GetAssembly(new Uri("avares://SmartTrade/Assets"));
                //using Stream stream = assembly.GetManifestResourceStream("SmartTrade.Assets.SimplePostData.json");
                //using StreamWriter writer = new(filePath.LocalPath);
                //await writer.WriteAsync(jsonPosts);
                _connectedToApi = true;
            }
            catch(Exception _)
            {
                Console.WriteLine("Could not reach API. Getting local data...");

                if (AssetLoader.Exists(filePath))
                {
                    Stream stream = AssetLoader.Open(filePath);
                    StreamReader reader = new(stream);
                    string jsonPosts = await reader.ReadToEndAsync();

                    SetPosts(JsonConvert.DeserializeObject<List<SimplePostDTO>>(jsonPosts));
                    _connectedToApi = false;
                }
                else
                {
                    Console.WriteLine("Local data not found. Exiting...");
                    Environment.Exit(1);
                }
            }
        }

        public async Task<PostDTO> GetPostAsync(int postId)
        {
            if (_connectedToApi)
            {
                PostDTO? post = _completePosts.Find(x => x.Id == postId);
                if(post != null) return post;

                post = await _service.GetPostAsync(postId);
                _completePosts.Add(post);
                return post;
            }
            else
            {
                throw new Exception("Cannot get post from local data.");
            }
        }

        public async Task<List<SimplePostDTO>?> UpdatePostsAsync()
        {
            if (!_initialized)
            {
                await InitializeProxyAsync();
                return Posts;
            }

            if (_connectedToApi)
            {
                SetPosts(await _service.GetPostsAsync());
                return Posts;
            }
            
            return Posts;
        }
    }
}
