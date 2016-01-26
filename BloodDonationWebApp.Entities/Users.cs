 using System;

 namespace ES.Entities
 {
	public partial class Users : IBaseEntity
	{
		private int m_userid;
		private string m_username;
		private string m_email;
		private string m_password;
		private string m_contactno;
		private string m_role;
		private string m_country;
		private string m_city;
		private string m_area;
		private bool m_isdonor;
		private bool m_isactive;
		private string m_registrationdate;
		private string m_updationdate;

		public int UserID
		{
			get { return  m_userid; } 
			set{ m_userid = value; NotifyPropertyChanged("UserID"); }
		}

		public string UserName
		{
			get { return  m_username; } 
			set{ m_username = value; NotifyPropertyChanged("UserName"); }
		}

		public string Email
		{
			get { return  m_email; } 
			set{ m_email = value; NotifyPropertyChanged("Email"); }
		}

		public string Password
		{
			get { return  m_password; } 
			set{ m_password = value; NotifyPropertyChanged("Password"); }
		}

		public string ContactNo
		{
			get { return  m_contactno; } 
			set{ m_contactno = value; NotifyPropertyChanged("ContactNo"); }
		}

		public string Role
		{
			get { return  m_role; } 
			set{ m_role = value; NotifyPropertyChanged("Role"); }
		}

		public string Country
		{
			get { return  m_country; } 
			set{ m_country = value; NotifyPropertyChanged("Country"); }
		}

		public string City
		{
			get { return  m_city; } 
			set{ m_city = value; NotifyPropertyChanged("City"); }
		}

		public string Area
		{
			get { return  m_area; } 
			set{ m_area = value; NotifyPropertyChanged("Area"); }
		}

		public bool isDonor
		{
			get { return  m_isdonor; } 
			set{ m_isdonor = value; NotifyPropertyChanged("isDonor"); }
		}

		public bool isActive
		{
			get { return  m_isactive; } 
			set{ m_isactive = value; NotifyPropertyChanged("isActive"); }
		}

		public string RegistrationDate
		{
			get { return  m_registrationdate; } 
			set{ m_registrationdate = value; NotifyPropertyChanged("RegistrationDate"); }
		}

		public string UpdationDate
		{
			get { return  m_updationdate; } 
			set{ m_updationdate = value; NotifyPropertyChanged("UpdationDate"); }
		}

	}
}