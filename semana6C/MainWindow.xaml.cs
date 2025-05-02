using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace semana6C
{
    public partial class MainWindow : Window
    {
        
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-58CNH6N\\SQLEXPRESS;Initial Catalog=Neptuno; User ID = rolando; Password=rolando2001;TrustServerCertificate=true");

       

        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("USP_ListarClientes", connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        IdCliente = dataReader["idCliente"].ToString(),
                        NombreCompañia = dataReader["NombreCompañia"].ToString(),
                        NombreContacto = dataReader["NombreContacto"].ToString(),
                        CargoContacto = dataReader["CargoContacto"].ToString(),
                        Direccion = dataReader["Direccion"].ToString(),
                        Ciudad = dataReader["Ciudad"].ToString(),
                        Region = dataReader["Region"].ToString(),
                        CodPostal = dataReader["CodPostal"].ToString(),
                        Pais = dataReader["Pais"].ToString(),
                        Telefono = dataReader["Telefono"].ToString(),
                        Fax = dataReader["Fax"].ToString(),
                    });
                }

                connection.Close();
                dgClientes.ItemsSource = clientes;  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message);
            }
        }

        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("USP_InsCliente", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@idCliente", txtIdCliente.Text.Trim());
                command.Parameters.AddWithValue("@NombreCompania", txtNombreCompania.Text.Trim());
                command.Parameters.AddWithValue("@NombreContacto", txtNombreContacto.Text.Trim());
                command.Parameters.AddWithValue("@CargoContacto", txtCargoContacto.Text.Trim());
                command.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim());
                command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text.Trim());
                command.Parameters.AddWithValue("@Region", txtRegion.Text.Trim());
                command.Parameters.AddWithValue("@CodPostal", txtCodPostal.Text.Trim());
                command.Parameters.AddWithValue("@Pais", txtPais.Text.Trim());
                command.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                command.Parameters.AddWithValue("@Fax", txtFax.Text.Trim());

                command.ExecuteNonQuery();
                MessageBox.Show("Cliente registrado con éxito.");
                connection.Close();

                
                Button_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar cliente: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

       
        private void dgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClientes.SelectedItem is Cliente cliente)
            {
                txtIdCliente.Text = cliente.IdCliente;
                txtNombreCompania.Text = cliente.NombreCompañia;
                txtNombreContacto.Text = cliente.NombreContacto;
                txtCargoContacto.Text = cliente.CargoContacto;
                txtDireccion.Text = cliente.Direccion;
                txtCiudad.Text = cliente.Ciudad;
                txtRegion.Text = cliente.Region;
                txtCodPostal.Text = cliente.CodPostal;
                txtPais.Text = cliente.Pais;
                txtTelefono.Text = cliente.Telefono;
                txtFax.Text = cliente.Fax;
            }
        }

    
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("USP_UpdCliente", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCliente", txtIdCliente.Text);
                cmd.Parameters.AddWithValue("@NombreCompania", txtNombreCompania.Text);
                cmd.Parameters.AddWithValue("@NombreContacto", txtNombreContacto.Text);
                cmd.Parameters.AddWithValue("@CargoContacto", txtCargoContacto.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                cmd.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                cmd.Parameters.AddWithValue("@Region", txtRegion.Text);
                cmd.Parameters.AddWithValue("@CodPostal", txtCodPostal.Text);
                cmd.Parameters.AddWithValue("@Pais", txtPais.Text);
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@Fax", txtFax.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Cliente actualizado correctamente.");
                connection.Close();

           
                Button_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdCliente.Text))
            {
                MessageBox.Show("Selecciona un cliente para eliminar.");
                return;
            }

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("USP_DelCliente", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", txtIdCliente.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Cliente eliminado lógicamente.");
                connection.Close();


                Button_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar cliente: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}