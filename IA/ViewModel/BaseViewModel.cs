﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace IA
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		protected Page page;
		public BaseViewModel(Page page)
		{
			this.page = page;
		}

		private string title = string.Empty;
		public const string TitlePropertyName = "Title";

		/// <summary>
		/// Gets or sets the "Title" property
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value, TitlePropertyName); }
		}

		private string subTitle = string.Empty;
		/// <summary>
		/// Gets or sets the "Subtitle" property
		/// </summary>
		public const string SubtitlePropertyName = "Subtitle";
		public string Subtitle
		{
			get { return subTitle; }
			set { SetProperty(ref subTitle, value, SubtitlePropertyName); }
		}

		private string icon = null;
		/// <summary>
		/// Gets or sets the "Icon" of the viewmodel
		/// </summary>
		public const string IconPropertyName = "Icon";
		public string Icon
		{
			get { return icon; }
			set { SetProperty(ref icon, value, IconPropertyName); }
		}

		private bool isBusy;
		/// <summary>
		/// Gets or sets if the view is busy.
		/// </summary>
		public const string IsBusyPropertyName = "IsBusy";
		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				SetProperty(ref isBusy, value, IsBusyPropertyName);
				SetProperty(ref isBusyRev, !isBusy, IsBusyRevPropertyName);
			}
		}

		private bool isBusyRev;
		/// <summary>
		/// Gets or sets the reverse of  isbusy. Handy for hiding views during busy times.
		/// </summary>
		public const string IsBusyRevPropertyName = "IsBusyRev";
		public bool IsBusyRev
		{
			get { return isBusyRev; }
			set { SetProperty(ref isBusyRev, value, IsBusyRevPropertyName); }
		}

		protected void SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
		{

			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return;

			backingStore = value;

			onChanged?.Invoke();

			OnPropertyChanged(propertyName);
		}

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
