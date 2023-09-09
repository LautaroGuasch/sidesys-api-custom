using STE.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Configuration;
using System.Linq;
using RestSharp;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace STE.Service
{
    public class UserService : IUserService
    {
        public IConfiguration _configuration;
        public string _clientUrlBase;
        public RestClient _firebase;
        public UserService(IConfiguration configuration) 
        {
            _configuration = configuration;
            _clientUrlBase = _configuration.GetConnectionString("DefaultConnection");
            _firebase = new RestClient(_clientUrlBase);
        }

        public async Task<UserModel> GetUserById(string userId) 
        {

            try
            {

                var request = new RestRequest($"Users/{userId}.json", Method.Get);

                var response = await _firebase.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<UserModel>(response.Content);

                }
                else
                {
                    Console.WriteLine("Error al recuperar usuario: " + response.ErrorMessage);
                    throw new Exception(_clientUrlBase + response.ErrorMessage);
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar usuario: " + ex.Message);
                throw new Exception(_clientUrlBase + ex.Message);
            }


        }

        public async Task<object> CreateUser(UserModel model)
        {

            try
            {
                
                var data = JsonConvert.SerializeObject(model);

                var request = new RestRequest($"Users.json", Method.Post);

                request.AddStringBody(data, DataFormat.Json);

                var response = await _firebase.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<object>(response.Content);

                }
                else
                {
                    Console.WriteLine("Error al crear usuario: " + response.ErrorMessage);
                    throw new Exception(_clientUrlBase + response.ErrorMessage);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear usuario: " + ex.Message);
                throw new Exception(_clientUrlBase + ex.Message);
            }


        }

        public async Task<string> DeleteUser(string userId)
        {

            try
            {

                var request = new RestRequest($"Users/{userId}.json", Method.Delete);

                var response = await _firebase.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return $"El elemento con id:{userId} se ha eliminado correctamente.";
                }
                else
                {
                    Console.WriteLine("Error al eliminar usuario: " + response.ErrorMessage);
                    throw new Exception(_clientUrlBase + response.ErrorMessage);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar usuarioa: " + ex.Message);
                throw new Exception(_clientUrlBase + ex.Message);
            }


        }

        public async Task<UserModel> UpdateUser(UserModel model, string userId)
        {

            try
            {
                var data = JsonConvert.SerializeObject(model);

                var request = new RestRequest($"Users/{userId}.json", Method.Put);

                request.AddStringBody(data, DataFormat.Json);

                var response = await _firebase.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return model;
                }
                else
                {
                    Console.WriteLine("Error al actualizar usuario: " + response.ErrorMessage);
                    throw new Exception(_clientUrlBase + response.ErrorMessage);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar usuario: " + ex.Message);
                throw new Exception(_clientUrlBase + ex.Message);
            }


        }



    }
}
