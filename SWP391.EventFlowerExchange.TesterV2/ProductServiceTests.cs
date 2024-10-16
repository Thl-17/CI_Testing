using Moq;
using SWP391.EventFlowerExchange.Application;
using SWP391.EventFlowerExchange.Domain.Entities;
using SWP391.EventFlowerExchange.Domain.ObjectValues;
using SWP391.EventFlowerExchange.Infrastructure;


namespace SWP391.EventFlowerExchange.Test
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockRepo;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IProductRepository>();
            _productService = new ProductService(_mockRepo.Object);
        }


        [Test]
        public async Task GetEnableProductListFromAPIAsync_ReturnsProductList()
        {
            // Arrange
            var products = new List<Product> { new Product { ProductName = "Flower" } };
            _mockRepo.Setup(repo => repo.GetEnableProductListAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetEnableProductListFromAPIAsync();

            // Assert
            Assert.AreEqual("Flower" , result[0].ProductName); // Kiểm tra tên sản phẩm
        }

        [Test]
        public async Task GetEnableProductListFromAPIAsync_NoProducts_ReturnsEmptyList()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetEnableProductListAsync()).ReturnsAsync(new List<Product>());

            // Act
            var result = await _productService.GetEnableProductListFromAPIAsync();

            // Assert
            Assert.IsNotNull(result); // Kết quả không được null
            Assert.AreEqual(0, result.Count); // Kiểm tra danh sách trống
        }

        [Test]
        public async Task CreateNewProductFromAPIAsync_ValidProduct_ReturnsSuccess()
        {
            // Arrange
            CreateProduct product = new CreateProduct { ProductName = "Rose", SellerId = "ididsj" };
            _mockRepo.Setup(repo => repo.CreateNewProductAsync(product)).ReturnsAsync(true);

            // Act
            var result = await _productService.CreateNewProductFromAPIAsync(product);

            // Assert
            Assert.IsTrue(result); // Kết quả phải là true
        }

        [Test]
        public async Task ViewProductByIdFromAPIAsync_ValidProduct_ReturnsSuccess()
        {
            int productId = 1;
            // Arrange
            Product product = new Product()
            {
                ProductId = productId
            };
            _mockRepo.Setup(repo => repo.SearchProductByIdAsync(product)).ReturnsAsync(product);

            // Act
            var result = await _productService.SearchProductByIdFromAPIAsync(product);

            // Assert
            Assert.AreEqual(1, result.ProductId); // Kết quả phải là true
        }

        [Test]
        public async Task DeleteProductAsync_ValidProductId_ReturnsSuccess()
        {
            // Arrange
            Product product = new Product() { ProductId = 7 };
            _mockRepo.Setup(repo => repo.RemoveProductAsync(product)).ReturnsAsync(true);

            // Act
            var result = await _productService.RemoveProductFromAPIAsync(product);

            // Assert
            Assert.AreEqual(true,result); // Kết quả phải là true
        }
    }
}