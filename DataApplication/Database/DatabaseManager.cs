using DataApplication.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataApplication.Database
{
    public class DatabaseManager
    {

        private const string _dbName = "carservicedb";
        private const string _customerTableName = "customer";
        private const string _bookingTableName = "booking";
        private const string _carTableName = "car";
        private const string _locationTableName = "location";
        private const string _customerTypeTableName = "customertype";



        private readonly MySqlConnection _connection;
        public DatabaseManager()
        {
            _connection = new MySqlConnection("server=localhost;database=carservicedb;user=root;password=root");
        }

        public IEnumerable<Customer> GetCustomers()
        {
            IList<Customer> items = null;
            _connection.Open();
            var query = $"SELECT * FROM `{_customerTableName}`";
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var da = new MySqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    items = dt.ConvertDataTable<Customer>();
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }

            return items;
        }

        public bool AddCustomer(Customer customer)
        {
            var result = false;
            _connection.Open();
            var query = Extensions.GenerateInsertQuery(customer, _customerTableName);
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var rowsAffected = command.ExecuteNonQuery();
                    result = rowsAffected != 0; 
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public IEnumerable<CustomerType> GetCustomerTypes()
        {
            IList<CustomerType> items = null;
            _connection.Open();
            var query = $"SELECT * FROM `{_customerTypeTableName}`";
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var da = new MySqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    items = dt.ConvertDataTable<CustomerType>();
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }

            return items;
        }

        public bool AddCustomerType(CustomerType customer)
        {
            var result = false;
            _connection.Open();
            var query = Extensions.GenerateInsertQuery(customer, _customerTypeTableName);
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var rowsAffected = command.ExecuteNonQuery();
                    result = rowsAffected != 0;
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public IEnumerable<Booking> GetBookings()
        {
            IList<Booking> items = null;
            _connection.Open();
            var query = $"SELECT * FROM `{_bookingTableName}`";
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var da = new MySqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    items = dt.ConvertDataTable<Booking>();
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }

            return items;
        }

        public IEnumerable<Booking> GetCustomerBookings(int customerId)
        {
            IList<Booking> items = null;
            _connection.Open();
            var query = $"SELECT * FROM `{_bookingTableName}` where CustId = {customerId}";
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var da = new MySqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    items = dt.ConvertDataTable<Booking>();
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }

            return items;
        }

        public IEnumerable<Booking> GetCarBookings(Car car)
        {
            IList<Booking> items = null;
            _connection.Open();
            var query = $"SELECT * FROM `{_bookingTableName}` where CarId = {car.CarId}";
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var da = new MySqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    items = dt.ConvertDataTable<Booking>();
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }

            return items;
        }

        public bool AddBooking(Booking booking)
        {
            var result = false;
            _connection.Open();
            var query = Extensions.GenerateInsertQuery(booking, _bookingTableName);
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var rowsAffected = command.ExecuteNonQuery();
                    result = rowsAffected != 0;
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public IEnumerable<Car> GetCars()
        {
            IList<Car> items = null;
            _connection.Open();
            var query = $"SELECT * FROM `{_carTableName}`";
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var da = new MySqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    items = dt.ConvertDataTable<Car>();
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }

            return items;
        }

        public Car GetCar(int carId)
        {
            IList<Car> items = null;
            _connection.Open();
            var query = $"SELECT * FROM `{_carTableName}` where CarId = {carId}";
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var da = new MySqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    items = dt.ConvertDataTable<Car>();
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }

            return items.FirstOrDefault();
        }

        public bool AddCar(Car car)
        {
            var result = false;
            _connection.Open();
            var query = Extensions.GenerateInsertQuery(car, _carTableName);
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var rowsAffected = command.ExecuteNonQuery();
                    result = rowsAffected != 0;
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public IEnumerable<Location> GetLocations()
        {
            IList<Location> items = null;
            _connection.Open();
            var query = $"SELECT * FROM `{_locationTableName}`";
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var da = new MySqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    items = dt.ConvertDataTable<Location>();
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }

            return items;
        }

        public bool AddLocation(Location location)
        {
            var result = false;
            _connection.Open();
            var query = Extensions.GenerateInsertQuery(location, _locationTableName);
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var rowsAffected = command.ExecuteNonQuery();
                    result = rowsAffected != 0;
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public int GetCustomerTypeFromCustomer(int CustomerId)
        {
            IList<Customer> items = null;
            Customer customer = null;
            int customerType = -1;
            _connection.Open();
            var query = $"SELECT * FROM `{_customerTableName}` where CustomerId = {CustomerId}";
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var da = new MySqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    items = dt.ConvertDataTable<Customer>();
                    customer = items.First();
                    customerType = customer.Category;
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }

            return customerType;
        }

        public string GetCustomerTypeName(int CustomerTypeId)
        {
            IList<CustomerType> items = null;
            CustomerType customerType = null;
            string customerTypeName = string.Empty;
            _connection.Open();
            var query = $"SELECT * FROM `{_customerTypeTableName}` where CustomerTypeId = {CustomerTypeId}";
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var da = new MySqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    items = dt.ConvertDataTable<CustomerType>();
                    customerType = items.First();
                    customerTypeName = customerType.CustomerTypeName;
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }

            return customerTypeName;
        }

        public int GetCustomerBookingsCountWithinYear(int customerId)
        {
            var year = $"SELECT EXTRACT(YEAR FROM CURRENT_DATE)";
            IList<Booking> items = null;
            int count = 0;
            _connection.Open();
            var query = $"SELECT * FROM `{_bookingTableName}` where CustId = {customerId} and YEAR(Time) = {year}";
            try
            {
                using (var command = new MySqlCommand(query, _connection))
                {
                    var da = new MySqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    items = dt.ConvertDataTable<Booking>();
                    count = items.Count;
                }
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database Conenction Error!");
            }
            finally
            {
                _connection.Close();
            }

            return count;
        }
    }
}
