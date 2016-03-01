 using System;

 namespace BloodDonationWebApp.Entities
 {
	public partial class Roles
	{
		private int m_roleid;
		private string m_description;
		private string m_code;

		public int RoleID
		{
			get { return  m_roleid; } 
			set{ m_roleid = value;}
		}

		public string Description
		{
			get { return  m_description; } 
			set{ m_description = value;}
		}

		public string Code
		{
			get { return  m_code; } 
			set{ m_code = value;}
		}

	}
}