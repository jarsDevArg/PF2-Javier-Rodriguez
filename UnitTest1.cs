namespace PF2_Rodriguez_Javier;
/*
Alumno: Javier Alberto Rodriguez Santonocito
Comisión: C
*/
public class Tests
{
    
    [Test]
    public void CrearUnProducto()
    {   
        Product producto = new(25, "cosa", 12.0, Product.CategoryName.Electronica);
        Assert.Multiple(() =>
        {
            Assert.That(producto, Is.InstanceOf<Product>());
            Assert.That(producto.id, Is.EqualTo(25));
            Assert.That(producto.name, Is.EqualTo("cosa"));
            Assert.That(producto.price, Is.EqualTo(12.0));
            Assert.That(producto.category, Is.EqualTo(Product.CategoryName.Electronica));
        });
    }
    
    [Test]
    public void AgregarProducto()
    {
        List<Product> productList = [];
        Product producto = new(25, "cosa", 12, Product.CategoryName.Electronica);
        ProductManager.addProduct(ref productList, producto);
        Assert.That(productList[0], Is.EqualTo(producto));
    }

    [Test]
    public void EcontrarProducto()
    {
        List<Product> productList = [];

        Product productoRelleno1 = new(1, "cosaRelleno1", 12, Product.CategoryName.Electronica);
        ProductManager.addProduct(ref productList, productoRelleno1);
        Product productoParaEncontrar = new(2, "cosaBuscda", 12, Product.CategoryName.Electronica);
        ProductManager.addProduct(ref productList, productoParaEncontrar);
        Product productoRelleno2 = new(3, "cosaRelleno2", 12, Product.CategoryName.Electronica);
        ProductManager.addProduct(ref productList, productoRelleno2);

        Product? productoBuscado = ProductManager.findProductById(productList, 2);
        Assert.That(productoBuscado, Is.EqualTo(productoParaEncontrar));
    }

    [Test]
    public void PrecioTotalElectronica()
    {   
        Product producto = new(25, "cosa", 10, Product.CategoryName.Electronica);
        double totalPrice = ProductManager.calculateTotalPrice(producto);
        Assert.That(totalPrice, Is.EqualTo(11));
    }
    [Test]
    public void PrecioTotalAlimentos()
    {   
        Product producto = new(25, "cosa", 10, Product.CategoryName.Alimentos);
        double totalPrice = ProductManager.calculateTotalPrice(producto);
        Assert.That(totalPrice, Is.EqualTo(10.5));
    }

}

public class Product{
    readonly public int id;
    readonly public string name;
    public double price;
    public enum CategoryName {Electronica, Alimentos};
    readonly public CategoryName category;

    public Product(int id, string name, double price, CategoryName category){
        this.id = id;
        this.name = name;
        if (price < 0)
        {
            throw new ArgumentException("El precio no puede ser negativo", nameof(price));
        }else{
            this.price = price;
        }
        this.category = category;
    }
}
public static class ProductManager{
    public static List<Product> addProduct(ref List<Product> productList, Product newProduct){
        productList.Add(newProduct);
        return productList;
    }

    public static double calculateTotalPrice(Product producto){
        double totalPrice = producto.category switch
        {
            Product.CategoryName.Electronica => producto.price * 1.1,
            Product.CategoryName.Alimentos => producto.price * 1.05,
            _ => producto.price,
        };

        return totalPrice;
    }

    public static Product? findProductById(List<Product> productList, int productId){
        Product? producto;
        try{
            producto = productList.Find(x => x.id == productId);
        }
        catch{
            Console.WriteLine("Producto no encontrado");
            return null;
        }
        return producto;
    }
}
