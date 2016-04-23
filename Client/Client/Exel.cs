namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.IO; // для класса 
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using EnumDialogResult = System.Windows.Forms.DialogResult;
    using Microsoft.Office.Interop.Excel;
    using System.Runtime.InteropServices;
    using Excel = Microsoft.Office.Interop.Excel;
   public class Exel
    {
        private Excel.Application ObjExcel;
        private Excel.Workbook ObjWorkBook;
        private Excel.Worksheet ObjWorkSheet;
        public void ESave(DataGridView dataGridView1)
   {
             try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Файл Excel|*.xls"; 
                save.ShowDialog(); // Показать диалог сохранения
                string fileName = save.FileName;
                ObjExcel = new Excel.Application(); // Книга.
                ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value); // Таблица.
                ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1]; // цикл cтрока
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i]; // создаем строку
                    // цикл стоблец
                    for (int j = 0; j < row.Cells.Count; j++) 
                    {
                        ObjExcel.Cells[i + 1, j + 1] = row.Cells[j].Value;  // Поле
                    }
                }
                ObjWorkBook.SaveAs(fileName); // Заносим в файл
                dataGridView1.Rows.Clear(); // Стираем значения
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Error write"); // Ошибка
            }
            finally 
            {
                {
                    ObjWorkBook.Close(); // Закрытие приложения Excel.
                    ObjExcel.Quit(); 
                    ObjWorkBook = null;
                    ObjWorkSheet = null;
                    ObjExcel = null;
                    GC.Collect();
                }
            }
   }

        public void ELoad(DataGridView dataGridView1)
       {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файл Excel|*.XLSX;*.XLS";
            openDialog.ShowDialog();

            try
            {
                ObjExcel = new Microsoft.Office.Interop.Excel.Application(); // Книга.
                ObjWorkBook = ObjExcel.Workbooks.Open(openDialog.FileName); // Таблица.
                ObjWorkSheet = ObjExcel.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
                Microsoft.Office.Interop.Excel.Range rg = null;

                Int32 row = 1;
                dataGridView1.Rows.Clear();
                List<String> arr = new List<string>();
                while (ObjWorkSheet.get_Range("a" + row, "a" + row).Value != null)
                {
                    // Читаем данные из ячейки
                    rg = ObjWorkSheet.get_Range("a" + row, "e" + row);
                    foreach (Microsoft.Office.Interop.Excel.Range item in rg)
                    {
                        try
                        {
                            arr.Add(item.Value.ToString().Trim());
                        }
                        catch 
                        {
                            arr.Add(""); 
                        }
                    }
                    dataGridView1.Rows.Add(arr[0], arr[1], arr[2], arr[3], arr[4]);
                    arr.Clear();
                    row++;
                }

                MessageBox.Show("Файл успешно считан!", "Считывания excel файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message, "Ошибка при считывании excel файла", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                ObjWorkBook.Close(false, "", null);
                // Закрытие приложения Excel.
                ObjExcel.Quit();
                ObjWorkBook = null;
                ObjWorkSheet = null;
                ObjExcel = null;
                GC.Collect();
            }

       }

        public List<List<string>> ESaveUp(DataGridView dataGridView1)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файл Excel|*.XLSX;*.XLS";
            openDialog.ShowDialog();
            List<List<string>> Cool = new List<List<string>>();
            try
            {
                ObjExcel = new Microsoft.Office.Interop.Excel.Application(); // Книга.
                ObjWorkBook = ObjExcel.Workbooks.Open(openDialog.FileName);  // Таблица.
                ObjWorkSheet = ObjExcel.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
                Microsoft.Office.Interop.Excel.Range rg = null;

                Int32 row = 1;
                List<string> arr = new List<string>();
                while (ObjWorkSheet.get_Range("a" + row, "a" + row).Value != null)
                {
                    
                    rg = ObjWorkSheet.get_Range("a" + row, "e" + row); // Читаем данные из ячейки
                    foreach (Microsoft.Office.Interop.Excel.Range item in rg)
                    {
                        try
                        {
                            arr.Add(item.Value.ToString().Trim());
                           
                        }
                        catch
                        {
                            arr.Add("");
                        }
                    }
                    Cool.Add(arr);
                    arr.Clear();
                    row++;
                } 

                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                DataGridViewRow row1 = dataGridView1.Rows[i]; // создаем строку
                                // цикл стоблец
                                List<string> a = new List<string>();
                                for (int j = 0; j < row1.Cells.Count; j++)
                                {
                                    a.Add(row1.Cells[j].Value.ToString());
                                }
                                Cool.Add(a);
                            }
                           

            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message, "Ошибка при считывании excel файла", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                ObjWorkBook.Close(false, "", null); // Закрытие приложения Excel.
                ObjExcel.Quit();
                ObjWorkBook = null;
                ObjWorkSheet = null;
                ObjExcel = null;
                GC.Collect();
            }


            return Cool;
        }    
    }
   
}
