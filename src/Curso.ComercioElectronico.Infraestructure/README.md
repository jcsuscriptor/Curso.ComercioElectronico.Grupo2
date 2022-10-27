

Ayuda. Commandos utilizados con visual code. 

Dependencias para utilizar migraciones. 
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design

Commando para agregar una nueva migracion
dotnet ef migrations add InitialCreate

Commado para actualizar nuestra base de datos, con las migraciones
dotnet ef database update
