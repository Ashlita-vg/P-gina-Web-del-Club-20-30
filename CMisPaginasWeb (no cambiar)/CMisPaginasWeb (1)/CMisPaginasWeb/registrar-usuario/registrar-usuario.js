// ✅ URL base de la API
const API_BASE_URL = "https://localhost:7217/Usuario";

// ✅ Esperar a que el DOM esté cargado
document.addEventListener('DOMContentLoaded', () => {
  const form = document.getElementById('registroForm');

  if (form) {
    form.addEventListener('submit', async (e) => {
      e.preventDefault(); // Evita que la página se recargue

      // --- Obtener valores ---
      const id = -1;
       const cedula = 201110111;
     var nombre = document.getElementById('nombre').value.trim();
     const apellidos = document.getElementById('apellidos').value.trim();

     nombre = `${nombre + ' ' +apellidos}`;

      const correo = document.getElementById('correo').value.trim();
      const telefono = document.getElementById('telefono').value.trim();
      const password = document.getElementById('contrasena').value.trim();
      const confirmar = document.getElementById('confirmar').value.trim();
 const tipo_usuario_id = 0;

      // --- Validaciones ---
      if (!nombre || !correo || !telefono || !password || !confirmar) {
        alert("Por favor complete todos los campos.");
        return;
      }

      if (password !== confirmar) {
        alert("Las contraseñas no coinciden.");
        return;
      }

      // --- Crear el objeto de usuario ---
      const usuario = {
        id,
        cedula,
        nombre,
        correo,
        password,
        telefono,
        tipo_usuario_id
      };

      try {

        // --- Enviar datos a la API ---
        const response = await fetch(API_BASE_URL, {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(usuario)
        });

        if (response.ok) {
          alert("✅ Registro exitoso");
          form.reset();
        } else {
          alert(`❌ Error del servidor: ${response.status}\n${errorText}`);
        }
      } catch (error) {
        console.error("Error de conexión:", error);
        alert("⚠️ No se pudo conectar con la API. Verifica que el servidor esté ejecutándose.");
      }
    });
  } else {
    console.error("No se encontró el formulario con id='registroForm'");
  }
});
