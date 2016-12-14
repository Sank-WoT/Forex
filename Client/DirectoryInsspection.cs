namespace Client
{
    using System;
    using System.Drawing;
    using System.IO; // для класса 
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    class DirectoryInsspection
    {
        /// <summary>
        /// Проверка существования директории с уведомлением
        /// </summary>
        /// <param name="pathDirectory">путь к директории</param>
        static public void  Set(string pathDirectory)
        {
            #region Проверка существования директрории по pathDirectory
            //// Проверка  на существование директории
            if (!Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory); // создание директории 
                MessageBox.Show("Директория создана путь : " + pathDirectory); // сообщение о создании директории
            }
            #endregion 
        }
    }
}
