using baithiTuananh.Entities;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace baithiTuananh.Model
{
    class ContactModel
    {
        private static string DatabaseName = "uwp_exam.db";

    

        public bool Save(ContactEntities Contactmodel)
        {
            try
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DatabaseName);
                using (SqliteConnection db =
                  new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "INSERT INTO contacts VALUES (@Name, @Phone);";
                    insertCommand.Parameters.AddWithValue("@Name", Contactmodel.FullName);
                    insertCommand.Parameters.AddWithValue("@Phone", Contactmodel.PhoneNumber);
                    insertCommand.ExecuteReader();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public ContactEntities FindByName(string name)
        {
            List<ContactEntities> contactList = new List<ContactEntities>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DatabaseName);
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ($"SELECT * from contacts where name like %{name}% ", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    var contact = new ContactEntities()
                    {
                        FullName = query.GetString("name"),
                        PhoneNumber = query.GetString("phone"),
                    };
                    contactList.Add(contact);
                }
                db.Close();
            }
            return contactList[0];
        }

        public List<ContactEntities> FindAll()
        {
            List<ContactEntities> contactList = new List<ContactEntities>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DatabaseName);
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ($"SELECT * from contacts", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    var contact = new ContactEntities()
                    {
                        FullName = query.GetString("name"),
                        PhoneNumber = query.GetString("phone"),
                    };
                    contactList.Add(contact);
                }
                db.Close();
            }
            return contactList;
        }
    }
}
