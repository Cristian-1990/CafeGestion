using CafeGestion.Models;
using CafeGestion.Enums;
using FluentAssertions;

namespace CafeGestion.Tests.Models;
[TestFixture]
public class CafeTests
{
    [TestFixture]
    public class CasosValidos
    {
        private Cafe _cafeA = null!;
        private Cafe _cafeB = null!;

        [SetUp]
        public void Setup()
        {
            _cafeA = new Cafe(){
                Id = 1, 
                Nombre = "La Cima", 
                Origen = TipoOrigen.Brasil,
                Region = "Minas Gerais", 
                Variedad = TipoVariedad.Bourbon,
                Proceso = TipoProceso.Natural, 
                Puntuacion = 8.3,
                NotaDeCata = "Chocolate con leche, frutal, toffee",
                Cantidad = 3, 
                Disponible = true,
                Entrada = DateTime.Now, 
                FechaTueste = new DateTime(2026, 4, 19)};
        }
/// <summary>
/// Cafés totalmente distitntos salvo el Id que es igual
/// </summary>
        [Test]
        public void Equals_MismoIdDistintoCafe_ReturnSonIguales()
        {
            var cafeB = _cafeA with {Nombre ="Engoto", Origen = TipoOrigen.Etiopia, Region = "Norte", Variedad = TipoVariedad.Geisha,Proceso = TipoProceso.Anaerobico,Puntuacion = 9.2 , NotaDeCata = "Miel, frutas de hueso" , Cantidad = 12 , Disponible = false, Entrada = new DateTime(12/12/24) ,FechaTueste = new DateTime(2/2/2024)
            };
            cafeB.Equals(_cafeA).Should().BeTrue();
        }
/// <summary>
/// Cafes con todos los campos igaules salvo el Id
/// </summary>
        [Test]
        public void Equals_MismoCafeConDistintoId_ReturnSonDistintos()
        {
            var cafeB = _cafeA with { Id = 99 };
            cafeB.Should().NotBe(_cafeA);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void HashCode_MismoIdDistintoCafe_ReturnMismoHashCode()
        {
            var cafeB = _cafeA with { Nombre = "Engoto", Origen = TipoOrigen.Etiopia };
            cafeB.GetHashCode().Should().Be(_cafeA.GetHashCode());
        }
    }
}