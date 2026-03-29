using FlightSearchEngine.Models.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using FlightSearchEngine.Models.ViewModels;

namespace FlightSearchEngine.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        // ==============================
        // Constructor
        // ==============================
        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection");
        }

        // ==============================
        // 1. Get Sources
        // ==============================
        public async Task<List<string>> GetSourcesAsync()
        {
            List<string> sources = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetSources", con);

            cmd.CommandType = CommandType.StoredProcedure;

            await con.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                sources.Add(reader.GetString(0));
            }

            return sources;
        }

        // ==============================
        // 2. Get Destinations
        // ==============================
        public async Task<List<string>> GetDestinationsAsync()
        {
            List<string> destinations = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd =
                new SqlCommand("sp_GetDestinations", con);

            cmd.CommandType = CommandType.StoredProcedure;

            await con.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                destinations.Add(reader.GetString(0));
            }

            return destinations;
        }

        // ==============================
        // 3. Search Flights Only
        // ==============================
        public async Task<List<FlightResult>> SearchFlightsAsync(
            string source,
            string destination,
            int persons)
        {
            List<FlightResult> flights = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd =
                new SqlCommand("sp_SearchFlights", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Source", source);
            cmd.Parameters.AddWithValue("@Destination", destination);
            cmd.Parameters.AddWithValue("@Persons", persons);

            await con.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                flights.Add(new FlightResult
                {
                    FlightId = Convert.ToInt32(reader["FlightId"]),
                    FlightName = reader["FlightName"].ToString(),
                    FlightType = reader["FlightType"].ToString(),
                    Source = reader["Source"].ToString(),
                    Destination = reader["Destination"].ToString(),
                    TotalCost = Convert.ToDecimal(reader["TotalCost"])
                });
            }

            return flights;
        }

        // ==============================
        // 4. Search Flights + Hotels
        // ==============================
        public async Task<List<FlightHotelResult>>
            SearchFlightsWithHotelsAsync(
            string source,
            string destination,
            int persons)
        {
            List<FlightHotelResult> packages = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd =
                new SqlCommand("sp_SearchFlightsWithHotels", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Source", source);
            cmd.Parameters.AddWithValue("@Destination", destination);
            cmd.Parameters.AddWithValue("@Persons", persons);

            await con.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                packages.Add(new FlightHotelResult
                {
                    FlightId = Convert.ToInt32(reader["FlightId"]),
                    FlightName = reader["FlightName"].ToString(),
                    Source = reader["Source"].ToString(),
                    Destination = reader["Destination"].ToString(),
                    HotelName = reader["HotelName"].ToString(),
                    TotalCost = Convert.ToDecimal(reader["TotalCost"])
                });
            }

            return packages;
        }
    }
}