using HR.Infraestructure.DBContexto.Conversiones;
using KDP_EC.Core.Interfaces;
using KDP_EC.Core.Models;
using KDP_EC.Infraestructure.DBContext.SQLDBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Infraestructure.Implementations.EC_KDP
{
    public class PersonRepository: IPerson
    {
        private readonly SqlDbManager _db;

        public PersonRepository(SqlDbManager db)
        {
            _db = db;
        }

        public bool CreatePerson(Person person)
        {
            var sql = "EXEC [dbo].[spCreatePerson] @Identification, @Name, @LastName, @Phone, @Email, @CountryId, " +
                      "@CreatedAt, @DeletedAt, @UpdatedAt,@IdentificationType,@Gender";

            var parameters = new Dictionary<string, object>
            {
                ["@Identification"] = person.Identification,
                ["@Name"] = person.Name,
                ["@LastName"] = person.LastName,
                ["@Phone"] = person.Phone,
                ["@Email"] = person.Email,
                ["@CountryId"] = person.CountryId,
                ["@CreatedAt"] = DBNull.Value,
                ["@DeletedAt"] = DBNull.Value,
                ["@UpdatedAt"] = DBNull.Value,
                ["@IdentificationType"]= person.IdentificationType,
                ["@Gender"]= person.Gender,

            };

            var result = _db.ExecuteQuery(sql, parameters);

            return true;
        }

        public List<Person> GetPersons()
        {
            var sql = "EXEC [dbo].[spGetPerson]";
            var table = _db.ExecuteQuery(sql, null);
            var persons = new List<Person>();

            foreach (DataRow row in table.Rows)
            {
                var person = new Person
                {
                    Identification = row["Identification"]?.ToString(),
                    Name = row["Name"]?.ToString(),
                    LastName = row["LastName"]?.ToString(),
                    Phone = row["Phone"]?.ToString(),
                    Email = row["Email"]?.ToString(),
                    CountryId = Guid.TryParse(row["CountryId"]?.ToString(), out var countryId) ? countryId : Guid.Empty,
                    CreatedAt = Conv.AFecha(row["CreatedAt"]),
                    UpdatedAt = Conv.AFecha(row["UpdatedAt"]),
                    DeletedAt = Conv.AFecha(row["DeletedAt"]),
                    IdentificationType = row["IdentificationType"]?.ToString(),
                    Gender = row["Gender"]?.ToString()
                };

                persons.Add(person);
            }

            return persons;
        }
    }
}
