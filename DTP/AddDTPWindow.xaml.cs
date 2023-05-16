using System;
using System.Windows;
using System.Windows.Controls;

namespace DTPRegistrationApp
{
	public partial class AddDTPWindow : Window
	{
		public DTPOccurrence DTPOccurrence { get; private set; }

		public AddDTPWindow()
		{
			InitializeComponent();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			// Создаем новый объект DTPOccurrence и заполняем его данными из полей окна
			DTPOccurrence = new DTPOccurrence
			{
				Date = DatePicker.SelectedDate ?? DateTime.Now,
				Location = LocationTextBox.Text,
				Drivers = DriversTextBox.Text,
				LicensePlates = LicensePlatesTextBox.Text
			};

			// Устанавливаем результат модального окна как "true" для обозначения успешного добавления
			DialogResult = true;

			// Закрываем окно
			Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			// Закрываем окно без добавления нового случая ДТП
			DialogResult = false;
			Close();
		}
	}
}