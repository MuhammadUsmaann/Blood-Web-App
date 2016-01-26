 using System;

 namespace ES.Entities
 {
	public partial class Roles : IBaseEntity
	{
		private int m_roleid;
		private string m_description;
		private string m_code;

		public int RoleID
		{
			get { return  m_roleid; } 
			set{ m_roleid = value; NotifyPropertyChanged("RoleID"); }
		}

		public string Description
		{
			get { return  m_description; } 
			set{ m_description = value; NotifyPropertyChanged("Description"); }
		}

		public string Code
		{
			get { return  m_code; } 
			set{ m_code = value; NotifyPropertyChanged("Code"); }
		}

	}
}