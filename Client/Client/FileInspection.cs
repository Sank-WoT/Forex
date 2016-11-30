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
    class FileInspection
    {
        /// <summary>
        /// Приветствие нового пользователя
        /// </summary>
        /// <param name="pathFile">Путь к файлу </param>
        static public void Set(string pathFile)
        {
            #region Проверка существования файла по pathDirectory
            //// Проверка на существование файла
            if (!File.Exists(pathFile) && WString.RUS == true)
            {
                MessageBox.Show("Вас приветствует программа Project Mordor, спасибо за то что вы с нами, желаем успешных торгов и хорошей прибыли"); // сообщение о создании файла
                FileInfo writel = new FileInfo(pathFile); // получаем путь 
                StreamWriter l = writel.CreateText(); // создаем текст
                l.Close(); // закрыть запись
            } // Развертывание сервера в заранее известном каталоге 
            #endregion
        }
    }
}
