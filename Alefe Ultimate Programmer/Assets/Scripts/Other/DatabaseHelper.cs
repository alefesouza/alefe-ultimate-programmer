using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Other
{
    public class DatabaseHelper
    {
        public IDbConnection dbcon = new SqliteConnection("URI=file:" + Application.dataPath + "/AlefeUltimateProgrammer.db");

        public Character GetPlayer(int id)
        {
            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();
            dbcmd.CommandText = "SELECT * FROM characters WHERE id=" + id;
            IDataReader reader = dbcmd.ExecuteReader();
            reader.Read();

            Character player = new Character();
            player.Name = reader.GetString(1);
            player.Sprites = reader.GetString(2);
            player.Ninjutsu = reader.GetString(3);
            player.Ultimate = reader.GetString(4);

            reader.Close();
            dbcmd.Dispose();
            dbcon.Close();

            return player;
        }

        public void InsertCharacter(string Name, string Sprites, string Ninjustu, string Ultimate)
        {
            IDbCommand dbcmd = dbcon.CreateCommand();
            dbcmd.CommandText = "INSERT into characters (name, sprites, ninjutsu, ultimate) VALUES ('" + Name +"', '" + Sprites +"', '" + Ninjustu +"', '" + Ultimate +"')";
            dbcmd.ExecuteReader();
            dbcmd.Dispose();
        }

        public void DoSql(string Command)
        {
            IDbCommand dbcmd = dbcon.CreateCommand();
            dbcmd.CommandText = Command;
            dbcmd.ExecuteReader();
            dbcmd.Dispose();
        }
    }
}
