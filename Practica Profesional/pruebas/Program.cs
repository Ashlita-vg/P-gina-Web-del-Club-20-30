using Capadedatos;
using Capadedatos.dsCentroDesarrolloTableAdapters;



tipo_usuarioTableAdapter tipo_UsuarioAdapter = new tipo_usuarioTableAdapter();

dsCentroDesarrollo.tipo_usuarioDataTable tipo_UsuarioTable = tipo_UsuarioAdapter.GetData();

foreach (dsCentroDesarrollo.tipo_usuarioRow row in tipo_UsuarioTable)
{
    Console.WriteLine($"nombre {row.nombre_tipo_usuario}, Descripción: {row.descripcion}");
}
Console.ReadLine();


