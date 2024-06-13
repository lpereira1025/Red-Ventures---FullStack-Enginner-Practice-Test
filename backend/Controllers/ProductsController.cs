using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> Broths = new List<Product>
    {
        new Product
        {
            Id = "1",
            ImageInactive = "images/salt-inactive.svg",
            ImageActive = "images/salt-active.svg",
            Name = "Salt",
            Description = "Simple like the seawater, nothing more",
            Price = 10
        }
    };

    private static readonly List<Product> Proteins = new List<Product>
    {
        new Product
        {
            Id = "1",
            ImageInactive = "images/pork-inactive.svg",
            ImageActive = "images/pork-active.svg",
            Name = "Chasu",
            Description = "A sliced flavourful pork meat with a selection of season vegetables.",
            Price = 10
        }
    };

    [HttpGet("broths")]
    [ApiKeyAuth]
    public ActionResult<IEnumerable<Product>> GetBroths()
    {
        return Ok(Broths);
    }

    [HttpGet("proteins")]
    [ApiKeyAuth]
    public ActionResult<IEnumerable<Product>> GetProteins()
    {
        return Ok(Proteins);
    }

    [HttpPost("orders")]
    [ApiKeyAuth]
    public ActionResult<OrderResponse> PlaceOrder([FromBody] Order order)
    {
        // Verifica se os parâmetros necessários estão presentes
        if (order == null || string.IsNullOrEmpty(order.BrothId) || string.IsNullOrEmpty(order.ProteinId))
        {
            return BadRequest(new { error = "both brothId and proteinId are required" });
        }

        // Verifica se há um caldo (broth) e uma proteína correspondentes aos IDs fornecidos
        var broth = Broths.FirstOrDefault(b => b.Id == order.BrothId);
        var protein = Proteins.FirstOrDefault(p => p.Id == order.ProteinId);

        if (broth == null || protein == null)
        {
            // Se não encontrar um caldo ou uma proteína correspondente, retorna erro interno do servidor
            return StatusCode(500, new { error = "could not place order" });
        }

        // Se tudo estiver ok, cria uma resposta de pedido bem-sucedido
        var response = new OrderResponse
        {
            Id = Guid.NewGuid().ToString(), // Simula um ID único para o pedido
            Description = $"{broth.Name} and {protein.Name} Ramen",
            Image = "images/ramenChasu.png"
        };

        // Retorna resposta 201 Created com os detalhes do pedido
        return CreatedAtAction(nameof(PlaceOrder), response);
    }
}