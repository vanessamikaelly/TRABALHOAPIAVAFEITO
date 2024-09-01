using Microsoft.AspNetCore.Mvc;
using TRAPALHOAPIAVA.Models;

namespace TRAPALHOAPIAVA.Controllers
{
    [Route("APICLIENTE [CONTROLLER]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private static List<Cliente> clientes = new List<Cliente>();

        public ClientesController()
        {
            if (clientes.Count == 0)
            {
                clientes.Add(new Cliente
                {
                    Nome = "Vanessa Mikaelly",
                    DataNascimento = new DateTime(2006, 2, 11),
                    Sexo = "F",
                    RG = "123456",
                    CPF = "049.232.052-70",
                    Endereco = "Rua Monte Castelo 1425",
                    Cidade = "JIPA",
                    Estado = "RO",
                    Telefone = "(69) 99291-0804",
                    Email = "vanessa@eemail.com"
                });

                clientes.Add(new Cliente
                {
                    Nome = "Paulo Rogerio",
                    DataNascimento = new DateTime(2007, 09, 04),
                    Sexo = "M",
                    RG = "123456",
                    CPF = "987.654.321-00",
                    Endereco = "Avenida B, 456",
                    Cidade = "JIPA",
                    Estado = "R0",
                    Telefone = "(69) 99289-1569",
                    Email = "paulo@gmail.com"
                });

                SalvarTXT();
            }
        }

    
        [HttpGet]
        public IActionResult GetClientes()
        {
            return Ok(clientes);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetCliente(string cpf)
        {
            var cliente = clientes.FirstOrDefault(c => c.CPF == cpf);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Cliente inválido.");
            }

            if (!VCPF(cliente.CPF))
            {
                return BadRequest("CPF inválido.");
            }

            clientes.Add(cliente);

            SalvarTXT();
            return StatusCode(StatusCodes.Status201Created, cliente);
        }

        
        [HttpPut("{cpf}")]
        public IActionResult Put(string cpf, [FromBody] Cliente clienteAtualizado)
        {
            var cliente = clientes.FirstOrDefault(c => c.CPF == cpf);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            cliente.Nome = clienteAtualizado.Nome;
            cliente.DataNascimento = clienteAtualizado.DataNascimento;
            cliente.Sexo = clienteAtualizado.Sexo;
            cliente.RG = clienteAtualizado.RG;
            cliente.Endereco = clienteAtualizado.Endereco;
            cliente.Cidade = clienteAtualizado.Cidade;
            cliente.Estado = clienteAtualizado.Estado;
            cliente.Telefone = clienteAtualizado.Telefone;
            cliente.Email = clienteAtualizado.Email;

            SalvarTXT();
            return Ok(cliente);
        }

     
        [HttpDelete("{cpf}")]
        public IActionResult Delete(string cpf)
        {
            var cliente = clientes.FirstOrDefault(c => c.CPF == cpf);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            clientes.Remove(cliente);

            SalvarTXT();
            return Ok("Cliente deletado com sucesso!");
        }

       
        private bool VCPF(string cpf)
        {
            return ValidarCPF.ValidaCPF(cpf);
        }

       
        private void SalvarTXT()
        {
            using (StreamWriter writer = new StreamWriter("clientes.txt"))
            {
                foreach (var cliente in clientes)
                {
                    writer.WriteLine($"{cliente.Nome},{cliente.DataNascimento},{cliente.Sexo},{cliente.RG},{cliente.CPF},{cliente.Endereco},{cliente.Cidade},{cliente.Estado},{cliente.Telefone},{cliente.Email}");
                }
            }
        }
    }


}



