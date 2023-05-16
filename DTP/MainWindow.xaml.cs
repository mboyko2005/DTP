using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace DTPRegistrationApp
{
    public partial class MainWindow : Window
    {
        private List<DTPOccurrence> _dtpOccurrences;

        public MainWindow()
        {
            InitializeComponent();

            // Инициализируем список случаев ДТП из файла
            LoadDTPOccurrences();

            // Отображаем список случаев ДТП в ListBox
            DTPListBox.ItemsSource = _dtpOccurrences.OrderBy(dtp => dtp.Date);
        }

        private void AddDTPButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем модальное окно для добавления нового случая ДТП
            var addDTPWindow = new AddDTPWindow();
            addDTPWindow.ShowDialog();

            // Если пользователь нажал кнопку "Добавить", то добавляем новый случай ДТП в список
            if (addDTPWindow.DialogResult.HasValue && addDTPWindow.DialogResult.Value)
            {
                var newDTPOccurrence = addDTPWindow.DTPOccurrence;

                // Добавляем новый случай ДТП в список и отображаем обновленный список в ListBox
                _dtpOccurrences.Add(newDTPOccurrence);
                DTPListBox.ItemsSource = _dtpOccurrences.OrderBy(dtp => dtp.Date);

                // Сохраняем обновленный список в файл
                SaveDTPOccurrences();
            }
        }

        private void EditDTPButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный случай ДТП из ListBox
            var selectedDTPOccurrence = DTPListBox.SelectedItem as DTPOccurrence;

            if (selectedDTPOccurrence != null)
            {
                // Открываем модальное окно для редактирования случая ДТП
                var editDTPWindow = new EditDTPWindow(selectedDTPOccurrence);
                editDTPWindow.ShowDialog();

                // Если пользователь нажал кнопку "Сохранить", то обновляем данные случая ДТП в списке
                if (editDTPWindow.DialogResult.HasValue && editDTPWindow.DialogResult.Value)
                {
                    // Обновляем данные случая ДТП
                    selectedDTPOccurrence.Date = editDTPWindow.DTPOccurrence.Date;
                    selectedDTPOccurrence.Location = editDTPWindow.DTPOccurrence.Location;
                    selectedDTPOccurrence.Drivers = editDTPWindow.DTPOccurrence.Drivers;
                    selectedDTPOccurrence.LicensePlates = editDTPWindow.DTPOccurrence.LicensePlates;

                    // Обновляем отображение списка в ListBox
                    DTPListBox.Items.Refresh();

                    // Сохраняем обновленный список в файл
                    SaveDTPOccurrences();
                }
            }
        }

        private void DeleteDTPButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный случай ДТП из ListBox
            var selectedDTPOccurrence = DTPListBox.SelectedItem as DTPOccurrence;

            if (selectedDTPOccurrence != null)
            {
                // Удаляем выбранный случай ДТП из списка и отображаем обновленный список в ListBox
                _dtpOccurrences.Remove(selectedDTPOccurrence);
                DTPListBox.ItemsSource = _dtpOccurrences.OrderBy(dtp => dtp.Date);
                // Сохраняем обновленный список в файл
                SaveDTPOccurrences();
            }
        }
        private void LoadDTPOccurrences()
        {
            try
            {
                // Читаем список случаев ДТП из файла
                var jsonString = File.ReadAllText("dtp.json");
                _dtpOccurrences = JsonSerializer.Deserialize<List<DTPOccurrence>>(jsonString);
            }
            catch (Exception)
            {
                // Если возникла ошибка при чтении файла, то создаем пустой список
                _dtpOccurrences = new List<DTPOccurrence>();
            }
        }

        private void SaveDTPOccurrences()
        {
            // Сохраняем список случаев ДТП в файл
            var jsonString = JsonSerializer.Serialize(_dtpOccurrences);
            File.WriteAllText("dtp.json", jsonString);
        }
    }

    public class DTPOccurrence
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Drivers { get; set; }
        public string LicensePlates { get; set; }
    }
}