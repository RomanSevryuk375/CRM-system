using CRMSystemMobile.Extensions;
using Shared.Contracts.AcceptanceImg;
using Shared.Filters;
using System.Net.Http.Headers;

namespace CRMSystemMobile.Services;

public class AcceptanceImgService(HttpClient httpClient)
{
    public async Task<List<AcceptanceImgResponse>?> GetPhotos(long acceptanceId)
    {
        var filter = new AcceptanceImgFilter([acceptanceId], 1, 100);
        var (items, _) = await httpClient.GetPagedAsync<AcceptanceImgResponse>("api/v1/acceptance-images", filter);
        return items;
    }

    public async Task<byte[]?> DownloadPhotoBytes(long imgId)
    {
        try
        {
            return await httpClient.GetByteArrayAsync($"api/v1/acceptance-images/{imgId}/img");
        }
        catch { return null; }
    }

    public async Task<string?> UploadPhoto(long acceptanceId, FileResult fileResult, string description)
    {
        try
        {
            using var content = new MultipartFormDataContent();
            
            content.Add(new StringContent(acceptanceId.ToString()), "AcceptanceId");
            
            var stream = await fileResult.OpenReadAsync();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(fileResult.ContentType);
            content.Add(fileContent, "File", fileResult.FileName);

            content.Add(new StringContent(description ?? ""), "Description");

            var response = await httpClient.PostAsync("api/v1/acceptance-images", content);
            
            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex) { return ex.Message; }
    }
}