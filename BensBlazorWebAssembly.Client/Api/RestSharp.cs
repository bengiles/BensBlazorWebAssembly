using RestSharp;

namespace BensBlazorWebAssembly.Client.Api
{
    public interface IRestSharpHelper
    {
        Task<T> AsyncRequest<T>(Method method, string apiPathEndPoint, object body);
        Task<bool> AsyncRequest(Method method, string apiPathEndPoint, object body);

    }
    public class RestSharpHelper : IRestSharpHelper
    {
        private readonly string authToken = string.Empty;
        public RestSharpHelper()
        {

        }

        public async Task<T> AsyncRequest<T>(Method method, string apiPathEndPoint, object body)
        {
            var baseUrl = "https://localhost:7093/";
            var address = baseUrl + apiPathEndPoint;
            try
            {
                var client = new RestClient();              
                if (string.IsNullOrWhiteSpace(authToken))
                {
                    client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", authToken));
                }

                var request = new RestRequest(address, method);
                if (body != null)
                {
                    request.AddJsonBody(body);
                }

                switch (method)
                {
                    case Method.Get:
                        return await client.GetAsync<T>(request);
                    case Method.Post:
                        return await client.PostAsync<T>(request);
                    case Method.Put:
                        return await client.PutAsync<T>(request);
                    case Method.Delete:
                        return await client.DeleteAsync<T>(request);
                    default:
                        return default(T);
                }
            }
            catch (Exception ex)
            {
                // Handle exception here
                return default(T);
            }
        }

        public async Task<bool> AsyncRequest(Method method, string apiPathEndPoint, object body)
        {
            var baseUrl = "https://localhost:7093/";
            var address = baseUrl + apiPathEndPoint;
            try
            {
                var client = new RestClient();
                if (string.IsNullOrWhiteSpace(authToken))
                {
                    client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", authToken));
                }

                var request = new RestRequest(address, method);
                if (body != null)
                {
                    request.AddJsonBody(body);
                }

                switch (method)
                {
                    case Method.Get:
                        await client.GetAsync(request);
                        return true;
                    case Method.Post:
                        await client.PostAsync(request);
                        return true;
                    case Method.Put:
                        await client.PutAsync(request);
                        return true;
                    case Method.Delete:
                        await client.DeleteAsync(request);
                        return true;
                }
            }
            catch (Exception ex)
            {
                // Handle exception here
                return false;
            }
        }


    }
}
