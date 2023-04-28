using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using ClinicaModels;

namespace ProyectoClinica.Services
{
    public class APIServices
    {
        private static HttpClient _client = new();
        private static HttpClientHandler _clientHandler = new();
        private static string url = "https://localhost:7026/";

        public static string token = "";

        private static async Task<T> Get<T>(string path)
        {
            /*_clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (!token.IsNullOrEmpty())
            {
                _client.DefaultRequestHeaders.Authorization = null;
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }*/
            var response = await _client.GetAsync(path);
            int statusCode = (int)response.StatusCode;
            if (statusCode >= 200 && statusCode < 300)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        private static async Task<T> Post<T>(string path, object? data)
        {
            var json_ = JsonConvert.SerializeObject(data);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            /*_clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (!token.IsNullOrEmpty())
            {
                _client.DefaultRequestHeaders.Authorization = null;
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }*/
            var response = await _client.PostAsync(path, content);
            int statusCode = (int)response.StatusCode;
            if (statusCode >= 200 && statusCode < 300)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        private static async Task<T> Put<T>(string path, object? data)
        {
            
            var json_ = JsonConvert.SerializeObject(data);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            /*_clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (!token.IsNullOrEmpty())
            {
                _client.DefaultRequestHeaders.Authorization = null;
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }*/
            var response = await _client.PutAsync(path, content);
            int statusCode = (int)response.StatusCode;
            if (statusCode >= 200 && statusCode < 300)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        private static async Task<T> Delete<T>(string path)
        {/*
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            if (!token.IsNullOrEmpty())
            {
                _client.DefaultRequestHeaders.Authorization = null;
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }*/
            var response = await _client.DeleteAsync(path);
            int statusCode = (int)response.StatusCode;
            if (statusCode >= 200 && statusCode < 300)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<IEnumerable<Rol>> GetRoles()
        {
            return await Get<IEnumerable<Rol>>(url + "Role/GetRolesList");
        }

        public static async Task<Rol> GetRole(short? id)
        {
            return await Get<Rol>(url + "Role/GetRole/" + id);
        }

        public static async Task<GeneralResult> CreateRole(Rol rol)
        {
            return await Post<GeneralResult>(url + "Role/CreateRole", rol);
        }

        public static async Task<GeneralResult> EditRole(Rol rol)
        {
            return await Put<GeneralResult>(url + "Role/EditRole/" + rol.Id, rol);
        }

        public static async Task<GeneralResult> DeleteRole(short id)
        {
            return await Delete<GeneralResult>(url + "Role/DeleteRole/" + id);
        }

        public static async Task<IEnumerable<Usuario>> GetUsers()
        {
            return await Get<IEnumerable<Usuario>>(url + "User/GetUsersList");
        }

        public static async Task<Usuario> GetUser(int? id)
        {
            return await Get<Usuario>(url + "User/GetUser/" + id);
        }

        public static async Task<GeneralResult> CreateUser(Usuario user)
        {
            return await Post<GeneralResult>(url + "User/CreateUser", user);
        }

        public static async Task<GeneralResult> EditUser(Usuario user)
        {
            return await Put<GeneralResult>(url + "User/EditUser/" + user.Id, user);
        }

        public static async Task<GeneralResult> DeleteUser(int id)
        {
            return await Delete<GeneralResult>(url + "User/DeleteUser/" + id);
        }

        public static async Task<IEnumerable<Paciente>> GetPacients()
        {
            return await Get<IEnumerable<Paciente>>(url + "Pacient/GetPacientsList");
        }

        public static async Task<Paciente> GetPacient(int? id)
        {
            return await Get<Paciente>(url + "Pacient/GetPacient/" + id);
        }

        public static async Task<GeneralResult> CreatePacient(Paciente paciente)
        {
            return await Post<GeneralResult>(url + "Pacient/CreatePacient", paciente);
        }

        public static async Task<GeneralResult> EditPacient(Paciente pacient)
        {
            return await Put<GeneralResult>(url + "Pacient/EditPacient/" + pacient.Id, pacient);
        }

        public static async Task<GeneralResult> DeletePacient(int id)
        {
            return await Delete<GeneralResult>(url + "Pacient/DeletePacient/" + id);
        }

        public static async Task<IEnumerable<Caso>> GetCases()
        {
            return await Get<IEnumerable<Caso>>(url + "Case/GetCasesList");
        }

        public static async Task<Caso> GetCase(int? id)
        {
            return await Get<Caso>(url + "Case/GetCase/" + id);
        }

        public static async Task<GeneralResult> CreateCase(Caso caso)
        {
            return await Post<GeneralResult>(url + "Case/CreateCase", caso);
        }

        public static async Task<GeneralResult> EditCase(Caso caso)
        {
            return await Put<GeneralResult>(url + "Case/EditCase/" + caso.Id, caso);
        }

        public static async Task<GeneralResult> DeleteCase(int id)
        {
            return await Delete<GeneralResult>(url + "Case/DeleteCase/" + id);
        }

        public static async Task<IEnumerable<Consulta>> GetConsultations()
        {
            return await Get<IEnumerable<Consulta>>(url + "Consulta/GetConsultasList");
        }

        public static async Task<Consulta> GetConsultation(int? id)
        {
            return await Get<Consulta>(url + "Consulta/GetConsulta/" + id);
        }

        public static async Task<GeneralResult> CreateConsultation(Consulta consulta)
        {
            return await Post<GeneralResult>(url + "Consulta/CreateConsulta", consulta);
        }

        public static async Task<GeneralResult> EditConsultation(Consulta consulta)
        {
            return await Put<GeneralResult>(url + "Consulta/EditConsulta/" + consulta.Id, consulta);
        }

        public static async Task<GeneralResult> DeleteConsultation(int id)
        {
            return await Delete<GeneralResult>(url + "Consulta/DeleteConsulta/" + id);
        }
    }
}
