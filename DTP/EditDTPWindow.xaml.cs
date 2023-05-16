using System;
using System.Windows;
using System.Windows.Controls;

namespace DTPRegistrationApp
{
	public partial class EditDTPWindow : Window
	{
		public DTPOccurrence DTPOccurrence { get; private set; }

		public EditDTPWindow(DTPOccurrence dtpOccurrence)
		{
			InitializeComponent();

			// Заполняем поля окна данными из переданного случая ДТП
			DatePicker.SelectedDate = dtpOccurrence.Date;
			LocationTextBox.Text = dtpOccurrence.Location;
			DriversTextBox.Text = dtpOccurrence.Drivers;
			LicensePlatesTextBox.Text = dtpOccurrence.LicensePlates;
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			// Создаем новый объект DTPOccurrence и заполняем его данными из полей окна
			DTPOccurrence = new DTPOccurrence
			{
				Date = DatePicker.SelectedDate ?? DateTime.Now,
				Location = LocationTextBox.Text,
				Drivers = DriversTextBox.Text,
				LicensePlates = LicensePlatesTextBox.Text
			};

			// Устанавливаем результат модального окна как "true" для обозначения успешного сохранения
			DialogResult = true;

			// Закрываем окно
			Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			// Закрываем окно без сохранения изменений
			DialogResult = false;
			Close();
		}
	}
}