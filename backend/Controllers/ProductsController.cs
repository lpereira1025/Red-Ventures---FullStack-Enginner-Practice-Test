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
            ImageInactive = "http://localhost:5010/Icons/salt/inactive.png",
            ImageActive = "http://localhost:5010/Icons/salt/active.png",
            ImageInactiveDesktop = "http://localhost:5010/Icons/salt/inactiveDesktop.png",
            ImageActiveDesktop = "http://localhost:5010/Icons/salt/activeDesktop.png",
            Name = "Salt",
            Description = "Simple like the seawater, nothing more",
            Price = 10
        },
        new Product
        {
            Id = "2",
            ImageInactive = "http://localhost:5010/Icons/shoyu/inactive.png",
            ImageActive = "http://localhost:5010/ICons/shoyu/active.png",
            ImageInactiveDesktop = "http://localhost:5010/Icons/shoyu/inactiveDesktop.png",
            ImageActiveDesktop = "http://localhost:5010/Icons/shoyu/activeDesktop.png",
            Name = "Shoyu",
            Description = "The good old and traditional soy sauce",
            Price = 10
        },
        new Product
        {
            Id = "3",
            ImageInactive = "http://localhost:5010/Icons/miso/inactive.png",
            ImageActive = "http://localhost:5010/Icons/miso/active.png",
            ImageInactiveDesktop = "http://localhost:5010/Icons/miso/inactiveDesktop.png",
            ImageActiveDesktop = "http://localhost:5010/Icons/miso/activeDesktop.png",
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
            ImageInactive = "http://localhost:5010/Icons/pork/inactive.png",
            ImageActive = "http://localhost:5010/Icons/pork/active.png",
            ImageInactiveDesktop = "http://localhost:5010/Icons/pork/inactiveDesktop.png",
            ImageActiveDesktop = "http://localhost:5010/Icons/pork/activeDesktop.png",
            Name = "Chasu",
            Description = "A sliced flavourful pork meat with a selection of season vegetables.",
            Price = 10
        },
        new Product
        {
            Id = "2",
            ImageInactive = "http://localhost:5010/Icons/yasai/inactive.png",
            ImageActive = "http://localhost:5010/Icons/yasai/active.png",
            ImageInactiveDesktop = "http://localhost:5010/Icons/yasai/inactiveDesktop.png",
            ImageActiveDesktop = "http://localhost:5010/Icons/yasai/activeDesktop.png",
            Name = "Yasai Vegetarian",
            Description = "A delicious vegetarian lamen with a selection of season vegetables.",
            Price = 10
        },
        new Product
        {
            Id = "3",
            ImageInactive = "http://localhost:5010/Icons/chicken/inactive.png",
            ImageActive = "http://localhost:5010/Icons/chicken/active.png",
            ImageInactiveDesktop = "http://localhost:5010/Icons/chicken/inactiveDesktop.png",
            ImageActiveDesktop = "http://localhost:5010/Icons/chicken/activeDesktop.png",
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
            switch (order.BrothId)
            {
                case "1":
                    response = new OrderResponse
                    {
                        Id = "1",
                        Description = "Salt",
                        Image = "http://localhost:5010/Icons/salt/salt_only.png"
                    };
                    break;
                case "2":
                    response = new OrderResponse
                    {
                        Id = "2",
                        Description = "Shoyu",
                        Image = "http://localhost:5010/Icons/shoyu/shoyu_only.png"
                    };
                    break;
                case "3":
                    response = new OrderResponse
                    {
                        Id = "3",
                        Description = "Miso",
                        Image = "http://localhost:5010/Icons/miso/miso_only.png"
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
                                Image = "http://localhost:5010/Icons/salt/salt_chasu.png"
                            };
                            break;
                        case "2":
                            response = new OrderResponse
                            {
                                Id = "12",
                                Description = "Salt and Yasai Vegetarian",
                                Image = "http://localhost:5010/Icons/salt/salt_yasai.png"
                            };
                            break;
                        case "3":
                            response = new OrderResponse
                            {
                                Id = "13",
                                Description = "Salt and Karaague",
                                Image = "http://localhost:5010/Icons/salt/salt_karaague.png"
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
                                Image = "http://localhost:5010/Icons/shoyu/shoyu_chasu.png"
                            };
                            break;
                        case "2":
                            response = new OrderResponse
                            {
                                Id = "22",
                                Description = "Shoyu and Yasai Vegetarian",
                                Image = "http://localhost:5010/Icons/shoyu/shoyu_yasai.png"
                            };
                            break;
                        case "3":
                            response = new OrderResponse
                            {
                                Id = "23",
                                Description = "Shoyu and Karaague",
                                Image = "http://localhost:5010/Icons/shoyu/shoyu_karaague.png"
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
                                Image = "http://localhost:5010/Icons/miso/miso_chasu.png"
                            };
                            break;
                        case "2":
                            response = new OrderResponse
                            {
                                Id = "32",
                                Description = "Miso and Yasai Vegetarian",
                                Image = "http://localhost:5010/Icons/miso/miso_yasai.png"
                            };
                            break;
                        case "3":
                            response = new OrderResponse
                            {
                                Id = "33",
                                Description = "Miso and Karaague",
                                Image = "http://localhost:5010/Icons/miso/miso_karaage.png"
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
