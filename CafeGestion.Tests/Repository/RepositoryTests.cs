using CafeGestion.Enums;
using CafeGestion.Models;
using CafeGestion.Repository.Productos;
using FluentAssertions;

namespace CafeGestion.Tests.Repository;

[TestFixture]
public class ProductoRepoTests
{
    private ProductoRepo _repo = null!;
    private Cafe _cafe = null!;

    [SetUp]
    public void Setup()
    {
        _repo = new ProductoRepo();
        _cafe = new Cafe
        {
            Id = 0, Nombre = "Ococha",
            Origen = TipoOrigen.Guatemala, Region = "Huehuetenango",
            Variedad = TipoVariedad.Bourbon, Proceso = TipoProceso.Lavado,
            Puntuacion = 8.5, NotaDeCata = "Cítrico",
            Cantidad = 10, Disponible = true,
            FechaTueste = DateTime.Now
        };
    }

    [Test]
    public void GetAll_RepoVacio_DevuelveListaVacia()
    {
        _repo.GetAll().Should().BeEmpty();
    }

    [Test]
    public void Create_CafeValido_DevuelveCafeCreado()
    {
        var resultado = _repo.Create(_cafe);
        resultado.Should().NotBeNull();
        resultado!.Nombre.Should().Be("Ococha");
    }

    [Test]
    public void Create_CafeDuplicado_DevuelveNull()
    {
        _repo.Create(_cafe);
        var duplicado = _repo.Create(_cafe);
        duplicado.Should().BeNull();
    }

    [Test]
    public void GetById_CafeExistente_DevuelveCafe()
    {
        var creado = _repo.Create(_cafe);
        var resultado = _repo.GetById(creado!.Id);
        resultado.Should().NotBeNull();
    }

    [Test]
    public void GetById_CafeNoExistente_DevuelveNull()
    {
        _repo.GetById(99).Should().BeNull();
    }

    [Test]
    public void Update_CafeExistente_DevuelveCafeActualizado()
    {
        var creado = _repo.Create(_cafe);
        var actualizado = _cafe with { Nombre = "Engoto" };
        var resultado = _repo.Update(creado!.Id, actualizado);
        resultado.Should().NotBeNull();
        resultado!.Nombre.Should().Be("Engoto");
    }

    [Test]
    public void Delete_CafeExistente_DevuelveCafeBorrado()
    {
        var creado = _repo.Create(_cafe);
        var borrado = _repo.Delete(creado!.Id);
        borrado.Should().NotBeNull();
        borrado!.Disponible.Should().BeFalse();
    }

    [Test]
    public void Delete_CafeNoExistente_DevuelveNull()
    {
        _repo.Delete(99).Should().BeNull();
    }

    [Test]
    public void Existe_CafeCreado_DevuelveTrue()
    {
        var creado = _repo.Create(_cafe);
        _repo.Existe(creado!.Id).Should().BeTrue();
    }

    [Test]
    public void DeleteAll_LimpiaElRepositorio()
    {
        _repo.Create(_cafe);
        _repo.DeleteAll();
        _repo.GetAll().Should().BeEmpty();
    }
}