using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using TakeNotesClient.Models;

namespace TakeNotesClient.Requesters
{
    public class LoginRequester
    {
        public static RestClient client = null;
        private static RegisteredUser user = new RegisteredUser();

        public LoginRequester(string apiUrl)
        {
            if (client == null)
            {
                client = new RestClient(apiUrl);
            }
        }

        public LoginRequester(RestClient restClient)
        {
            client = restClient;
        }

        public int UserId
        {
            get
            {
                return user == null ? 0 : user.UserId;
            }
        }

        public string UserName
        {
            get
            {
                return user == null ? "anonymous" : user.Username;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrWhiteSpace(user.Token);
            }
        }

        public bool Register(NewUser registerUser)
        {
            var request = new RestRequest("login/register").AddJsonBody(registerUser);
            var response = client.Post<RegisteredUser>(request);

            return true;
        }

        public RegisteredUser Login(NewUser loginUser)
        {
            var request = new RestRequest("login").AddJsonBody(loginUser);
            var response = client.Post<RegisteredUser>(request);
            user.UserId = response.UserId;
            user.Username = response.Username;
            user.Token = response.Token;
            client.Authenticator = new JwtAuthenticator(user.Token);

            return response;
        }

        public void Logout()
        {
            user = new RegisteredUser();
            client.Authenticator = null;
        }

        //protected void CheckForError(RestResponse response)
        //{
        //    string message;
        //    if (response.ResponseStatus != ResponseStatus.Completed)
        //    {
        //        message = $"Error occurred - unable to reach server. Response status was '{response.ResponseStatus}'.";
        //        throw new HttpRequestException(message, response.ErrorException);
        //    }
        //    else if (!response.IsSuccessful)
        //    {
        //        if (response.StatusCode == HttpStatusCode.Unauthorized)
        //        {
        //            message = $"Authorization is required and the user has not logged in.";
        //        }
        //        else if (response.StatusCode == HttpStatusCode.Forbidden)
        //        {
        //            message = $"The user does not have permission.";
        //        }
        //        else
        //        {
        //            message = $"An http error occurred. Status code {(int)response.StatusCode} {response.StatusDescription}";
        //        }
        //        throw new HttpRequestException(message, response.ErrorException);
        //    }
        //}
    }
}
