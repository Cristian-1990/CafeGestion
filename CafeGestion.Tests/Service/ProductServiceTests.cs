using CafeGestion.Enums;
using CafeGestion.Exceptions.Productos;
using CafeGestion.Models;
using CafeGestion.Repository.Productos.Base;
using CafeGestion.Service.Productos;
using CafeGestion.Storage.Common;
using CafeGestion.Validators;
using FluentAssertions;
using Moq;

namespace CafeGestion.Tests.Service;

[TestFixture]
public class ProductoServiceTests
{
    private Mock<IProductoRepo> _repoMock = null!;
    private Mock<IStorage<Producto>> _storageMock = null!;
    private Mock<IValidador<Cafe>> _validadorMock = null!;
    private IProductoService _service = null!;
    private Cafe _cafe = null!;

    [SetUp]
    public void SetUp()
    {
        _repoMock = new Mock<IProductoRepo>();
        _storageMock = new Mock<IStorage<Producto>>();
        _validadorMock = new Mock<IValidador<Cafe>>();
        _service = new ProductoService(_repoMock.Object, _storageMock.Object, _validadorMock.Object);

        _cafe = new Cafe
        {
            Id = 1, Nombre = "Ococha",
            Origen = TipoOrigen.Guatemala, Region = "Huehuetenango",
            Variedad = TipoVariedad.Bourbon, Proceso = TipoProceso.Lavado,
            Puntuacion = 8.5, NotaDeCata = "Cítrico",
            Cantidad = 10, Disponible = true,
            FechaTueste = DateTime.Now
        };
    }

    [Test]
    public void GetAll_DevuelveTodosLosProductos()
    {
        _repoMock.Setup(r => r.GetAll()).Returns(new List<Producto> { _cafe });
        _service.GetAll().Should().HaveCount(1);
    }

    [Test]
    public void GetById_CafeExistente_DevuelveCafe()
    {
        _repoMock.Setup(r => r.GetById(1)).Returns(_cafe);
        _service.GetById(1).Should().Be(_cafe);
    }

    [Test]
    public void GetById_CafeNoExistente_DevuelveNull()
    {
        _repoMock.Setup(r => r.GetById(99)).Returns((Producto?)null);
        _service.GetById(99).Should().BeNull();
    }

    [Test]
    public void Guardar_CafeValido_LlamaAlRepoUnaVez()
    {
        _repoMock.Setup(r => r.Create(_cafe)).Returns(_cafe);
        _service.Guardar(_cafe);
        _repoMock.Verify(r => r.Create(_cafe), Times.Once);
    }

    [Test]
    public void Delete_CafeExistente_LlamaAlRepoUnaVez()
    {
        _repoMock.Setup(r => r.Delete(1)).Returns(_cafe);
        _service.Delete(1);
        _repoMock.Verify(r => r.Delete(1), Times.Once);
    }

    [Test]
    public void Actualizar_CafeNoExistente_LanzaNotFound()
    {
        _repoMock.Setup(r => r.Update(99, _cafe)).Returns((Producto?)null);
        var act = () => _service.Actualizar(99, _cafe);
        act.Should().Throw<ProductosException.NotFound>();
    }

    [Test]
    public void Actualizar_CafeExistente_DevuelveCafeActualizado()
    {
        var actualizado = _cafe with { Nombre = "Engoto" };
        _repoMock.Setup(r => r.Update(1, _cafe)).Returns(actualizado);
        var resultado = _service.Actualizar(1, _cafe);
        resultado.Should().NotBeNull();
        resultado.Nombre.Should().Be("Engoto");
    }

    [Test]
    public void Existe_CafeExistente_DevuelveTrue()
    {
        _repoMock.Setup(r => r.Existe(1)).Returns(true);
        _service.Existe(1).Should().BeTrue();
    }
}