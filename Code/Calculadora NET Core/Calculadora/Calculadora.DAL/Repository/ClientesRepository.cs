using System;
using System.Linq;
using Calculadora.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculadora.DAL.Repository
{
    public class ClientesRepository : BaseRepository
    {
        private readonly VanguardaContext db;

        public ClientesRepository(VanguardaContext context)
        {
            db = context;
        }

        public IQueryable<Cliente> GetClientes(string sortOrder, string currentFilter, string searchString, int? page)
        {
            IQueryable<Cliente> clientes = from s in db.Clientes
                                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(s => s.Nome.Contains(searchString)
                                       || s.Cpf.Contains(searchString));
            }
            switch (sortOrder)
            {
                //case "name_desc":
                //    indicesCorrecao = indicesCorrecao.OrderByDescending(s => s.Nome);
                //    break;

                default:  // Name ascending 
                    clientes = clientes.OrderBy(s => s.Nome);
                    break;
            }

            return clientes;
        }

        public IQueryable<Cliente> GetAllClientes()
        {
            IQueryable<Cliente> clientes = from s in db.Clientes
                                           select s;


            return clientes;
        }

        public Cliente GetClienteId(int? id, bool clienteCompleto = true)
        {
            try
            {
                Cliente cliente = null;
                if (clienteCompleto)
                    cliente = (Cliente)db.Clientes.Include(e => e.Enderecos).Include(e => e.Escritorio).Where(c => c.PessoaID == id).FirstOrDefault();
                else
                    cliente = (Cliente)db.Clientes.Where(c => c.PessoaID == id).FirstOrDefault();

                return cliente;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public void SetCliente(Cliente cliente)
        {
            try
            {
                db.Pessoas.Add(cliente);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteCliente(int id)
        {
            try
            {
                Cliente cliente = GetClienteId(id);

                foreach (Endereco end in cliente.Enderecos)
                {
                    db.Enderecos.Remove(end);
                }

                db.Clientes.Remove(cliente);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GetSimulacaoId(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCliente(Cliente cliente)
        {
            try
            {
                //db.Entry(cliente.Enderecos).State = EntityState.Modified;

                foreach (Endereco endereco in cliente.Enderecos)
                {
                    if (!string.IsNullOrEmpty(endereco.Cep))
                    {
                        endereco.PessoaID = cliente.PessoaID;
                        db.Entry(endereco).State = (endereco.EnderecoID <= 0) ? EntityState.Added : EntityState.Modified;
                    }
                    // db.SaveChanges();
                }
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ExisteCPF(string cpf)
        {
            if (db.Pessoas.Where(p => p.Cpf == cpf).Count() > 0) return true;
            else return false;

        }

        public bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
    }
