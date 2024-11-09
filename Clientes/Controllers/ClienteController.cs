using Clientes.Aplicacao.Servicos;
using Clientes.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController(ILogger<ClienteController> logger, ClienteServico clienteServico) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var clientes = await clienteServico.ListarClientes();
            return Ok(clientes);
           
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Erro ao buscar os bloqueios cadastrados: {ex.Message}");
            return StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Obter(int id)
    {
        try
        {
            var cliente = await clienteServico.ObterCliente(id);
            if (cliente == null)
            {
                logger.LogWarning("Cliente com ID {Id} não encontrado", id);
                return NotFound();
            }

            logger.LogInformation("Cliente com ID {Id} encontrado", id);
            return Ok(cliente);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao obter cliente com ID {Id}", id);
            return StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] Cliente cliente)
    {
        try
        {
            await clienteServico.CadastrarCliente(cliente);

            logger.LogInformation("Cliente cadastrado com sucesso. ID: {Id}", cliente.Id);
            return CreatedAtAction(nameof(Obter), new { id = cliente.Id }, cliente);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao cadastrar um novo cliente");
            return StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Atualizar([FromBody]Cliente cliente)
    {
        try
        {
            var clienteExistente = await clienteServico.ObterCliente(cliente.Id);

            if (clienteExistente == null)
            {
                logger.LogWarning("Cliente com ID {Id} não encontrado para atualização", cliente.Id);
                return NotFound();
            }

            await clienteServico.AtualizarCliente(cliente);
            logger.LogInformation("Cliente com ID {Id} atualizado com sucesso", cliente.Id);

            return NoContent();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar cliente com ID {Id}", cliente.Id);
            return StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await clienteServico.RemoverCliente(id);
            logger.LogInformation("Cliente com ID {id} removido com sucesso", id);

            return NoContent();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Erro ao remover cliente com ID {id}", id);
            return StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
        }
    }
}
