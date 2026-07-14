using BiSoft.Consultorio.Dominio.Entidades;

namespace BiSoft.Consultorio.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            //Act
            var doctor= new Doctor("Juan Perez","Cardiologia");
            //Assert
            Assert.Equal("Juan Perez", doctor.Nombre);
            Assert.Equal("Cardiologia", doctor.Especialidad);
            Assert.NotEqual(Guid.Empty, doctor.Id);
            Assert.True(doctor.Nombre.Length > 5);
            Assert.True(doctor.Nombre.Contains(' '));
            Assert.True(doctor.Nombre.Length < 50);
        }
        [Theory]
        [InlineData("JuanPerez", "Cardiologia")]
        [InlineData("", "Pediatria")]
        [InlineData("Ana Perez", "")]
        [InlineData("An a", "Pediatria")]
        [InlineData("Este es un ejemplo de nombre bastante largo que no deberia de ser aceptado por nuestro sistema de consultorio", "Pediatria")]
        public void Incorrectdata(string nombre, string especialidad)
        {
            //Arrange
            //Act

            //Assert
            Assert.Throws<ArgumentException>(() => new Doctor(nombre, especialidad));
        }
        [Fact]
        public void Actualizar()
        {
            //Arrange
            //Act
            var doctor = new Doctor("Juan Perez", "Cardiologia");
            //Assert
            doctor.Actualizar("Ejemplo0 si", "General");
            Assert.Equal("Ejemplo0 si", doctor.Nombre);
            Assert.Equal("General", doctor.Especialidad);
            
        }
    }
}
