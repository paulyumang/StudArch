using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using StudentRecordPDC50.Model;
using static StudentRecordPDC50.Model.User;

namespace StudentRecordPDC50.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost/studentrecordfinalproj/";
        public UserService()
        {
            _httpClient = new HttpClient();
        }

        //GetFromJsonAsync - method call HTTP GET
        //PostAsJsonAsync - method to call HTTP POST
        //ReadAsStringAsync - method to read the current of HTTPContent

        //Get user
        public async Task<List<User>> GetUsersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<User>>($"{BaseUrl}get_user.php");
            return response ?? new List<User>();
        }

        public async Task<List<AcademicHistory>> GetAcademicHistoryAsync(string studentID)
        {
            var response = await _httpClient.GetFromJsonAsync<List<AcademicHistory>>($"{BaseUrl}get_academic_history.php?studentID={studentID}");
            return response ?? new List<AcademicHistory>();
        }


        //Add User
        public async Task<string> AddUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}add_user.php", user);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        public async Task<string> AddAcademicHistoryAsync(AcademicHistory history)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}add_academic_history.php", history);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        //Update User
        public async Task<string> UpdateUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}update_user.php", user);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        //Delete User
        public async Task<string> DeleteUserAsync(int userId)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}delete_user.php", new { id = userId });
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }


        //Log in Function
        public async Task<LoginResponse> LoginAsync(string idno, string password)
        {
            var loginData = new
            {
                Idno = idno,        // Send 'Name' instead of 'Idno' or 'username'
                Password = password
            };

            try
            {
                // Send the POST request with login data
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}login.php", loginData);

                // Read the raw response content as a string (before deserialization)
                string responseContent = await response.Content.ReadAsStringAsync();

                // Log the raw response for debugging
                System.Diagnostics.Debug.WriteLine($"API Response: {responseContent}");

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // If the response is valid JSON, attempt to deserialize it
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    return loginResponse;
                }
                else
                {
                    // If not successful, return a generic error message
                    return new LoginResponse
                    {
                        Message = "Login failed. Please try again."
                    };
                }
            }
            catch (Exception ex)
            {
                // Handle the error (e.g., network issues)
                return new LoginResponse
                {
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<AttendanceResponse> RecordEntryAsync(string studentidno)
        {
            var attendanceData = new
            {
                Studentidno = studentidno // Correct case
            };

            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(attendanceData),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync($"{BaseUrl}insert_entry.php", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<AttendanceResponse>(result);
                }
                else
                {
                    return new AttendanceResponse { Message = "Failed to record entry." };
                }
            }
            catch (Exception ex)
            {
                return new AttendanceResponse { Message = $"Error: {ex.Message}" };
            }
        }

        // Record exit
        public async Task<AttendanceResponse> RecordExitAsync(string studentidno)
        {
            var attendanceData = new
            {
                Studentidno = studentidno // Correct case
            };

            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(attendanceData),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync($"{BaseUrl}insert_exit.php", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<AttendanceResponse>(result);
                }
                else
                {
                    return new AttendanceResponse { Message = "Failed to record exit." };
                }
            }
            catch (Exception ex)
            {
                return new AttendanceResponse { Message = $"Error: {ex.Message}" };
            }
        }

        // Get attendance
        public async Task<List<AttendanceRecord>> GetAttendanceAsync(string studentidno)
        {
            var response = await _httpClient.GetFromJsonAsync<List<AttendanceRecord>>($"{BaseUrl}get_attendance.php?Studentidno={studentidno}");
            return response ?? new List<AttendanceRecord>();
        }

        public async Task<List<StudentDetails>> GetStudentDetailsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<StudentDetails>>($"{BaseUrl}get_student_details.php");
            return response ?? new List<StudentDetails>();
        }


    }
}