using System;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc
{
	
	public class DataService : DataTable
	{
        
		// The connection to a database of this data service.
		private static SqlConnection	m_Connection;

        //
        public static String m_ConnectString = "Server=.\\SQLEXPRESS;Initial Catalog=QLCHNongDuoc;Integrated Security=SSPI;TrustServerCertificate=True;";
		// The command to execute query or non-query command on a database of this data service.
		private SqlCommand		m_Command;
      
		// The data adapter to execute query on a database of this data service.
		private SqlDataAdapter	m_DataAdapter;

        public DataService(){}


        public SqlCommand Command
        {
            get { return m_Command; }
            set { m_Command = value; }
        }

		public void Load(SqlCommand command)
		{
            OpenConnection();
            m_Command = command;
            try
            {
                
                m_Command.Connection = m_Connection;

                m_DataAdapter = new SqlDataAdapter();
                m_DataAdapter.SelectCommand = m_Command;

                this.Clear();
                m_DataAdapter.Fill(this);

            }
            catch (Exception e) 
            {
                // Log error để dễ debug
                System.Diagnostics.Debug.WriteLine("DataService.Load Error: " + e.Message);
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối database:\n" + e.Message + "\n\nConnection: " + m_ConnectString, "Lỗi", 
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
		}


        public static bool OpenConnection()
        {
            try
            {
                if (m_Connection == null)
                   
                    m_Connection = new SqlConnection(m_ConnectString);
                    
                    
                if (m_Connection.State == ConnectionState.Closed)
                    m_Connection.Open();
                return true;
            }
            catch (Exception e)
            {
                m_Connection.Close();
                return false;
            }
            
        }
		
		public void CloseConnection()
		{
			m_Connection.Close();
		}

        
        public int ExecuteNoneQuery()
		{
            int result = 0;
            SqlTransaction tr = null;
			try
			{
                tr =  m_Connection.BeginTransaction();

                m_Command.Connection = m_Connection;
                m_Command.Transaction = tr;

                m_DataAdapter = new SqlDataAdapter();
                m_DataAdapter.SelectCommand = m_Command;

                SqlCommandBuilder builder = new SqlCommandBuilder(m_DataAdapter);                

                result = m_DataAdapter.Update(this);
               

                tr.Commit();
                
			}
			catch ( Exception e)
            {
                if (tr != null) tr.Rollback();
           
            }
            return result;
		}
        
        /// <param name="command">SqlCommand hay Store Procedure</param>
        
        public int ExecuteNoneQuery(SqlCommand cmd)
        {

            int result = 0;
            SqlTransaction tr = null;

            try
            {
                tr = m_Connection.BeginTransaction();

                cmd.Connection = m_Connection;

                cmd.Transaction = tr;

                result = cmd.ExecuteNonQuery();

                this.AcceptChanges();

                tr.Commit();

            }
            catch(Exception e)
            {
                if (tr != null) tr.Rollback();
                throw;
            }
            return result;
            
        }

        public object ExecuteScalar(SqlCommand cmd)
        {
            object result = null;
            SqlTransaction tr = null;
            
            try
            {
                tr = m_Connection.BeginTransaction();

                cmd.Connection = m_Connection;

                cmd.Transaction = tr;

                result = cmd.ExecuteScalar();

                this.AcceptChanges();

                tr.Commit();

                if (result == DBNull.Value)
                {
                    result = null;
                }
            }
            catch
            {
                if (tr != null) tr.Rollback();
                throw;
            }
            return result;
        }
	}
}