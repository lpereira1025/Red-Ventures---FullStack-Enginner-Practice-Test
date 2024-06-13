using Microsoft.AspNetCore.Mvc;
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
            ImageInactive = "Icons/salt/inactive.svg",
            ImageActive = "Icons/salt/active.svg",
            Name = "Salt",
            Description = "Simple like the seawater, nothing more",
            Price = 10
        },
        new Product
        {
            Id = "2",
            ImageInactive = "Icons/shoyu/inactive.svg",
            ImageActive = "Icons/shoyu/active.svg",
            Name = "Shoyu",
            Description = "The good old and traditional soy sauce",
            Price = 10
        },
        new Product
        {
            Id = "3",
            ImageInactive = "Icons/miso/inactive.svg",
            ImageActive = "Icons/miso/active.svg",
            Name = "Miso",
            Description = "Paste made of fermented soybeans",
            Price = 12
        }
    };

    private static readonly List<Product> Proteins = new List<Product>
    {
        new Product
        {
            Id = "1",
            ImageInactive = "Icons/pork/inactive.svg",
            ImageActive = "Icons/pork/active.svg",
            Name = "Chasu",
            Description = "A sliced flavourful pork meat with a selection of season vegetables.",
            Price = 10
        },
        new Product
        {
            Id = "2",
            ImageInactive = "Icons/yasai/inactive.svg",
            ImageActive = "Icons/yasai/active.svg",
            Name = "Yasai Vegetarian",
            Description = "A delicious vegetarian lamen with a selection of season vegetables.",
            Price = 10
        },
        new Product
        {
            Id = "3",
            ImageInactive = "Icons/chicken/inactive.svg",
            ImageActive = "Icons/chicken/active.svg",
            Name = "Karaague",
            Description = "Three units of fried chicken, moyashi, ajitama egg and other vegetables.",
            Price = 12
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
        if (order == null || string.IsNullOrEmpty(order.BrothId))
        {
            return BadRequest(new { error = "both brothId and proteinId are required" });
        }

        var broth = Broths.FirstOrDefault(b => b.Id == order.BrothId);

        if (broth == null)
        {
            return StatusCode(500, new { error = "could not place order" });
        }

        OrderResponse response;

        if (string.IsNullOrEmpty(order.ProteinId))
        {
            // Caso onde apenas brothId é fornecido
            switch (order.BrothId)
            {
                case "1":
                    response = new OrderResponse
                    {
                        Id = "1",
                        Description = "Salt",
                        Image = "Images/Salt/salt_only.png"
                    };
                    break;
                case "2":
                    response = new OrderResponse
                    {
                        Id = "2",
                        Description = "Shoyu",
                        Image = "Images/Shoyu/shoyu_only.png"
                    };
                    break;
                case "3":
                    response = new OrderResponse
                    {
                        Id = "3",
                        Description = "Miso",
                        Image = "Images/Miso/miso_only.png"
                    };
                    break;
                default:
                    return StatusCode(500, new { error = "could not place order" });
            }
        }
        else
        {
            var protein = Proteins.FirstOrDefault(p => p.Id == order.ProteinId);

            if (protein == null)
            {
                return StatusCode(500, new { error = "could not place order" });
            }

            // Combinações específicas de brothId e proteinId
            switch (order.BrothId)
            {
                case "1":
                    switch (order.ProteinId)
                    {
                        case "1":
                            response = new OrderResponse
                            {
                                Id = "11",
                                Description = "Salt and Chasu Ramen",
                                Image = "Images/Salt/salt_chasu.png"
                            };
                            break;
                        case "2":
                            response = new OrderResponse
                            {
                                Id = "12",
                                Description = "Salt and Yasai Vegetarian",
                                Image = "Images/Salt/salt_yasai.png"
                            };
                            break;
                        case "3":
                            response = new OrderResponse
                            {
                                Id = "13",
                                Description = "Salt and Karaague",
                                Image = "Images/Salt/salt_karaague.png"
                            };
                            break;
                        default:
                            return StatusCode(500, new { error = "could not place order" });
                    }
                    break;

                case "2":
                    switch (order.ProteinId)
                    {
                        case "1":
                            response = new OrderResponse
                            {
                                Id = "21",
                                Description = "Shoyu and Chasu Ramen",
                                Image = "Images/Shoyu/shoyu_chasu.png"
                            };
                            break;
                        case "2":
                            response = new OrderResponse
                            {
                                Id = "22",
                                Description = "Shoyu and Yasai Vegetarian",
                                Image = "Images/Shoyu/shoyu_yasai.png"
                            };
                            break;
                        case "3":
                            response = new OrderResponse
                            {
                                Id = "23",
                                Description = "Shoyu and Karaague",
                                Image = "Images/Shoyu/shoyu_karaague.png"
                            };
                            break;
                        default:
                            return StatusCode(500, new { error = "could not place order" });
                    }
                    break;

                case "3":
                    switch (order.ProteinId)
                    {
                        case "1":
                            response = new OrderResponse
                            {
                                Id = "31",
                                Description = "Miso and Chasu Ramen",
                                Image = "Images/Miso/miso_chasu.png"
                            };
                            break;
                        case "2":
                            response = new OrderResponse
                            {
                                Id = "32",
                                Description = "Miso and Yasai Vegetarian",
                                Image = "Images/Miso/miso_yasai.png"
                            };
                            break;
                        case "3":
                            response = new OrderResponse
                            {
                                Id = "33",
                                Description = "Miso and Karaague",
                                Image = "Images/Miso/miso_karaage.png"
                            };
                            break;
                        default:
                            return StatusCode(500, new { error = "could not place order" });
                    }
                    break;

                default:
                    return StatusCode(500, new { error = "could not place order" });
            }
        }

        return CreatedAtAction(nameof(PlaceOrder), response);
    }
}


