using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

public class ProductController : ControllerBase
{
    private List<Product> products = new List<Product>();
    private List<Seller> sellers = new List<Seller>();

    [HttpGet("/products")]
    public IActionResult GetAllProducts([FromQuery] string sortBy = "added_time", [FromQuery] int limit = 10)
    {
        var sortedProducts = SortProducts(sortBy);
        var limitedProducts = sortedProducts.Take(limit);
        return Ok(limitedProducts);
    }

    [HttpGet("/products/search")]
    public IActionResult SearchProducts([FromQuery] string searchTerm)
    {
        var searchResults = products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(searchResults);
    }

    [HttpGet("/products/{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost("/products")]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        var seller = product.Seller;
        if (seller == null)
        {
            return BadRequest("Seller not found");
        }

        if (!IsSellerAuthorized(seller))
        {
            return Unauthorized("You are not authorized to create a product for this seller");
        }

        product.AddedTime = DateTime.UtcNow;
        products.Add(product);

        return Created($"/products/{product.Id}", product);
    }

    [HttpPut("/products/{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        var existingProduct = products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        if (!IsSellerAuthorized(existingProduct.Seller))
        {
            return Unauthorized("You are not authorized to update this product");
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Image = updatedProduct.Image;
        existingProduct.Price = updatedProduct.Price;
        existingProduct.Available = updatedProduct.Available;

        return Ok(existingProduct);
    }

    [HttpDelete("/products/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var productToDelete = products.FirstOrDefault(p => p.Id == id);
        if (productToDelete == null)
        {
            return NotFound();
        }

        if (!IsSellerAuthorized(productToDelete.Seller))
        {
            return Unauthorized("You are not authorized to delete this product");
        }

        products.Remove(productToDelete);
        return NoContent();
    }

    // Méthodes d'assistance
    private IEnumerable<Product> SortProducts(string sortBy)
    {
        switch (sortBy.ToLower())
        {
            case "date":
                return products.OrderBy(p => p.AddedTime);
            case "type":
                // Implémenter le tri par type le cas échéant
                break;
            case "name":
                return products.OrderBy(p => p.Name);
            case "price":
                return products.OrderBy(p => p.Price);
            default:
                return products.OrderBy(p => p.AddedTime);
        }
        return products.OrderBy(p => p.AddedTime);
    }

    private Seller GetSellerById(int sellerId)
    {
        return sellers.FirstOrDefault(s => s.Id == sellerId);
    }

    private bool IsSellerAuthorized(Seller seller)
    {
        // Logique pour vérifier si l'utilisateur actuel est le vendeur
         // Cela pourrait être basé sur l'authentification ou toute autre logique personnalisée
         // Pour plus de simplicité, supposons un identifiant de vendeur codé en dur pour l'utilisateur actuel
        int currentUserId = 1; // Remplacer par la logique réelle pour obtenir l'ID utilisateur actuel
        return currentUserId == seller.Id;
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; }
    public DateTime AddedTime { get; set; }
    public required Seller Seller { get; set; }
}

public class Seller
{
    public int Id { get; set; }
    public string Name { get; set; }
    // Autres propriétés du vendeur
}
