using CafeGestion.Enums;
using CafeGestion.Models;
using CafeGestion.Validators;
using FluentAssertions;

namespace CafeGestion.Tests.Validators;
/// <summary>
/// 
/// </summary>
public class ValidadorCafeTest
{
    
   
    //-------------------------------------------------------
    //========================================================================================   
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
                Id = 7,
                Nombre = "Ococha",
                Origen = TipoOrigen.Guatemala,
                Region = "Papua",
                Variedad = TipoVariedad.Pacamara,
                Proceso = TipoProceso.Anaerobico,
                Puntuacion = 8,
                NotaDeCata = "Frutos secos",
                Cantidad = 2,
                Disponible = true,
                FechaTueste = DateTime.Now,
            };
        }
        
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void Validar_CafeValido_SinErrores()
        {
            var cafe = new Cafe
            {
                Id = 7,
                Nombre = "Ococha",
                Origen = TipoOrigen.Guatemala,
                Region = "Papua",
                Variedad = TipoVariedad.Pacamara,
                Proceso = TipoProceso.Anaerobico,
                Puntuacion = 8,
                NotaDeCata = "Frutos secos",
                Cantidad = 2,
                Disponible = true,
                FechaTueste = DateTime.Now,
            };
            var errores = _validador.Validar(cafe);
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
//======================================================================================
    [TestFixture]
    public class CasosInvalidos
    {
        private ValidadorCafe _validador = null!;
        private Cafe _cafeValido = null!;

        [SetUp]
        public void SetUp()
        {
            _validador = new ValidadorCafe();
            _cafeValido = new Cafe()
            {
                Id = 7,
                Nombre = "Ococha",
                Origen = TipoOrigen.Guatemala,
                Region = "Papua",
                Variedad = TipoVariedad.Pacamara,
                Proceso = TipoProceso.Anaerobico,
                Puntuacion = 8,
                NotaDeCata = "Frutos secos",
                Cantidad = 2,
                Disponible = true,
                FechaTueste = DateTime.Now,
            };
        }

        [Test]
        public void Validar_Cantidad0_ConError()
        {
            var cafe = _cafeValido with {Cantidad = 0};
            var errores = _validador.Validar(cafe);
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_PuntuacionLimiteInferior_ConError()
        {
            var cafe = _cafeValido with { Puntuacion = 6.99 };
            var errores = _validador.Validar(cafe);
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_PuntuacionLimiteSuperior_ConError()
        {
            var cafe = _cafeValido with { Puntuacion = 10.01 };
            var errores = _validador.Validar(cafe);
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_OrigenInexistente_ConError()
        {
            var cafe = _cafeValido with { Origen = (TipoOrigen)10 };
            var errores = _validador.Validar(cafe);
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_ProcesoInexistente_ConError()
        {
            var cafe = _cafeValido with { Proceso = (TipoProceso)9 };
            var errores = _validador.Validar(cafe);
            errores.Should().BeEmpty();
        }

        [Test]
        public void Validar_VariedadInexistente_ConError()
        {
            var cafe = _cafeValido with{Variedad = (TipoVariedad)8 };
            var errores = _validador.Validar(cafe);
        }
    }

}