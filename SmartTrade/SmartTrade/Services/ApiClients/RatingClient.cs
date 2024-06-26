﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.Services.ApiClients;

public class RatingClient : ApiClient
{
    public RatingClient(SmartTradeBroker broker) : base(broker, "Rating") { }

    public async Task<int> CreateRatingAsync(RatingDTO ratingDTO)
    {
        return int.Parse(await PerformApiInstructionAsync($"CreateRating", ApiInstruction.Post, ratingDTO));
    }

    public async Task DeleteRatingAsync(int ratingId)
    {
        await PerformApiInstructionAsync($"DeleteRating?id={ratingId}", ApiInstruction.Delete);
    }

    public async Task<List<RatingDTO>?> GetRatingListAsync(int postId)
    {
        return JsonConvert.DeserializeObject<List<RatingDTO>>(await PerformApiInstructionAsync($"GetRatingList?id={postId}", ApiInstruction.Get));
    }
}