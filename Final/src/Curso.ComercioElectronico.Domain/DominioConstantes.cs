namespace Curso.ComercioElectronico.Domain;



public static class DominioConstantes{

    public const int ID_MAXIMO = 12;
     
    public const int NOMBRE_MAXIMO = 80;

    public static class ExpressionRegular
    {
        public const string ALFANUMERICOS = @"^\w+$";

        public const string ALFANUMERICOS_GUIONES_PUNTO_ESPACIO = @"^[a-zA-Z0-9-_\s.·ÈÌÛ˙¡…Õ”⁄Ò—]+$";
    }

}


public static class ClienteConstantes
{

    public const int IDENTIFICACION_MAXIMO = 30; 


}

public static class UsuarioConstantes
{

    public const int USUARIO_MAXIMO = 160;


}