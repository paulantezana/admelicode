using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.Componentes
{
    class ExternalFiles
    {
        public static DataTable ImporExcel()
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Archivo Excel|*.xls;*.xlsx;*.xlsm;*.csv";
                open.Title = "Abrir archivo Excel";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    string ExcelFileName = open.FileName;
                    string connectionString = string.Empty;

                    if (Path.GetExtension(ExcelFileName) == ".xlsx")
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=No;IMEX=1\";", ExcelFileName);
                    }
                    else
                    {
                        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFileName + ";Extended Properties=Excel 8.0;";
                    }
                    OleDbConnection connExcel = new OleDbConnection(connectionString);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    cmdExcel.Connection = connExcel;

                    // Obtener el nombre de First Sheet
                    connExcel.Open();
                    DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                    // Leer datos de la primera hoja
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;

                    // LLenando datos de la tabla
                    DataTable dt = new DataTable();
                    oda.Fill(dt);
                    connExcel.Close();

                    // Tratamiento de la tabla
                    DataRow row = dt.Rows[0];
                    int index = 0;
                    foreach (var item in row.ItemArray)
                    {
                        dt.Columns[index].ColumnName = (item.ToString() == "") ? " " : item.ToString();
                        index++;
                    }
                    dt.Rows[0].Delete();
                    dt.AcceptChanges();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }

}
