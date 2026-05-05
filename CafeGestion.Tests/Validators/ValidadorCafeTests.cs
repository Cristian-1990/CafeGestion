
using CafeGestion.Enums;
using CafeGestion.Models;
using CafeGestion.Validators;
using FluentAssertions;

namespace CafeGestion.Tests.Validators;
[TestFixture]
public class ValidadorCafeTests
{
    [TestFixture]
    public class CasosNoValidos
    {
        private ValidadorCafe _validadorCafe = null!;
        private Cafe _cafeInValido = null!;

        [SetUp]
        public void SetUp()
        {
            _validadorCafe = new ValidadorCafe();
            _cafeInValido = new Cafe
            {
                Id = 7, Nombre = "Ococha", Origen = TipoOrigen.Guatemala,
                Region = "Papua", Variedad = TipoVariedad.Pacamara,
                Proceso = TipoProceso.Anaerobico, Puntuacion = 8,
                NotaDeCata = "Frutos secos", Cantidad = 2,
                Disponible = true, FechaTueste = DateTime.Now,
            };
        }

        [Test]
        public void Validar_CafeInvalidoCantidad_ConErrores()
        {
            var cafe = _cafeInValido with { Cantidad = -1 };
            var errores = _validadorCafe.Validar(cafe);
            errores.Should().NotBeEmpty();
        }

        [Test]
        public void Validar_CafeInvalidoPuntuacionMinima()
        {
            var cafe = _cafeInValido with { Puntuacion = 6.99 };
            var errores = _validadorCafe.Validar(cafe);
            errores.Should().NotBeEmpty();
        }

        [Test]
        public void Validar_CafeInvalidoPuntuacionMaxima()
        {
            var cafe = _cafeInValido with { Puntuacion = 10.01 };
            var errores = _validadorCafe.Validar(cafe);
            errores.Should().NotBeEmpty();
        }

        [Test]
        public void Validar_CafeInvalidoOrigen()
        {
            var cafe = _cafeInValido with { Origen = (TipoOrigen)8 };
            var errores = _validadorCafe.Validar(cafe);
            errores.Should().NotBeEmpty();
        }

        [Test]
        public void Validar_CafeInvalidProceso()
        {
            var cafe = _cafeInValido with { Proceso = (TipoProceso)9 };
            var errores = _validadorCafe.Validar(cafe);
            errores.Should().NotBeEmpty();
        }

        [Test]
        public void Validar_CafeInvalidoVariedad()
        {
            var cafe = _cafeInValido with { Variedad = (TipoVariedad)10 };
            var errores = _validadorCafe.Validar(cafe);
            errores.Should().NotBeEmpty();
        }
        
    }
    [TestFixture]
    public class CasosValidos
    {
        private ValidadorCafe _validador = null!;
        private Cafe _cafeValido = null!;

        [SetUp]
        public void SetUp()
        {
            _validador = new ValidadorCafe();
            _cafeValido = new Cafe
            {
                Id = 7, Nombre = "Ococha", Origen = TipoOrigen.Guatemala,
                Region = "Papua", Variedad = TipoVariedad.Pacamara,
                Proceso = TipoProceso.Anaerobico, Puntuacion = 8,
                NotaDeCata = "Frutos secos", Cantidad = 2,
                Disponible = true, FechaTueste = DateTime.Now,
            };
        }

        [Test]
        public void Validar_CafeValido_SinErrores()
        {
            var errores = _validador.Validar(_cafeValido);
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_CafeConCantidadMinima_SinErrors()
        {
            var cafe = _cafeValido with { Cantidad = 1 };
            var errores = _validador.Validar(cafe);
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_CafePuntuacionMinima_SinErrores()
        {
            var cafe = _cafeValido with { Puntuacion = 7 };
            var errores = _validador.Validar(cafe);
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_CafePuntuacionMaxima_SinErrores()
        {
            var cafe = _cafeValido with { Puntuacion = 10 };
            var errores = _validador.Validar(cafe);
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_CafeOrigenValido_SinErrores()
        {
            var cafe = _cafeValido with { Origen = TipoOrigen.Brasil };
            var errores = _validador.Validar(cafe);
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_CafeProcesoValido_SinErrores()
        {
            var cafe = _cafeValido with { Proceso = TipoProceso.Natural };
            var errores = _validador.Validar(cafe);
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_CafeVariedadValida_SinErrores()
        {
            var cafe = _cafeValido with { Variedad = TipoVariedad.Bourbon };
            var errores = _validador.Validar(cafe);
            errores.Should().BeEmpty();
        }
    }
}
