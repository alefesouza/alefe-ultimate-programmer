using UnityEngine;
using System.Collections;
using Assets.Scripts.Other;

public class Initializer : MonoBehaviour
{
    void Awake()
    {
        if(!PlayerPrefs.HasKey("DefaultTables")) {
            DatabaseHelper dbHelper = new DatabaseHelper();

            dbHelper.dbcon.Open();
            dbHelper.DoSql("CREATE TABLE IF NOT EXISTS characters (id integer primary key autoincrement, name varchar(255), sprites varchar(255), ninjutsu varchar(255), ultimate varchar(255))");
            dbHelper.InsertCharacter("Alefe Souza (Modo sênior dos Seis Trimestres)", "Alefe_Souza", "Moneyshuriken do Mascote Open Source", "Seis Trimestres: Mega Moneyshuriken");
            dbHelper.InsertCharacter("Alefe Souza (Modo vínculo com Firefox)", "Alefe_Souza", "Mini Open Source Dama", "Moneyshuriken Planetário Open Source");
            dbHelper.InsertCharacter("Alefe Souza (Modo sênior)", "Alefe_Souza", "Moneyrengan", "Estilo Esforço Verdadeiro: Moneyshuriken");
            dbHelper.InsertCharacter("Alefe Souza", "Alefe_Souza", "Moneygan", "Estilo Esforço: Moneyshuriken");
            dbHelper.InsertCharacter("Letícia Pacheco", "Leticia_Pacheco", "?????", "?????");
            dbHelper.InsertCharacter("Amabile Tolio", "Amabile_Tolio", "?????", "?????");
            dbHelper.InsertCharacter("?????", "?????", "Cdori", "Flecha de Turing");
            dbHelper.InsertCharacter("?????", "?????", "?????", "8 bytes 64 bits");
            dbHelper.InsertCharacter("Steve Jobs", "Steve_Jobs", "Estilo Apple: Jutsu do dragão de dinheiro", "Arte Sênior: Estilo Apple: Mil fanboys verdadeiros");
            dbHelper.InsertCharacter(";", "Ponto_Virgula", "Log Divino", "Devastação empresária");
            dbHelper.InsertCharacter("Homem Encriptado", "Homem_Encriptado", "Funeral de explosão entre o servidor e o cliente", "Invocação: Firefox");
            dbHelper.InsertCharacter("Ejo", "Ejo", "?????", "Invocação: Estátua Leigo");
            dbHelper.InsertCharacter("Joe", "Joe", "Calor do caminho 6 > 3", "Espada de NuNuGet");
            dbHelper.InsertCharacter("Bill Gates (Seis trimestres)", "Bill_Gates", "Puxão #000000", "Datacenter do horizonte");
            dbHelper.InsertCharacter("Ada Lovelace", "Ada_Lovelace", "Ataque de vácuo de 80 master", "Pixel 6 > 3 final");
            dbHelper.dbcon.Close();

            PlayerPrefs.SetString("DefaultTables", "true");
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
