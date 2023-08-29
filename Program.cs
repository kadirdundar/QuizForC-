using System;
using System.Data;
using System.Xml.Serialization;

class Program
{
    static void Main(){
        //1
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new DataTable();
        //2 
        dataTable1.Columns.Add("value", typeof(int));
        dataTable1.Columns.Add("random", typeof(string));

        dataTable2.Columns.Add("value",typeof(int));
        dataTable2.Columns.Add("random",typeof(string));

        //3
        for(int i = 1;i < 20;i++){
            if(i%2 == 0){ 
                DataRow row = dataTable2.NewRow();
                row["value"] = i;
                row["random"] = RandomText(5);
                dataTable2.Rows.Add(row);
            }
            else{
                DataRow roww = dataTable1.NewRow();
                roww["value"] = i;
                roww["random"] = RandomText(5);
                dataTable1.Rows.Add(roww);
            }

        }

        //5
        dataTable1.Merge(dataTable2, false, MissingSchemaAction.Add);
        //6
        DataTable dataTableDeleted = deleteOperation(dataTable1); 

        createFolder();
        createFolders(dataTableDeleted);

        
            foreach (DataRow row in dataTableDeleted.Rows)
            {
                foreach (DataColumn column in dataTableDeleted.Columns)
                {
                    Console.Write(row[column] + " ");
                }
                Console.WriteLine();
            }
        }

    static string RandomText(int length){
        Random random = new Random();

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char[] randomChars = new char[length];

        for(int i=0;i<length;i++){
            int index = random.Next(chars.Length);
            randomChars[i] = chars[index];


        }
         string randomString = new string(randomChars);
        return randomString;

    }
   

    static DataTable deleteOperation (DataTable mergedDataTable){//6 için

            for (int i = mergedDataTable.Rows.Count - 1; i >= 0; i--)
            {
            int value = Convert.ToInt32(mergedDataTable.Rows[i]["value"]); 
            if (value % 5 == 0)
                 {
                mergedDataTable.Rows.RemoveAt(i);
                 }
             }
             return mergedDataTable;
        }

    //7
    static void createFolder(){
            {
        string kullaniciAdi = Environment.UserName;
        string masaustuYolu = Path.Combine("/Users", kullaniciAdi, "Desktop", "C#");

        try
        {
            Directory.CreateDirectory(masaustuYolu);

        }
        catch (Exception ex)
        {
            Console.WriteLine("Klasör oluşturma hatası: " + ex.Message);
        }
    }
    }
    //8
    static void createFolders(DataTable dataTable){
        string kullaniciAdi = Environment.UserName;
        string masaustuYolu = Path.Combine("/Users", kullaniciAdi, "Desktop", "C#");

         foreach (DataRow row in dataTable.Rows)
        
        {
            int value = (int)row["value"];
            string klasorAdi = "Klasor_" + value.ToString();

            string klasorYolu = Path.Combine(masaustuYolu, klasorAdi);

            string dosyaAdi = "notepad.txt";
            string dosyaYolu = Path.Combine(klasorYolu, dosyaAdi);

            string randomDeger = (string)row["random"];

            try
            {
                Directory.CreateDirectory(klasorYolu);
                Console.WriteLine("Klasör oluşturuldu: " + klasorYolu);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Klasör oluşturma hatası: " + ex.Message);
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(dosyaYolu))
                {
                    writer.WriteLine(randomDeger);
                    writer.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
                }

                Console.WriteLine("txt dosyası oluşturuldu: " + dosyaYolu);
            }
            catch (Exception ex)
            {
                Console.WriteLine("txt dosyası oluşturma hatası: " + ex.Message);
            }
        }
    }
}